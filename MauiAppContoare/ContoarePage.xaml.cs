using MauiAppContoare.Models;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Net.Http.Json;

namespace MauiAppContoare;

public partial class ContoarePage : ContentPage
{
    private readonly IRestService _restService;

    private const string ApiUrl = "http://localhost:5031/api/contoare";

    public ContoarePage()
    {
        InitializeComponent();
        _restService = DependencyService.Get<IRestService>();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await LoadContoare();
    }

    private async Task LoadContoare()
    {
        try
        {
            var contoare = await _restService.GetContoareAsync();
            ContoareListView.ItemsSource = contoare;
        }
        catch (Exception ex)
        {
            await DisplayAlert("Eroare", $"Nu s-a putut încărca lista de contoare: {ex.Message}", "OK");
        }
    }

    //private async Task OnContorSelected(object sender, SelectedItemChangedEventArgs e)
    //{
    //    // Verifică dacă un contor a fost selectat
    //    if (e.SelectedItem is Contor contor)
    //    {
    //        // Afișează un meniu cu opțiuni pentru editare sau ștergere
    //        var response = await DisplayActionSheet("Alege o acțiune:", "Anulează", null, "Editare", "Ștergere");

    //        // În funcție de alegerea utilizatorului, efectuează acțiunea corespunzătoare
    //        if (response == "Editare")
    //        {
    //            // Redirecționează către funcția de editare
    //            await OnEditClicked(sender, e);
    //        }
    //        else if (response == "Ștergere")
    //        {
    //            // Redirecționează către funcția de ștergere
    //            await OnDeleteClicked(sender, e);
    //        }

    //        // Dezactivează selecția contorului după procesare
    //        ContoareListView.SelectedItem = null;
    //    }
    //}



    private async void OnAddClicked(object sender, EventArgs e)
    {
        string numarSerie = await DisplayPromptAsync("Adaugare Contor", "Introduceți numărul de serie:");
        if (string.IsNullOrWhiteSpace(numarSerie)) return;

        string consumatorIdString = await DisplayPromptAsync("Adaugare Contor", "Introduceți ID-ul Consumatorului:");
        if (!int.TryParse(consumatorIdString, out int consumatorId))
        {
            await DisplayAlert("Eroare", "ID-ul Consumatorului trebuie să fie un număr.", "OK");
            return;
        }

        var contor = new Contor
        {
            NumarSerie = numarSerie,
            ValoareActuala = 0,
            ConsumatorId = consumatorId
        };

        try
        {
            await _restService.AddContorAsync(contor);
            await LoadContoare();
        }
        catch (Exception ex)
        {
            await DisplayAlert("Eroare", $"Nu s-a putut adăuga contorul: {ex.Message}", "OK");
        }
    }

    private async void OnEditClicked(object sender, EventArgs e)
    {
        if (SelectedContor == null)
        {
            await DisplayAlert("Eroare", "Selectați un contor pentru a-l edita.", "OK");
            return;
        }

        var updatedContor = await ShowContorForm(SelectedContor);
        if (updatedContor != null)
        {
            try
            {
                var httpClient = new HttpClient();
                var response = await httpClient.PutAsJsonAsync($"{ApiUrl}/{SelectedContor.ContorId}", updatedContor);

                if (response.IsSuccessStatusCode)
                {
                    await LoadContoare(); // Reîncarcă lista contoarelor
                    await DisplayAlert("Succes", "Contor actualizat!", "OK");
                }
                else
                {
                    await DisplayAlert("Eroare", "Nu s-a putut actualiza contorul.", "OK");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Eroare", $"Eroare la actualizarea contorului: {ex.Message}", "OK");
            }
        }
    }

    private async void OnDeleteClicked(object sender, EventArgs e)
    {
        if (SelectedContor == null)
        {
            await DisplayAlert("Eroare", "Selectați un contor pentru a-l șterge.", "OK");
            return;
        }

        var confirm = await DisplayAlert("Confirmare", $"Sigur doriți să ștergeți contorul cu numărul de serie {SelectedContor.NumarSerie}?", "Da", "Nu");
        if (confirm)
        {
            try
            {
                var httpClient = new HttpClient();
                var response = await httpClient.DeleteAsync($"{ApiUrl}/{SelectedContor.ContorId}");

                if (response.IsSuccessStatusCode)
                {
                    await LoadContoare(); // Reîncarcă lista contoarelor
                    await DisplayAlert("Succes", "Contor șters!", "OK");
                }
                else
                {
                    await DisplayAlert("Eroare", "Nu s-a putut șterge contorul.", "OK");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Eroare", $"Eroare la ștergerea contorului: {ex.Message}", "OK");
            }
        }
    }

    private async Task<Contor> ShowContorForm(Contor contor = null)
    {
        string numarSerie = await DisplayPromptAsync("Număr Serie", "Introduceți numărul de serie:", initialValue: contor?.NumarSerie);
        string valoareActualaString = await DisplayPromptAsync("Valoare Actuală", "Introduceți valoarea actuală:", initialValue: contor?.ValoareActuala.ToString());
        string consumatorIdString = await DisplayPromptAsync("ID Consumator", "Introduceți ID-ul Consumatorului:", initialValue: contor?.ConsumatorId.ToString());

        if (string.IsNullOrWhiteSpace(numarSerie) || string.IsNullOrWhiteSpace(valoareActualaString) || string.IsNullOrWhiteSpace(consumatorIdString))
        {
            await DisplayAlert("Eroare", "Toate câmpurile sunt obligatorii.", "OK");
            return null;
        }

        if (!int.TryParse(valoareActualaString, out int valoareActuala) || !int.TryParse(consumatorIdString, out int consumatorId))
        {
            await DisplayAlert("Eroare", "Valoarea actuală și ID-ul Consumatorului trebuie să fie numere valide.", "OK");
            return null;
        }

        return new Contor
        {
            ContorId = contor?.ContorId ?? 0,
            NumarSerie = numarSerie,
            ValoareActuala = valoareActuala,
            ConsumatorId = consumatorId
        };
    }


    private Contor SelectedContor
    {
        get => ContoareListView.SelectedItem as Contor;
    }

}
