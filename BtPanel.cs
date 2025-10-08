using BtPanelApi.site;
using System.Security.Cryptography;
using System.Text;

namespace BtPanelApi
{
    /// <summary>
    /// BtPanel Api
    /// </summary>
    /// <param name="BtPanel">面板URL地址</param>
    /// <param name="BtKey">ApiKey</param>
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
            var requestTime = DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString();
            var requestToken = GetMd5Hash(requestTime + GetMd5Hash(BtKey));
            Form.Add("request_time", requestTime);
            Form.Add("request_token", requestToken);
            return Form;
        }
        public HttpRequestClass SendConfig(string action)
        {
            HttpRequestClass http = new HttpRequestClass();
            http.Open(BtPanel + "/config?action=" + action, HttpMethod.Post);
            var PostData = CreateForm();
            http.Send(PostData);
            return http;
        }
        public HttpRequestClass SendConfig(string action,Dictionary<string,string> Form)
        {
            HttpRequestClass http = new HttpRequestClass();
            http.Open(BtPanel + "/config?action=" + action, HttpMethod.Post);
            var PostData = CreateForm(Form);
            http.Send(PostData);
            return http;
        }
        public Php php => new Php(BtPanel, BtKey);
    }
}
