using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;

namespace CardHolder
{
    internal class DataContractJsonSerializer
    {
        private Type type;

        public DataContractJsonSerializer(Type type)
        {
            this.type = type;
        }

        [DataContract]
        public class RecaptchaApiResponse
        {
            [DataMember(Name = "success")]
            public bool Success;

            [DataMember(Name = "error-codes")]
            public List<string> ErrorCodes;
        }// </string>

        //internal RecaptchaApiResponse ReadObject(MemoryStream ms)
        //{
        //    throw new NotImplementedException();
        //}
    }
}