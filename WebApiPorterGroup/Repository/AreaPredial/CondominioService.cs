using Contracts.Request;
using Contracts.Result;
using Entities.AreaPredial;
using Infrastructure.Generic;
using Infrastructure.ObjectsDao.Interfaces;
using Services.AreaPredial.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.AreaPredial
{
    public class CondominioService : ICondominioService
    {
        private readonly ICondominioDAO _condominioDAO;
        private readonly IBlocoDAO _blocoDAO;

        public CondominioService(ICondominioDAO condominioDAO, IBlocoDAO blocoDAO)
        {
            _condominioDAO = condominioDAO;
            _blocoDAO = blocoDAO;
        }

        private void VerificarDados(CondominioRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Nome))
            {
                throw new BusinessException("Nome do condomínio não informado");
            }

            if (string.IsNullOrWhiteSpace(request.TelefoneSindico))
            {
                throw new BusinessException("Telefone do síndico não informado");
            }

            if (string.IsNullOrWhiteSpace(request.EmailSindico))
            {
                throw new BusinessException("E-mail do síndico não informado");
            }
        }

        public async Task<CondominioResult> Adicionar(CondominioRequest request)
        {
            try
            {
                this.VerificarDados(request);

                Condominio condominio = new()
                {
                    EmailSindico = request.EmailSindico,
                    TelefoneSindico = request.TelefoneSindico,
                    Nome = request.Nome
                };

                await _condominioDAO.Add(condominio);

                return new CondominioResult()
                {
                    Id = condominio.Id
                };
            } catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<CondominioResult> Alterar(int id, CondominioRequest request)
        {
            try
            {
                if (id <= 0)
                {
                    throw new BusinessException($"Id informado não possui valor");
                }

                this.VerificarDados(request);

                var condominio = await _condominioDAO.Get(id);

                if (condominio == null)
                {
                    throw new BusinessException($"Condomínio com o id: {id} não encontrado");
                }

                condominio.Nome = request.Nome;
                condominio.TelefoneSindico = request.TelefoneSindico;
                condominio.EmailSindico = request.EmailSindico;

                await _condominioDAO.Update(condominio);

                return new CondominioResult()
                {
                    Id = condominio.Id,
                    Nome = condominio.Nome,
                    EmailSindico = condominio.EmailSindico,
                    TelefoneSindico = condominio.TelefoneSindico
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

                var blocos = await _blocoDAO.BuscarBlocoPorCondominio(id);

                if (blocos is not null)
                {
                    throw new BusinessException($"Existem Blocos de apartamentos ligados a este condominio");
                }

                var condominio = await _condominioDAO.Get(id);

                if (condominio == null)
                {
                    throw new BusinessException($"Condomínio com o id: {id} não encontrado");
                }

                await _condominioDAO.Remove(condominio);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<CondominioResult> RetornarCondominio(string nome)
        {
            try
            {
                var condominio = await _condominioDAO.GetByName(nome);

                if (condominio == null)
                {
                    throw new BusinessException($"Condomínio com o nome: {nome} não encontrado");
                }

                return new CondominioResult()
                {
                    Id = condominio.Id,
                    Nome = condominio.Nome,
                    EmailSindico = condominio.EmailSindico,
                    TelefoneSindico = condominio.TelefoneSindico
                };
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
