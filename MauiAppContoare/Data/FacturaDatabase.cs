using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MauiAppContoare.Models;

namespace MauiAppContoare.Data
{
    public class FacturaDatabase
    {
        IRestService restService;

        public FacturaDatabase(IRestService service)
        {
            restService = service;
        }

        public Task<List<Factura>> GetFacturiAsync()
        {
            return restService.RefreshDataAsync();
        }
    }
}
