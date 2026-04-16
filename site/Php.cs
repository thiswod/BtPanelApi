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

        private static int ParseRawInt(string responseBody) => int.Parse(responseBody.Trim().Trim('"'));

        /// <summary>
        /// 通用请求执行方法（带返回值），统一处理请求发送、响应解析和异常包装
        /// </summary>
        /// <typeparam name="T">解析后的返回值类型</typeparam>
        /// <param name="requestFactory">请求工厂方法，用于构造并发送HTTP请求</param>
        /// <param name="parser">响应体解析方法，用于将响应字符串解析为指定类型</param>
        /// <param name="errorMessagePrefix">异常信息前缀，用于标识错误来源</param>
        /// <returns>解析后的响应结果</returns>
        /// <exception cref="Exception">请求或解析过程中发生错误时抛出，异常信息以<paramref name="errorMessagePrefix"/>开头</exception>
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

        /// <summary>
        /// 通用请求执行方法（无返回值），统一处理请求发送和异常包装
        /// </summary>
        /// <param name="requestFactory">请求工厂方法，用于构造并发送HTTP请求</param>
        /// <param name="errorMessagePrefix">异常信息前缀，用于标识错误来源</param>
        /// <exception cref="Exception">请求过程中发生错误时抛出，异常信息以<paramref name="errorMessagePrefix"/>开头</exception>
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
        /// <param name="errorMessagePrefix">错误信息前缀</param>
        /// <returns>文件内容</returns>
        /// <exception cref="Exception">获取文件内容失败</exception>
        public FileBody GetFileBody(string path, string errorMessagePrefix = "获取文件内容失败") => Execute(
            () => SendFiles("GetFileBody", new Dictionary<string, string> { ["path"] = path }),
            ParseObject<FileBody>,
            errorMessagePrefix);

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
        public FileBody GetDefaultPage() => GetFileBody(DefaultPagePath, "获取默认页面内容失败");

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
        public FileBody Get404Page() => GetFileBody(NotFoundPagePath, "获取404页面内容失败");

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
        public FileBody GetNoWebSitePage() => GetFileBody(NoWebsitePagePath, "获取无网站页面内容失败");

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
        public FileBody GetStopPage() => GetFileBody(StopPagePath, "获取网站停用后提示页面内容失败");

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
        #region 站点设置
        /// <summary>
        /// 获取指定站点的HTTPS端口
        /// </summary>
        /// <param name="siteName">站点名称</param>
        /// <returns>HTTPS端口</returns>
        /// <exception cref="Exception">获取网站HTTPS端口失败</exception>
        public int GetHttpsPort(string siteName) => Execute(
            () => SendData("get_https_port", new Dictionary<string, string> { ["siteName"] = siteName }),
            ParseRawInt,
            "获取网站HTTPS端口失败");

        /// <summary>
        /// 设置指定站点的HTTPS端口
        /// </summary>
        /// <param name="siteName">站点名称</param>
        /// <param name="port">HTTPS端口</param>
        /// <returns>是否设置成功</returns>
        /// <exception cref="Exception">设置网站HTTPS端口失败</exception>
        public bool SetHttpsPort(string siteName, int port) => Execute(
            () => SendData("set_https_port", new Dictionary<string, string>
            {
                ["siteName"] = siteName,
                ["port"] = port.ToString()
            }),
            ParseStatus,
            "设置网站HTTPS端口失败");

        /// <summary>
        /// 获取指定站点的伪静态配置文件内容
        /// </summary>
        /// <param name="siteName">站点名称</param>
        /// <returns>伪静态配置文件内容</returns>
        /// <exception cref="Exception">获取网站伪静态配置失败</exception>
        public FileBody GetRewriteConfig(string siteName) =>
            GetFileBody($"/www/server/panel/vhost/rewrite/{siteName}.conf", "获取网站伪静态配置失败");

        #endregion
        /// <summary>
        /// 获取cdn ip设置
        /// </summary>
        /// <returns>cdn ip设置</returns>
        /// <exception cref="Exception">获取cdn ip设置失败</exception>
        public CdnIpSettings GetCdnIpSettings() => Execute(() => SendSite("get_cdn_ip_settings"), ParseObject<CdnIpSettings>, "获取cdn ip设置失败");

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
