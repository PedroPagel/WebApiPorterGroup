using Contracts.Request;
using Contracts.Result;
using Entities.AreaPredial;
using Infrastructure.Generic;
using Infrastructure.ObjectsDao.Interfaces;
using Services.AreaPredial.Interfaces;
using System;
using System.Threading.Tasks;

namespace Services.AreaPredial
{
    public class ApartamentoService : IApartamentoService
    {
        private readonly IApartamentoDAO _apartamentoDAO;
        private readonly IMoradorDAO _moradorDAO;

        public ApartamentoService(IApartamentoDAO apartamentoDAO, IMoradorDAO moradorDAO)
        {
            _apartamentoDAO = apartamentoDAO;
            _moradorDAO = moradorDAO;
        }

        public async Task<ApartamentoResult> Adicionar(ApartamentoRequest request)
        {
            try
            {
                if (request.IdCondominio <= 0)
                {
                    throw new BusinessException("Id do condominio não informado");
                }

                if (request.IdBloco <= 0)
                {
                    throw new BusinessException("Id do bloco não informado");
                }

                if (request.Andar <= 0)
                {
                    throw new BusinessException("Andar do apartamento não informado");
                }

                if (request.Numero <= 0)
                {
                    throw new BusinessException("Número do apartamento não informado");
                }

                Apartamento apartamento = new()
                {
                    Andar = request.Andar,
                    Numero = request.Numero,
                    BlocoId = request.IdBloco,
                    CondominioId = request.IdCondominio
                };

                await _apartamentoDAO.Add(apartamento);

                return new ApartamentoResult()
                {
                    Id = apartamento.Id,
                    Andar = apartamento.Andar,
                    Numero = apartamento.Numero,
                    IdBloco = apartamento.BlocoId,
                    IdCondominio = apartamento.CondominioId
                };
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<ApartamentoResult> Alterar(int id, ApartamentoRequest request)
        {
            try
            {
                if (id <= 0)
                {
                    throw new BusinessException($"Id informado não possui valor");
                }

                if (request.Andar <= 0)
                {
                    throw new BusinessException("Andar do apartamento não informado");
                }

                if (request.Numero <= 0)
                {
                    throw new BusinessException("Número do apartamento não informado");
                }

                var apartamento = await _apartamentoDAO.Get(id);

                if (apartamento is null)
                {
                    throw new BusinessException($"Apartamento com o id: {id} não encontrado");
                }

                apartamento.Andar = request.Andar;
                apartamento.Numero = request.Numero;

                await _apartamentoDAO.Update(apartamento);

                return new ApartamentoResult()
                {
                    Id = apartamento.Id,
                    Andar = apartamento.Andar,
                    Numero = apartamento.Numero,
                    IdBloco = apartamento.BlocoId,
                    IdCondominio = apartamento.CondominioId
                };
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task Remover(int id)
        {
            try
            {
                if (id <= 0)
                {
                    throw new BusinessException($"Id informado não possui valor");
                }

                var apartamento = await _apartamentoDAO.Get(id);

                if (apartamento is null)
                {
                    throw new BusinessException($"Apartamento com o id: {id} não encontrado");
                }

                var morador = await _moradorDAO.BuscarMoradorPorApartamento(id);

                if (morador is not null)
                {
                    throw new BusinessException($"Existem morador(es) ligado(s) a este apartamento");
                }

                await _apartamentoDAO.Remove(apartamento);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<ApartamentoResult> RetornarApartamento(int numeroAp, int andar, int condiminio, int bloco)
        {
            try
            {
                if (condiminio <= 0)
                {
                    throw new BusinessException($"Id condominio informado não possui valor");
                }

                if (bloco <= 0)
                {
                    throw new BusinessException($"Id bloco informado não possui valor");
                }

                if (andar <= 0)
                {
                    throw new BusinessException("Andar do apartamento não informado");
                }

                if (numeroAp <= 0)
                {
                    throw new BusinessException("Número do apartamento não informado");
                }

                var apartamento = await _apartamentoDAO.BuscarApartamentoPorCondominio(numeroAp, andar, condiminio, bloco);

                if (apartamento is null)
                {
                    throw new BusinessException("Apartamento não encontrado");
                }

                return new ApartamentoResult()
                {
                    Id = apartamento.Id,
                    Andar = apartamento.Andar,
                    Numero = apartamento.Numero,
                    IdBloco = apartamento.BlocoId,
                    IdCondominio = apartamento.CondominioId
                };
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
