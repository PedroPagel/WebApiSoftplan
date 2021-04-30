using Contract.Models.TaxaJuros.Result;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Services.CalculoJuros
{
    public class CalculoJurosService : ICalculoJurosService
    {
        private readonly HttpClient _httpClient;

        public CalculoJurosService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<double> GerarCalculo(double valorInicial, int tempo)
        {
            if (valorInicial <= 0)
            {
                throw new Exception("Valor não informado corretamente");
            }

            if (tempo <= 0)
            {
                throw new Exception("Tempo não informado corretamente");
            }

            var response = await _httpClient.GetAsync("https://localhost:44367/api/RetornarTaxaJuros/");

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Não foi possível realizar a busca de taxa");
            }

            var json = await response.Content.ReadAsStringAsync();
            var taxa = JsonConvert.DeserializeObject<TaxaJurosResult>(json);
            var taxaMensal = taxa.ValorTaxa / 100;
            double valorFinal = valorInicial;
            double jurosMes;

            for (int meses = 0; meses < tempo; meses++)
            {
                jurosMes = taxaMensal * valorFinal;
                valorFinal += jurosMes;
            }

            return Math.Round(valorFinal, 2);
        }
    }
}
