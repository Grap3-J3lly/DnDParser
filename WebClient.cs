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

        public static async Task<string[]> GetDataAtURLBulk(string baseURL, List<string> urls)
        {
            using(HttpClient client = CreateBasicHttpClient())
            {
                // Create an array of tasks that will run concurrently
                IEnumerable<Task<string>> tasks = urls.Select(url => client.GetStringAsync(baseURL + url));

                // Await all tasks to finish and collect their results
                return await Task.WhenAll(tasks);
            }
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
            // TODO: Destroying and recreating the HttpClient is likely causing issues.
            // Need to support sending a list of urls and only continuing once all getasync tasks have finished returning


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
/*
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

public class BulkDownloader
{
    private readonly HttpClient _client;

    public BulkDownloader(HttpClient client)
    {
        _client = client;
    }

    public async Task<string[]> LoadUrlsInBulkAsync(IEnumerable<string> urls)
    {
        // Create an array of tasks that will run concurrently
        IEnumerable<Task<string>> tasks = urls.Select(url => _client.GetStringAsync(url));

        // Await all tasks to finish and collect their results
        return await Task.WhenAll(tasks);
    }
}


public async Task ProcessUrlsConcurrentlyAsync(IEnumerable<string> urls)
{
    var options = new ParallelOptions 
    { 
        MaxDegreeOfParallelism = 10 // Limit to 10 concurrent requests
    };

    await Parallel.ForEachAsync(urls, options, async (url, cancellationToken) =>
    {
        var response = await _client.GetStringAsync(url, cancellationToken);
        Console.WriteLine($"Processed {url}. Length: {response.Length}");
    });
}

*/