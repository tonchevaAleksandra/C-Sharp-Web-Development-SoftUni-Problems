using System;

namespace AspNetAppForTestingRazor.Services
{
    public class ShortStringService : IShortStringService
    {
        public string GetShort(string str, int maxLength )
        {
            if (string.IsNullOrEmpty(str))
            {
                return str;
            }

            if (str.Length<=maxLength)
            {
                return str;
            }


            return str.Substring(0, maxLength) + "...";
        }
    }
}
