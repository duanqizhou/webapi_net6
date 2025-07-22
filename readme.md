# 项目介绍

本项目是一个基于 ASP.NET Core 6.0 的 WebAPI 后端服务，集成了 SqlSugar 作为 ORM 框架，支持 JWT 鉴权、全局异常处理、统一响应格式、Swagger 文档、权限管理等常用企业级开发功能。适合作为中小型管理系统、后台接口服务的脚手架或学习参考。

---


## ✨ 功能模块与技术栈

- **ASP.NET Core 6.0**：WebAPI 框架，支持中间件、依赖注入、路由等
- **SqlSugar**：高性能 .NET ORM，支持 CodeFirst / DbFirst 模式
- **JWT 鉴权**：基于 `Microsoft.AspNetCore.Authentication.JwtBearer` 用户登录颁发 Token，保护后续 API 接口
- **RBAC 权限管理**：支持角色、权限、用户关系绑定，接口访问控制
- **统一响应结构**：接口统一格式返回，前后端交互清晰
- **全局异常处理中间件**：统一拦截并格式化异常输出
- **Swagger 接口文档**：自动生成 RESTful API 文档
- **log4net 日志**：记录调试信息、异常信息，便于排查问题
- **仓储 + 服务分层架构**：清晰的 Controller → Service → Repository 层级结构
- **权限自动扫描工具**：支持扫描 Controller 方法自动写入权限表
- **可选扩展**：FluentValidation 参数验证、AutoMapper 对象映射、Mapster 等




## 权限控制（RBAC）

本项目实现了基于角色的访问控制（RBAC，Role-Based Access Control）权限体系，主要包括以下内容：

- **用户（Users）**：系统登录用户，每个用户可以分配多个角色。
- **角色（Roles）**：定义一组权限的集合，如“管理员”“普通用户”等。
- **权限（Permissions）**：系统中的具体操作或资源访问点，如“查看用户”“编辑数据”等。
- **用户角色关系（UserRoles）**：用于关联用户和角色，实现一对多或多对多关系。
- **角色权限关系（RolePermissions）**：用于关联角色和权限，实现灵活的权限分配。

### 权限控制流程

1. 用户登录后，系统根据其分配的角色加载对应的权限。
2. 每次访问受保护的 API 时，后端会校验当前用户是否具备访问该资源的权限。
3. 权限校验可通过自定义中间件或 Action Filter 实现，确保接口安全。

### 表结构说明

- `Users`：用户表
- `Roles`：角色表
- `Permissions`：权限表
- `UserRoles`：用户与角色关联表
- `RolePermissions`：角色与权限关联表
## 📚 数据库表结构说明

### 1. `Users`（用户表）

| 字段名        | 类型       | 说明         |
|---------------|------------|--------------|
| Id            | int        | 主键         |
| UserName      | nvarchar   | 登录用户名   |
| PasswordHash  | nvarchar   | 密码（哈希） |
| IsActive      | bit        | 是否启用     |

---

### 2. `Roles`（角色表）

| 字段名   | 类型       | 说明       |
|----------|------------|------------|
| Id       | int        | 主键       |
| Name     | nvarchar   | 角色名     |
| Remark   | nvarchar   | 备注说明   |

---

### 3. `Permissions`（权限表）

用于记录系统中所有接口的 URL 与操作权限。

| 字段名   | 类型       | 说明                   |
|----------|------------|------------------------|
| Id       | int        | 主键                   |
| Name     | nvarchar   | 权限名称（接口功能）   |
| Url      | nvarchar   | API 接口地址           |
| Method   | nvarchar   | 请求类型：GET/POST 等  |

> ✅ 支持从控制器代码中自动扫描 `[HttpGet]`、`[HttpPost]` 等特性并入库。

---

### 4. `UserRoles`（用户-角色关联表）

| 字段名   | 类型     | 说明           |
|----------|----------|----------------|
| Id       | int      | 主键           |
| UserId   | int      | 对应用户       |
| RoleId   | int      | 对应角色       |

---

### 5. `RolePermissions`（角色-权限关联表）

| 字段名     | 类型     | 说明           |
|------------|----------|----------------|
| Id         | int      | 主键           |
| RoleId     | int      | 对应角色       |
| PermissionId | int    | 对应权限       |

---

通过 RBAC 设计，系统可以灵活地为不同用户分配不同角色和权限，满足企业级后台管理系统的权限需求。
## 🔐 权限控制（RBAC）

### ✅ 流程说明：

1. 用户登录，系统根据其角色加载所有权限。
2. 每次访问受保护接口时，校验用户是否具备对应权限。
3. 权限校验由 `PermissionFilter` 全局过滤器实现，支持路由 + 方法名验证。
4. 支持通过代码扫描控制器 API 自动同步 `Permissions` 表。

---

### ✅ 权限扫描工具

在开发阶段，你可以运行以下代码：

```csharp
PermissionScanner.GeneratePermissions(sqlSugarClient);
---

## 主要目录结构


| 目录                 | 说明                          |
| ------------------ | --------------------------- |
| `Controllers/`     | 控制器，处理请求并返回响应               |
| `Services/`        | 服务层，编写业务逻辑                  |
| `Repository/`      | 仓储层，封装 SqlSugar 数据访问        |
| `Models/`          | 实体类，对应数据库表结构                |
| `Common/`          | 工具类，如 JwtHelper、ApiResponse |
| `Middleware/`      | 中间件，如全局异常处理                 |
| `Filters/`         | 权限过滤器                       |
| `Configs/`         | 配置类（如 JWT 设置等）              |
| `Dtos/`            | 数据传输对象 DTO                  |
| `appsettings.json` | 配置文件（数据库连接等）                |
| `init.sql`         | 数据库初始化脚本                    |

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
| 9️⃣ | **验证与对象映射**    | 集成 FluentValidation 进行参数验证，Mapster 做对象映射 |

---


如需详细开发文档或遇到问题，欢迎提 issue 或交