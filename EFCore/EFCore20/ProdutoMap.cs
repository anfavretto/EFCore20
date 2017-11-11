using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCore20
{
    public class ProdutoMap : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder.ToTable("Produto");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).ValueGeneratedOnAdd();
            builder.Property(p => p.Nome).HasMaxLength(150).IsRequired();
            builder.Property(p => p.Ativo).IsRequired().HasDefaultValue(false);

            // Owned Type
            builder.OwnsOne(p => p.Preco);

            // One to One
            builder.HasOne(p => p.PrecoVenda).WithOne(pv => pv.Produto).HasForeignKey<PrecoVenda>(v => v.Id);
        }
    }
}
