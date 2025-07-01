IF OBJECT_ID('UserToken', 'U') IS NOT NULL DROP TABLE UserToken;
IF OBJECT_ID('Users', 'U') IS NOT NULL DROP TABLE Users;
IF OBJECT_ID('Roles', 'U') IS NOT NULL DROP TABLE Roles;
IF OBJECT_ID('Permissions', 'U') IS NOT NULL DROP TABLE Permissions;
IF OBJECT_ID('UserRoles', 'U') IS NOT NULL DROP TABLE UserRoles;
IF OBJECT_ID('RolePermissions', 'U') IS NOT NULL DROP TABLE RolePermissions;

CREATE TABLE UserToken
(
    Id INT IDENTITY PRIMARY KEY,
    UserId INT NOT NULL,
    RefreshToken NVARCHAR(200) NOT NULL,
    ExpireAt DATETIME NOT NULL
)


-- 数据库初始化脚本

-- 用户表
CREATE TABLE Users (
    Id INT IDENTITY PRIMARY KEY,
    Username NVARCHAR(50),
    PasswordHash NVARCHAR(200)
);
INSERT INTO [dbo].[Users] ([Id] ,[Username] ,[PasswordHash])
VALUES (1 ,N'admin' ,N'123')
-- 用户角色关系
CREATE TABLE UserRoles (
    Id INT IDENTITY PRIMARY KEY,
    UserId INT,
    RoleId INT
);
INSERT INTO [dbo].[UserRoles] ([UserId] ,[RoleId])
VALUES (1 ,1)

INSERT INTO [dbo].[UserRoles] ([UserId] ,[RoleId])
VALUES (1 ,2)
-- 角色表
CREATE TABLE Roles (
    Id INT IDENTITY PRIMARY KEY,
    Name NVARCHAR(50)
);
INSERT INTO [dbo].[Roles] ([Id] ,[Name]) 
VALUES (1 ,N'管理员')

INSERT INTO [dbo].[Roles] ([Id] ,[Name]) 
VALUES (1 ,N'普通用户')

-- 角色权限关系 id设置为自增
CREATE TABLE RolePermissions (
    Id INT IDENTITY PRIMARY KEY,
    RoleId INT,
    PermissionId INT
);


INSERT INTO [dbo].[RolePermissions] ([RoleId] ,[PermissionId])
VALUES (1 ,2)
INSERT INTO [dbo].[RolePermissions] ([RoleId] ,[PermissionId])
VALUES (2 ,3)
USE [NXYiBao]

-- 权限表（接口或菜单）
CREATE TABLE Permissions (
    Id INT IDENTITY PRIMARY KEY,
    Name NVARCHAR(50),
    Url NVARCHAR(200),  -- 接口地址
    Method NVARCHAR(10) -- GET, POST...
);


INSERT INTO Permissions (Name, Url, Method)
VALUES ('用户登录', '/api/Auth/login', 'POST');

INSERT INTO Permissions (Name, Url, Method)
VALUES ('Test', '/api/Test/GetAll', 'GET');

INSERT INTO Permissions (Name, Url, Method)
VALUES ('Test', '/api/Test/Create', 'POST');



