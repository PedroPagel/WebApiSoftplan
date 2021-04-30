using Contract.Models.CalculoJuros.Result;
using Contract.Models.TaxaJuros.Result;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Net.Http;
using WebApiSoftplan2;

namespace TestProject.Integration
{
    [TestClass]
    public class IntegrationTest
    {
        private readonly IConfiguration _configuration;
        private readonly TestServer _server;
        private readonly HttpClient _client;

        public IntegrationTest()
        {
            var builder = new ConfigurationBuilder().SetBasePath("C:\\Users\\pedro\\source\\repos\\WebApiSoftplan\\WebApiSoftplan\\").AddJsonFile($"appsettings.json", true, true).AddInMemoryCollection();

            _configuration = builder.Build();

            _server = new TestServer(new WebHostBuilder().UseStartup<Startup>());
            _client = _server.CreateClient();
        }

        [TestMethod]
        public async System.Threading.Tasks.Task Executar_teste_de_integracao_retornando_taxa_umAsync()
        {
            var response = await _client.GetAsync("api/RetornarTaxaJuros");

            //var result = await apiRequest.GetAsync<TaxaJurosResult>("TaxaDeJuros", string.Empty);

            TaxaJurosResult taxa = new()
            {
                ValorTaxa = 1
            };
        }


        [TestMethod]
        public void Executar_teste_de_integracao_retornando_calculo_de_taxa()
        {
            CalculoJurosResult calculo = new()
            {
                Valor = 105.10
            };

            
        }
    }
}
