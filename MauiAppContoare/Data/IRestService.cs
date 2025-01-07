using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MauiAppContoare.Models;

namespace MauiAppContoare.Data
{
    public interface IRestService
    {
        Task<List<Factura>> RefreshDataAsync();
        //Task SaveFacturaAsync(Factura factura, bool isNewItem);
        //Task DeleteFacturaAsync(int facturaId);
        //Task<Factura?> GetFacturaByIdAsync(int facturaId);
    }
}
