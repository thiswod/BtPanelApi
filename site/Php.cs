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
        /// <summary>
        /// 获取网站分类分组
        /// </summary>
        /// <returns>网站分类分组</returns>
        /// <exception cref="Exception">获取网站分类分组失败</exception>
        public List<Types> GetSiteTypes()
        {
            try
            {
                var Result = SendSite("get_site_types");
                List<Types> types = EasyJson.ParseJsonObject<List<Types>>(Result.GetResponse().Body);
                return types;
            }
            catch (Exception ex)
            {
                throw new Exception("获取网站分类分组失败:" + ex.Message);
            }
        }

        /// <summary>
        /// 获取PHPCli版本
        /// </summary>
        /// <returns>PHPCli版本</returns>
        /// <exception cref="Exception">获取PHPCli版本失败</exception>
        public PhpCliVersion GetCliPhpVersion()
        {
            var Result = SendConfig("get_cli_php_version");
            try
            {
                PhpCliVersion PhpCliVersion = EasyJson.ParseJsonObject<PhpCliVersion>(Result.GetResponse().Body);
                return PhpCliVersion;
            }
            catch (Exception ex)
            {
                throw new Exception("获取PHPCli版本失败:" + ex.Message);
            }
        }

        /// <summary>
        /// 设置PHPCli版本
        /// </summary>
        /// <param name="php_version">PHP版本 如：74、83、84</param>
        /// <returns>是否设置成功</returns>
        /// <exception cref="Exception">设置PHP版本失败</exception>
        public bool SetCliPhpVersion(string php_version)
        {
            var Result = SendConfig("set_cli_php_version", new Dictionary<string, string>()
            {
                ["php_version"] = php_version
            });
            try
            {
                dynamic dynamic = EasyJson.ParseJsonToDynamic(Result.GetResponse().Body);
                return dynamic.status;
            }
            catch (Exception ex)
            {
                throw new Exception("设置PHP版本失败:" + ex.Message);
            }
        }

        /// <summary>
        /// 获取文件内容
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <returns>文件内容</returns>
        /// <exception cref="Exception">获取文件内容失败</exception>
        public FileBody GetFileBody(string path)
        {
            var Result = SendFiles("GetFileBody", new Dictionary<string, string>()
            {
                ["path"] = path
            });
            try
            {
                FileBody FileBody = EasyJson.ParseJsonObject<FileBody>(Result.GetResponse().Body);
                return FileBody;
            }
            catch (Exception ex)
            {
                throw new Exception("获取文件内容失败:" + ex.Message);
            }
        }

        /// <summary>
        /// 保存文件内容
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <param name="data">文件内容</param>
        /// <param name="encoding">文件编码</param>
        /// <returns>是否保存成功</returns>
        /// <exception cref="Exception">保存文件内容失败</exception>
        public bool SaveFileBody(string path, string data, string encoding = "utf-8")
        {
            var Result = SendFiles("SaveFileBody", new Dictionary<string, string>()
            {
                ["path"] = path,
                ["data"] = data,
                ["encoding"] = encoding
            });
            try
            {
                dynamic dynamic = EasyJson.ParseJsonToDynamic(Result.GetResponse().Body);
                return dynamic.status;
            }
            catch (Exception ex)
            {
                throw new Exception("保存文件内容失败:" + ex.Message);
            }
        }

        /// <summary>
        /// 通用获取页面内容方法
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="errorMessagePrefix">错误信息前缀</param>
        /// <returns>文件内容</returns>
        /// <exception cref="Exception">获取文件内容失败</exception>
        private FileBody GetPageContent(string filePath, string errorMessagePrefix)
        {
            try
            {
                return GetFileBody(filePath);
            }
            catch (Exception ex)
            {
                throw new Exception($"{errorMessagePrefix}:{ex.Message}");
            }
        }

        /// <summary>
        /// 通用保存页面内容方法
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="data"></param>
        /// <param name="encoding"></param>
        /// <param name="errorMessagePrefix"></param>
        /// <returns></returns>
        /// <exception cref="Exception">保存文件内容失败</exception>
        private bool SetPageContent(string filePath, string data, string encoding, string errorMessagePrefix)
        {
            try
            {
                return SaveFileBody(filePath, data, encoding);
            }
            catch (Exception ex)
            {
                throw new Exception($"{errorMessagePrefix}:{ex.Message}");
            }
        }

        /// <summary>
        /// 获取默认页面内容
        /// </summary>
        /// <returns>默认页面文件内容</returns>
        /// <exception cref="Exception">获取默认页面内容失败</exception>
        public FileBody GetDefaultPage()
        {
            return GetFileBody("/www/server/panel/data/defaultDoc.html");
        }

        /// <summary>
        /// 设置默认页面内容
        /// </summary>
        /// <param name="data">默认页面文件内容</param>
        /// <returns>是否设置成功</returns>
        /// <exception cref="Exception">设置默认页面内容失败</exception>
        public bool SetDefaultPage(string data)
        {
            return SaveFileBody("/www/server/panel/data/defaultDoc.html", data);
        }

        /// <summary>
        /// 获取404页面内容
        /// </summary>
        /// <returns>404页面文件内容</returns>
        /// <exception cref="Exception">获取404页面内容失败</exception>
        public FileBody Get404Page() => GetPageContent("/www/server/panel/data/404.html", "获取404页面内容失败");

        /// <summary>
        /// 设置404页面内容
        /// </summary>
        /// <param name="data">404页面文件内容</param>
        /// <param name="encoding">文件编码</param>
        /// <returns>是否设置成功</returns>
        /// <exception cref="Exception">设置404页面内容失败</exception>
        public bool Set404Page(string data, string encoding = "utf-8") => SetPageContent("/www/server/panel/data/404.html", data, encoding, "设置404页面内容失败");

        public bool Get404PageStatus()
        {
            try
            {
                HttpRequestClass http = SendSite("get_404_config");
                dynamic dynamic = EasyJson.ParseJsonToDynamic(http.GetResponse().Body);
                return dynamic.status == 1;
            }
            catch (Exception ex)
            {
                throw new Exception("获取404页面状态失败:" + ex.Message);
            }
        }

        public bool Set404PageStatus(bool status)
        {
            try
            {
                HttpRequestClass http = SendSite("set_404_config", new Dictionary<string, string>()
                {
                    ["status"] = status ? "1" : "0"
                });
                dynamic dynamic = EasyJson.ParseJsonToDynamic(http.GetResponse().Body);
                return dynamic.status;
            }
            catch (Exception ex)
            {
                throw new Exception("设置404页面状态失败:" + ex.Message);
            }
        }

        /// <summary>
        /// 获取无网站页面内容
        /// </summary>
        /// <returns>无网站页面文件内容</returns>
        /// <exception cref="Exception">获取无网站页面内容失败</exception>
        public FileBody GetNoWebSitePage() => GetPageContent("/www/server/nginx/html/index.html", "获取无网站页面内容失败");

        /// <summary>
        /// 设置无网站页面内容
        /// </summary>
        /// <param name="data">无网站页面文件内容</param>
        /// <param name="encoding">文件编码</param>
        /// <returns>是否设置成功</returns>
        /// <exception cref="Exception">设置无网站页面内容失败</exception>
        public bool SetNoWebSitePage(string data, string encoding = "utf-8") => SetPageContent("/www/server/nginx/html/index.html", data, encoding, "设置无网站页面内容失败");

        /// <summary>
        /// 获取网站停用后提示页面内容
        /// </summary>
        /// <returns>网站停用后提示页面文件内容</returns>
        /// <exception cref="Exception">获取网站停用后提示页面内容失败</exception>
        public FileBody GetStopPage() => GetPageContent("/www/server/stop/index.html", "获取网站停用后提示页面内容失败");

        /// <summary>
        /// 设置网站停用后提示页面内容
        /// </summary>
        /// <param name="data">网站停用后提示页面文件内容</param>
        /// <param name="encoding">文件编码</param>
        /// <returns>是否设置成功</returns>
        /// <exception cref="Exception">设置网站停用后提示页面内容失败</exception>
        public bool SetStopPage(string data, string encoding = "utf-8") => SetPageContent("/www/server/stop/index.html", data, encoding, "设置网站停用后提示页面内容失败");

        /// <summary>
        /// 获取默认站点
        /// </summary>
        /// <returns>默认网站信息</returns>
        /// <exception cref="Exception">获取默认网站失败</exception>
        public DefaultWebSite GetDefaultWebSite()
        {
            HttpRequestClass http = SendSite("GetDefaultSite");
            DefaultWebSite defaultWebSite = EasyJson.ParseJsonObject<DefaultWebSite>(http.GetResponse().Body);
            return defaultWebSite;
        }

        /// <summary>
        /// 设置默认站点
        /// </summary>
        /// <param name="defaultSite">默认网站名称(0表示不设置默认站点)</param>
        /// <returns>是否设置成功</returns>
        /// <exception cref="Exception">设置默认网站失败</exception>
        public bool SetDefaultWebSite(string defaultSite = "0")
        {
            HttpRequestClass http = SendSite("SetDefaultSite", new Dictionary<string, string>()
            {
                ["name"] = defaultSite
            });

            try
            {
                dynamic dynamic = EasyJson.ParseJsonToDynamic(http.GetResponse().Body);
                return dynamic.status;
            }
            catch (Exception ex)
            {
                throw new Exception("设置默认网站失败:" + ex.Message);
            }
        }

        /// <summary>
        /// 获取HTTPS防窜站模式
        /// </summary>
        /// <returns>是否开启HTTPS防窜站模式</returns>
        /// <exception cref="Exception">获取HTTPS防窜站模式失败</exception>
        public bool GetHttpsMode()
        {
            HttpRequestClass http = SendSite("get_https_mode");
            try
            {
                string responseBody = http.GetResponse().Body;

                if (bool.TryParse(responseBody, out bool result))
                {
                    return result;
                }

                throw new Exception($"获取https模式失败: 无法将响应 '{responseBody}' 解析为布尔值");
            }
            catch (Exception ex)
            {
                throw new Exception("获取https模式失败:" + ex.Message);
            }
        }

        /// <summary>
        /// 切换HTTPS防窜站模式
        /// </summary>
        /// <param name="enable">是否开启HTTPS防窜站模式</param>
        /// <returns>是否设置成功</returns>
        /// <exception cref="Exception">设置HTTPS防窜站模式失败</exception>
        public bool SetHttpsMode(bool enable)
        {
            HttpRequestClass http = SendSite("set_https_mode");
            try
            {
                dynamic dynamic = EasyJson.ParseJsonToDynamic(http.GetResponse().Body);
                return dynamic.status;
            }
            catch (Exception ex)
            {
                throw new Exception("设置https模式失败:" + ex.Message);
            }
        }

        /// <summary>
        /// 获取SSL协议版本
        /// </summary>
        /// <returns>SSL协议版本</returns>
        /// <exception cref="Exception">获取ssl协议失败</exception>
        public TLSversion GetSslProtocol()
        {
            HttpRequestClass http = SendSite("get_ssl_protocol");
            try
            {
                TLSversion tLSversion = EasyJson.ParseJsonObject<TLSversion>(http.GetResponse().Body);
                return tLSversion;
            }
            catch (Exception ex)
            {
                throw new Exception("获取ssl协议失败:" + ex.Message);
            }
        }

        /// <summary>
        /// 设置SSL协议版本
        /// </summary>
        /// <param name="tls">SSL协议版本(TLSv1.1,TLSv1.2,TLSv1.3,TLSv1)</param>
        /// <returns>是否设置成功</returns>
        /// <exception cref="Exception">设置ssl协议失败</exception>
        public bool SetSslProtocol(string[] tls)
        {
            Dictionary<string, string> tlsDict = new Dictionary<string, string>()
            {
                ["use_protocols"] = string.Join(",", tls)
            };
            HttpRequestClass http = SendSite("set_ssl_protocol", tlsDict);
            try
            {
                dynamic dynamic = EasyJson.ParseJsonToDynamic(http.GetResponse().Body);
                return dynamic.status;
            }
            catch (Exception ex)
            {
                throw new Exception("设置ssl协议失败:" + ex.Message);
            }
        }

        /// <summary>
        /// 获取cdn ip设置
        /// </summary>
        /// <returns>cdn ip设置</returns>
        /// <exception cref="Exception">获取cdn ip设置失败</exception>
        public CdnIpSettings GetCdnIpSettings()
        {
            HttpRequestClass http = SendSite("get_cdn_ip_settings");
            try
            {
                CdnIpSettings cdnIpSettings = EasyJson.ParseJsonObject<CdnIpSettings>(http.GetResponse().Body);
                return cdnIpSettings;
            }
            catch (Exception ex)
            {
                throw new Exception("获取cdn ip设置失败:" + ex.Message);
            }
        }

        /// <summary>
        /// 创建默认配置文件
        /// </summary>
        /// <exception cref="Exception">获取创建默认配置文件失败</exception>
        public void CreateDefaultConf()
        {
            HttpRequestClass http = SendSite("create_default_conf");
            try
            {
            }
            catch (Exception ex)
            {
                throw new Exception("获取创建默认配置文件失败:" + ex.Message);
            }
        }

        /// <summary>
        /// 获取网络信息
        /// </summary>
        /// <returns>网络信息</returns>
        /// <exception cref="Exception">获取网络信息失败</exception>
        public NetWorkInfo GetNetWork()
        {
            HttpRequestClass http = SendSite("get_network");
            try
            {
                NetWorkInfo netWorkInfo = EasyJson.ParseJsonObject<NetWorkInfo>(http.GetResponse().Body);
                return netWorkInfo;
            }
            catch (Exception ex)
            {
                throw new Exception("获取网络信息失败:" + ex.Message);
            }
        }

        /// <summary>
        /// 发送网站请求
        /// </summary>
        /// <param name="action">请求动作</param>
        /// <returns>请求结果</returns>
        /// <exception cref="Exception">发送网站请求失败</exception>
        HttpRequestClass SendSite(string action)
        {
            HttpRequestClass http = new HttpRequestClass();
            http.Open(BtPanel + "/site?action=" + action, HttpMethod.Post);
            var PostData = CreateForm();
            http.Send(PostData);
            return http;
        }

        /// <summary>
        /// 发送网站请求
        /// </summary>
        /// <param name="action">请求动作</param>
        /// <param name="Form">请求参数</param>
        /// <returns>请求结果</returns>
        /// <exception cref="Exception">发送网站请求失败</exception>
        HttpRequestClass SendSite(string action, Dictionary<string, string> Form)
        {
            HttpRequestClass http = new HttpRequestClass();
            http.Open(BtPanel + "/site?action=" + action, HttpMethod.Post);
            var PostData = CreateForm(Form);
            http.Send(PostData);
            return http;
        }
    }
}


