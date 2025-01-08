using MauiAppContoare.Data;
namespace MauiAppContoare
{
    public partial class App : Application
    {
        public static FacturaDatabase Database { get; private set; }

        public App()
        {
            InitializeComponent();

           
            Database = new FacturaDatabase(new RestService());
            DependencyService.Register<IRestService, RestService>();

            MainPage = new NavigationPage(new FacturaListPage());
        }
    }
}
