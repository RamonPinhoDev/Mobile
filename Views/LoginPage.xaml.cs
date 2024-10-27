
using ScheduleListUI.Services;
using ScheduleListUI.Validations;
namespace ScheduleListUI.Views;


public partial class LoginPage : ContentPage
{
    private readonly ApiServices _apiServices;
    private readonly IValidator _validator;
    public LoginPage(ApiServices apiServices, IValidator validator)
    {
        InitializeComponent();
        _apiServices = apiServices;
        _validator = validator;
    }
    private async void BtnSignIn_Clicked(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(EntEmail.Text))
        {
            await DisplayAlert("Erro", "Informe o email", "Cancelar");
            return;
        }
        if (string.IsNullOrEmpty(EntPassword.Text))
        {
            await DisplayAlert("Erro", "Informe o senha", "Cancelar");
            return;
        }

        var response = await _apiServices.Login(EntEmail.Text, EntPassword.Text);
        if (response.HasError)
        {
            Application.Current!.MainPage = new AppShell();
        }
        else
        {

            await DisplayAlert("Erro", "Algo de errado", "Cancelar");
        }
    }

    private async void TapRegister_Tapped(object sender, TappedEventArgs e)
    {
        await Navigation.PushAsync(new inscricaoPage(_apiServices, _validator));
    }
}