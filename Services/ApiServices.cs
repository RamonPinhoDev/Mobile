using Microsoft.Extensions.Logging;
using ScheduleListUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;


namespace ScheduleListUI.Services
{
    public class ApiServices
    {

        private readonly HttpClient _httpClient;
        //private static string _baseUrl = "https://8680hvff-7066.brs.devtunnels.ms/";
        private static string _baseUrl = "http://localhost:5284";

        private readonly ILogger<ApiServices> _logger;
        JsonSerializerOptions _serializerOptions;

        public ApiServices(HttpClient httpClient, ILogger<ApiServices> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<ApiResponse<bool>> RegistrarUsuarios(string nome, string email, string password, string telefone)
        {
            try
            {
                var register = new Register() { Nome = nome, Email = email, Telefone = telefone, Senha = password };
                var json = JsonSerializer.Serialize(register, _serializerOptions);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await PostRequest("api/Usuarios/Register", content);

                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogError($"Erro ao enviar requisição HTTP: {response.StatusCode}");

                    return new ApiResponse<bool> { ErrorMessage = $"Erro ao enviar requisição HTTP: {response.StatusCode}" };
                }

                return new ApiResponse<bool> { Data = true };
            }

            catch (Exception ex)
            {
                _logger.LogError($"Erro ao enviar requisição HTTP: {ex.Message}");
                return new ApiResponse<bool> { ErrorMessage = ex.Message };
            }

        }




        public async Task<ApiResponse<bool>> Login(String email, string password)
        {
            try
            {
                var login = new Login() { Email = email, Senha = password };

                var json = JsonSerializer.Serialize(login, _serializerOptions);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await PostRequest("api/Usuarios/Login", content);

                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogError($"Erro ao enviar requisição HTTP :{response.StatusCode}");
                    return new ApiResponse<bool> { ErrorMessage = $"Erro ao enviar requisição HTTP :{response.StatusCode}" };
                }
                //Ler o conteúdo da resposta HTTP como uma string de forma assíncrona.
                var jsonResult = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<Token>(jsonResult, _serializerOptions);


                //Armazena dados do token
                Preferences.Set("acesstoken", result!.AcessToken);
                Preferences.Set("Usuarioid", (int)result.UsuarioId!);
                Preferences.Set("usuarionome", result.UsuarioNome);
                return new ApiResponse<bool> { Data = true };
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro no login : {ex.Message}");
                return new ApiResponse<bool> { ErrorMessage = ex.Message };
            }



        }
        private async Task<HttpResponseMessage> PostRequest(string uri, HttpContent content)
        {
            var enderecoUrL = _baseUrl + uri;
            try
            {

                var result = await _httpClient.PostAsync(enderecoUrL, content);
                return result;
            }
            catch (Exception ex)
            { // Log o erro ou trate conforme necessário

                _logger.LogError($"Erro ao enviar requisição POST para {uri}: {ex.Message}");
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
        }
    }

}
