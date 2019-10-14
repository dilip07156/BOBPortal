using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CardHolder
{
    public class InternationlLimitRequest : CommonRequest
    {
        public Double Amount { get; set; }
        public string Flag { get; set; }
    }
}