# BtPanelApi

BtPanel (宝塔面板) 的 C# API 封装库，提供类型安全的接口用于管理网站、PHP 配置、SSL 证书等。

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
var version = await bt.Php.GetPhpCliVersion();

// 设置网站 PHP 版本
await bt.Php.SetSitePhpVersion("example.com", "74");

// 获取网络信息
var network = await bt.Php.GetNetworkInfo();
```

## 功能

### 网站管理
- 获取/设置网站 PHP 版本
- 默认页面配置 (404、停用、无站点等)
- 伪静态规则
- 文件读写

### SSL/HTTPS
- HTTPS 设置管理
- TLS 版本配置

### CDN
- CDN IP 设置
- 源站 IP 配置

### 系统信息
- 网络接口统计
- CPU/内存/磁盘信息
- Docker 状态

## 项目结构

```
BtPanelApi/
├── BtPanel.cs              # API 入口
└── site/
    ├── Php.cs               # PHP 网站模块
    └── Models/              # 数据模型
```

## 许可证

MIT
