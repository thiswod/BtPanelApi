namespace BtPanelApi.site
{
    /// <summary>
    /// 创建默认配置文件
    /// </summary>
    public class CreateDefaultConf
    {
        /// <summary>
        /// 是否创建 404 页面配置。
        /// </summary>
        public bool page_404 { get; set; }
        /// <summary>
        /// 是否创建默认首页配置。
        /// </summary>
        public bool page_index { get; set; }
        /// <summary>
        /// 是否开启日志切割。
        /// </summary>
        public bool log_split { get; set; }
        /// <summary>
        /// 是否开启 CDN 递归查询。
        /// </summary>
        public bool cdn_recursive { get; set; }
        /// <summary>
        /// 是否启用 CDN IP 设置。
        /// </summary>
        public bool cdn_ip { get; set; }
        /// <summary>
        /// 网站日志保存路径。
        /// </summary>
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
    /// 网站PHP版本信息
    /// </summary>
    public class SitePhpVersion
    {
        /// <summary>
        /// PHP版本号，如 "85" 表示 PHP 8.5
        /// </summary>
        public string phpversion { get; set; } = string.Empty;
        /// <summary>
        /// Tomcat状态，-1 表示未安装
        /// </summary>
        public int tomcat { get; set; }
        /// <summary>
        /// Tomcat版本，false 表示未安装，字符串表示版本号
        /// </summary>
        public dynamic tomcatversion { get; set; } = false;
        /// <summary>
        /// Node.js版本，false 表示未安装，字符串表示版本号
        /// </summary>
        public dynamic nodejsversion { get; set; } = false;
        /// <summary>
        /// 其他PHP版本信息
        /// </summary>
        public string php_other { get; set; } = string.Empty;
    }

    /// <summary>
    /// 安全告警插件首页信息
    /// </summary>
    public class SecurityNoticeIndex
    {
        /// <summary>
        /// PHP版本列表
        /// </summary>
        public List<SecurityNoticePhpVersion> php_versions { get; set; } = [];
        /// <summary>
        /// 总数
        /// </summary>
        public int total { get; set; }
        /// <summary>
        /// 安全时间
        /// </summary>
        public int safe_time { get; set; }
    }

    /// <summary>
    /// 安全告警插件中的PHP版本项
    /// </summary>
    public class SecurityNoticePhpVersion
    {
        /// <summary>
        /// 完整版本号
        /// </summary>
        public string version { get; set; } = string.Empty;
        /// <summary>
        /// 简短版本号
        /// </summary>
        public string v { get; set; } = string.Empty;
        /// <summary>
        /// 攻击测试命令
        /// </summary>
        public string attack { get; set; } = string.Empty;
        /// <summary>
        /// 防护模块状态
        /// </summary>
        public int state { get; set; }
        /// <summary>
        /// 站点数量
        /// </summary>
        public int site_count { get; set; }
    }

    /// <summary>
    /// 安全告警插件站点列表
    /// </summary>
    public class SecurityNoticeSites
    {
        /// <summary>
        /// 总数
        /// </summary>
        public int total { get; set; }
        /// <summary>
        /// 安全时间
        /// </summary>
        public int safe_time { get; set; }
        /// <summary>
        /// 站点列表
        /// </summary>
        public List<SecurityNoticeSite> sites { get; set; } = [];
    }

    /// <summary>
    /// 安全告警插件站点项
    /// </summary>
    public class SecurityNoticeSite
    {
        /// <summary>
        /// 站点路径
        /// </summary>
        public string path { get; set; } = string.Empty;
        /// <summary>
        /// 是否开启
        /// </summary>
        public bool open { get; set; }
        /// <summary>
        /// 站点名称
        /// </summary>
        public string site_name { get; set; } = string.Empty;
        /// <summary>
        /// 是否停用
        /// </summary>
        public int is_stop { get; set; }
        /// <summary>
        /// PHP版本
        /// </summary>
        public string version { get; set; } = string.Empty;
        /// <summary>
        /// 统计信息
        /// </summary>
        public SecurityNoticeSiteTotal total { get; set; } = new();
        /// <summary>
        /// 配置信息
        /// </summary>
        public SecurityNoticeSiteConfig config { get; set; } = new();
        /// <summary>
        /// 站点防护信息
        /// </summary>
        public SecurityNoticeSiteInfo site_info { get; set; } = new();
    }

    /// <summary>
    /// 安全告警插件站点统计信息
    /// </summary>
    public class SecurityNoticeSiteTotal
    {
        /// <summary>
        /// 总数
        /// </summary>
        public int total { get; set; }
        /// <summary>
        /// 当日总数
        /// </summary>
        public int day_total { get; set; }
    }

    /// <summary>
    /// 安全告警插件站点配置
    /// </summary>
    public class SecurityNoticeSiteConfig
    {
        /// <summary>
        /// 文件信息
        /// </summary>
        public Dictionary<string, object> file_info { get; set; } = new();
    }

    /// <summary>
    /// 安全告警插件站点防护信息
    /// </summary>
    public class SecurityNoticeSiteInfo
    {
        /// <summary>
        /// 是否开启
        /// </summary>
        public string open { get; set; } = string.Empty;
        /// <summary>
        /// SQL防护
        /// </summary>
        public string sql { get; set; } = string.Empty;
        /// <summary>
        /// 上传防护
        /// </summary>
        public string upload { get; set; } = string.Empty;
        /// <summary>
        /// Open_basedir防护
        /// </summary>
        public string open_basedir { get; set; } = string.Empty;
        /// <summary>
        /// SSRF防护
        /// </summary>
        public string ssrf { get; set; } = string.Empty;
        /// <summary>
        /// WebShell防护
        /// </summary>
        public string webshell { get; set; } = string.Empty;
        /// <summary>
        /// XSS防护
        /// </summary>
        public string xss { get; set; } = string.Empty;
        /// <summary>
        /// CSRF防护
        /// </summary>
        public string csrf { get; set; } = string.Empty;
        /// <summary>
        /// RCE防护
        /// </summary>
        public string rce { get; set; } = string.Empty;
        /// <summary>
        /// 执行防护
        /// </summary>
        public string execution { get; set; } = string.Empty;
        /// <summary>
        /// 下载防护
        /// </summary>
        public string download { get; set; } = string.Empty;
        /// <summary>
        /// 写入防护
        /// </summary>
        public string write { get; set; } = string.Empty;
        /// <summary>
        /// 包含防护
        /// </summary>
        public string include { get; set; } = string.Empty;
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

    /// <summary>
    /// 安全告警添加站点配置响应
    /// </summary>
    public class SecurityNoticeAddSiteConfigResponse
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool status { get; set; }
        /// <summary>
        /// 消息
        /// </summary>
        public string msg { get; set; } = string.Empty;
    }

    /// <summary>
    /// 扫描配置
    /// </summary>
    public class ScanConfig
    {
        /// <summary>
        /// 扫描参数
        /// </summary>
        public bool scan_args { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public bool title { get; set; }
        /// <summary>
        /// 关键词
        /// </summary>
        public bool keywords { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public bool descriptions { get; set; }
        /// <summary>
        /// 标题哈希
        /// </summary>
        public bool title_hash { get; set; }
        /// <summary>
        /// 尾部哈希
        /// </summary>
        public bool tail_hash { get; set; }
        /// <summary>
        /// 搜索监控
        /// </summary>
        public bool search_monitor { get; set; }
        /// <summary>
        /// 扫描UA
        /// </summary>
        public string scan_ua { get; set; } = string.Empty;
        /// <summary>
        /// 词库
        /// </summary>
        public int thesaurus { get; set; }
    }

    /// <summary>
    /// 定时任务信息
    /// </summary>
    public class CrontabInfo
    {
        /// <summary>
        /// 任务ID
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 任务名称
        /// </summary>
        public string name { get; set; } = string.Empty;
        /// <summary>
        /// 执行周期类型
        /// </summary>
        public string type { get; set; } = string.Empty;
        /// <summary>
        /// 执行小时
        /// </summary>
        public int where_hour { get; set; }
        /// <summary>
        /// 执行分钟
        /// </summary>
        public int where_minute { get; set; }
        /// <summary>
        /// 添加时间
        /// </summary>
        public string addtime { get; set; } = string.Empty;
        /// <summary>
        /// 状态
        /// </summary>
        public int status { get; set; }
        /// <summary>
        /// 执行脚本
        /// </summary>
        public string sBody { get; set; } = string.Empty;
        /// <summary>
        /// 循环描述
        /// </summary>
        public string cycle { get; set; } = string.Empty;
    }

    /// <summary>
    /// 内容监控信息
    /// </summary>
    public class ContentMonitorInfo
    {
        /// <summary>
        /// 监控ID
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 监控名称
        /// </summary>
        public string name { get; set; } = string.Empty;
        /// <summary>
        /// 监控方法
        /// </summary>
        public int method { get; set; }
        /// <summary>
        /// 站点名称
        /// </summary>
        public string site_name { get; set; } = string.Empty;
        /// <summary>
        /// 监控URL
        /// </summary>
        public string url { get; set; } = string.Empty;
        /// <summary>
        /// 创建时间
        /// </summary>
        public int time { get; set; }
        /// <summary>
        /// 是否本地
        /// </summary>
        public int is_local { get; set; }
        /// <summary>
        /// 发送消息
        /// </summary>
        public int send_msg { get; set; }
        /// <summary>
        /// 定时任务ID
        /// </summary>
        public int cron_id { get; set; }
        /// <summary>
        /// 扫描配置
        /// </summary>
        public ScanConfig scan_config { get; set; } = new();
        /// <summary>
        /// 定时任务状态
        /// </summary>
        public int crontab_status { get; set; }
        /// <summary>
        /// 定时任务信息
        /// </summary>
        public CrontabInfo crontab_info { get; set; } = new();
        /// <summary>
        /// 最后扫描时间
        /// </summary>
        public List<object> last_scan_time { get; set; } = [];
        /// <summary>
        /// 测试ID
        /// </summary>
        public string testing_id { get; set; } = string.Empty;
        /// <summary>
        /// 最后风险数量
        /// </summary>
        public int last_risk_count { get; set; }
    }

    /// <summary>
    /// 添加内容监控请求
    /// </summary>
    public class AddContentMonitorRequest
    {
        /// <summary>
        /// 监控名称
        /// </summary>
        public string name { get; set; } = string.Empty;
        /// <summary>
        /// 监控方法
        /// </summary>
        public int method { get; set; }
        /// <summary>
        /// 站点名称
        /// </summary>
        public string site_name { get; set; } = string.Empty;
        /// <summary>
        /// 监控URL
        /// </summary>
        public string url { get; set; } = string.Empty;
        /// <summary>
        /// 发送消息
        /// </summary>
        public int send_msg { get; set; }
        /// <summary>
        /// 执行周期
        /// </summary>
        public string type { get; set; } = string.Empty;
        /// <summary>
        /// 执行小时
        /// </summary>
        public int hour { get; set; }
        /// <summary>
        /// 执行分钟
        /// </summary>
        public int minute { get; set; }
        /// <summary>
        /// 监控ID
        /// </summary>
        public string id { get; set; } = string.Empty;
        /// <summary>
        /// 扫描配置
        /// </summary>
        public ScanConfig scan_config { get; set; } = new();
    }
}
