using Contract.Models.CalculoJuros.Result;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Services.CalculoJuros;
using System;
using System.Threading.Tasks;

namespace WebApiSoftplan.Controllers
{
    [ApiController]
    [Route("api/[controller]/{valorInicial}&{tempo}")]
    public class CalcularJurosController : ControllerBase
    {
        private readonly ILogger<CalcularJurosController> _logger;
        private readonly ICalculoJurosService _calculoJurosService;

        public CalcularJurosController(ILogger<CalcularJurosController> logger, ICalculoJurosService calculoJurosService)
        {
            _logger = logger;
            _calculoJurosService = calculoJurosService;
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
                var valorCalculado = await _calculoJurosService.GerarCalculo(valorInicial, tempo);
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
