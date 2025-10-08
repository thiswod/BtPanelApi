using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BtPanelApi.site
{
    /// <summary>
    /// 网站管理 PHP网站类
    /// </summary>
    /// <param name="BtPanel">BtPanel地址</param>
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
            catch(Exception ex)
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
            catch(Exception ex)
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
            catch(Exception ex)
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
            var Result = SendFiles("GetFileBody",new Dictionary<string, string>()
            {
                ["path"] = path
            });
            try
            {
                FileBody FileBody = EasyJson.ParseJsonObject<FileBody>(Result.GetResponse().Body);
                return FileBody;
            }
            catch(Exception ex)
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
        public bool SaveFileBody(string path,string data,string encoding = "utf-8")
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
            catch(Exception ex)
            {
                throw new Exception("保存文件内容失败:" + ex.Message);
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
            return SaveFileBody("/www/server/panel/data/defaultDoc.html",data);
        }
        /// <summary>
        /// 获取404页面内容
        /// </summary>
        /// <returns>404页面文件内容</returns>
        /// <exception cref="Exception">获取404页面内容失败</exception>
        public FileBody Get404Page()
        {
            try
            {
                return GetFileBody("/www/server/panel/data/404.html");
            }
            catch(Exception ex)
            {
                throw new Exception("获取404页面内容失败:" + ex.Message);
            }
        }
        /// <summary>
        /// 设置404页面内容
        /// </summary>
        /// <param name="data">404页面文件内容</param>
        /// <returns>是否设置成功</returns>
        /// <exception cref="Exception">设置404页面内容失败</exception>
        public bool Set404Page(string data)
        {
            try
            {
                return SaveFileBody("/www/server/panel/data/404.html",data);
            }
            catch(Exception ex)
            {
                throw new Exception("设置404页面内容失败:" + ex.Message);
            }
        }
        public bool Get404PageStatus()
        {
            try
            {
                HttpRequestClass http = SendSite("get_404_config");
                dynamic dynamic = EasyJson.ParseJsonToDynamic(http.GetResponse().Body);
                return dynamic.status==1?true:false;
            }
            catch(Exception ex)
            {
                throw new Exception("获取404页面状态失败:" + ex.Message);
            }
        }
        HttpRequestClass SendSite(string action)
        {
            HttpRequestClass http = new HttpRequestClass();
            http.Open(BtPanel + "/site?action=" + action, HttpMethod.Post);
            var PostData = CreateForm();
            http.Send(PostData);
            return http;
        }
        HttpRequestClass SendSite(string action, Dictionary<string, string> Form)
        {
            HttpRequestClass http = new HttpRequestClass();
            http.Open(BtPanel + "/site?action=" + action, HttpMethod.Post);
            var PostData = CreateForm(Form);
            http.Send(PostData);
            return http;
        }

    }
    public class FileBody 
    { 
        /// <summary>
        /// 文件状态
        /// </summary>
        public bool status { get; set; }
        /// <summary>
        /// 是否只读
        /// </summary>
        public bool only_read { get; set; }
        /// <summary>
        /// 文件大小
        /// </summary>
        public int size { get; set; }
        /// <summary>
        /// 文件编码
        /// </summary>
        public string encoding { get; set; }
        /// <summary>
        /// 文件内容
        /// </summary>
        public string data { get; set; }
        /// <summary>
        /// 文件历史版本
        /// </summary>
        public List<object>? historys { get; set; }
        /// <summary>
        /// 是否自动保存
        /// </summary>
        public bool? auto_save { get; set; }
        /// <summary>
        /// 文件修改时间
        /// </summary>
        public long? st_mtime { get; set; }
    }
    /// <summary>
    /// PhpCli版本
    /// </summary>
    public class PhpCliVersion 
    {
        public Select Select { get; set; }
        public List<versions> versions { get; set; }
    }
    /// <summary>
    /// PhpCli版本选择
    /// </summary>
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
    /// <summary>
    /// Php版本列表
    /// </summary>
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
    /// <summary>
    /// 网站分类
    /// </summary>
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
