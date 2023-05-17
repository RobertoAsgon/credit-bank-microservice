using Register.API.Data;
using Register.API.Models;

namespace Register.API.Services
{
    public class ClienteService : IClienteService
    {
        private readonly DbContextClass _dbContext;
        public ClienteService(DbContextClass dbContext)
        {
            _dbContext = dbContext;
        }

        public Cliente GetClienteById(int id)
        {
            return _dbContext.Cliente.Where(x => x.ID == id).FirstOrDefault();
        }
        public Cliente AddCliente(Cliente cliente)
        {
            var result = _dbContext.Cliente.Add(cliente);
            _dbContext.SaveChanges();
            return result.Entity;
        }
        public bool DeleteCliente(int Id)
        {
            var filteredData = _dbContext.Cliente.Where(x => x.ID == Id).FirstOrDefault();
            var result = _dbContext.Remove(filteredData);
            _dbContext.SaveChanges();
            return result != null ? true : false;
        }

    }
}
