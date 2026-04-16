using WodToolKit.Http;
using WodToolKit.Json;

namespace BtPanelApi.site
{
    /// <summary>
    /// 网站管理 PHP网站类
    /// </summary>
    /// <param name="BtPanel">BtPanel地址(http://127.0.0.1:8888)结尾不带"/"</param>
    /// <param name="BtKey">BtPanel密钥</param>
    public class Php(string BtPanel, string BtKey) : BtPanel(BtPanel, BtKey)
    {
        private const string SitePath = "/site";
        private const string DefaultPagePath = "/www/server/panel/data/defaultDoc.html";
        private const string NotFoundPagePath = "/www/server/panel/data/404.html";
        private const string NoWebsitePagePath = "/www/server/nginx/html/index.html";
        private const string StopPagePath = "/www/server/stop/index.html";

        private static T ParseObject<T>(string responseBody) => EasyJson.ParseJsonObject<T>(responseBody);

        private static bool ParseStatus(string responseBody)
        {
            dynamic result = EasyJson.ParseJsonToDynamic(responseBody);
            return result.status;
        }

        private static bool ParseNumericStatus(string responseBody)
        {
            dynamic result = EasyJson.ParseJsonToDynamic(responseBody);
            return Convert.ToInt32(result.status) == 1;
        }

        private T Execute<T>(Func<HttpRequestClass> requestFactory, Func<string, T> parser, string errorMessagePrefix)
        {
            try
            {
                return parser(requestFactory().GetResponse().Body);
            }
            catch (Exception ex)
            {
                throw new Exception($"{errorMessagePrefix}:{ex.Message}", ex);
            }
        }

        private void Execute(Func<HttpRequestClass> requestFactory, string errorMessagePrefix)
        {
            try
            {
                requestFactory();
            }
            catch (Exception ex)
            {
                throw new Exception($"{errorMessagePrefix}:{ex.Message}", ex);
            }
        }

        /// <summary>
        /// 获取网站分类分组
        /// </summary>
        /// <returns>网站分类分组</returns>
        /// <exception cref="Exception">获取网站分类分组失败</exception>
        public List<Types> GetSiteTypes() => Execute(() => SendSite("get_site_types"), ParseObject<List<Types>>, "获取网站分类分组失败");

        /// <summary>
        /// 获取PHPCli版本
        /// </summary>
        /// <returns>PHPCli版本</returns>
        /// <exception cref="Exception">获取PHPCli版本失败</exception>
        public PhpCliVersion GetCliPhpVersion() => Execute(() => SendConfig("get_cli_php_version"), ParseObject<PhpCliVersion>, "获取PHPCli版本失败");

        /// <summary>
        /// 设置PHPCli版本
        /// </summary>
        /// <param name="php_version">PHP版本 如：74、83、84</param>
        /// <returns>是否设置成功</returns>
        /// <exception cref="Exception">设置PHP版本失败</exception>
        public bool SetCliPhpVersion(string php_version) => Execute(
            () => SendConfig("set_cli_php_version", new Dictionary<string, string> { ["php_version"] = php_version }),
            ParseStatus,
            "设置PHP版本失败");

        /// <summary>
        /// 获取文件内容
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <returns>文件内容</returns>
        /// <exception cref="Exception">获取文件内容失败</exception>
        public FileBody GetFileBody(string path) => Execute(
            () => SendFiles("GetFileBody", new Dictionary<string, string> { ["path"] = path }),
            ParseObject<FileBody>,
            "获取文件内容失败");

        /// <summary>
        /// 保存文件内容
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <param name="data">文件内容</param>
        /// <param name="encoding">文件编码</param>
        /// <returns>是否保存成功</returns>
        /// <exception cref="Exception">保存文件内容失败</exception>
        public bool SaveFileBody(string path, string data, string encoding = "utf-8") => Execute(
            () => SendFiles("SaveFileBody", new Dictionary<string, string>
            {
                ["path"] = path,
                ["data"] = data,
                ["encoding"] = encoding
            }),
            ParseStatus,
            "保存文件内容失败");

        /// <summary>
        /// 通用获取页面内容方法
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="errorMessagePrefix">错误信息前缀</param>
        /// <returns>文件内容</returns>
        /// <exception cref="Exception">获取文件内容失败</exception>
        private FileBody GetPageContent(string filePath, string errorMessagePrefix) => Execute(
            () => SendFiles("GetFileBody", new Dictionary<string, string> { ["path"] = filePath }),
            ParseObject<FileBody>,
            errorMessagePrefix);

