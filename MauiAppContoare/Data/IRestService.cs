using System.Collections.Generic;
using System.Threading.Tasks;
using MauiAppContoare.Models;

namespace MauiAppContoare
{
    public interface IRestService
    {
        Task<List<Tarif>> GetTarifsAsync();
        Task<bool> AddTarifAsync(Tarif tarif);
        Task<bool> DeleteTarifAsync(int id);
        Task<List<Factura>> RefreshDataAsync(); 

        Task<List<Consumator>> GetConsumatoriAsync();
        Task<bool> AddConsumatorAsync(Consumator consumator);
        Task<bool> UpdateConsumatorAsync(Consumator consumator);
        Task<bool> DeleteConsumatorAsync(int id);

        Task<List<Contor>> GetContoareAsync();
        Task<bool> AddContorAsync(Contor contor);
        Task<Contor> UpdateContorAsync(Contor contor);
        Task DeleteContorAsync(int id);
    }
}
