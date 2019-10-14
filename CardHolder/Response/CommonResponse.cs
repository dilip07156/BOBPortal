using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CardHolder.Request.Response
{
    public class ATMPINResponse
    {
        public string TxnType { get; set; }

        public string RespCode { get; set; }

        public string RespDesc { get; set; }
    }
}