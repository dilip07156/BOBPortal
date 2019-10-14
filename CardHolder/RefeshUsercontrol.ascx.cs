using System;
using CardHolder.Utility;

namespace CardHolder
{
    public partial class RefeshUsercontrol : System.Web.UI.UserControl
    {
        Boolean IsPageRefresh;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ViewState["ViewStateId1"] = System.Guid.NewGuid().ToString();
                Session["SessionId1"] = ViewState["ViewStateId1"].ToString();

                // For Variant URI
                // string strCurrentURL = Request.Url.GetLeftPart(UriPartial.Path);
                ViewState["VwCurrentURL"] = Request.Url.GetLeftPart(UriPartial.Path);

                // End

                ViewState["ViewStateId"] = System.Guid.NewGuid().ToString();
                //Session["SessionId"] = ViewState["ViewStateId"].ToString();
                IsPageRefresh = false;


                if (Session["SessionId"] != null && (ViewState["VwCurrentURL"].ToString() == Session["SCurrentURL"].ToString()))
                {
                    if (ViewState["ViewStateId"].ToString() != Session["SessionId"].ToString())
                    {
                        IsPageRefresh = true;

                    }
                }
                //if (Session["SessionId"] != null)
                //{
                //    if (ViewState["ViewStateId"].ToString() != Session["SessionId"].ToString())
                //    {
                //        IsPageRefresh = true;

                //    }
                //}
                te_Click(te, null);

            }

            else
            {
                IsPageRefresh = false;
                if (ViewState["ViewStateId1"] != null && Session["SessionId1"] != null)
                {
                    if (ViewState["ViewStateId1"].ToString() != Session["SessionId1"].ToString())
                    {
                        IsPageRefresh = true;

                    }
                }
                Session["SessionId1"] = System.Guid.NewGuid().ToString();

                ViewState["ViewStateId1"] = Session["SessionId1"].ToString();

            }

            if (IsPageRefresh)
            {
                Session["SessionId1"] = null;
                Session["SessionId"] = null;
                Session["SCurrentURL"] = null;
                Response.Redirect(Constants.pErrorPage, false);
            }

        }
        protected void te_Click(object sender, EventArgs e)
        {
            Session["SessionId"] = System.Guid.NewGuid().ToString();
            Session["SCurrentURL"] = Request.Url.GetLeftPart(UriPartial.Path);
            //ViewState["ViewStateId"] = Session["SessionId"].ToString();
        }
    }
}