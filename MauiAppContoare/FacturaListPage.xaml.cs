﻿using System.Collections.ObjectModel;
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
    {//To do
        if (e.SelectedItem != null)
        {
            //
        }
    }
    async void OnConsumatoriButtonClicked(object sender, EventArgs e)
    {
       
        var restService = DependencyService.Get<IRestService>();

       
        var consumatoriPage = new ConsumatorPage();
        await Navigation.PushAsync(consumatoriPage);
    }

    
    async void OnTarifeButtonClicked(object sender, EventArgs e)
    {
        
        var restService = DependencyService.Get<IRestService>();

       
        var tarifePage = new TarifePage(restService);
        await Navigation.PushAsync(tarifePage);
    }
}
