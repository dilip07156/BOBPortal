
using System;
namespace CardHolder.DTO
{
    [Serializable]
   public class SYSCodeDTO
    {
       
       public string TYPE_ID { get; set; }
       public string CODE { get; set; }
       public string SHORT_NAME { get; set; }
       

       //For City & country
       public string COUNTRY_CODE { get; set; }
       public string COUNTRY_CODE_ALPHA { get; set; }
       public string COUNTRY_NAME { get; set; }

       public string CITY_CODE { get; set; }
       public string CITY_NAME { get; set; }


       public string PROMO_CODE { get; set; }
       public string DESCRIPTION { get; set; }
    }
}