        /// <summary>
        /// 通用保存页面内容方法
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="data"></param>
        /// <param name="encoding"></param>
        /// <param name="errorMessagePrefix"></param>
        /// <returns></returns>
        /// <exception cref="Exception">保存文件内容失败</exception>
        private bool SetPageContent(string filePath, string data, string encoding, string errorMessagePrefix) => Execute(
            () => SendFiles("SaveFileBody", new Dictionary<string, string>
            {
                ["path"] = filePath,
                ["data"] = data,
                ["encoding"] = encoding
            }),
            ParseStatus,
            errorMessagePrefix);
        #region 默认页面
        /// <summary>
        /// 获取默认页面内容
        /// </summary>
        /// <returns>默认页面文件内容</returns>
        /// <exception cref="Exception">获取默认页面内容失败</exception>
        public FileBody GetDefaultPage() => GetPageContent(DefaultPagePath, "获取默认页面内容失败");

        /// <summary>
        /// 设置默认页面内容
        /// </summary>
        /// <param name="data">默认页面文件内容</param>
        /// <returns>是否设置成功</returns>
        /// <exception cref="Exception">设置默认页面内容失败</exception>
        public bool SetDefaultPage(string data) => SetPageContent(DefaultPagePath, data, "utf-8", "设置默认页面内容失败");

        /// <summary>
        /// 获取404页面内容
        /// </summary>
        /// <returns>404页面文件内容</returns>
        /// <exception cref="Exception">获取404页面内容失败</exception>
        public FileBody Get404Page() => GetPageContent(NotFoundPagePath, "获取404页面内容失败");

        /// <summary>
        /// 设置404页面内容
        /// </summary>
        /// <param name="data">404页面文件内容</param>
        /// <param name="encoding">文件编码</param>
        /// <returns>是否设置成功</returns>
        /// <exception cref="Exception">设置404页面内容失败</exception>
        public bool Set404Page(string data, string encoding = "utf-8") => SetPageContent(NotFoundPagePath, data, encoding, "设置404页面内容失败");

        /// <summary>
        /// 获取404页面状态
        /// </summary>
        /// <returns>是否启用404页面</returns>
        /// <exception cref="Exception">获取404页面状态失败</exception>
        public bool Get404PageStatus() => Execute(() => SendSite("get_404_config"), ParseNumericStatus, "获取404页面状态失败");

        /// <summary>
        /// 设置404页面状态
        /// </summary>
        /// <param name="status">是否启用404页面</param>
        /// <returns>是否设置成功</returns>
        /// <exception cref="Exception">设置404页面状态失败</exception>
        public bool Set404PageStatus(bool status) => Execute(
            () => SendSite("set_404_config", new Dictionary<string, string> { ["status"] = status ? "1" : "0" }),
            ParseStatus,
            "设置404页面状态失败");

        /// <summary>
        /// 获取无网站页面内容
        /// </summary>
        /// <returns>无网站页面文件内容</returns>
        /// <exception cref="Exception">获取无网站页面内容失败</exception>
        public FileBody GetNoWebSitePage() => GetPageContent(NoWebsitePagePath, "获取无网站页面内容失败");

        /// <summary>
        /// 设置无网站页面内容
        /// </summary>
        /// <param name="data">无网站页面文件内容</param>
        /// <param name="encoding">文件编码</param>
        /// <returns>是否设置成功</returns>
        /// <exception cref="Exception">设置无网站页面内容失败</exception>
        public bool SetNoWebSitePage(string data, string encoding = "utf-8") => SetPageContent(NoWebsitePagePath, data, encoding, "设置无网站页面内容失败");

        /// <summary>
        /// 获取网站停用后提示页面内容
        /// </summary>
        /// <returns>网站停用后提示页面文件内容</returns>
        /// <exception cref="Exception">获取网站停用后提示页面内容失败</exception>
        public FileBody GetStopPage() => GetPageContent(StopPagePath, "获取网站停用后提示页面内容失败");

        /// <summary>
        /// 设置网站停用后提示页面内容
        /// </summary>
        /// <param name="data">网站停用后提示页面文件内容</param>
        /// <param name="encoding">文件编码</param>
        /// <returns>是否设置成功</returns>
        /// <exception cref="Exception">设置网站停用后提示页面内容失败</exception>
        public bool SetStopPage(string data, string encoding = "utf-8") => SetPageContent(StopPagePath, data, encoding, "设置网站停用后提示页面内容失败");
        #endregion
        #region 默认站点
        /// <summary>
        /// 获取默认站点
        /// </summary>
        /// <returns>默认网站信息</returns>
        /// <exception cref="Exception">获取默认网站失败</exception>
        public DefaultWebSite GetDefaultWebSite() => Execute(() => SendSite("GetDefaultSite"), ParseObject<DefaultWebSite>, "获取默认网站失败");

