namespace BtPanelApi.site
{
    /// <summary>
    /// 创建默认配置文件
    /// </summary>
    public class CreateDefaultConf
    {
        public bool page_404 { get; set; }
        public bool page_index { get; set; }
        public bool log_split { get; set; }
        public bool cdn_recursive { get; set; }
        public bool cdn_ip { get; set; }
        public string log_path { get; set; } = string.Empty;
    }

    /// <summary>
    /// cdn ip设置
    /// </summary>
    public class CdnIpSettings
    {
        /// <summary>
        /// 是否开启递归查询
        /// </summary>
        public bool cdn_recursive { get; set; }
        /// <summary>
        /// 白名单IP
        /// </summary>
        public string white_ips { get; set; } = string.Empty;
        /// <summary>
        /// 是否开启CDN IP
        /// </summary>
        public bool cdn_ip { get; set; }
        /// <summary>
        /// CDN头信息
        /// </summary>
        public string header_cdn { get; set; } = string.Empty;
    }

    /// <summary>
    /// TLS 协议版本配置
    /// </summary>
    public class TLSversion
    {
        /// <summary>
        /// 是否启用 TLSv1。
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("TLSv1")]
        public bool TLSv1 { get; set; }
        /// <summary>
        /// 是否启用 TLSv1.1。
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("TLSv1.1")]
        public bool TLSv1_1 { get; set; }
        /// <summary>
        /// 是否启用 TLSv1.2。
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("TLSv1.2")]
        public bool TLSv1_2 { get; set; }
        /// <summary>
        /// 是否启用 TLSv1.3。
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("TLSv1.3")]
        public bool TLSv1_3 { get; set; }
    }

    /// <summary>
    /// 默认站点信息
    /// </summary>
    public class DefaultWebSite
    {
        /// <summary>
        /// 可选站点列表。
        /// </summary>
        public List<SiteItem> sites { get; set; } = [];
        /// <summary>
        /// 默认网站，可以是字符串（表示网站名称）或布尔值（false表示无默认网站）
        /// </summary>
        public dynamic defaultSite { get; set; } = false;
    }

    /// <summary>
    /// 站点项
    /// </summary>
    public class SiteItem
    {
        /// <summary>
        /// 网站名称
        /// </summary>
        public string name { get; set; } = string.Empty;
    }

    /// <summary>
    /// 文件内容信息
    /// </summary>
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
        public string encoding { get; set; } = string.Empty;
        /// <summary>
        /// 文件内容
        /// </summary>
        public string data { get; set; } = string.Empty;
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
        /// <summary>
        /// PhpCli版本选择
        /// </summary>
        public Select Select { get; set; } = new();
        /// <summary>
        /// PhpCli版本列表
        /// </summary>
        public List<PhpVersionItem> versions { get; set; } = [];
    }

    /// <summary>
    /// PhpCli版本选择
    /// </summary>
    public class Select
    {
        /// <summary>
        /// 版本号
        /// </summary>
        public string version { get; set; } = string.Empty;
        /// <summary>
        /// 版本名称
        /// </summary>
        public string name { get; set; } = string.Empty;
        /// <summary>
        /// 状态
        /// </summary>
        public bool status { get; set; }
    }

    /// <summary>
    /// Php版本列表项
    /// </summary>
    public class PhpVersionItem
    {
        /// <summary>
        /// 版本号
        /// </summary>
        public string version { get; set; } = string.Empty;
        /// <summary>
        /// 版本名称
        /// </summary>
        public string name { get; set; } = string.Empty;
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
        public string name { get; set; } = string.Empty;
    }
}
