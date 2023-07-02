using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppbd
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ServiceCrud sc = new ServiceCrud(null);

            //inserir(sc);

            List<Cliente> clis = getClis(sc);
            Cliente cli = getCli(sc, 1);

            atualizar(sc, cli);

            Console.ReadKey();
        }

        private static List<Cliente> getClis(ServiceCrud sc)
        {
            return sc.GetAll();
        }

        private static void atualizar(ServiceCrud sc, Cliente cli)
        {
            cli.Nome = "Paulo Antonio Bispo";
            cli.Email = "pauloab@hotmail.com";
            sc.Update(cli);
        } 
        private static Cliente getCli(ServiceCrud sc, int id)
        {
            return sc.GetById(id);
        }

        private static void inserir(ServiceCrud sc)
        {
            sc.Insert(new Cliente() { Nome = "Fulano", Email = "fulano.silva@hotmail.com" });
        }
    }
}
