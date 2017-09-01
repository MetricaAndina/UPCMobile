using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UPCMobile.AD;

namespace WebApplication2
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ADHelper.LoginResult res = ADHelper.Login("dpisco", "Pass@word1");
            Response.Write(res.ToString());
        }
    }
}