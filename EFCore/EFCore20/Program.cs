using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace EFCore20
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new Context())
            {
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();

                db.Produtos.Add(new Produto
                {
                    Nome = "Geladeira",
                    Preco = new Preco() { Valor = 1000.99 },
                    Ativo = true,
                    PrecoVenda = new PrecoVenda()
                    {
                        Valor = 1500.00
                    }
                });
                db.Produtos.Add(new Produto { Nome = "Tablet", Preco = new Preco() { Valor = 299.99 }, Ativo = true });
                db.Produtos.Add(new Produto { Nome = "Televisão", Preco = new Preco() { Valor = 1000.00 }, Ativo = false });

                db.SaveChanges();
                // GLOBAL QUERY FILTERS
                Console.WriteLine("------------ Produtos ------------");
                db.Produtos.ToList().ForEach(p => Console.WriteLine(string.Format("Título: {0} R$ {1}", p.Nome, p.Preco.Valor)));
                System.Console.WriteLine();
                Console.WriteLine("------------ Produtos - Ignorando Global Query Filter ------------");
                db.Produtos.IgnoreQueryFilters().ToList().ForEach(p => Console.WriteLine(string.Format("Título: {0} R$ {1}", p.Nome, p.Preco.Valor)));
                Console.ReadKey();
            }
        }
    }
}
