using System;

namespace CardHolder.DTO
{
    public class Email_LoggerDTO
    {
       // public System.Int64 Id { get; set; }
        public string FromEmail { get; set; }
        public string ToEmail { get; set; }
        public string Body { get; set; }
        public string Subject { get; set; }
        public System.Int64 SendBy { get; set; }
        public DateTime SendDate { get; set; }
        public string EmailStatus { get; set; }
    }
    public class SMS_LoggerDTO
    {
      //  public System.Int64 Id { get; set; }
        public string Number { get; set; }
        public string Category { get; set; }
        public long SendBy { get; set; }
        public DateTime SendDate { get; set; }
        public string SMSStatus { get; set; }
        
    }
}
