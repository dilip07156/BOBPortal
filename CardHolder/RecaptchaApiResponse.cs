using System.Collections.Generic;
using System.Runtime.Serialization;

namespace CardHolder
{
    //internal class RecaptchaApiResponse
    //{
    //}
    [DataContract]
    public class RecaptchaApiResponse
    {
        [DataMember(Name = "success")]
        public bool Success;

        [DataMember(Name = "error-codes")]
        public List<string> ErrorCodes;
    } //</string>
}