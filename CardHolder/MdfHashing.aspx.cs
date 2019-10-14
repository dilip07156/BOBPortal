using System;
using System.Web.UI;
using CardHolder.BAL;
using CardHolder.DTO;
using System.Text;
using System.Security.Cryptography;

namespace CardHolder
{
    public partial class MdfHashing : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btngetlist_Click(object sender, EventArgs e)
        {
            var lst = new CardHolderManager().GetListCardHolders();
            gvCardholderListing.DataSource = lst;
            gvCardholderListing.DataBind();
        }

        protected void btnConvert_Click(object sender, EventArgs e)
        {
            // string pwd = EncryptValue_MD5Hash("Bob*3a6b11");
            var lst = new CardHolderManager().GetListCardHolders();
            foreach (var userList in lst)
            {

                var objUser = new CardHolder_MstDTO();


                objUser.MdfHashingPwd = EncryptValue_MD5Hash(userList.User_pwd);
                objUser.CardHolder_Id = userList.CardHolder_Id;
                var obj = new CardHolderManager();
                try
                {
                    obj.UpdateCardHolderPassword(objUser);

                }
                catch (Exception ex)
                {
                    throw ex;
                }


            }
        }

        protected string EncryptValue_MD5Hash(string Password)
        {
            var strBuilder = new StringBuilder();

            try
            {

                MD5 md5 = new MD5CryptoServiceProvider();

                //compute hash from the bytes of text
                md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(Password));

                //get hash result after compute it
                byte[] result = md5.Hash;

                for (int i = 0; i < result.Length; i++)
                {
                    //change it into 2 hexadecimal digits
                    //for each byte
                    strBuilder.Append(result[i].ToString("x2"));
                }
            }
            catch (Exception)
            {
                strBuilder.Clear();
            }
            return strBuilder.ToString();
        }


    }
}