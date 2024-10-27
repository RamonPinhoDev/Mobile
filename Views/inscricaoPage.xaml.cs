using ScheduleListUI.Services;
using ScheduleListUI.Validations;
using System.ComponentModel.Design;

namespace ScheduleListUI.Views;

public partial class inscricaoPage : ContentPage
{
    private readonly ApiServices _apiservices;
    private readonly IValidator _validator;



    public inscricaoPage(ApiServices services, IValidator validator)
    {
        InitializeComponent();
        _apiservices = services;
        _validator = validator;
    }

    private async void BtnSignup_Clicked(object sender, EventArgs e)
    { if (await _validator.Validar(EntNome.Text, EntEmail.Text, EntPassword.Text, EntTelefone.Text))
        {
            var response = await _apiservices.RegistrarUsuarios(EntNome.Text, EntEmail.Text, EntPassword.Text, EntTelefone.Text);

            if (!response.HasError)
            {
                await DisplayAlert("Aviso", "Sua conta foi criada com sucesso!!", "OK");
                await Navigation.PushAsync(new LoginPage(_apiservices, _validator));
            }
            else
            {

                await DisplayAlert("Erro", "Algo deu errado!!!", "Cancelar");

            }
        }
        else
            {
            string mensagemErro = "";
            mensagemErro += _validator.NomeErro != null ? $"\n- {_validator.NomeErro}" : "";
            mensagemErro += _validator.EmailErro != null ? $"\n- {_validator.EmailErro}" : "";
            mensagemErro += _validator.TelefoneErro != null ? $"\n- {_validator.TelefoneErro}" : "";
            mensagemErro += _validator.SenhaErro != null ? $"\n- {_validator.SenhaErro}" : "";

            await DisplayAlert("Erro", mensagemErro, "OK");


        }

    }

        
    


    private async void TapLogin_Tapped(object sender, TappedEventArgs e)
    {
        await Navigation.PushAsync(new LoginPage(_apiservices, _validator));
    }
}