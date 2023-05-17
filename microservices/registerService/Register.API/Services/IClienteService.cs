using Register.API.Models;

namespace Register.API.Services
{
    public interface IClienteService
    {
        public Cliente GetClienteById(int id);
        public Cliente AddCliente(Cliente cliente);
        public bool DeleteCliente(int Id);
    }
}
