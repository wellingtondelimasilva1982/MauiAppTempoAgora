using MauiAppTempoAgora.Models;
using MauiAppTempoAgora.Services;

namespace MauiAppTempoAgora
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            try
            {
                // Verifica conexão com a internet
                if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
                {
                    await DisplayAlert("Sem conexão", "Você está sem acesso à internet. Verifique sua conexão e tente novamente.", "OK");
                    return;
                }

                if (!string.IsNullOrEmpty(txt_cidade.Text))
                {
                    lbl_res.Text = "Buscando dados...";
                    Tempo? t = await DataService.GetPrevisao(txt_cidade.Text);

                    if (t != null && !string.IsNullOrEmpty(t.main))
                    {
                        string dados_previsao = $"Localização: {txt_cidade.Text}\n" +
                            $"Latitude: {t.lat} \n" +
                            $"Longitude: {t.lon} \n" +
                            $"Nascer do Sol: {t.sunrise} \n" +
                            $"Por do Sol: {t.sunset} \n" +
                            $"Temperatura Máxima: {t.temp_max} \n" +
                            $"Temperatura Mínima: {t.temp_min} \n" +
                            $"Clima: {t.main} \n" +
                            $"Velocidade do vento: {t.speed} \n" +
                            $"Visibilidade: {t.visibility} \n";

                        lbl_res.Text = dados_previsao;
                    }
                    else
                    {
                        lbl_res.Text = $"Não foi possível encontrar dados para a cidade \"{txt_cidade.Text}\". Verifique se o nome está correto.";
                    }
                }
                else
                {
                    lbl_res.Text = "Por favor, preencha o nome da cidade.";
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Ops", ex.Message, "OK");
            }
        }
    }
}
