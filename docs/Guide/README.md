# 指南

## 项目简介

一个轻量的 Web 应用框架, 具有优雅、高效、简洁、富于表达力等优点。采用 前后端分离 设计，是崇尚开发效率的全栈框架

- **简洁友好** - 统一的设计规范，精心打磨的操作界面回应你的期待。
- **易扩展** - 一套完整的插件机制，以 约定优于配置 为中心的项目结构，无论是对开发者还是使用者都如此友好。

前端 基于 `vue-element-admin`，后端 基于 `.NET Core3.1` ， RESTful + Semantic WebAPI 设计，采用 `IdentityServer4`（UHub） 完成认证授权。

Remember.Core 的前生 是 在线学习系统 [remember](https://github.com/yiyungent/remember)，Remember.Core 继承 remember 遗志，旨在让每个人都能轻松建站。

## 框架技术栈


## 项目分层

![](/docs/.vuepress/public/images/project-structure.png)

## 功能一览

- **上传本地插件** - 热插拔：无论是加载，卸载都无需重启你的站点
- **放置钩子** - 让插件行为加入框架
- **动态扩展 WebAPI** - 每个插件都是一个 WebAPI
- **一套完整的 插件生命周期** - 在需要时做你想做
- **全程 `依赖注入`** - 你可在插件生命周期获取你注入的任何服务
- **以 `约定优于配置`** 为中心的项目结构 - 只需关注你的业务
- **一插件一 LoadContext** - 插件间彼此隔离
- **Framework 域共享机制** - 免去重复加载
- **简单易用** - `PluginFinder`、`PluginManager` 或许你仅仅需要它们

![](/docs/.vuepress/public/images/screenshot/2020-10-29-18-33-40.png)
![](/docs/.vuepress/public/images/screenshot/2020-10-29-18-40-28.png)
![](/docs/.vuepress/public/images/screenshot/2020-10-29-18-41-59.png)
![](/docs/.vuepress/public/images/screenshot/2020-10-29-18-42-27.png)
![](/docs/.vuepress/public/images/screenshot/2020-10-29-18-44-05.png)
