using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Submit(object sender, EventArgs e)
        {
            string name = Request.Form["Name"];
            var  user = Global._data.userList.FirstOrDefault(item => item.name == name);
            if (user == null)
                return;
            HttpContext.Current.Cache["name"] = user;
            Server.Transfer("Default.aspx", true);
        }
    }

}