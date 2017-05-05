using System;
using System.Collections.Generic;
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
        public string loginid { get; set; }
        public string password { get; set; }
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
        public string loginid { set; get; }
        public string password { set; get; }
        public string name { set; get; }
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