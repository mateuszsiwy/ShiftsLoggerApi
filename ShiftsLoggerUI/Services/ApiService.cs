using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShiftsLoggerUI.Services
{
    internal class ApiService
    {
        private readonly HttpClient _httpClient;
        public ApiService() 
        { 
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7004/api/ShiftItems");
        }

        public async Task<HttpResponseMessage> GetAsync(string endpoint)
        {
            try
            {
                return await _httpClient.GetAsync(endpoint);
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Request error: {ex.Message}");
                throw;
            }
        }

        public async Task<HttpResponseMessage> PostAsync(string endpoint, HttpContent content)
        {
            try
            {
                return await _httpClient.PostAsync(endpoint, content);
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Request error: {ex.Message}");
                throw;
            }
        }

            public async Task<HttpResponseMessage> DeleteAsync(string endpoint)
            {
                try
                {
                    Console.WriteLine($"Requesting DELETE for URL: {_httpClient.BaseAddress+endpoint}");

                    return await _httpClient.DeleteAsync(_httpClient.BaseAddress + endpoint);
                }
                catch (HttpRequestException ex)
                {
                    Console.WriteLine($"Request error: {ex.Message}");
                    throw;
                }
            }

        public async Task<HttpResponseMessage> PutAsync(string endpoint, HttpContent content)
        {
            try
            {
                Console.WriteLine($"Requesting PUT for URL: {_httpClient.BaseAddress + endpoint}");

                return await _httpClient.PutAsync(_httpClient.BaseAddress + endpoint, content);
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Request error: {ex.Message}");
                throw;
            }
        }

    }
}
