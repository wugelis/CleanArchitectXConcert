# 軟體架構設計：有『題』- API 設計原則以『線上售票系統為例』

## 第一版範例程式

這是將透過 LINQPad 只花費 20 分鐘直接撰寫而成的初稿，貼到 Visual Studio 改以方案檔方式呈現。

此範例一樣主要展示透過事件風暴 (Event Storming) 在透過 DDD 的戰略建模的過程裡，以及 Problem Space 來看我們的這個（產品/專案）的商業目標，我可以將這些商業目標拆分如下的 Subdomain

* 購票：Core Domain 
* 登入購票網站：Generic Subdomain 
* 簡訊通知：Generic Subdomain 
* 管理票卷：Supporting Domain

最後以最短的時間，撰寫出來的 Clean Architecture 初版程式碼。