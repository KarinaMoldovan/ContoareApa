using System.Collections.ObjectModel;
using MauiAppContoare.Models;

namespace MauiAppContoare;

public partial class TarifePage : ContentPage
{
    private readonly IRestService _restService;
    public ObservableCollection<Tarif> Tarife { get; set; } = new ObservableCollection<Tarif>();

    public TarifePage(IRestService restService)
    {
        InitializeComponent();
        _restService = restService;
        BindingContext = this;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await LoadTarife();
    }

    private async Task LoadTarife()
    {
        try
        {
            var tarife = await _restService.GetTarifsAsync();
            Tarife.Clear();
            foreach (var tarif in tarife)
            {
                Tarife.Add(tarif);
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Eroare", $"Nu s-au putut încărca tarifele: {ex.Message}", "OK");
        }
    }

    private async void OnAddTarifClicked(object sender, EventArgs e)
    {
        try
        {
            if (decimal.TryParse(pretEntry.Text, out var pret) && dataInceputPicker.Date != null)
            {
                var tarif = new Tarif
                {
                    PretPeMetruCub = pret,
                    DataInceput = dataInceputPicker.Date,
                    DataSfarsit = dataSfarsitPicker.Date
                };

                if (await _restService.AddTarifAsync(tarif))
                {
                    await DisplayAlert("Succes", "Tariful a fost adăugat!", "OK");
                    await LoadTarife();
                }
                else
                {
                    await DisplayAlert("Eroare", "Nu s-a putut adăuga tariful.", "OK");
                }
            }
            else
            {
                await DisplayAlert("Eroare", "Datele introduse nu sunt valide.", "OK");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Eroare", $"A apărut o problemă: {ex.Message}", "OK");
        }
    }

    private async void OnDeleteTarifClicked(object sender, EventArgs e)
    {
        try
        {
            if (tarifeListView.SelectedItem is Tarif selectedTarif)
            {
                if (await _restService.DeleteTarifAsync(selectedTarif.TarifId))
                {
                    await DisplayAlert("Succes", "Tariful a fost șters!", "OK");
                    await LoadTarife();
                }
                else
                {
                    await DisplayAlert("Eroare", "Nu s-a putut șterge tariful.", "OK");
                }
            }
            else
            {
                await DisplayAlert("Atenție", "Selectează un tarif pentru a șterge.", "OK");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Eroare", $"A apărut o problemă: {ex.Message}", "OK");
        }
    }
}
