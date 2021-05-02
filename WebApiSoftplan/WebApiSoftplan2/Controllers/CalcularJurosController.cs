using Contract.Models.CalculoJuros.Result;
using Contract.Models.TaxaJuros.Result;
using Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Services.CalculoJuros;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace WebApiSoftplan.Controllers
{
    [ApiController]
    [Route("api/[controller]/{valorInicial}&{tempo}")]
    public class CalcularJurosController : Controller
    {
        private readonly ILogger<CalcularJurosController> _logger;
        private readonly ICalculoJurosService _calculoJurosService;
        private readonly ApiGetter _apiGetter;

        public CalcularJurosController(ILogger<CalcularJurosController> logger, ICalculoJurosService calculoJurosService,
            HttpClient client, ApiGetter apiGetter)
        {
            _logger = logger;
            _calculoJurosService = calculoJurosService;
            _apiGetter = apiGetter;
        }

        /// <summary>
        /// Api de calculo de taxa de juros composto
        /// </summary>
        /// <param name="valorInicial"></param>
        /// <param name="tempo"></param>
        /// <returns>Objeto com o valor de calculo de juros</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<CalculoJurosResult> GetAsync(double valorInicial, int tempo)
        {
            _logger.LogInformation("Calcular Juros", "Iniciando");
            try
            {
                var taxa = await _apiGetter.Get<TaxaJurosResult>("https://localhost:44367/api/RetornarTaxaJuros/");
                var valorCalculado = _calculoJurosService.GerarCalculo(valorInicial, tempo, taxa.ValorTaxa);
                CalculoJurosResult result = new()
                {
                    Valor = valorCalculado,
                };

                _logger.LogInformation("Calcular Juros", "Processado");
                return result;
            }
            catch (Exception e)
            {
                _logger.LogError("Calcular Juros", args: string.Format("Erro: ", e.Message));
                throw;
            }
        }
    }
}
