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
    public class BlocoService : IBlocoService
    {
        private readonly IBlocoDAO _blocoDao;
        private readonly IApartamentoDAO _apartamentoDAO;

        public BlocoService(IBlocoDAO blocoDao, IApartamentoDAO apartamentoDAO)
        {
            _blocoDao = blocoDao;
            _apartamentoDAO = apartamentoDAO;
        }

        public async Task<BlocoResult> Adicionar(BlocoRequest request)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(request.Nome))
                {
                    throw new BusinessException("Nome do bloco não informado");
                }

                if (request.IdCondominio <= 0)
                {
                    throw new BusinessException("Id do condominio não informado");
                }

                Bloco bloco = new()
                {
                    Nome = request.Nome,
                    CondominioId = request.IdCondominio
                };

                await _blocoDao.Add(bloco);

                return new BlocoResult()
                {
                    Id = bloco.Id,
                    IdCondominio = bloco.CondominioId,
                    Nome = bloco.Nome
                };
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<BlocoResult> Alterar(int id, BlocoRequest request)
        {
            try
            {
                if (id <= 0)
                {
                    throw new BusinessException($"Id informado não possui valor");
                }

                if (string.IsNullOrWhiteSpace(request.Nome))
                {
                    throw new BusinessException("Nome do bloco não informado");
                }

                if (request.IdCondominio <= 0)
                {
                    throw new BusinessException("Id do condominio não informado");
                }

                var bloco = await _blocoDao.Get(id);

                if (bloco == null)
                {
                    throw new BusinessException($"Bloco com o id: {id} não encontrado");
                }

                bloco.Nome = request.Nome;

                await _blocoDao.Update(bloco);

                return new BlocoResult()
                {
                    Id = bloco.Id,
                    IdCondominio = bloco.CondominioId,
                    Nome = bloco.Nome
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

                var bloco = await _blocoDao.Get(id);

                if (bloco is null)
                {
                    throw new BusinessException($"Bloco com o id: {id} não encontrado");
                }
                
                var apartamento = await _apartamentoDAO.BuscarPorBlocosCondominios(bloco.CondominioId, bloco.Id);

                if (apartamento is not null)
                {
                    throw new BusinessException($"Existem apartamento(s) ligado(s) a este bloco");
                }

                await _blocoDao.Remove(bloco);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<BlocoResult> RetornarBloco(string nome)
        {
            try
            {
                var bloco = await _blocoDao.GetByName(nome);

                if (bloco == null)
                {
                    throw new BusinessException($"Bloco com o nome: {nome} não encontrado");
                }

                return new BlocoResult()
                {
                    Id = bloco.Id,
                    IdCondominio = bloco.CondominioId,
                    Nome = bloco.Nome
                };
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
