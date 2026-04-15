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
        /// 创建md5 hash
        /// </summary>
        /// <param name="Key">Key</param>
        /// <returns></returns>
        public string GetMd5Hash(string Key)
        {
            var md5 = MD5.Create();
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
            Form.Add("request_time", requestTime);
            // 将计算出的签名 request_token 写入表单，供宝塔服务端校验
            Form.Add("request_token", requestToken);
            return Form;
        }
        /// <summary>
        /// 发送配置请求
        /// </summary>
        /// <param name="action">操作</param>
        /// <returns></returns>
        public HttpRequestClass SendConfig(string action)
        {
            HttpRequestClass http = new HttpRequestClass();
            http.Open(BtPanel + "/config?action=" + action, HttpMethod.Post);
            var PostData = CreateForm();
            http.Send(PostData);
            return http;
        }
        /// <summary>
        /// 发送配置请求
        /// </summary>
        /// <param name="action">操作</param>
        /// <param name="Form">表单</param>
        /// <returns></returns>
        public HttpRequestClass SendConfig(string action,Dictionary<string,string> Form)
        {
            HttpRequestClass http = new HttpRequestClass();
            http.Open(BtPanel + "/config?action=" + action, HttpMethod.Post);
            var PostData = CreateForm(Form);
            http.Send(PostData);
            return http;
        }
        /// <summary>
        /// 发送文件请求
        /// </summary>
        /// <param name="action">操作</param>
        /// <returns></returns>
        public HttpRequestClass SendFiles(string action)
        {
            HttpRequestClass http = new HttpRequestClass();
            http.Open(BtPanel + "/files?action=" + action, HttpMethod.Post);
            var PostData = CreateForm();
            http.Send(PostData);
            return http;
        }
        /// <summary>
        /// 发送文件请求
        /// </summary>
        /// <param name="action">操作</param>
        /// <param name="Form">表单</param>
        /// <returns></returns>
        public HttpRequestClass SendFiles(string action,Dictionary<string,string> Form)
        {
            HttpRequestClass http = new HttpRequestClass();
            http.Open(BtPanel + "/files?action=" + action, HttpMethod.Post);
            var PostData = CreateForm(Form);
            http.Send(PostData);
            return http;
        }
        public Php php => new Php(BtPanel, BtKey);
    }
}
