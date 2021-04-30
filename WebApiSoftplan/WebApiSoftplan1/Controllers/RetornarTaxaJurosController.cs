using Contract.Models.TaxaJuros.Result;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Services.TaxaJuros;
using System;
using System.Threading.Tasks;

namespace WebApiSoftplan.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RetornarTaxaJurosController : ControllerBase
    {
        private readonly ILogger<RetornarTaxaJurosController> _logger;
        private readonly ITaxaJurosService _taxaJurosService;

        public RetornarTaxaJurosController(ILogger<RetornarTaxaJurosController> logger, ITaxaJurosService taxaJurosService)
        {
            _logger = logger;
            _taxaJurosService = taxaJurosService;
        }

        /// <summary>
        /// Retorna o valor fixo de uma taxade juros
        /// </summary>
        /// <returns>Retorna a tax fixa de juros</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<TaxaJurosResult> GetAsync()
        {
            _logger.LogInformation("Retorno Taxa", "Iniciando");
            try
            {
                TaxaJurosResult result = new()
                {
                    ValorTaxa = _taxaJurosService.RetonarTaxaJuros(),
                };

                _logger.LogInformation("Retorno Taxa", "Processado");
                return result;
            }
            catch (Exception e)
            {
                await Task.Delay(1);
                _logger.LogError("Retorno Taxa", args: string.Format("Erro: ", e.Message));
                throw;
            }
        }
    }
}
