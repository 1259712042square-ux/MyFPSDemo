# MyFPSDemo

基于 Unreal Engine 5 First Person 模板开发的局域网 1v1 第一人称射击对战 Demo。

## 游戏概述

两名玩家通过局域网进入同一张地图，在 60 秒内争夺金币。地图中持续存在 5 个 AI 敌人和 7 枚金币，玩家可以拾取金币得分，也可以使用枪械攻击敌人或对方玩家。比赛结束后根据金币数量判定胜负。

### 核心玩法

- 拾取金币增加分数，击杀敌人和对方玩家
- AI 敌人主动追踪并攻击玩家，射击命中可一击击杀
- 玩家死亡后金币清零，5 秒倒计时后可随机重生
- 60 秒比赛倒计时，时间结束后比较金币数量判定胜负

## 技术实现

### 多人网络架构

采用 Listen Server 模式，通过三个核心蓝图实现多人同步：

- **BP_FPSGameMode**（仅服务器端）：负责比赛流程控制、敌人/金币动态生成与补刷、玩家死亡和重生处理
- **BP_FPSGameState**：同步全局比赛状态（剩余时间、比赛进行状态），通过 Replicated + RepNotify 驱动客户端 HUD 更新
- **BP_FPSPlayerState**：保存玩家金币数据，通过复制机制同步到所有客户端

### 敌人 AI

- 基于 BP_EnnemyAI 蓝图，开启 Replicates 和 Replicate Movement
- AI 逻辑仅在服务器端运行（Has Authority 判断），客户端只接收同步数据
- 通过 OnSeePawn 感知玩家，使用 SphereTrace 检测攻击命中
- 攻击动画通过 Multicast 广播至所有客户端
- 敌人死亡后由 GameMode 在随机 NavMesh 位置补刷，始终维持 5 个

### 射击与伤害系统

- 射击输入通过 ServerFire RPC 发送至服务器，由服务器生成子弹
- 子弹命中敌人直接击杀，命中玩家造成 5 点伤害（一击必杀）
- 包含自伤判断（Instigator 检查）
- 生命值通过 BP_HealthComponent 管理，CurrentHealth 使用 RepNotify 同步 HUD

### 金币系统

- 拾取逻辑仅在服务器端执行（Has Authority 判断）
- 金币数据存储于 PlayerState，通过 Replicated + RepNotify 自动刷新 HUD
- 始终维持地图中 7 枚金币，被拾取后在随机 NavMesh 位置补刷
- 玩家死亡时金币清零

### 联机大厅

- 主菜单关卡 MainMenu_Map + WBP_LobbyMenu 大厅界面
- 创建房间：通过 `Open Level(地图名?listen)` 启动 Listen Server
- 加入房间：客户端输入主机局域网 IP，通过 `open [IP]` 连接

## 运行环境

- Unreal Engine 5.x
- Windows 10/11

## 如何运行

1. 安装对应版本的 Unreal Engine
2. 克隆本仓库
3. 双击 `FPS_Game.uproject` 打开项目
4. 运行游戏，在大厅中创建或加入房间

## 已知问题

- 未使用 FirstPersonArm 区分第一人称和第三人称模型，导致持枪姿势在不同视角下表现不一致
- 子弹生成位置改为摄像机中央以保证第一人称体验，但第三人称视角下表现异常
- 固定点武器被拾取后未在场景中正常销毁

## 参考资料

- PVE 部分参考 B 站教程：[在2025年使用UE5.5制作你的第一款游戏](https://www.bilibili.com/video/BV1EN6nYaEZp/)
