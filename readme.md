# 项目介绍

本项目是一个基于 ASP.NET Core 6.0 的 WebAPI 后端服务，集成了 SqlSugar 作为 ORM 框架，支持 JWT 鉴权、全局异常处理、统一响应格式、Swagger 文档、权限管理等常用企业级开发功能。适合作为中小型管理系统、后台接口服务的脚手架或学习参考。

---

## 功能模块与技术栈

- **ASP.NET Core 6.0**：主框架，负责 WebAPI 路由、依赖注入等基础能力
- **SqlSugar**：高效易用的 .NET ORM，支持 DbFirst/CodeFirst，简化数据库操作
- **JWT 鉴权**：基于 `Microsoft.AspNetCore.Authentication.JwtBearer` 实现用户登录、Token 校验
- **全局异常处理中间件**：统一捕获和记录接口异常，返回友好错误信息
- **统一响应格式**：所有接口返回结构统一（code/message/data），便于前端处理
- **Swagger**：自动生成 API 文档，支持在线调试
- **权限管理**：支持用户、角色、权限、角色权限、用户角色等基础表结构
- **仓储与服务分层**：Repository 封装数据访问，Service 编写业务逻辑，Controller 只处理请求与响应
- **日志**：集成 log4net，支持日志文件输出
- **可选扩展**：FluentValidation 参数验证、AutoMapper 对象映射、Mapster 等

---

## 主要目录结构

- `Controllers/` —— 控制器，API 入口
- `Services/` —— 业务服务层
- `Repository/` —— 仓储层，封装数据库操作
- `Models/` —— 实体类（可通过 SqlSugar DbFirst 自动生成）
- `Common/` —— 通用工具类（如 JwtHelper、ApiResponse 等）
- `Middleware/` —— 中间件（如全局异常处理）
- `Dtos/` —— 数据传输对象
- `Configs/` —— 配置相关类
- `appsettings.json` —— 配置文件（数据库连接、JWT 密钥等）
- `init.sql` —— 数据库初始化脚本

---

## 快速开始

1. **数据库准备**  
   - 使用 `init.sql` 初始化数据库表结构和基础数据

2. **配置数据库连接**  
   - 在 `appsettings.json` 中配置 `ConnectionStrings:Default`

3. **启动项目**  
   - 运行 `dotnet run` 或在 VS Code/Visual Studio 中启动

4. **访问 Swagger 文档**  
   - 默认地址：`http://localhost:端口/swagger`

5. **登录与鉴权**  
   - 通过 `/auth/login` 获取 JWT Token，后续接口需携带 `Authorization: Bearer {token}`

---

## 典型业务流程

1. 用户登录，获取 JWT Token
2. 前端携带 Token 访问受保护接口
3. 后端通过中间件校验 Token 并授权
4. 控制器调用 Service，Service 调用 Repository 访问数据库
5. 所有接口返回统一格式，异常自动捕获并记录日志

---

## 适用场景

- 管理后台、B/S 系统、RESTful API 服务
- 需要权限管理、Token 鉴权、统一异常和日志的中小型项目
- .NET Core WebAPI 学习与实践

---

## 参考步骤

| 步骤  | 模块                  | 内容                                |
| --- | -------------------     | --------------------------------- |
| 1️⃣ | **基础配置**            | 配置连接字符串，注册 SqlSugar 到 DI 容器           |
| 2️⃣ | **实体类生成（DbFirst）** | 使用 SqlSugar DbFirst 自动生成数据库实体类         |
| 3️⃣ | **仓储层（Repository）**  | 封装 SqlSugar 的数据访问操作，便于维护和复用        |
| 4️⃣ | **服务层（Service）**     | 编写业务逻辑，调用仓储层方法                      |
| 5️⃣ | **控制器（Controller）**  | 提供 WebAPI 接口，处理客户端请求                   |
| 6️⃣ | **统一响应格式**          | 统一接口返回结构（如 code/message/data）           |
| 7️⃣ | **中间件：全局异常处理**    | 捕获并处理全局异常，返回友好错误信息                |
| 8️⃣ | **Swagger 集成**      | 集成 Swagger，自动生成和测试 API 文档              |
| 9️⃣ | **可选：验证与对象映射**    | 集成 FluentValidation 进行参数验证，AutoMapper 做对象映射（可选） |

---

如需详细开发文档或遇到问题，欢迎提 issue 或交