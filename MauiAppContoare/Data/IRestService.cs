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
        Task<List<Factura>> RefreshDataAsync(); // TODO: Implement this method
    }
}
