namespace Karim.Customer.HrApplication.Application._Common.DateConverter
{
    internal static class DatesConverter
    {
        public static DateTime? Connverter(DateTime? date)
        {
            if(date == null) return null;
            //Get Date Based On Outcome Date
            var Date = new DateTime(date.Value.Year, date.Value.Month, date.Value.Day);
            return Date;
        }
    }
}
