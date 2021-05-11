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
    public class BlocoController : Controller
    {
        private readonly ILogger<BlocoController> _logger;
        private readonly IBlocoService _condominio;

        public BlocoController(ILogger<BlocoController> logger, IBlocoService condominio)
        {
            _logger = logger;
            _condominio = condominio;
        }

        /// <summary>
        /// Retorna os dados de um bloco com o nome informado
        /// </summary>
        /// <param name="nome"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/Bloco/")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<BlocoResult> Retornar([Required] string nome)
        {
            _logger.LogInformation(this.GetType().Name, "Iniciando");
            try
            {
                return await _condominio.RetornarBloco(nome);
            }
            catch (Exception e)
            {
                _logger.LogError(this.GetType().Name, args: string.Format("Erro: ", e.Message));
                throw;
            }
        }

        /// <summary>
        /// Altera os dados de um bloco
        /// </summary>
        /// <param name="blocoId"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("api/Bloco/")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<BlocoResult> Alterar([Required] int blocoId, [FromBody] BlocoRequest request)
        {
            _logger.LogInformation(this.GetType().Name, "Iniciando");
            try
            {
                return await _condominio.Alterar(blocoId, request);
            }
            catch (Exception e)
            {
                _logger.LogError(this.GetType().Name, args: string.Format("Erro: ", e.Message));
                throw;
            }
        }

        /// <summary>
        /// Adiciona um novo bloco
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/Bloco/")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<BlocoResult> Adicionar([FromBody] BlocoRequest request)
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
        /// Deleta um bloco 
        /// </summary>
        /// <param name="blocoId"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("api/Bloco/")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<string> Remover([Required] int blocoId)
        {
            _logger.LogInformation(this.GetType().Name, "Iniciando");
            try
            {
                await _condominio.Remover(blocoId);
                return $"Bloco de id: {blocoId} foi removido";
            }
            catch (Exception e)
            {
                _logger.LogError(this.GetType().Name, args: string.Format("Erro: ", e.Message));
                throw;
            }
        }
    }
}
