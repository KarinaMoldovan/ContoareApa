using MauiAppContoare.Models;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Net.Http.Json;

namespace MauiAppContoare;

public partial class ConsumatorPage : ContentPage
{
    private const string ApiUrl = "http://localhost:5031/api/consumatori";
    public ObservableCollection<Consumator> Consumatori { get; set; } = new();
    public Consumator SelectedConsumator { get; set; }

    public ConsumatorPage()
    {
        InitializeComponent();
        BindingContext = this;
        LoadConsumatori();
    }

    private async void LoadConsumatori()
    {
        try
        {
            var httpClient = new HttpClient();
            var response = await httpClient.GetStringAsync(ApiUrl);
            var consumatori = JsonConvert.DeserializeObject<List<Consumator>>(response);

            Consumatori.Clear();
            foreach (var consumator in consumatori)
            {
                Consumatori.Add(consumator);
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Eroare", $"Nu s-au putut încărca consumatorii: {ex.Message}", "OK");
        }
    }

    private async void OnAddClicked(object sender, EventArgs e)
    {
        var newConsumator = await ShowConsumatorForm();
        if (newConsumator != null)
        {
            var httpClient = new HttpClient();
            var response = await httpClient.PostAsJsonAsync(ApiUrl, newConsumator);

            if (response.IsSuccessStatusCode)
            {
                Consumatori.Add(newConsumator);
                await DisplayAlert("Succes", "Consumator adăugat!", "OK");
            }
        }
    }

    private async void OnEditClicked(object sender, EventArgs e)
    {
        if (SelectedConsumator == null)
        {
            await DisplayAlert("Eroare", "Selectați un consumator pentru a-l edita.", "OK");
            return;
        }

        var updatedConsumator = await ShowConsumatorForm(SelectedConsumator);
        if (updatedConsumator != null)
        {
            var httpClient = new HttpClient();
            var response = await httpClient.PutAsJsonAsync($"{ApiUrl}/{SelectedConsumator.ConsumatorId}", updatedConsumator);

            if (response.IsSuccessStatusCode)
            {
                LoadConsumatori(); 
                await DisplayAlert("Succes", "Consumator actualizat!", "OK");
            }
        }
    }

    private async void OnDeleteClicked(object sender, EventArgs e)
    {
        if (SelectedConsumator == null)
        {
            await DisplayAlert("Eroare", "Selectați un consumator pentru a-l șterge.", "OK");
            return;
        }

        var confirm = await DisplayAlert("Confirmare", "Sigur doriți să ștergeți acest consumator?", "Da", "Nu");
        if (confirm)
        {
            var httpClient = new HttpClient();
            var response = await httpClient.DeleteAsync($"{ApiUrl}/{SelectedConsumator.ConsumatorId}");

            if (response.IsSuccessStatusCode)
            {
                LoadConsumatori(); 
                await DisplayAlert("Succes", "Consumator șters!", "OK");
            }
        }
    }


    private async Task<Consumator> ShowConsumatorForm(Consumator consumator = null)
    {
        string nume = await DisplayPromptAsync("Nume", "Introduceți numele:", initialValue: consumator?.Nume);
        string prenume = await DisplayPromptAsync("Prenume", "Introduceți prenumele:", initialValue: consumator?.Prenume);
        string email = await DisplayPromptAsync("Email", "Introduceți emailul:", initialValue: consumator?.Email);

        if (string.IsNullOrWhiteSpace(nume) || string.IsNullOrWhiteSpace(prenume) || string.IsNullOrWhiteSpace(email))
        {
            await DisplayAlert("Eroare", "Toate câmpurile sunt obligatorii.", "OK");
            return null;
        }

        return new Consumator
        {
            ConsumatorId = consumator?.ConsumatorId ?? 0,
            Nume = nume,
            Prenume = prenume,
            Email = email
        };
    }
}
