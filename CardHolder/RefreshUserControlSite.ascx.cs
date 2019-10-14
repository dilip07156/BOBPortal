using System;
using CardHolder.Utility;

namespace CardHolder
{
    public partial class RefreshUserControlSite : System.Web.UI.UserControl
    {
        Boolean IsPageRefreshSite;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ViewState["sViewStateId1"] = System.Guid.NewGuid().ToString();
                Session["sSessionId1"] = ViewState["sViewStateId1"].ToString();

                // For Variant URI
                // string strCurrentURL = Request.Url.GetLeftPart(UriPartial.Path);
                ViewState["sVwCurrentURL"] = Request.Url.GetLeftPart(UriPartial.Path);

                // End


                ViewState["sViewStateId"] = System.Guid.NewGuid().ToString();
                //Session["sSessionId"] = ViewState["sViewStateId"].ToString();
                IsPageRefreshSite = false;
                //if (Session["sSessionId"] != null)
                //{
                //    if (ViewState["sViewStateId"].ToString() != Session["sSessionId"].ToString())
                //    {
                //        IsPageRefreshSite = true;

                //    }
                //}

                if (Session["sSessionId"] != null && (ViewState["sVwCurrentURL"].ToString() == Session["sSCurrentURL"].ToString()))
                {
                    if (ViewState["sViewStateId"].ToString() != Session["sSessionId"].ToString())
                    {
                        IsPageRefreshSite = true;

                    }
                }
                tesite_Click(tesite, null);

            }

            else
            {
                IsPageRefreshSite = false;
                if (ViewState["sViewStateId1"] != null && Session["sSessionId1"] != null)
                {
                    if (ViewState["sViewStateId1"].ToString() != Session["sSessionId1"].ToString())
                    {
                        IsPageRefreshSite = true;

                    }
                }
                Session["sSessionId1"] = System.Guid.NewGuid().ToString();

                ViewState["sViewStateId1"] = Session["sSessionId1"].ToString();

            }

            string cRequestPath = Request.Url.AbsoluteUri.ToString().ToLower();


            if (IsPageRefreshSite)
            {
                if (cRequestPath.Contains("/card/paymentprocess.aspx"))
                {
                    //do nothing
                }
                else
                {
                    Session["sSessionId1"] = null;
                    Session["sSessionId"] = null;
                    Session["sSCurrentURL"] = null;
                    Response.Redirect(Constants.pErrorPage, false);
                }
            }

        }
        protected void tesite_Click(object sender, EventArgs e)
        {
            Session["sSessionId"] = System.Guid.NewGuid().ToString();
            Session["sSCurrentURL"] = Request.Url.GetLeftPart(UriPartial.Path);
            //ViewState["ViewStateId"] = Session["sSessionId"].ToString();
        }
    }
}