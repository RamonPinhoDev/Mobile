using ScheduleListUI.Services;
using ScheduleListUI.Validations;
using ScheduleListUI.Views;

namespace ScheduleListUI;


    public partial class App : Application
    {
        private readonly ApiServices _services;
    private readonly IValidator _validator;
        public App(ApiServices apiServices)
        {
            InitializeComponent();
            _services = apiServices;
            //MainPage = new AppShell();
            //configura inicialização
           //MainPage = new NavigationPage(new LoginPage(_services, _validator));
        SetMainPage();

        }

    private void SetMainPage()
    {
        var accessToken = Preferences.Get("accsstoken", string.Empty);
        if (string.IsNullOrEmpty(accessToken))
        {
            MainPage = new NavigationPage(new LoginPage(_services, _validator));
            return;

        }
        MainPage = new AppShell();
    }

   


    }


