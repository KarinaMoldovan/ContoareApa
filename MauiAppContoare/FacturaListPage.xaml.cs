using System;
using MauiAppContoare.Models;

namespace MauiAppContoare;

public partial class FacturaListPage : ContentPage
{
    public FacturaListPage()
    {
        InitializeComponent();
    }

    async void OnConsumatoriButtonClicked(object sender, EventArgs e)
    {
        var consumatoriPage = new ConsumatorPage();
        await Navigation.PushAsync(consumatoriPage);
    }

    async void OnContoareButtonClicked(object sender, EventArgs e)
    {
        var contoarePage = new ContoarePage();
        await Navigation.PushAsync(contoarePage);
    }

    async void OnTarifeButtonClicked(object sender, EventArgs e)
    {
        var restService = DependencyService.Get<IRestService>();
        var tarifePage = new TarifePage(restService);
        await Navigation.PushAsync(tarifePage);
    }
}
