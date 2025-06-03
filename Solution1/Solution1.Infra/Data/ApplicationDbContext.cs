using Microsoft.EntityFrameworkCore;
using Solution1.Domain.Entities;

namespace Solution1.Infra.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Fornecedor> Fornecedores { get; set; }
    public DbSet<TipoAtivo> TiposAtivo { get; set; }
    public DbSet<Ativo> Ativos { get; set; }
    public DbSet<ContratoVenda> ContratosVenda { get; set; }
    public DbSet<ItemContrato> ItensContrato { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Fornecedor>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Codigo).IsRequired().HasMaxLength(50);
            entity.Property(e => e.Descricao).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Cnpj).IsRequired().HasMaxLength(14);

            entity.HasIndex(e => e.Codigo).IsUnique();
            entity.HasIndex(e => e.Cnpj).IsUnique();
        });

        modelBuilder.Entity<TipoAtivo>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Codigo).IsRequired().HasMaxLength(50);
            entity.Property(e => e.Descricao).IsRequired().HasMaxLength(200);

            entity.HasIndex(e => e.Codigo).IsUnique();
        });

        modelBuilder.Entity<Ativo>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Codigo).IsRequired().HasMaxLength(50);
            entity.Property(e => e.Descricao).IsRequired().HasMaxLength(200);
            entity.Property(e => e.PrecoVenda).HasPrecision(18, 2);

            entity.HasIndex(e => e.Codigo).IsUnique();

            entity.HasOne(e => e.TipoAtivo)
                .WithMany(t => t.Ativos)
                .HasForeignKey(e => e.TipoAtivoId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<ContratoVenda>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.NumeroContrato).IsRequired().HasMaxLength(100);
            entity.Property(e => e.DataCriacao).IsRequired();
            entity.Property(e => e.DataAlteracao).IsRequired();
            entity.Property(e => e.Desconto).HasPrecision(18, 2);
            entity.Property(e => e.ValorTotal).HasPrecision(18, 2);

            entity.HasIndex(e => e.NumeroContrato).IsUnique();

            entity.HasOne(e => e.Fornecedor)
                .WithMany(f => f.Contratos)
                .HasForeignKey(e => e.FornecedorId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<ItemContrato>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Quantidade).IsRequired();
            entity.Property(e => e.PrecoUnitario).HasPrecision(18, 2);
            entity.Property(e => e.ValorTotal).HasPrecision(18, 2);

            entity.HasOne(e => e.ContratoVenda)
                .WithMany(c => c.Itens)
                .HasForeignKey(e => e.ContratoVendaId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(e => e.Ativo)
                .WithMany(a => a.ItensContrato)
                .HasForeignKey(e => e.AtivoId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        base.OnModelCreating(modelBuilder);
    }
}