using Contract.Models.ShowMeTheCode;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Services.ShowMeTheCode;
using System;
using System.Threading.Tasks;

namespace WebApiSoftplan2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShowMeTheCodeController : Controller
    {
        private readonly ILogger<ShowMeTheCodeController> _logger;
        private readonly IShowMeTheCode _showMeTheCode;

        public ShowMeTheCodeController(ILogger<ShowMeTheCodeController> logger, IShowMeTheCode showMeTheCode)
        {
            _logger = logger;
            _showMeTheCode = showMeTheCode;
        }

        /// <summary>
        /// Path relativo do diretório do projeto
        /// </summary>
        /// <returns>Retorna a string com a url do projeto</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ShowMeTheCodeResult> GetAsync()
        {
            _logger.LogInformation("ShowMeTheCode", "Iniciando");
            try
            {
                ShowMeTheCodeResult result = new()
                {
                    GitHubPath = _showMeTheCode.GetGitPath(),
                };

                _logger.LogInformation("ShowMeTheCode", "Processado");
                return result;
            }
            catch (Exception e)
            {
                await Task.Delay(1);
                _logger.LogError("ShowMeTheCode", args: string.Format("Erro: ", e.Message));
                throw;
            }
        }
    }
}
