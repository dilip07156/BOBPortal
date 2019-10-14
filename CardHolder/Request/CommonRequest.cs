using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace CardHolder
{
    public class CommonRequest
    {
        public string  TxnType { get; set; }
        public string CardNumber { get; set; }
        public string TransRefNo { get; set; }
        public string TransDateTime { get; set; }
        public string Flag { get; set; }
    }
}