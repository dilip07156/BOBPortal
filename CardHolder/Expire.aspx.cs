using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Web.Configuration;

namespace CardHolder
{
    public partial class Expire : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request["Reason"] == "Timeout")
            {
                lblError.Text = "Your Server session has timed out.";
            }
        }
    }
}