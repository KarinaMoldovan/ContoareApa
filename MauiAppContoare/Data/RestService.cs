using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using MauiAppContoare.Models;
using Newtonsoft.Json;

namespace MauiAppContoare
{
    public class RestService : IRestService
    {
        private readonly HttpClient _httpClient;

        public RestService()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("http://localhost:5031/api/")
            };
        }

        public async Task<List<Tarif>> GetTarifsAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("Tarifs");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<Tarif>>(content) ?? new List<Tarif>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching data: {ex.Message}");
            }

            return new List<Tarif>();
        }

        public async Task<bool> AddTarifAsync(Tarif tarif)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("Tarifs", tarif);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding tarif: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> DeleteTarifAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"Tarifs/{id}");
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting tarif: {ex.Message}");
                return false;
            }
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
