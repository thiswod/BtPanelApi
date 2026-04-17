# BtPanelApi

BtPanel (宝塔面板) 的 C# API 封装库，提供类型安全的接口用于管理网站、PHP 配置、SSL 证书、安全防护等功能。

## 环境要求

- .NET 10.0
- WodToolKit 1.0.2.6+

## 安装

```bash
dotnet add package WodToolKit
```

## 快速开始

```csharp
using BtPanelApi;

// 创建 API 实例
var bt = new BtPanel("http://127.0.0.1:8888", "your_api_key");

// 获取 PHP CLI 版本
var cliVersion = bt.php.GetCliPhpVersion();
Console.WriteLine($"当前 PHP CLI 版本: {cliVersion.Select.version}");

// 设置网站 PHP 版本
bt.php.SetSitePHPVersion("example.com", "81");

// 获取网络信息
var network = bt.php.GetNetWork();
```

## 主要功能

### 网站管理
- **PHP 版本管理**
  - 获取/设置 PHP CLI 版本
  - 获取/设置网站 PHP 版本
  - PHP Session 隔离配置
- **网站分类**
  - 获取网站分类分组
- **默认站点**
  - 获取/设置默认站点

### 默认页面配置
- **404 页面**: 获取/设置 404 页面内容和状态
- **默认首页**: 获取/设置默认首页内容
- **无网站页面**: 获取/设置无网站时的提示页面
- **停用页面**: 获取/设置网站停用后的提示页面

### SSL/HTTPS 管理
- 获取/设置 HTTPS 配置
- 切换 HTTPS 防窜站模式
- 全局 HTTP 转 HTTPS 设置
- TLS 协议版本管理 (TLSv1/1.1/1.2/1.3)
- 获取/设置网站 HTTPS 端口

### 网站配置
- 获取/设置伪静态配置
- CDN IP 设置和白名单管理
- 网络信息查询

### 安全防护
- **安全告警插件**
  - 获取安全防护状态概览
  - 获取站点安全防护状态
  - 开启/配置站点安全防护
  - 支持多种防护类型 (SQL注入、XSS、CSRF、RCE、WebShell 等)
- **内容监控**
  - 添加内容监控任务
  - 获取站点内容监控列表
  - 删除内容监控任务

### 日志管理
- 获取网站访问日志
- 支持 IP 地区显示

### 文件操作
- 读取文件内容 (支持编码检测)
- 保存文件内容

## API 示例

### PHP 版本管理

```csharp
// 获取 CLI PHP 版本
var cliVersion = bt.php.GetCliPhpVersion();
foreach (var ver in cliVersion.versions)
{
    Console.WriteLine($"{ver.name}: {ver.status}");
}

// 设置 CLI PHP 版本为 8.1
bt.php.SetCliPhpVersion("81");

// 获取网站 PHP 版本
var siteVersion = bt.php.GetSitePHPVersion("example.com");
Console.WriteLine($"网站 PHP 版本: {siteVersion.phpversion}");

// 设置网站 PHP 版本
bt.php.SetSitePHPVersion("example.com", "84");
```

### 默认页面管理

```csharp
// 获取 404 页面内容
var page404 = bt.php.Get404Page();
Console.WriteLine(page404.data);

// 设置自定义 404 页面
bt.php.Set404Page("<h1>页面未找到</h1>");

// 启用 404 页面
bt.php.Set404PageStatus(true);

// 设置默认首页
bt.php.SetDefaultPage("<h1>欢迎访问</h1>");
```

### HTTPS 配置

```csharp
// 获取 HTTPS 设置
var httpsSettings = bt.php.GetHttpsSettings();

// 获取 TLS 版本配置
var tlsVersion = bt.php.GetSslProtocol();
Console.WriteLine($"TLSv1.3: {tlsVersion.TLSv1_3}");

// 设置 TLS 版本 (仅启用 TLSv1.2 和 TLSv1.3)
bt.php.SetSslProtocol(new[] { "TLSv1.2", "TLSv1.3" });

// 设置全局 HTTP 转 HTTPS
bt.php.SetGlobal_Http2Https(1);

// 设置网站 HTTPS 端口
bt.php.SetHttpsPort("example.com", 443);
```

### 安全防护

```csharp
// 获取安全防护概览
var securityIndex = bt.php.GetSecurityIndex();
Console.WriteLine($"总站点数: {securityIndex.total}");

// 获取所有站点的安全状态
var sites = bt.php.GetSecuritySites();
foreach (var site in sites.sites)
{
    Console.WriteLine($"{site.site_name}: {site.site_info.open}");
}

// 开启站点安全防护
bt.php.StartSiteSecurity("example.com");

// 添加站点到安全告警防护
bt.php.AddSiteSecurityConfig(
    "/www/wwwroot/example.com",
    "dir",
    "example.com",
    "1,0,0,0" // 开启目录文件读取告警
);
```

### 内容监控

```csharp
// 添加内容监控任务
var monitorRequest = new AddContentMonitorRequest
{
    name = "首页监控",
    method = 1,
    site_name = "example.com",
    url = "https://example.com",
    send_msg = 1,
    type = "day",
    hour = 9,
    minute = 0,
    scan_config = new ScanConfig
    {
        title = true,
        keywords = true,
        scan_args = true
    }
};
bt.php.AddContentMonitorInfo(monitorRequest);

// 获取站点内容监控列表
var monitors = bt.php.GetSiteContentMonitorList("example.com");

// 删除内容监控任务
bt.php.DelContentMonitorInfo(1);
```

### CDN 和网络

```csharp
// 获取 CDN IP 设置
var cdnSettings = bt.php.GetCdnIpSettings();
Console.WriteLine($"CDN 递归查询: {cdnSettings.cdn_recursive}");

// 获取网络信息
var network = bt.php.GetNetWork();
```

### 网站日志

```csharp
// 获取网站日志 (显示 IP 地区)
var logs = bt.php.GetSiteLogs("example.com", 1);
Console.WriteLine(logs);
```

## 项目结构

```
BtPanelApi/
├── BtPanel.cs                      # API 基类，提供认证和请求方法
└── site/
    ├── Php.cs                       # PHP 网站管理模块
    └── Models/
        ├── CommonModels.cs          # 通用数据模型
        ├── HttpsSettingsModels.cs   # HTTPS 配置模型
        └── NetworkModels.cs         # 网络信息模型
```

## 错误处理

所有 API 方法在失败时会抛出 `Exception`，异常信息包含具体的错误描述：

```csharp
try
{
    bt.php.SetSitePHPVersion("example.com", "81");
}
catch (Exception ex)
{
    Console.WriteLine($"设置失败: {ex.Message}");
}
```

## 许可证

MIT
