
using Microsoft.EntityFrameworkCore;
using WebAPI_DotNet.Models;

namespace WebAPI_DotNet.Service.FuncionarioService
{
    public class FuncionarioService : IFuncionarioInterface
    {
        private readonly ApplicationDbContext _context;
        public FuncionarioService(ApplicationDbContext context) {
            _context = context;
        }

        public async Task<ServiceResponse<List<FuncionarioModel>>> GetFuncionarios()
        {
            ServiceResponse<List<FuncionarioModel>> serviceResponse = new ServiceResponse<List<FuncionarioModel>>();

            try{
                serviceResponse.Dados = _context.Funcionarios.ToList();

                if(serviceResponse.Dados.Count == 0)
                {
                    serviceResponse.Mensagem = "Nenhum dado encontrado";
                }
                else
                {
                    serviceResponse.Mensagem = "Dados encontrados";
                }

                await _context.SaveChangesAsync();
            }

            catch (Exception ex) {
                serviceResponse.Mensagem = ex.Message;
                serviceResponse.Sucesso = false;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<FuncionarioModel>> GetFuncionarioById(int id)
        {
            ServiceResponse<FuncionarioModel> serviceResponse = new ServiceResponse<FuncionarioModel>();

            try
            {
                FuncionarioModel funcionario = _context.Funcionarios.AsNoTracking().FirstOrDefault(x => x.Id == id);
                int funcionarioID = funcionario.Id;

                if(funcionario == null)
                {
                    serviceResponse.Dados = null;
                    serviceResponse.Mensagem = "Funcionário não encontrado";
                    serviceResponse.Sucesso = false;

                } else if(funcionario.Id == id)
                {
                    serviceResponse.Mensagem = $"Funcionário [{funcionarioID}] encontrado!";
                    serviceResponse.Sucesso = true;
                }

                serviceResponse.Dados = funcionario;
                await _context.SaveChangesAsync();
            }

            catch (Exception ex)
            {
                serviceResponse.Mensagem = ex.Message;
                serviceResponse.Sucesso = false;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<FuncionarioModel>>> CreateFuncionario(FuncionarioModel novoFuncionario)
        {
            ServiceResponse<List<FuncionarioModel>> serviceResponse = new ServiceResponse<List<FuncionarioModel>>();
            try
            {
                if(novoFuncionario == null)
                {
                    serviceResponse.Mensagem = "Informe os dados do Funcionário!";
                    serviceResponse.Sucesso = false;
                } else
                {
                    serviceResponse.Mensagem = $"Funcionário [{novoFuncionario.Nome}] criado!";
                    serviceResponse.Sucesso = true;
                }

                novoFuncionario.DataDeCriacao = DateTime.Now.ToLocalTime();
                novoFuncionario.DataDeAlteracao = DateTime.Now.ToLocalTime();
                _context.Add(novoFuncionario);
                await _context.SaveChangesAsync();

                serviceResponse.Dados = _context.Funcionarios.ToList();
            }
            catch (Exception ex)
            {
                serviceResponse.Mensagem = ex.Message;
                serviceResponse.Sucesso = false;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<FuncionarioModel>>> UpdateFuncionario(FuncionarioModel editadoFuncionario)
        {
            ServiceResponse<List<FuncionarioModel>> serviceResponse = new ServiceResponse<List<FuncionarioModel>>();

            FuncionarioModel funcionarioEditado = _context.Funcionarios.AsNoTracking().FirstOrDefault(x => x.Id == editadoFuncionario.Id);

            try
            {
                if(funcionarioEditado == null)
                {
                    serviceResponse.Dados = null;
                    serviceResponse.Mensagem = $"Funcionário [{editadoFuncionario.Id} não localizado!]";
                    serviceResponse.Sucesso = false;
                }
                else
                {
                    serviceResponse.Mensagem = $"Funcionário [{funcionarioEditado.Id}] editado!";
                    serviceResponse.Sucesso = true;
                }

                funcionarioEditado.DataDeAlteracao = DateTime.Now.ToLocalTime();
                _context.Funcionarios.Update(editadoFuncionario);
                await _context.SaveChangesAsync();

                serviceResponse.Dados = _context.Funcionarios.ToList();
            }

            catch (Exception ex)
            {
                serviceResponse.Mensagem = ex.Message;
                serviceResponse.Sucesso = false;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<FuncionarioModel>>> DeleteFuncionario(int id)
        {
            ServiceResponse<List<FuncionarioModel>> serviceResponse = new ServiceResponse<List<FuncionarioModel>>();
            FuncionarioModel funcionarioDeletar = _context.Funcionarios.AsNoTracking().FirstOrDefault(x => x.Id == id);

            int funcionarioDeletarID = funcionarioDeletar.Id;

            try {
                if(funcionarioDeletar == null) {
                    serviceResponse.Dados = null;
                    serviceResponse.Mensagem = $"Funcionário [{funcionarioDeletarID}] não localizado!";
                    serviceResponse.Sucesso = false;
                }
                else
                {
                    serviceResponse.Mensagem = $"Funcionário [{funcionarioDeletarID}] deletado!";
                    serviceResponse.Sucesso = true;
                }

                _context.Funcionarios.Remove(funcionarioDeletar);
                await _context.SaveChangesAsync();

                serviceResponse.Dados = _context.Funcionarios.ToList();
            }

            catch(Exception ex)
            {
                serviceResponse.Mensagem = ex.Message;
                serviceResponse.Sucesso = false;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<FuncionarioModel>>> InativaFuncionario(int id)
        {
            ServiceResponse<List<FuncionarioModel>> serviceResponse = new ServiceResponse<List<FuncionarioModel>>();
            FuncionarioModel funcionarioInativar = _context.Funcionarios.AsNoTracking().FirstOrDefault(x => x.Id == id);

            int funcionarioInativarID = funcionarioInativar.Id;

            try
            {
                if(funcionarioInativar == null)
                {
                    serviceResponse.Dados = null;
                    serviceResponse.Mensagem = $"Funcionário [{funcionarioInativarID}] não localizado!";
                    serviceResponse.Sucesso = false;
                }
                else
                {
                    serviceResponse.Mensagem = $"Funcionário [{funcionarioInativarID}] inativado!";
                    serviceResponse.Sucesso = true;
                }

                funcionarioInativar.Status = false;
                funcionarioInativar.DataDeAlteracao = DateTime.Now.ToLocalTime();
                _context.Funcionarios.Update(funcionarioInativar);
                await _context.SaveChangesAsync();

                serviceResponse.Dados = _context.Funcionarios.ToList();
            }

            catch(Exception ex)
            {
                serviceResponse.Mensagem = ex.Message;
                serviceResponse.Sucesso = false;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<FuncionarioModel>>> AtivaFuncionario(int id)
        {
            ServiceResponse<List<FuncionarioModel>> serviceResponse = new ServiceResponse<List<FuncionarioModel>>();
            FuncionarioModel funcionarioAtivar = _context.Funcionarios.AsNoTracking().FirstOrDefault(x => x.Id == id);

            int funcionarioAtivarID = funcionarioAtivar.Id;

            try
            {
                if(funcionarioAtivar == null)
                {
                    serviceResponse.Dados = null;
                    serviceResponse.Mensagem = $"Funcionário [{funcionarioAtivarID}] não localizado!";
                    serviceResponse.Sucesso = false;
                }
                else
                {
                    serviceResponse.Mensagem = $"Funcionário [{funcionarioAtivarID}] reativado!";
                    serviceResponse.Sucesso = true;
                }

                funcionarioAtivar.Status = true;
                funcionarioAtivar.DataDeAlteracao = DateTime.Now.ToLocalTime();

                _context.Funcionarios.Update(funcionarioAtivar);
                await _context.SaveChangesAsync();

                serviceResponse.Dados = _context.Funcionarios.ToList();
            }

            catch (Exception ex)
            {
                serviceResponse.Mensagem = ex.Message;
                serviceResponse.Sucesso = false;
            }

            return serviceResponse;
        }
    }
}
