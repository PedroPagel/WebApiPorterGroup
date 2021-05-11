using Contracts.Request;
using Contracts.Result;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Services.Pessoas;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace WebApiPorterGroup.Controllers
{
    public class MoradorController : Controller
    {
        private readonly ILogger<MoradorController> _logger;
        private readonly IMoradorService _morador;

        public MoradorController(ILogger<MoradorController> logger, IMoradorService morador)
        {
            _logger = logger;
            _morador = morador;
        }

        /// <summary>
        /// Retorna os dados de um morador com o nome informado
        /// </summary>
        /// <param name="nome"></param>
        /// <param name="cpf"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/Morador/")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<MoradorResult> Retornar([Required] string nome, [Required] string cpf)
        {
            _logger.LogInformation(this.GetType().Name, "Iniciando");
            try
            {
                return await _morador.RetornarMorador(nome, cpf);
            }
            catch (Exception e)
            {
                _logger.LogError(this.GetType().Name, args: string.Format("Erro: ", e.Message));
                throw;
            }
        }

        /// <summary>
        /// Altera os dados de um morador
        /// </summary>
        /// <param name="moradorId"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("api/Morador/")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<MoradorResult> Alterar([Required] int moradorId, [FromBody] MoradorRequest request)
        {
            _logger.LogInformation(this.GetType().Name, "Iniciando");
            try
            {
                return await _morador.Alterar(moradorId, request);
            }
            catch (Exception e)
            {
                _logger.LogError(this.GetType().Name, args: string.Format("Erro: ", e.Message));
                throw;
            }
        }

        /// <summary>
        /// Adiciona um novo morador
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/Morador/")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<MoradorResult> Adicionar([FromBody] MoradorRequest request)
        {
            _logger.LogInformation(this.GetType().Name, "Iniciando");
            try
            {
                return await _morador.Adicionar(request);
            }
            catch (Exception e)
            {
                _logger.LogError(this.GetType().Name, args: string.Format("Erro: ", e.Message));
                throw;
            }
        }

        /// <summary>
        /// Deleta um morador 
        /// </summary>
        /// <param name="moradorId"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("api/Morador/")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<string> Remover([Required] int moradorId)
        {
            _logger.LogInformation(this.GetType().Name, "Iniciando");
            try
            {
                await _morador.Remover(moradorId);
                return $"Morador de id: {moradorId} foi removido";
            }
            catch (Exception e)
            {
                _logger.LogError(this.GetType().Name, args: string.Format("Erro: ", e.Message));
                throw;
            }
        }
    }
}
