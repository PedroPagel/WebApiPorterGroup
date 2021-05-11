using Contracts.Request;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace TestProject.Integration
{
    [TestClass]
    public class ApartamentoTest : BaseTest
    {
        [TestMethod]
        public async Task Retornar_um_apartamento()
        {
            var response = await _client.GetAsync("api/Apartamento?numero=101&andar=1&condiminio=1&bloco=1");

            Assert.IsTrue(response.IsSuccessStatusCode == true, "Não foi possível buscar a api do retorno de apartamento");
        }

        [TestMethod]
        public async Task Adicionar_um_apartamento()
        {
            ApartamentoRequest request = new()
            {
                IdBloco = 1,
                IdCondominio = 1,
                Andar = 1,
                Numero = 1001
            };

            var response = await _client.PostAsJsonAsync("api/Apartamento", request);

            Assert.IsTrue(response.IsSuccessStatusCode == true, "Não foi possível buscar a api do adição de apartamento");
        }

        [TestMethod]
        public async Task Alterar_um_apartamento()
        {
            ApartamentoRequest request = new()
            {
                IdBloco = 1,
                IdCondominio = 1,
                Andar = 1,
                Numero = 1001
            };

            var response = await _client.PutAsJsonAsync("api/Apartamento?apartamentoId=1", request);

            Assert.IsTrue(response.IsSuccessStatusCode == true, "Não foi possível buscar a api do alteração de apartamento");
        }

        [TestMethod]
        public async Task Remover_um_apartamento()
        {
            var response = await _client.DeleteAsync("api/Apartamento?apartamentoId=5");

            Assert.IsTrue(response.IsSuccessStatusCode == true, "Não foi possível buscar a api do remoção de apartamento");
        }
    }
}
