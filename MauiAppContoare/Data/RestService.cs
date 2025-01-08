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
                var response = await _httpClient.GetAsync("api/facturi"); 
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<List<Factura>>() ?? new List<Factura>();
                }
                throw new HttpRequestException($"Failed to fetch facturi. Status code: {response.StatusCode}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching facturi: {ex.Message}");
                throw; 
            }
        }
        public async Task<List<Consumator>> GetConsumatoriAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("consumatori");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<Consumator>>(content) ?? new List<Consumator>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching consumatori: {ex.Message}");
            }
            return new List<Consumator>();
        }

        public async Task<bool> AddConsumatorAsync(Consumator consumator)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("consumatori", consumator);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding consumator: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> UpdateConsumatorAsync(Consumator consumator)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"consumatori/{consumator.ConsumatorId}", consumator);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating consumator: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> DeleteConsumatorAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"consumatori/{id}");
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting consumator: {ex.Message}");
                return false;
            }
        }
        public async Task<List<Contor>> GetContoareAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("contoare");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<Contor>>(content) ?? new List<Contor>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching contoare: {ex.Message}");
            }
            return new List<Contor>();
        }

        public async Task<bool> AddContorAsync(Contor contor)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("contoare", contor);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding contor: {ex.Message}");
                return false;
            }
        }

        public async Task<Contor> UpdateContorAsync(Contor contor)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"contoare/{contor.ContorId}", contor);
                if (response.IsSuccessStatusCode)
                {
                    var updatedContor = await response.Content.ReadFromJsonAsync<Contor>();
                    return updatedContor ?? contor; // Returnăm contorul actualizat sau cel original
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating contor: {ex.Message}");
            }
            return null; // Returnăm null în caz de eroare
        }


        public async Task DeleteContorAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"contoare/{id}");
                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Error deleting contor. Status Code: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting contor: {ex.Message}");
            }
        }

    }
}
