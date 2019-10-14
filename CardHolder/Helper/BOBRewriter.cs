using System;
using System.Collections.Generic;
using System.Web;
using System.Web.SessionState;
using CardHolder.Utility;

/// <summary>
/// 
/// </summary>
/// <remarks></remarks>
public class BOBRewriter : IHttpModule
{
    /// <summary>
    /// Disposes of the resources (other than memory) used by the module that implements <see cref="T:System.Web.IHttpModule"/>.
    /// </summary>
    /// <remarks></remarks>
    public void Dispose()
    {

    }

    /// <summary>
    /// Initializes a module and prepares it to handle requests.
    /// </summary>
    /// <param name="context">An <see cref="T:System.Web.HttpApplication"/> that provides access to the methods, properties, and events common to all application objects within an ASP.NET application</param>
    /// <remarks></remarks>
    public void Init(HttpApplication context)
    {
        context.PostAcquireRequestState += new EventHandler(App_PostAcquireRequestState);
        context.EndRequest += new EventHandler(context_EndRequest);

    }

    /// <summary>
    /// Handles the BeginRequest event of the context control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    /// <remarks></remarks>

    void context_EndRequest(object sender, EventArgs e)
    {
        string[] strExtenstionToNotCache = new string[] { ".aspx", ".asmx", ".ahsx", ".axd" };

        HttpContext objContext = ((System.Web.HttpApplication)(sender)).Context;

        if (strExtenstionToNotCache.ToString().Contains(objContext.Request.CurrentExecutionFilePathExtension))
        {

            objContext.Response.Cache.SetExpires(DateTime.UtcNow.AddDays(-1));

            objContext.Response.Cache.SetValidUntilExpires(false);

            objContext.Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);

            objContext.Response.Cache.SetCacheability(HttpCacheability.NoCache);

            objContext.Response.Cache.SetNoStore();

        }

    }

    /// <summary>
    /// Handles the PostAcquireRequestState event of the App control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    /// <remarks></remarks>
    void App_PostAcquireRequestState(object sender, EventArgs e)
    {
        // Create HttpApplication and HttpContext objects to access
        // request and response properties.
        HttpApplication application = (HttpApplication)sender;
        HttpResponse cResponse = application.Context.Response;
        HttpRequest cRequest = application.Context.Request;
        HttpSessionState cSession = application.Context.Session;
        string cRequestPath = cRequest.Url.ToString().ToLower();

        if (cSession == null) return;



        //if (!cRequestPath.Contains("/login.aspx"))

        if (!cRequestPath.Contains("/login.aspx") && !cRequestPath.Contains("/errorpage/codeerror.aspx")
            && !cRequestPath.Contains("/errorpage/weberror.aspx") && !cRequestPath.Contains("/forgotpassword.aspx")
            && !cRequestPath.Contains("/forgotusername.aspx") && !cRequestPath.Contains("/registration.aspx")
            && !cRequestPath.Contains("/application.aspx") && !cRequestPath.Contains("/applicationsuccess.aspx")
            && !cRequestPath.Contains("/applicationpreview.aspx") && !cRequestPath.Contains("/applicationformdownload.aspx")
            && !cRequestPath.Contains("/captchaimage.axd") && !cRequestPath.Contains("/loginnext.aspx") && !cRequestPath.Contains("/captchaa.aspx"))
        {

            //List<LoggedInUser> lstLoggedUsers = new List<LoggedInUser>();
            List<LoggedInUser> lstLoggedUsers = null;
            if (application.Context.Application["lstLoggedUsers"] != null)
            {
                lstLoggedUsers = (List<LoggedInUser>)application.Context.Application["lstLoggedUsers"];
            }
            else
            {
                lstLoggedUsers = new List<LoggedInUser>();
            }
            if (lstLoggedUsers != null && lstLoggedUsers.Count > 0)
            {
                string currentSessionID = application.Context.Session.SessionID;
                bool isCurrentSessionIDExistsInList = false;
                foreach (LoggedInUser l in lstLoggedUsers)
                {
                    if (l.SessionId == currentSessionID)
                    {
                        isCurrentSessionIDExistsInList = true;
                        break;
                    }
                }
                if (isCurrentSessionIDExistsInList == false)
                {
                    application.Context.Session.Abandon();
                    //   cResponse.Redirect("~/Login.aspx", true);
                    cResponse.Redirect(Constants.WebError, true);
                }

            }
        }

    }







}
