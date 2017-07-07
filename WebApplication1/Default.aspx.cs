using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class _Default : Page
    {
        App_Start.data.user user;
        protected void Page_Load(object sender, EventArgs e)
        {
            user = (HttpContext.Current.Cache["name"] as App_Start.data.user);
            if (user == null)
            {
                Server.Transfer("login.aspx", true);

            }

            foreach( var item in user.dateList)
                myLabel.Text += item.ToString()+ "<br/>";
        }

        protected void Submit(object sender, EventArgs e)
        {
            string date = Request.Form["Name"];
            user.dateList.Remove(date);
        }

        protected void Submit2(object sender, EventArgs e)
        {
            string date = Request.Form["Name"];
            user.dateList.Add(date);
        }

    }
}