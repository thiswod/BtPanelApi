namespace BtPanelApi.site
{
    /// <summary>
    /// HTTPS 设置响应
    /// </summary>
    public class HttpsSettingsResponse
    {
        /// <summary>
        /// 请求是否成功
        /// </summary>
        public bool status { get; set; }

        /// <summary>
        /// 响应消息
        /// </summary>
        public string msg { get; set; } = string.Empty;

        /// <summary>
        /// HTTPS 设置数据
        /// </summary>
        public HttpsSettingsData data { get; set; } = new();

        /// <summary>
        /// 响应代码
        /// </summary>
        public int code { get; set; }

        /// <summary>
        /// 时间戳
        /// </summary>
        public long timestamp { get; set; }
    }

    /// <summary>
    /// HTTPS 设置数据
    /// </summary>
    public class HttpsSettingsData
    {
        /// <summary>
        /// 是否开启 HTTPS 防窜站模式
        /// </summary>
        public bool https_mode { get; set; }

        /// <summary>
        /// 是否开启 HTTP 自动跳转 HTTPS
        /// </summary>
        public bool http2https { get; set; }
    }
}
