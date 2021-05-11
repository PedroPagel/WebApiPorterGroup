using Contracts.Request;
using Contracts.Result;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Services.AreaPredial.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace WebApiPorterGroup.Controllers
{
    public class CondominioController : Controller
    {
        private readonly ILogger<CondominioController> _logger;
        private readonly ICondominioService _condominio;

        public CondominioController(ILogger<CondominioController> logger, ICondominioService condominio)
        {
            _logger = logger;
            _condominio = condominio;
        }

        /// <summary>
        /// Retorna os dados de um condiminio com o nome informado
        /// </summary>
        /// <param name="nome"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/Condominio/")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<CondominioResult> Retornar([Required] string nome)
        {
            _logger.LogInformation(this.GetType().Name, "Iniciando");
            try
            {
                return await _condominio.RetornarCondominio(nome);
            }
            catch (Exception e)
            {
                _logger.LogError(this.GetType().Name, args: string.Format("Erro: ", e.Message));
                throw;
            }
        }

        /// <summary>
        /// Altera os dados de um condominio
        /// </summary>
        /// <param name="condominioId"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("api/Condominio/")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<CondominioResult> Alterar([Required] int condominioId, [FromBody] CondominioRequest request)
        {
            _logger.LogInformation(this.GetType().Name, "Iniciando");
            try
            {
                return await _condominio.Alterar(condominioId, request);
            }
            catch (Exception e)
            {
                _logger.LogError(this.GetType().Name, args: string.Format("Erro: ", e.Message));
                throw;
            }
        }

        /// <summary>
        /// Adiciona um novo condiminio
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/Condominio/")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<CondominioResult> Adicionar([FromBody] CondominioRequest request)
        {
            _logger.LogInformation(this.GetType().Name, "Iniciando");
            try
            {
                return await _condominio.Adicionar(request);
            }
            catch (Exception e)
            {
                _logger.LogError(this.GetType().Name, args: string.Format("Erro: ", e.Message));
                throw;
            }
        }

        /// <summary>
        /// Deleta um condominio 
        /// </summary>
        /// <param name="condominioId"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("api/Condominio/")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<string> Remover([Required] int condominioId)
        {
            _logger.LogInformation(this.GetType().Name, "Iniciando");
            try
            {
                await _condominio.Remover(condominioId);
                return $"Condominio de id: {condominioId} foi removido";
            }
            catch (Exception e)
            {
                _logger.LogError(this.GetType().Name, args: string.Format("Erro: ", e.Message));
                throw;
            }
        }
    }
}
