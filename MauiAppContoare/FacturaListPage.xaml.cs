using System.Collections.ObjectModel;
using System.Net.Http.Json;
using MauiAppContoare.Models;

namespace MauiAppContoare;

public partial class FacturaListPage : ContentPage
{
    private readonly HttpClient _httpClient;
    public ObservableCollection<Factura> Facturi { get; set; }

    public FacturaListPage()
    {
        InitializeComponent();
        Facturi = new ObservableCollection<Factura>();
        BindingContext = this;

        // Inițializează HttpClient pentru REST API
        _httpClient = new HttpClient
        {
            BaseAddress = new Uri("https://192.168.0.102:45455/api/")
        };
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        try
        {
            // Apelează API-ul pentru a obține facturile
            var facturi = await _httpClient.GetFromJsonAsync<List<Factura>>("facturi");
            if (facturi != null)
            {
                Facturi.Clear();
                foreach (var factura in facturi)
                {
                    Console.WriteLine($"Factura adăugată: {factura.Suma} - {factura.DataEmitere}");
                    Facturi.Add(factura);
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Eroare la obținerea facturilor: {ex.Message}");
        }
    }

    async void OnFacturaSelected(object sender, SelectedItemChangedEventArgs e)
    {
        if (e.SelectedItem != null)
        {
            // Navighează către o pagină de detalii dacă este necesar
        }
    }

    // Handler pentru butonul Tarife
    async void OnTarifeButtonClicked(object sender, EventArgs e)
    {
        // Obține instanța serviciului IRestService din DependencyService
        var restService = DependencyService.Get<IRestService>();

        // Navighează către pagina TarifePage și furnizează serviciul IRestService
        var tarifePage = new TarifePage(restService);
        await Navigation.PushAsync(tarifePage);
    }
}
