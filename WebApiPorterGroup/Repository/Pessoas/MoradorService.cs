using Contracts.Request;
using Contracts.Result;
using Entities.Pessoa;
using Infrastructure.Generic;
using Infrastructure.ObjectsDao.Interfaces;
using System;
using System.Threading.Tasks;

namespace Services.Pessoas
{
    public class MoradorService : IMoradorService
    {
        private readonly IMoradorDAO _moradorDAO;

        public MoradorService(IMoradorDAO moradorDAO)
        {
            _moradorDAO = moradorDAO;
        }

        private void VerificarDadosMorador(MoradorRequest request)
        {
            if (request.IdApartamento <= 0)
            {
                throw new BusinessException("Id do morador não informado");
            }

            if (string.IsNullOrWhiteSpace(request.Nome))
            {
                throw new BusinessException("Nome não informado");
            }

            if (string.IsNullOrWhiteSpace(request.Email))
            {
                throw new BusinessException("Email não informado");
            }

            if (string.IsNullOrWhiteSpace(request.Telefone))
            {
                throw new BusinessException("Telefone não informado");
            }

            if (string.IsNullOrWhiteSpace(request.Cpf))
            {
                throw new BusinessException("Cpf não informado");
            }

            if (request.DataNascimento == DateTime.MinValue)
            {
                throw new BusinessException("Data invalida");
            }
        }

        public async Task<MoradorResult> Adicionar(MoradorRequest request)
        {
            try
            {
                VerificarDadosMorador(request);

                Morador morador = new()
                {
                    Cpf = request.Cpf,
                    Email = request.Email,
                    DataNascimento = request.DataNascimento,
                    Telefone = request.Telefone,
                    Nome = request.Nome,
                    ApartamentoId = request.IdApartamento
                };

                await _moradorDAO.Add(morador);

                return new MoradorResult()
                {
                    Cpf = morador.Cpf,
                    Email = morador.Email,
                    DataNascimento = morador.DataNascimento,
                    Telefone = morador.Telefone,
                    Nome = morador.Nome,
                    IdApartamento = morador.ApartamentoId
                };
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<MoradorResult> Alterar(int id, MoradorRequest request)
        {
            try
            {
                if (id <= 0)
                {
                    throw new BusinessException($"Id informado não possui valor");
                }

                VerificarDadosMorador(request);

                var morador = await _moradorDAO.Get(id);

                if (morador is null)
                {
                    throw new BusinessException($"Morador com o id: {id} não encontrado");
                }

                morador.Cpf = request.Cpf;
                morador.DataNascimento = request.DataNascimento;
                morador.Email = request.Email;
                morador.Telefone = request.Telefone;
                morador.Nome = request.Nome;

                await _moradorDAO.Update(morador);

                return new MoradorResult()
                {
                    Cpf = morador.Cpf,
                    Email = morador.Email,
                    DataNascimento = morador.DataNascimento,
                    Telefone = morador.Telefone,
                    Nome = morador.Nome,
                    IdApartamento = morador.ApartamentoId
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

                var morador = await _moradorDAO.Get(id);

                if (morador is null)
                {
                    throw new BusinessException($"Apartamento com o id: {id} não encontrado");
                }

                await _moradorDAO.Remove(morador);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<MoradorResult> RetornarMorador(string nome, string cpf)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(nome))
                {
                    throw new BusinessException($"Nome informado não possui valor ou esta vazio");
                }

                if (string.IsNullOrWhiteSpace(cpf))
                {
                    throw new BusinessException($"Cpf não possui valor ou esta vazio");
                }

                var morador = await _moradorDAO.BuscarMorador(nome, cpf);

                if (morador is null)
                {
                    throw new BusinessException("Apartamento não encontrado");
                }

                return new MoradorResult()
                {
                    Cpf = morador.Cpf,
                    Email = morador.Email,
                    DataNascimento = morador.DataNascimento,
                    Telefone = morador.Telefone,
                    Nome = morador.Nome,
                    IdApartamento = morador.ApartamentoId
                };
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
