namespace EFCore20
{
    public class Produto
    {
        public long Id { get; internal set; }
        public string Nome { get; internal set; }
        public virtual Preco Preco { get; internal set; }
        public virtual PrecoVenda PrecoVenda { get; internal set; }
        public bool Ativo { get; set; }
    }
}