        /// <summary>
        /// 设置默认站点
        /// </summary>
        /// <param name="defaultSite">默认网站名称(0表示不设置默认站点)</param>
        /// <returns>是否设置成功</returns>
        /// <exception cref="Exception">设置默认网站失败</exception>
        public bool SetDefaultWebSite(string defaultSite = "0") => Execute(
            () => SendSite("SetDefaultSite", new Dictionary<string, string> { ["name"] = defaultSite }),
            ParseStatus,
            "设置默认网站失败");
        #endregion
        #region HTTPS管理
        /// <summary>
        /// 获取HTTPS设置
        /// </summary>
        /// <returns>HTTPS设置信息</returns>
        /// <exception cref="Exception">获取HTTPS设置失败</exception>
        public HttpsSettingsResponse GetHttpsSettings() => Execute(() => SendSite("get_https_settings"), ParseObject<HttpsSettingsResponse>, "获取HTTPS设置失败");

        /// <summary>
        /// 切换HTTPS防窜站模式
        /// </summary>
        /// <returns>是否设置成功</returns>
        /// <exception cref="Exception">设置HTTPS防窜站模式失败</exception>
        public bool SetHttpsMode() => Execute(() => SendSite("set_https_mode"), ParseStatus, "设置https模式失败");

        /// <summary>
        /// 设置全局HTTP转HTTPS状态
        /// </summary>
        /// <param name="status">状态值，`1` 为开启，`0` 为关闭</param>
        /// <returns>是否设置成功</returns>
        /// <exception cref="Exception">设置全局HTTP转HTTPS状态失败</exception>
        public bool SetGlobal_Http2Https(int status = 0) => Execute(
            () => SendSite("set_global_http2https", new Dictionary<string, string> { ["status"] = status.ToString() }),
            ParseStatus,
            "设置全局HTTP转HTTPS状态失败");
        #endregion
        #region TLS设置
        /// <summary>
        /// 获取SSL协议版本
        /// </summary>
        /// <returns>SSL协议版本</returns>
        /// <exception cref="Exception">获取ssl协议失败</exception>
        public TLSversion GetSslProtocol() => Execute(() => SendSite("get_ssl_protocol"), ParseObject<TLSversion>, "获取ssl协议失败");

        /// <summary>
        /// 设置SSL协议版本
        /// </summary>
        /// <param name="tls">SSL协议版本(TLSv1.1,TLSv1.2,TLSv1.3,TLSv1)</param>
        /// <returns>是否设置成功</returns>
        /// <exception cref="Exception">设置ssl协议失败</exception>
        public bool SetSslProtocol(string[] tls) => Execute(
            () => SendSite("set_ssl_protocol", new Dictionary<string, string> { ["use_protocols"] = string.Join(",", tls) }),
            ParseStatus,
            "设置ssl协议失败");
        #endregion
        /// <summary>
        /// 获取cdn ip设置
        /// </summary>
        /// <returns>cdn ip设置</returns>
        /// <exception cref="Exception">获取cdn ip设置失败</exception>
        public CdnIpSettings GetCdnIpSettings() => Execute(() => SendSite("get_cdn_ip_settings"), ParseObject<CdnIpSettings>, "获取cdn ip设置失败");

        /// <summary>
        /// 创建默认配置文件
        /// </summary>
        /// <exception cref="Exception">获取创建默认配置文件失败</exception>
        public void CreateDefaultConf() => Execute(() => SendSite("create_default_conf"), "创建默认配置文件失败");

        /// <summary>
        /// 获取网络信息
        /// </summary>
        /// <returns>网络信息</returns>
        /// <exception cref="Exception">获取网络信息失败</exception>
        public NetWorkInfo GetNetWork() => Execute(() => SendSite("get_network"), ParseObject<NetWorkInfo>, "获取网络信息失败");

        /// <summary>
        /// 发送网站请求
        /// </summary>
        /// <param name="action">请求动作</param>
        /// <param name="form">请求参数</param>
        /// <returns>请求结果</returns>
        /// <exception cref="Exception">发送网站请求失败</exception>
        HttpRequestClass SendSite(string action, Dictionary<string, string>? form = null) => SendRequest(SitePath, action, form);
    }
}
