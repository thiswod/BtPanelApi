namespace BtPanelApi.site
{
    /// <summary>
    /// 网络信息
    /// </summary>
    public class NetWorkInfo
    {
        /// <summary>
        /// 网卡信息集合，键为网卡名称，如 lo、eth0、docker0。
        /// </summary>
        public Dictionary<string, NetworkInterfaceStats> network { get; set; } = [];
        /// <summary>
        /// 全部网卡累计上传总字节数。
        /// </summary>
        public long upTotal { get; set; }
        /// <summary>
        /// 全部网卡累计下载总字节数。
        /// </summary>
        public long downTotal { get; set; }
        /// <summary>
        /// 当前总上传速度。
        /// </summary>
        public double up { get; set; }
        /// <summary>
        /// 当前总下载速度。
        /// </summary>
        public double down { get; set; }
        /// <summary>
        /// 全部网卡累计下载数据包数量。
        /// </summary>
        public long downPackets { get; set; }
        /// <summary>
        /// 全部网卡累计上传数据包数量。
        /// </summary>
        public long upPackets { get; set; }
        /// <summary>
        /// CPU信息原始数组。
        /// </summary>
        public List<object> cpu { get; set; } = [];
        /// <summary>
        /// CPU时间统计信息。
        /// </summary>
        public CpuTimes cpu_times { get; set; } = new();
        /// <summary>
        /// 系统负载信息。
        /// </summary>
        public LoadInfo load { get; set; } = new();
        /// <summary>
        /// 内存信息。
        /// </summary>
        public MemInfo mem { get; set; } = new();
        /// <summary>
        /// 面板或接口版本。
        /// </summary>
        public string version { get; set; } = string.Empty;
        /// <summary>
        /// 磁盘分区信息列表。
        /// </summary>
        public List<DiskInfo> disk { get; set; } = [];
        /// <summary>
        /// 服务器标题。
        /// </summary>
        public string title { get; set; } = string.Empty;
        /// <summary>
        /// 系统运行时间文本。
        /// </summary>
        public string time { get; set; } = string.Empty;
        /// <summary>
        /// 站点总数。
        /// </summary>
        public int site_total { get; set; }
        /// <summary>
        /// FTP总数。
        /// </summary>
        public int ftp_total { get; set; }
        /// <summary>
        /// 数据库总数。
        /// </summary>
        public int database_total { get; set; }
        /// <summary>
        /// 完整系统信息。
        /// </summary>
        public string system { get; set; } = string.Empty;
        /// <summary>
        /// 简化系统信息。
        /// </summary>
        public string simple_system { get; set; } = string.Empty;
        /// <summary>
        /// 是否已安装完成。
        /// </summary>
        public bool installed { get; set; }
        /// <summary>
        /// 磁盘 IO 统计集合，键为设备名，如 ALL、sda、nvme0n1。
        /// </summary>
        public Dictionary<string, IoStatInfo> iostat { get; set; } = [];
        /// <summary>
        /// Docker 是否正在运行。
        /// </summary>
        public bool docker_run { get; set; }
    }

    /// <summary>
    /// 网卡统计信息
    /// </summary>
    public class NetworkInterfaceStats
    {
        /// <summary>
        /// 该网卡累计上传总字节数。
        /// </summary>
        public long upTotal { get; set; }
        /// <summary>
        /// 该网卡累计下载总字节数。
        /// </summary>
        public long downTotal { get; set; }
        /// <summary>
        /// 该网卡当前上传速度。
        /// </summary>
        public double up { get; set; }
        /// <summary>
        /// 该网卡当前下载速度。
        /// </summary>
        public double down { get; set; }
        /// <summary>
        /// 该网卡累计下载数据包数量。
        /// </summary>
        public long downPackets { get; set; }
        /// <summary>
        /// 该网卡累计上传数据包数量。
        /// </summary>
        public long upPackets { get; set; }
    }

    /// <summary>
    /// CPU时间统计
    /// </summary>
    public class CpuTimes
    {
        /// <summary>
        /// 用户态 CPU 占用百分比。
        /// </summary>
        public double user { get; set; }
        /// <summary>
        /// nice 值调整后用户态 CPU 占用百分比。
        /// </summary>
        public double nice { get; set; }
        /// <summary>
        /// 内核态 CPU 占用百分比。
        /// </summary>
        public double system { get; set; }
        /// <summary>
        /// 空闲 CPU 百分比。
        /// </summary>
        public double idle { get; set; }
        /// <summary>
        /// IO 等待 CPU 百分比。
        /// </summary>
        public double iowait { get; set; }
        /// <summary>
        /// 硬中断 CPU 百分比。
        /// </summary>
        public double irq { get; set; }
        /// <summary>
        /// 软中断 CPU 百分比。
        /// </summary>
        public double softirq { get; set; }
        /// <summary>
        /// 虚拟化环境中被其他虚拟机占用的 CPU 百分比。
        /// </summary>
        public double steal { get; set; }
        /// <summary>
        /// 运行虚拟 CPU 的时间占比。
        /// </summary>
        public double guest { get; set; }
        /// <summary>
        /// 运行带 nice 值虚拟 CPU 的时间占比。
        /// </summary>
        public double guest_nice { get; set; }

        /// <summary>
        /// 系统总进程数。
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("总进程数")]
        public int total_process_count { get; set; }

        /// <summary>
        /// 当前活动进程数。
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("活动进程数")]
        public int active_process_count { get; set; }
    }

    /// <summary>
    /// 系统负载信息
    /// </summary>
    public class LoadInfo
    {
        /// <summary>
        /// 1分钟平均负载。
        /// </summary>
        public double one { get; set; }
        /// <summary>
        /// 5分钟平均负载。
        /// </summary>
        public double five { get; set; }
        /// <summary>
        /// 15分钟平均负载。
        /// </summary>
        public double fifteen { get; set; }
        /// <summary>
        /// 最大负载参考值。
        /// </summary>
        public int max { get; set; }
        /// <summary>
        /// 当前负载上限。
        /// </summary>
        public int limit { get; set; }
        /// <summary>
        /// 安全负载阈值。
        /// </summary>
        public double safe { get; set; }
    }

    /// <summary>
    /// 内存信息
    /// </summary>
    public class MemInfo
    {
        /// <summary>
        /// 总内存大小。
        /// </summary>
        public int memTotal { get; set; }
        /// <summary>
        /// 空闲内存大小。
        /// </summary>
        public int memFree { get; set; }
        /// <summary>
        /// 缓冲区内存大小。
        /// </summary>
        public int memBuffers { get; set; }
        /// <summary>
        /// 缓存内存大小。
        /// </summary>
        public int memCached { get; set; }
        /// <summary>
        /// 可用内存大小。
        /// </summary>
        public int memAvailable { get; set; }
        /// <summary>
        /// 共享内存大小。
        /// </summary>
        public int memShared { get; set; }
        /// <summary>
        /// 实际已使用内存大小。
        /// </summary>
        public int memRealUsed { get; set; }
        /// <summary>
        /// 格式化后的总内存列表，如 ["31.2", "GB"]。
        /// </summary>
        public List<string> memNewTotalList { get; set; } = [];
        /// <summary>
        /// 格式化后的实际已用内存列表，如 ["7.1", "GB"]。
        /// </summary>
        public List<string> memNewRealUsedList { get; set; } = [];
        /// <summary>
        /// 格式化后的实际已用内存值。
        /// </summary>
        public string memNewRealUsed { get; set; } = string.Empty;
        /// <summary>
        /// 格式化后的总内存值。
        /// </summary>
        public string memNewTotal { get; set; } = string.Empty;
    }

    /// <summary>
    /// 磁盘信息
    /// </summary>
    public class DiskInfo
    {
        /// <summary>
        /// 字节级磁盘容量信息，通常为总量、已用、可用。
        /// </summary>
        public List<long> byte_size { get; set; } = [];
        /// <summary>
        /// 挂载路径。
        /// </summary>
        public string path { get; set; } = string.Empty;
        /// <summary>
        /// 格式化后的磁盘容量信息列表。
        /// </summary>
        public List<string> size { get; set; } = [];
        /// <summary>
        /// 文件系统设备名。
        /// </summary>
        public string filesystem { get; set; } = string.Empty;
        /// <summary>
        /// 文件系统类型。
        /// </summary>
        public string type { get; set; } = string.Empty;
        /// <summary>
        /// inode 信息列表。
        /// </summary>
        public List<object> inodes { get; set; } = [];
        /// <summary>
        /// 磁盘大小附加信息。
        /// </summary>
        public string d_size { get; set; } = string.Empty;
        /// <summary>
        /// 真实挂载名称。
        /// </summary>
        public string rname { get; set; } = string.Empty;
    }

    /// <summary>
    /// 磁盘IO统计
    /// </summary>
    public class IoStatInfo
    {
        /// <summary>
        /// 读操作次数。
        /// </summary>
        public long read_count { get; set; }
        /// <summary>
        /// 写操作次数。
        /// </summary>
        public long write_count { get; set; }
        /// <summary>
        /// 读取字节数。
        /// </summary>
        public long read_bytes { get; set; }
        /// <summary>
        /// 写入字节数。
        /// </summary>
        public long write_bytes { get; set; }
        /// <summary>
        /// 读耗时。
        /// </summary>
        public long read_time { get; set; }
        /// <summary>
        /// 写耗时。
        /// </summary>
        public long write_time { get; set; }
        /// <summary>
        /// 合并读请求次数。
        /// </summary>
        public long read_merged_count { get; set; }
        /// <summary>
        /// 合并写请求次数。
        /// </summary>
        public long write_merged_count { get; set; }
    }
}
