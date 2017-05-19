using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Collections.Generic;


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

    public class m_Blog_extinfo
    #region
    {
        public int ID { get; set; }
        public int blogid { get; set; }
        public int followid { get; set; }
        public bool vertify { get; set; }
    }
    public class m_Blog_extinfoContext : DbContext
    {
        public m_Blog_extinfoContext() : base("name=StdBlogContext") { }

        public DbSet<m_Blog_extinfo> m_Blog_extinfos { get; set; }
    }
    #endregion

    public class m_BlogCommment
    #region 
    {
        public int ID { get; set; }
        public DateTime time { get; set; }
        public int senderid { get; set; }
        public int blogid { get; set; }
        public string content { get; set; }
    }
    public class m_BlogCommmentContext : DbContext
    {
        public m_BlogCommmentContext() : base("name=StdBlogContext") { }

        public DbSet<m_BlogCommment> m_BlogCommments { get; set; }
    }
    #endregion

    public class m_BlogCommment_name
    {
        public m_BlogCommment bc { get; set; }
        public string name { set; get; }
        public m_BlogCommment_name(m_BlogCommment bc, string name)
        {
            this.bc = bc;
            this.name = name;
        }
    }



    public class m_Admin
    #region Admin
    {
        public int ID { get; set; }
        [Display(Name = "Email")]
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

    public class m_UserMes
    #region 
    {
        public int Id { set; get; }
        public string content { get; set; }
        public int senderid { get; set; }
        public int recieverid { set; get; }
        public DateTime time { set; get; }
    }
    public class m_UserMesContext : DbContext
    {
        public m_UserMesContext() : base("name=StdBlogContext") { }

        public DbSet<m_UserMes> m_UserMess { get; set; }
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

    public class m_recblog
    #region 
    {
        public int ID { get; set; }
        public DateTime pushtime { get; set; }
        public DateTime deletetime { get; set; }
        public int blogid { get; set; }
    }
    public class m_recblogContext : DbContext
    {
        public m_recblogContext() : base("name=StdBlogContext") { }

        public DbSet<m_recblog> m_recblogs { get; set; }
    }
    #endregion

    public class m_recblog_blog
    {
        public IEnumerable<m_Blog> blogs { get; set; }
        public IEnumerable<m_Blog> recs { get; set; }
    }
}