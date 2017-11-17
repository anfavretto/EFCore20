using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics;
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
                TempoDecorrido(() =>
                {
                    db.Produtos.ToList().ForEach(
                        p => Console.WriteLine(string.Format("Título: {0} R$ {1}", p.Nome, p.Preco.Valor)));
                });
                Console.WriteLine();

                Console.WriteLine("------------ Produtos - Ignorando Global Query Filter ------------");
                db.Produtos.IgnoreQueryFilters().ToList().ForEach(p => Console.WriteLine(string.Format("Título: {0} R$ {1}", p.Nome, p.Preco.Valor)));
                Console.WriteLine("");

                Console.WriteLine("------------ Produtos - Compiled Queries (Primeira vez)------------");
                TempoDecorrido(() =>
                {
                    var p = _buscaProduto(db, "Geladeira");
                    Console.WriteLine(string.Format("Título: {0} R$ {1}", p.Nome, p.Preco.Valor));
                });
                Console.WriteLine("");
                Console.WriteLine("------------ Produtos - Compiled Queries (Segunda vez)------------");
                TempoDecorrido(() =>
                {
                    var p = _buscaProduto(db, "Geladeira");
                    Console.WriteLine(string.Format("Título: {0} R$ {1}", p.Nome, p.Preco.Valor));
                });
                Console.WriteLine("");
                Console.ReadKey();
            }
        }
        private static Func<Context, string, Produto> _buscaProduto =
            EF.CompileQuery<Context, string, Produto>((context, nomeProduto) =>
                context.Produtos.SingleOrDefault(x => x.Nome.Equals(nomeProduto))
            );
        private static void TempoDecorrido(Action acao)
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            acao.Invoke();
            stopWatch.Stop();

            var ts = stopWatch.Elapsed;
            var elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                ts.Hours, ts.Minutes, ts.Seconds,
                ts.Milliseconds / 10
            );
            Console.WriteLine("RunTime " + elapsedTime);
        }
    }
}
