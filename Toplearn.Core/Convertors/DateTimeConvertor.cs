using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toplearn.Core.Convertors
{
    public static class DateTimeConvertor
    {

        public static string ToShamsi(this DateTime value)
        {
            PersianCalendar pCalender = new PersianCalendar();
            string shamsiDate = pCalender.GetYear(value).ToString() + '/' +
                pCalender.GetMonth(value).ToString("00") + '/' +
                pCalender.GetDayOfMonth(value).ToString("00");
            return shamsiDate;
        }

    }
}
