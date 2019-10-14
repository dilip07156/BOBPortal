using System;
using System.Collections.Generic;
using System.Web;
using System.Security.Cryptography;

/// <summary>
/// base page class which to be inharited on all pages.
/// </summary>
/// <remarks></remarks>
public class PageBase : System.Web.UI.Page
{
    /// <summary>
    /// 
    /// </summary>
    private bool _isXsrf;

    /// <summary>
    /// 
    /// </summary>
    private const string _XsrfName = "_XSID";
    /// <summary>
    /// Gets a value indicating whether this instance is XSRF.
    /// </summary>
    /// <remarks></remarks>
    public bool IsXsrf
    {
        get { return _isXsrf; }
    }
    /// <summary>
    /// Raises the <see cref="E:System.Web.UI.Page.PreLoad"/> event after postback data is loaded into the page server controls but before the <see cref="M:System.Web.UI.Control.OnLoad(System.EventArgs)"/> event.
    /// </summary>
    /// <param name="e">An <see cref="T:System.EventArgs"/> that contains the event data.</param>
    /// <remarks></remarks>
    protected override void OnPreLoad(EventArgs e)
    {
        base.OnPreLoad(e);
        string sessionXsrfId = Session[_XsrfName] as string;
        if (IsPostBack)
        {
            string vwId = ViewState[_XsrfName] as string;
            _isXsrf = true;
            if (!string.IsNullOrEmpty(vwId) && vwId.Equals(sessionXsrfId))
            {
                _isXsrf = false;
            }
        }
        else
        {
            if (string.IsNullOrEmpty(sessionXsrfId))
            {
                sessionXsrfId = GenerateCode();
                Session.Add(_XsrfName, sessionXsrfId);
                ViewState.Add(_XsrfName, sessionXsrfId);
            }
        }
        //Functions.GetLoginLanguageID(this);
    }
    /// <summary>
    /// Generates the code.
    /// </summary>
    /// <returns></returns>
    /// <remarks></remarks>
    private static string GenerateCode()
    {
        RNGCryptoServiceProvider random = new RNGCryptoServiceProvider();
        byte[] randBytes = new byte[32];
        random.GetNonZeroBytes(randBytes);
        return Convert.ToBase64String(randBytes);
    }
}
