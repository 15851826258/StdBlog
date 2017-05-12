using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;


namespace StdBlog.Models
{
    public class m_Blog
    #region
    {
        public int ID { get; set; }
        public int ownerid { get; set; }
        public string title { get; set; }
        public string content { get; set; }
        public int visit_count { get; set; }
        public DateTime last_modify_time { get; set; }
        public string classify { get; set; }

    }
    public class m_BlogContext : DbContext
    {
        public m_BlogContext() : base("name=StdBlogContext") { }

        public DbSet<m_Blog> m_Blogs { get; set; }
    }
    #endregion

    public class m_Admin
    #region Admin
    {
        public int ID { get; set; }
        [Display(Name ="Email")]
        public string loginid { get; set; }
        [Display(Name = "Password")]
        public string password { get; set; }
        [Display(Name = "Name")]
        public string name { set; get; }


    }
    public class m_AdminContext : DbContext
    {
        public m_AdminContext() : base("name=StdBlogContext") { }

        public DbSet<m_Admin> m_Admin { get; set; }
    }
    #endregion

    public class m_User
    #region
    {

        public enum Gender { female, male, secret }
        public int ID { set; get; }
        [Display(Name = "Email")]
        public string loginid { set; get; }
        [Display(Name = "Password")]
        public string password { set; get; }
        [Display(Name = "Name")]
        public string name { set; get; }
        [Display(Name = "Gender")]
        public Gender gender { set; get; }
        public string follows { set; get; }

    }
    public class m_UserContext : DbContext
    {
        public m_UserContext() : base("name=StdBlogContext") { }

        public DbSet<m_User> m_Users { get; set; }
    }
    #endregion

    public class m_UserComment
    #region 
    {
        public int Id { set; get; }
        public string content { get; set; }
        public int senderid { get; set; }
        public int recieverid { set; get; }
        public DateTime time { set; get; }
    }
    public class m_UserCommentContext : DbContext
    {
        public m_UserCommentContext() : base("name=StdBlogContext") { }

        public DbSet<m_UserComment> m_UserComments { get; set; }
    }
    #endregion

    public class m_sensitivekeyword
    #region
    {
        public int ID { get; set; }
        public string key { get; set; }
    }
    public class m_sensitivekeywordContext : DbContext
    {
        public m_sensitivekeywordContext() : base("name=StdBlogContext") { }

        public DbSet<m_sensitivekeyword> m_sensitivekeywords { get; set; }
    }
    #endregion
}