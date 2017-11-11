
namespace EFCore20
{
    public class Categoria
    {
        public long Id { get; set; }
        public string Nome { get; set; }
        public virtual Produto Produto { get; set; }
    }
}
