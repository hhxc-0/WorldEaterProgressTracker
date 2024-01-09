<div align="center">
  
# 世界吞噬者进度追踪器

[English](/README.md) | 简体中文

这是一个简单的 [Minecraft 控制台客户端 (MCC)](https://mccteam.github.io/) 脚本，它使得 Minecraft 红石服务器上的玩家可以在不进入游戏的情况下监控世界吞噬者的进度。
它使用 [MCDReforged](https://github.com/Fallen-Breath/MCDReforged) 的 [Where Is](https://github.com/Lazy-Bing-Server/WhereIs-MCDR) 插件提供的 `!!vris` 命令来追踪附着在世界吞噬者飞行机器上的挂机玩家或假人的位置。

</div>

## 如何使用
### 下载脚本
请下载脚本，并将其放置在与您的 Minecraft 控制台客户端 (MCC) 可执行文件相同的文件夹中。
### 必须设置的变量
要使用此脚本，您必须根据您的世界吞噬者的条件，在脚本开头修改这些变量：\
`botName`：挂机玩家或假人的名称。\
`worldEaterDirection`：世界吞噬者的运行方向，可以设置为 `worldEaterDirections.xDirection` 或 `worldEaterDirections.yDirection`。\
`worldEaterStartPos`：假人在运行方向上可以达到的最小坐标。\
`worldEaterEndPos`：假人在运行方向上可以达到的最大坐标。

### 可选设置的变量
您还可以设置这些可选变量：\
`autoReconnect`：当连接丢失时是否尝试重新连接。\
`runningRetrieveInterval`：世界吞噬者运行时的数据检索间隔（**秒**）。\
`stoppedRetrieveInterval`：世界吞噬者停止时的数据检索间隔（**秒**）。
> 通过将间隔分成两个变量，您可以设置较低的运行间隔以获得准确数据，并设置较高的停止间隔以避免刷屏。

`refreshInterval`：显示刷新间隔（**毫秒**）。\
`debugMode`：显示额外的调试信息。

### 在 MCC 中运行脚本
在使用MCC登录到您的服务器后，使用命令 `/script WorldEaterProgressTracker.cs` 来运行脚本。
如需额外指导，您可以参考 [官方 MCC 文档](https://mccteam.github.io/guide/)。
