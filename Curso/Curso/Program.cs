using Curso.Domain;
using Curso.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
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
            //ConsultarDados();
            //CadastrarPedido();
            //ConsultarPedidoCarregamentoAdiantado();
            //AtualizarDados();
            RemoverRegistros();
        }

        private static void RemoverRegistros()
        {
            using var db = new Data.ApplicationContext();
            //var cliente = db.Clientes.Find(3);
            var cliente = new Cliente { Id=4};
            //db.Clientes.Remove(cliente);
            //db.Remove(cliente);
            db.Entry(cliente).State = EntityState.Deleted;

            db.SaveChanges();
            Console.ReadKey();
        }
        private static void AtualizarDados()
        {
            using var db = new Data.ApplicationContext();
            //var cliente = db.Clientes.Find(1);

            var cliente = new Cliente
            {
                Id =1
            };

            var clienteDesconectado = new
            {
                Nome = "Cliente desconectado passo 3",
                Telefone = "7851245632"
            };

            //rastrear o objeto
            db.Attach(cliente);
            db.Entry(cliente).CurrentValues.SetValues(clienteDesconectado);

            //db.Clientes.Update(cliente);
            db.SaveChanges();

            Console.ReadKey();
        }
        private static void ConsultarPedidoCarregamentoAdiantado()
        {
            using var db = new Data.ApplicationContext();
            var pedidos = db
                .Pedidos
                .Include(p=>p.Itens)
                .ThenInclude(p=>p.Produto) //o que está dentro de include (itens)
                .ToList();

            Console.WriteLine(pedidos.Count);
        }
        private static void CadastrarPedido()
        {
            using var db = new Data.ApplicationContext();

            var cliente = db.Clientes.FirstOrDefault();
            var produto = db.Produtos.FirstOrDefault();

            var pedido = new Pedido
            {
                ClienteId = cliente.Id,
                IniciadoEm = DateTime.Now,
                FinalizadoEm = DateTime.Now,
                Observacao = "Pedido teste",
                Status = StatusPedido.Anlise,
                TipoFrete = TipoFrete.SemFrete,
                Itens = new List<PedidoItem>
                {
                    new PedidoItem
                    {
                        ProdutoId = produto.Id,
                        Desconto =0,
                        Quantidade = 1,
                        Valor = 10,
                    }
                }    
            };
            db.Pedidos.Add(pedido);
            db.SaveChanges();

            Console.ReadKey();
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
