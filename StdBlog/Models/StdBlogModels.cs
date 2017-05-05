using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Xml.Linq;


namespace StdBlog.Models
{

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


}