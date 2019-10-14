using System;
using CardHolder.Utility;
namespace CardHolder
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks></remarks>
    public partial class Simaple : System.Web.UI.MasterPage
    {
        //Boolean IsPageRefresh;
        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        /// <remarks></remarks>
        protected void Page_Load(object sender, EventArgs e)
        {

            RegisterJavaScriptAndCss();
            RegisterJavaScriptlibraries();
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            string sb;
            sb = "<script language=javascript>\n";
            sb += "window.history.forward(1);\n";
            sb += "\n</script>";

            Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "clientScript", sb);
        }

        /// <summary>
        /// Registers the java script and CSS.
        /// </summary>
        /// <remarks></remarks>
        private void RegisterJavaScriptAndCss()
        {
            int index = 0;
            //this.Page.RegisterStyleSheet("style.css", index++);
            //this.Page.RegisterJavaScript("jquery-1.7.1.min.js");
            this.Page.RegisterJavaScript("jquery-1.8.3.js");
            this.Page.RegisterJavaScript("css_browser_selector.js");
            this.Page.RegisterJavaScript("jquery-ui-1.8.18.custom.min.js");
            Page.RegisterJavaScript("bind.js");
            this.Page.RegisterJavaScript("General.js");
            this.Page.RegisterJavaScript("VirtualKeyboard/vkbobcard.js");

        }
			private void RegisterJavaScriptlibraries()
        {
            int index = 0;
            //Page.RegisterNewCSS("bootstrap.min.css", index++);
            Page.RegisterNewCSS("bob.min.css", index++);
            Page.RegisterNewCSS("jquery.mCustomScrollbar.min.css");
            Page.RegisterNewCSS("slick.css");
            Page.RegisterNewCSS("slick-theme.css");
            Page.RegisterNewCSS("bootstrap-datepicker.min.css");

            Page.RegisterNewJavaScript("jquery.min.js");
            Page.RegisterNewJavaScript("popper.min.js");
            Page.RegisterNewJavaScript("bootstrap.min.js");
            Page.RegisterNewJavaScript("slick.min.js");
            Page.RegisterNewJavaScript("jquery.mCustomScrollbar.concat.min.js");
            Page.RegisterNewJavaScript("jquery.nice-select.min.js");
            Page.RegisterNewJavaScript("bootstrap-datepicker.min.js");
            Page.RegisterNewJavaScript("modernizr.min.js");
        }
    }
}