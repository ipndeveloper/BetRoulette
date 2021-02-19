using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Masiv.BetApi.Utilty
{
    public static class Utility
    {
        public static  DateTime GetFormatDateTimeIso8601()
        {

            Chilkat.CkDateTime dateTime = new Chilkat.CkDateTime();

            dateTime.SetFromCurrentSystemTime();
            string dateIso8601 = dateTime.GetAsIso8601("YYYY-MM-DDThh:mmTZD", false);

            return DateTime.Parse(dateIso8601);
        }
    }
}
