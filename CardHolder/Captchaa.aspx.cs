using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Drawing.Imaging;

namespace CardHolder
{
    public partial class Captchaa : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            Bitmap bmp = new Bitmap(170, 54);
            Graphics g = Graphics.FromImage(bmp);

            //g.Clear(Color.FromArgb(243, 244, 244));
            g.Clear(Color.White);

            string randomString = GenerateCapth.GetCaptchString(6);
            Session.Add("strRandomCH", randomString);

            g.DrawString(randomString, new Font("#45698d", 19, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), 30, 0);
            Response.ContentType = "image/jpeg";

            bmp.Save(Response.OutputStream, System.Drawing.Imaging.ImageFormat.Jpeg);
            bmp.Dispose();
        }
    }
    public static class GenerateCapth
    {
        public static string[] strArray = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };

        public static string GetCaptchString(int Length)
        {
            string strRandom = "";
            Random autoRand = new Random();
            int x;
            for (x = 0; x < Length; x++)
            {
                int i = Convert.ToInt32(autoRand.Next(0, 25));
                strRandom += strArray[i].ToString();
            }
            return strRandom;
        }
    }
}