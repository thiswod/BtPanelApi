using BtPanelApi.site;
using System.Security.Cryptography;
using System.Text;
using WodToolKit.Http;

namespace BtPanelApi
{
    /// <summary>
    /// BtPanel Api 基类
    /// </summary>
    /// <param name="BtPanel">BtPanel地址</param>
    /// <param name="BtKey">BtPanel密钥</param>
    public class BtPanel(string BtPanel,string BtKey)
    {
        /// <summary>
        /// 网站管理 PHP 模块
        /// </summary>
        public Php php => new Php(BtPanel, BtKey);
        /// <summary>
        /// 创建md5 hash
        /// </summary>
        /// <param name="Key">Key</param>
        /// <returns></returns>
        public string GetMd5Hash(string Key)
        {
            using var md5 = MD5.Create();
            var inputBytes = Encoding.ASCII.GetBytes(Key);
            var hashBytes = md5.ComputeHash(inputBytes);
            var sb = new StringBuilder();
            foreach (var t in hashBytes)
            {
                sb.Append(t.ToString("x2"));
            }

            return sb.ToString();
        }
        /// <summary>
        /// 创建表单
        /// </summary>
        /// <param name="Form"></param>
        /// <returns></returns>
        public Dictionary<string,string> CreateForm(Dictionary<string,string>? Form = null)
        {
            Form ??= new();
            // 宝塔接口要求每次请求都附带当前 Unix 时间戳，用于参与签名计算
            var requestTime = DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString();
            // request_token 的生成规则：md5(request_time + md5(接口密钥))
            var requestToken = GetMd5Hash(requestTime + GetMd5Hash(BtKey));
            // 将鉴权所需的 request_time 写入表单
            Form["request_time"] = requestTime;
            // 将计算出的签名 request_token 写入表单，供宝塔服务端校验
            Form["request_token"] = requestToken;
            return Form;
        }

        /// <summary>
        /// 发送请求
        /// </summary>
        /// <param name="path">接口路径</param>
        /// <param name="action">操作,可以为空</param>
        /// <param name="form">表单,可以为空</param>
        /// <returns>请求结果</returns>
        protected HttpRequestClass SendRequest(string path, string? action=null, Dictionary<string, string>? form = null)
        {
            HttpRequestClass http = new HttpRequestClass();
            if (action != null)
            {
                http.Open($"{BtPanel}{path}?action={action}", HttpMethod.Post);
            }
            else
            {
                http.Open($"{BtPanel}{path}", HttpMethod.Post);
            }
            http.Send(CreateForm(form));
            return http;
        }

        /// <summary>
        /// 发送请求（支持额外查询参数）
        /// </summary>
        /// <param name="path">接口路径</param>
        /// <param name="action">操作</param>
        /// <param name="queryParams">额外查询参数</param>
        /// <param name="form">表单</param>
        /// <returns>请求结果</returns>
        protected HttpRequestClass SendRequest(string path, string action, Dictionary<string, string>? queryParams, Dictionary<string, string>? form = null)
        {
            HttpRequestClass http = new HttpRequestClass();
            var url = $"{BtPanel}{path}?action={action}";
            if (queryParams != null)
            {
                foreach (var param in queryParams)
                {
                    url += $"&{Uri.EscapeDataString(param.Key)}={Uri.EscapeDataString(param.Value)}";
                }
            }
            http.Open(url, HttpMethod.Post);
            http.Send(CreateForm(form));
            return http;
        }
        /// <summary>
        /// 发送配置请求
        /// </summary>
        /// <param name="action">操作</param>
        /// <returns></returns>
        public HttpRequestClass SendConfig(string action) => SendRequest("/config", action);
        /// <summary>
        /// 发送配置请求
        /// </summary>
        /// <param name="action">操作</param>
        /// <param name="Form">表单</param>
        /// <returns></returns>
        public HttpRequestClass SendConfig(string action,Dictionary<string,string> Form) => SendRequest("/config", action, Form);
        /// <summary>
        /// 发送文件请求
        /// </summary>
        /// <param name="action">操作</param>
        /// <returns></returns>
        public HttpRequestClass SendFiles(string action) => SendRequest("/files", action);
        /// <summary>
        /// 发送文件请求
        /// </summary>
        /// <param name="action">操作</param>
        /// <param name="Form">表单</param>
        /// <returns></returns>
        public HttpRequestClass SendFiles(string action,Dictionary<string,string> Form) => SendRequest("/files", action, Form);
        /// <summary>
        /// 发送数据请求
        /// </summary>
        /// <param name="action">操作</param>
        /// <returns></returns>
        public HttpRequestClass SendData(string action) => SendRequest("/data", action);
        /// <summary>
        /// 发送数据请求
        /// </summary>
        /// <param name="action">操作</param>
        /// <param name="Form">表单</param>
        /// <returns></returns>
        public HttpRequestClass SendData(string action, Dictionary<string, string> Form) => SendRequest("/data", action, Form);

        /// <summary>
        /// 发送定时任务请求
        /// </summary>
        /// <param name="action">操作</param>
        /// <returns>请求结果</returns>
        public HttpRequestClass SendCronTab(string action) => SendRequest("/crontab", action);
        /// <summary>
        /// 发送定时任务请求
        /// </summary>
        /// <param name="action">操作</param>
        /// <param name="Form">表单</param>
        /// <returns>请求结果</returns>
        public HttpRequestClass SendCronTab(string action, Dictionary<string, string> Form) => SendRequest("/crontab", action, Form);
        /// <summary>
        /// 发送插件请求
        /// </summary>
        /// <param name="action">插件操作</param>
        /// <param name="name">插件名称</param>
        /// <returns>请求结果</returns>
        protected HttpRequestClass SendPlugin(string action, string name) =>
            SendRequest("/plugin", "a", new Dictionary<string, string> { ["name"] = name, ["s"] = action });

        /// <summary>
        /// 发送插件请求
        /// </summary>
        /// <param name="action">插件操作</param>
        /// <param name="name">插件名称</param>
        /// <param name="Form">表单</param>
        /// <returns>请求结果</returns>
        protected HttpRequestClass SendPlugin(string action, string name, Dictionary<string, string>? Form) =>
            SendRequest("/plugin", "a", new Dictionary<string, string> { ["name"] = name, ["s"] = action }, Form);
    }
}
