using Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using WebApiSoftplan2;

namespace TestProject.Integration
{
    [TestClass]
    public class IntegrationTest
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;
        private readonly ApiGetter _apiGetter;

        public IntegrationTest()
        {
            var webHostBuilder = new WebHostBuilder()
            .UseConfiguration(new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build())
            .UseStartup<Startup>();

            _server = new TestServer(webHostBuilder);
            _client = _server.CreateClient();
            _client.BaseAddress = new Uri("http://localhost:44367/");
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _apiGetter = new(_client);
        }

        [TestMethod]
        public async Task Executar_teste_de_integracao_retornando_taxa()
        {
            var response = await _client.GetAsync("api/RetornarTaxaJuros");

            Assert.IsTrue(response.IsSuccessStatusCode == true, "Não foi possível buscar a api de taxa");
        }

        [TestMethod]
        public async Task Buscar_Path_relativo_projeto_Softplan_api()
        {
            var response = await _client.GetAsync("api/ShowMeTheCode");

            Assert.IsTrue(response.IsSuccessStatusCode == true, "Não foi possível buscar a api ShowMeTheCode");
        }


        //[TestMethod]
        //public async Task Executar_teste_de_integracao_retornando_calculo_de_taxa()
        //{
            
        //    var response = await _client.GetAsync("api/CalcularJuros/100&5");

        //    Assert.AreEqual(response.IsSuccessStatusCode == true, "Não foi possível buscar a api de taxa");
        //}
    }
}
