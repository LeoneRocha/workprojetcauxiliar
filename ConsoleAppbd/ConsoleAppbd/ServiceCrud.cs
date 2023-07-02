using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppbd
{
    public class ServiceCrud
    {
        string _connectionString = "Data Source=localhost,1439;Initial Catalog=ExemploCRUD;Integrated Security=True;MultipleActiveResultSets=True;";
        IRepository<Cliente> _clienteRepository;
        public ServiceCrud(string connectionString = null)
        {
            this._connectionString = string.IsNullOrEmpty(connectionString) ? this._connectionString : connectionString;
            this._clienteRepository = new Repository<Cliente>(this._connectionString);
        }
        public void Update(Cliente cliente)
        {
            // Atualizando um cliente existente
            Cliente clienteParaAtualizar = _clienteRepository.GetById(cliente.Id);
            if (clienteParaAtualizar != null)
            {
                clienteParaAtualizar.Nome = cliente.Nome;
                _clienteRepository.Update(clienteParaAtualizar);
                Console.WriteLine("Cliente atualizado com sucesso!");
            }
            else
            {
                Console.WriteLine("Cliente não encontrado.");
            }
        }
        public Cliente Insert(Cliente novoCliente)
        {
            // Inserindo um novo cliente 
            Console.WriteLine("Novo cliente inserido com sucesso!");
            return _clienteRepository.Insert(novoCliente);
        }
        public Cliente GetById(int clienteId)
        {
            // Obtendo um cliente pelo ID 
            Cliente clienteById = _clienteRepository.GetById(clienteId);
            if (clienteById != null)
            {
                Console.WriteLine($"Cliente pelo ID: {clienteById.Id}, Nome: {clienteById.Nome}, Email: {clienteById.Email}");
            }
            else
            {
                Console.WriteLine("Cliente não encontrado.");
            }
            return clienteById;

        }
        public List<Cliente> GetAll()
        {
            IRepository<Cliente> clienteRepository = new Repository<Cliente>(this._connectionString);

            // Obtendo todos os clientes
            List<Cliente> clientes = clienteRepository.GetAll();
            foreach (Cliente cliente in clientes)
            {
                Console.WriteLine($"ID: {cliente.Id}, Nome: {cliente.Nome}, Email: {cliente.Email}");
            }

            return clientes;
        }

        public void Delete(int clienteParaDeletarId)
        {
            // Deletando um cliente 
            _clienteRepository.Delete(clienteParaDeletarId);
            Console.WriteLine("Cliente deletado com sucesso!");
        }
    }
}
