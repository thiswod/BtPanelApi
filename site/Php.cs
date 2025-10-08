using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BtPanelApi.site
{
    public class Php(string BtPanel, string BtKey) : BtPanel(BtPanel, BtKey)
    {
        /// <summary>
        /// 获取网站分类分组
        /// </summary>
        /// <returns></returns>
        public List<Types> GetSiteTypes()
        {
            try
            {
                var Result = SendSite("get_site_types");
                List<Types> types = EasyJson.ParseJsonObject<List<Types>>(Result.GetResponse().Body);
                return types;
            }
            catch(Exception ex)
            {
                throw new Exception("获取网站分类分组失败:" + ex.Message);
                return null;
            }
        }
        /// <summary>
        /// 获取PHPCli版本
        /// </summary>
        public PhpCliVersion GetCliPhpVersion()
        {
            var Result = SendConfig("get_cli_php_version");
            PhpCliVersion PhpCliVersion = EasyJson.ParseJsonObject<PhpCliVersion>(Result.GetResponse().Body);
            return PhpCliVersion;
        }
        /// <summary>
        /// 设置PHPCli版本
        /// </summary>
        /// <param name="php_version">PHP版本 如：74、83、84</param>
        /// <returns></returns>
        public PhpCliVersion SetCliPhpVersion(string php_version)
        {
            var Result = SendConfig("set_cli_php_version", new Dictionary<string, string>()
            {
                ["php_version"] = php_version
            });
            PhpCliVersion PhpCliVersion = EasyJson.ParseJsonObject<PhpCliVersion>(Result.GetResponse().Body);
            return PhpCliVersion;
        }
        public HttpRequestClass SendSite(string action)
        {
            HttpRequestClass http = new HttpRequestClass();
            http.Open(BtPanel + "/site?action=" + action, HttpMethod.Post);
            var PostData = CreateForm();
            http.Send(PostData);
            return http;
        }
        public HttpRequestClass SendSite(string action, Dictionary<string, string> Form)
        {
            HttpRequestClass http = new HttpRequestClass();
            http.Open(BtPanel + "/site?action=" + action, HttpMethod.Post);
            var PostData = CreateForm(Form);
            http.Send(PostData);
            return http;
        }

    }
    public class PhpCliVersion 
    {
        public Select Select { get; set; }
        public List<versions> versions { get; set; }
    }
    public class Select
    {
        /// <summary>
        /// 版本号
        /// </summary>
        public string version { get; set; }
        /// <summary>
        /// 版本名称
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public bool status { get; set; }
    }
    public class versions
    {
        /// <summary>
        /// 版本号
        /// </summary>
        public string version { get; set; }
        /// <summary>
        /// 版本名称
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public bool status { get; set; }
    }
    public class Types
    {
        /// <summary>
        /// 分类ID
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 分类名称
        /// </summary>
        public string name { get; set; }
    }
}
