# Stb
和顺师徒帮平台、网站

由于拆分成三个项目，调试起来不太方便，最终还是把官网、平台和API放在一个项目里，通过Area进行划分。

代码结构
<pre>-- src
     -- Stb
        -- Areas
           -- Api 师徒帮APP API
           -- Official 师徒帮官网
           -- Platform 师徒帮平台
        -- Data 数据库上下文和模型定义
</pre>