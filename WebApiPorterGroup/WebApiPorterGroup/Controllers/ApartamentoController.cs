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
    public class ApartamentoController : Controller
    {
        private readonly ILogger<ApartamentoController> _logger;
        private readonly IApartamentoService _apartamento;

        public ApartamentoController(ILogger<ApartamentoController> logger, IApartamentoService apartamento)
        {
            _logger = logger;
            _apartamento = apartamento;
        }

        /// <summary>
        /// Retorna os dados de um apartamento com o nome informado
        /// </summary>
        /// <param name="numero"></param>
        /// <param name="andar"></param>
        /// <param name="condiminio"></param>
        /// <param name="bloco"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/Apartamento/")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ApartamentoResult> Retornar([Required] int numero, [Required] int andar, [Required] int condiminio, [Required] int bloco)
        {
            _logger.LogInformation(this.GetType().Name, "Iniciando");
            try
            {
                return await _apartamento.RetornarApartamento(numero, andar, condiminio, bloco);
            }
            catch (Exception e)
            {
                _logger.LogError(this.GetType().Name, args: string.Format("Erro: ", e.Message));
                throw;
            }
        }

        /// <summary>
        /// Altera os dados de um apartamento
        /// </summary>
        /// <param name="apartamentoId"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("api/Apartamento/")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ApartamentoResult> Alterar([Required] int apartamentoId, [FromBody] ApartamentoRequest request)
        {
            _logger.LogInformation(this.GetType().Name, "Iniciando");
            try
            {
                return await _apartamento.Alterar(apartamentoId, request);
            }
            catch (Exception e)
            {
                _logger.LogError(this.GetType().Name, args: string.Format("Erro: ", e.Message));
                throw;
            }
        }

        /// <summary>
        /// Adiciona um novo apartamento
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/Apartamento/")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ApartamentoResult> Adicionar([FromBody] ApartamentoRequest request)
        {
            _logger.LogInformation(this.GetType().Name, "Iniciando");
            try
            {
                return await _apartamento.Adicionar(request);
            }
            catch (Exception e)
            {
                _logger.LogError(this.GetType().Name, args: string.Format("Erro: ", e.Message));
                throw;
            }
        }

        /// <summary>
        /// Deleta um apartamento 
        /// </summary>
        /// <param name="apartamentoId"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("api/Apartamento/")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<string> Remover([Required] int apartamentoId)
        {
            _logger.LogInformation(this.GetType().Name, "Iniciando");
            try
            {
                await _apartamento.Remover(apartamentoId);
                return $"Apartamento de id: {apartamentoId} foi removido";
            }
            catch (Exception e)
            {
                _logger.LogError(this.GetType().Name, args: string.Format("Erro: ", e.Message));
                throw;
            }
        }
    }
}
