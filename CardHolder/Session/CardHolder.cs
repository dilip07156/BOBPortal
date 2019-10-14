using System.Web.SessionState;
using CardHolder.DTO;
namespace CardHolder.Session
{
    public static class CardHolder
    {
        //public static string GetUserId(this HttpSessionState session)
        //{
        //    var val = session["User"] as CardHolder_MstDTO;
        //    return val == null ? "" : val.CardHolder_Id.ToString();
        //}
        //public static string GetUserName(this HttpSessionState session)
        //{
        //    var val = session["User"] as CardHolder_MstDTO;
        //    return val == null ? "" : val.User_nm;
        //}
        public static string GetUserPassword(this HttpSessionState session)
        {
            var val = session["User"] as CardHolder_MstDTO;
            return val == null ? "" : val.User_pwd;
        }
        //public static string GetUserEmail(this HttpSessionState session)
        //{
        //    var val = session["User"] as CardHolder_MstDTO;
        //    return val == null ? "" : val.CH_Card.EMAIL_ID; //Fetching from Oracle
        //}

        public static string GetMobileNum(this HttpSessionState session)
        {
            var val = session["User"] as CardHolder_MstDTO;
            return val == null ? "" : val.CH_Card.PHONE_MOBILE;  //Fetching from Oracle
        }

        public static CardHolder_MstDTO GetUserDto(this HttpSessionState session)
        {
            return session["User"] as CardHolder_MstDTO;
        }
        public static void SaveUserDto(this HttpSessionState session, CardHolder_MstDTO user)
        {
            session["User"] = user;
        }
        //public static void RemoveUserDto(this HttpSessionState session)
        //{
        //    session["User"] = null;
        //}
    }
}