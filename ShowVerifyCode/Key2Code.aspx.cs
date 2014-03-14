using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VerifyCodeBase;

namespace ShowVerifyCode
{
    public partial class Key2Code : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Label1.Text = Couch.Get(TextBox1.Text).ToString();
        }
    }
}