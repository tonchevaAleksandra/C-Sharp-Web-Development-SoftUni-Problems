using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace SandBox
{
    public class Program
    {
        public async Task Main(string[] args)
        {
            //Input.Email: aleksandra_toncheva@yahoo.com
            //Input.Password: ******
            //__RequestVerificationToken: CfDJ8EgpRzniKKxKv - WUQxTUl4oSRrA0lm15gMYyXX2eCEb6BC99vkx0XR2ewqgqRazOGMCkb4EMs90upjPL2nQZhmYCono4lTBevVHXgEZjSplXYWwjMh0ZOew2y9X9WMGr17PnCgHuvz31Omm_nJfOGh0
            //Input.RememberMe: false

            var listOfParameters = new List<KeyValuePair<string, string>>();
            listOfParameters.Add(new KeyValuePair<string, string>("Input.Email", "aleksandra_toncheva@yahoo.com"));

            listOfParameters.Add(new KeyValuePair<string, string>("Input.Password", "******"));

            //listOfParameters.Add(new KeyValuePair<string, string>("__RequestVerificationToken", "CfDJ8EgpRzniKKxKv - WUQxTUl4oSRrA0lm15gMYyXX2eCEb6BC99vkx0XR2ewqgqRazOGMCkb4EMs90upjPL2nQZhmYCono4lTBevVHXgEZjSplXYWwjMh0ZOew2y9X9WMGr17PnCgHuvz31Omm_nJfOGh0"));

            //listOfParameters.Add(new KeyValuePair<string, string>("Input.RememberMe", "false"));

            HttpClient httpClient = new HttpClient();

            var response = await httpClient.PostAsync("https://presscenters.com/Identity/Account/Login",
                new FormUrlEncodedContent(listOfParameters));
        }
    }
}
