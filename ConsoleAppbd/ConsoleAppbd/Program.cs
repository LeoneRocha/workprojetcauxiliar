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

            Cliente cliins = inserir(sc);

            List<Cliente> clis = getClis(sc);
            Cliente cliget = getCli(sc, cliins.Id);

            atualizar(sc, cliget);

            deletar(sc, cliins.Id);

            Console.ReadKey();
        }

        private static void deletar(ServiceCrud sc, int id)
        {
            sc.Delete(id);
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

        private static Cliente inserir(ServiceCrud sc)
        {
            return sc.Insert(new Cliente() { Nome = "Fulano", Email = "fulano.silva@hotmail.com" });
        }
    }
}
