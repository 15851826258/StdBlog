# StdBlog

StdBlog Projext Files(项目文档名称为StdBlog_ProjectDocument.docx)

由于数据和iis设置较为复杂  推荐直接访问azure托管的项目网站 http://stdblog.chinacloudsites.cn

请使用基于webkit的浏览器

网站默认admin账户为admin@stdblog.com password:admin

如果想要构建项目，请先修改web.config文件的connectstring结点，并且调用StdblogModels.cs下的每个实现DbContext类的ToList（）方法以创建EF的数据区实例 否则可能出现404 或者500
