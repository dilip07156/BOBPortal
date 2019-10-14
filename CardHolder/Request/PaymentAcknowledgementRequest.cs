using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CardHolder
{
    public class PaymentAcknowledgementRequest : CommonRequest
    {
        public double Amount { get; set; }
        public string BankRefNumber { get; set; }
        public  string Narration { get; set; }
    }
}