namespace EFCore20
{
    public class PrecoVenda
    {
        public long Id { get; set; }
        public double Valor { get; set; }
        public Produto Produto { get; set; }
    }
}
