using System;
using System.Net.Http;
using System.Threading.Tasks;


namespace CovidDataApp
{
    class DataAPI
    {
        private string Data = "";
        public async Task AccessAsync()
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://covid-193.p.rapidapi.com/statistics"),
                Headers =  {
                                { "x-rapidapi-host", "covid-193.p.rapidapi.com" },
                                { "x-rapidapi-key", "e7a5bf539cmshdea0035a82c9f6fp110a3djsn8e7de6d41511" },
                             },
            };
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var body = await response.Content.ReadAsStringAsync();
            Set(body);
        }
        public void Set(string body)
        {
            Data = body;
        }
        public string Get()
        {
            return Data;
        }
    }

}
