using System;
using CardHolder.Utility;

namespace CardHolder
{
    public partial class EncryptDecrcypt4All : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void btnDecrypt_Click(object sender, EventArgs e)
        {
            if (txtencrypt.Text != "")
            {
                txtdecrypt.Text = txtencrypt.Text.Trim().Decrypt();
            }
        }

        protected void btnEncryp_Click(object sender, EventArgs e)
        {
            if (txtencrypt.Text != "")
            {
                txtdecrypt.Text = txtencrypt.Text.Trim().Encrypt();
            }
        }
    }
}