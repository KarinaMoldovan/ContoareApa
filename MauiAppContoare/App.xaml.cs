using MauiAppContoare.Data;
namespace MauiAppContoare
{
    public partial class App : Application
    {
        public static FacturaDatabase Database { get; private set; }

        public App()
        {
            InitializeComponent();

            //// Creează instanța de HttpClient și o transmite constructorului RestService
            //var httpClient = new HttpClient();
            Database = new FacturaDatabase(new RestService());

            MainPage = new NavigationPage(new FacturaListPage());
        }
    }
}
