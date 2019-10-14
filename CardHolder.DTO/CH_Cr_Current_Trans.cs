using System;

namespace CardHolder.DTO
{
    public class CH_Cr_Current_Trans
    {
        public DateTime Transaction_Date { get; set; }
        public string Description { get; set; }
        public double Amount { get; set; }
        public string DETAILS { get; set; }
        public string MonthYear { get; set; }
    }
}
