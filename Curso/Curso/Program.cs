using Curso.Domain;
using Curso.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Linq;

namespace Curso
{
    class Program
    {
        static void Main(string[] args)
        {
            using var db = new Data.ApplicationContext();
            //aplicando as migrations na base de dados
            //db.Database.Migrate();

            //var existe = db.Database.GetPendingMigrations().Any();
            //if (existe)
            //{
            //    //validando se existe migrations pendentes
            //}

            //InserirDados();
            //InserirDadosEmMassa();
            ConsultarDados();
        }

        private static void ConsultarDados()
        {
            using var db = new Data.ApplicationContext();
            //var consultaPorSintaxe = (from c in db.Clientes where c.Id > 0 select c).ToList();
            var consultaPorMetodo = db.Clientes
                .Where(p => p.Id > 0)
                .OrderBy(p=>p.Id>0)
                .ToList();
            foreach(var cliente in consultaPorMetodo)
            {
                Console.WriteLine($"Consultando Cliente: {cliente.Id}");
                //db.Clientes.Find(cliente.Id);
                db.Clientes.FirstOrDefault(p => p.Id == cliente.Id);
            }

            Console.ReadKey();
        }

        private static void InserirDadosEmMassa()
        {
            var produto = new Produto
            {
                Descricao = "Produto Teste",
                CodigoBarras = "12365447856981",
                Valor = 10m,
                TipoProduto = TipoProduto.MercadoriaParaRevenda,
                Ativo = true
            };

            var cliente = new Cliente
            {
                Nome = "Usuario Teste",
                CEP = "88887775",
                Cidade = "Itabaiana",
                Estado = "SP",
                Telefone = "11236541236",
            };

            var listaClientes = new[]
            {
                new Cliente
                {
                Nome = "Usuario Teste 2",
                CEP = "88887775",
                Cidade = "Itabaiana",
                Estado = "SP",
                Telefone = "11236541236",
                },
                new Cliente

            {
                Nome = "Usuario Teste 3",
                CEP = "88887775",
                Cidade = "Itabaiana",
                Estado = "SP",
                Telefone = "11236541237",
                }
            };

            using var db = new Data.ApplicationContext();
            // db.AddRange(produto, cliente);

            db.Set<Cliente>().AddRange(listaClientes);

            //db.AddRange(listaClientes);

            var registros = db.SaveChanges();
            Console.WriteLine($"Total Registro(s): {registros}");
            Console.ReadKey();

        }

        private static void InserirDados()
        {
            var produto = new Produto
            {
                Descricao = "Produto Teste",
                CodigoBarras = "12365447856981",
                Valor = 10m,
                TipoProduto = TipoProduto.MercadoriaParaRevenda,
                Ativo = true
            };

            using var db = new Data.ApplicationContext();
            db.Produtos.Add(produto);
            db.Set<Produto>().Add(produto);
            db.Entry(produto).State = EntityState.Added;
            db.Add(produto);

            var registros = db.SaveChanges();
            Console.WriteLine($"Total Registro(s): {registros}");
            Console.ReadKey();
        }
    }
}
