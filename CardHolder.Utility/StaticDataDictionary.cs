using System.Collections.Generic;
using System.Xml;
namespace CardHolder.Utility
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks></remarks>
    public class StaticDataDictionary : XmlDocument
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StaticDataDictionary"/> class.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <remarks></remarks>
        public StaticDataDictionary(string path)
        {
            this.Load(path);
        }

        /// <summary>
        /// Loads the list control.
        /// </summary>
        /// <param name="ddl">The DDL.</param>
        /// <param name="type">The type.</param>
        /// <param name="lang">The lang.</param>
        /// <remarks></remarks>
        public void LoadListControl(System.Web.UI.WebControls.ListControl ddl, string type, string lang = "en")
        {
            XmlNodeList filter = this.SelectNodes("//" + type + "/Detail[@lang='" + lang + "']");
            Dictionary<string, string> myDictionary = new Dictionary<string, string>();
            foreach (XmlNode item in filter)
            {
                myDictionary.Add(item.Attributes["text"].InnerText, item.Attributes["value"].InnerText);
            }
            ddl.DataSource = myDictionary;
            ddl.DataTextField = "key";
            ddl.DataValueField = "value";
            ddl.DataBind();
        }
        /// <summary>
        /// Loads the text control.
        /// </summary>
        /// <param name="txt">The TXT.</param>
        /// <param name="type">The type.</param>
        /// <param name="lang">The lang.</param>
        /// <remarks></remarks>
        public void LoadTextControl(System.Web.UI.WebControls.TextBox txt, string type, string lang = "en")
        {
            XmlNodeList filter = this.SelectNodes("//" + type + "/Detail[@lang='" + lang + "']");
            foreach (XmlNode item in filter)
            {
                txt.Text = item.Attributes["text"].InnerText;
            }
        }
        
    }
}
