using System;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json.Linq;

namespace serdditcli;

internal class Program
{

    #region Main Entry Point

    static void Main(string[] args)
    {
        var client = new HttpClient();
        client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("ChangeMeClient", "0.1"));

        var clientId = "YOUR_CLIENT_ID";
        var secret = "YOUR_SECRET";
        var bytes = Encoding.ASCII.GetBytes($"{clientId}:{secret}");
        var clientAuth = new AuthenticationHeaderValue(
            "Basic", Convert.ToBase64String(bytes)
        );
        
        var postContent = new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string, string>("grant_type", "password"),
            new KeyValuePair<string, string>("username", "USERNAME"),
            new KeyValuePair<string, string>("password", "PASSWORD")
        });
        
        client.DefaultRequestHeaders.Authorization = clientAuth;
        
        var response = client.PostAsync("https://www.reddit.com/api/v1/access_token", postContent).GetAwaiter().GetResult();
        var responseContent = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
        Console.WriteLine(responseContent);
    }

    #endregion
    
}