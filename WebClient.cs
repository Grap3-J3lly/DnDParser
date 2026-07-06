using System.Net.Http.Headers;

namespace DndParser
{
    public class WebClient
    {
        private HttpClient client;
        
        private string baseURL;

        /// <summary>
        /// Initializes a WebClient with a base URL and creates a basic HttpClient with JSON format headers.
        /// </summary>
        /// <param name="baseURL"></param>
        public WebClient(string baseURL)
        {
            this.baseURL = baseURL;
            client = CreateBasicHttpClient();
        }

        /// <summary>
        /// Creates a basic HttpClient with JSON format headers.
        /// </summary>
        /// <returns></returns>
        private static HttpClient CreateBasicHttpClient()
        {
            HttpClient client = new();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return client;
        }

        /// <summary>
        /// Asynchronously retrieves data from the specified URL and stores the response in the WebClient instance.
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="client"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        public static async Task<string> GetDataAtURL(string baseURL, string url)
        {            
            // This only currently works for basic HTTP Clients, this would need to be amended for future use with other types of clients
            using(HttpClient client = CreateBasicHttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(baseURL + url);

                if(response.IsSuccessStatusCode)
                {
                    return await client.GetStringAsync(baseURL + url);    
                }
                else
                {
                    Console.WriteLine($"Request failed with status {response.StatusCode}");
                    return "";
                }
            }
        }
    }   
}