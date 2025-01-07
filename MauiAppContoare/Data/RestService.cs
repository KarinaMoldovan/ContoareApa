using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Threading.Tasks;
using MauiAppContoare.Models;
using MauiAppContoare.Data;

namespace MauiAppContoare.Data
{
    public class RestService : IRestService
    {
        private readonly HttpClient _httpClient;

        public RestService()
        {
            var handler = new HttpClientHandler
            {
                // Dezactivează verificarea certificatului pentru dezvoltare
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
            };

            _httpClient = new HttpClient(handler)
            {
                BaseAddress = new Uri("http://localhost:5031/") // Setează URL-ul de bază
            };
        }

        public async Task<List<Factura>> RefreshDataAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/facturi"); // Endpoint relativ
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<List<Factura>>() ?? new List<Factura>();
                }
                throw new HttpRequestException($"Failed to fetch facturi. Status code: {response.StatusCode}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching facturi: {ex.Message}");
                throw; // Aruncă excepția pentru a fi gestionată la nivel superior
            }
        }


    }
}
