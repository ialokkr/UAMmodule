using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace UAMmodule
{
    class Program
    {

        const string tenantId = "b759a000-2d88-41f3-8342-da932d621ed5";
        const string tokenEndPoint = "https://login.microsoftonline.com/common/oauth2/token";

        static async Task Main(string[] args)
        {

            string Token = await AccessTokenGenerator();

        }

        public static async Task<string> AccessTokenGenerator()

        {
            TokenModel token = new TokenModel();
            var client = new HttpClient();
           // ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));



            var Parameters = new List<KeyValuePair<String, String>>();
            Parameters.Add(new KeyValuePair<String, String>("grant_type", "password"));
            Parameters.Add(new KeyValuePair<String, String>("client_id", "cc1f9793-3910-4118-9385-9db53f2166cd"));
            Parameters.Add(new KeyValuePair<String, String>("resource", "00000003-0000-0000-c000-000000000000"));
            Parameters.Add(new KeyValuePair<String, String>("client_secret", "DBx0BVG~8_Qxs2V-QRBfg.EmUSzBt98i~8"));
            Parameters.Add(new KeyValuePair<String, String>("scope", "openid"));
            Parameters.Add(new KeyValuePair<String, String>("username", "alok@iamaorg.onmicrosoft.com"));
            Parameters.Add(new KeyValuePair<String, String>("password", "#Shivlokant98"));



            HttpContent content = new FormUrlEncodedContent(Parameters);



            var uri = String.Format(tokenEndPoint, tenantId);
            var response = client.PostAsync(uri, content).Result;
            if (response.IsSuccessStatusCode)
            {
                if (response.Content != null)
                {
                    var successStream = await response.Content.ReadAsStreamAsync();
                    using (var streamReader = new StreamReader(successStream))
                    {
                        using (var jsonTextReader = new JsonTextReader(streamReader))
                        {
                            var jsonSerializer = new JsonSerializer();
                            token = jsonSerializer.Deserialize<TokenModel>(jsonTextReader);
                        }
                    }
                }
               
            }
            return token.access_token.ToString();

        }
    }
}



