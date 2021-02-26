using System;
using System.Text;

namespace TestConverter
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(GetHtml());
        }

        static string GetHtml()
        {
            var sb = new StringBuilder();
            sb.AppendLine("<h1>Hello</h1>");
            if (DateTime.UtcNow.Year == 2021)
            {
                for (int i = 1; i <= 10; i++)
                {
                    if (i % 3 == 0)
                    {
                        sb.AppendLine("<p><span>" + i + "</span> <span>" + DateTime.UtcNow.Year + "</span></p>");
                    }
                }
            }

            return sb.ToString();
        }
    }
}
