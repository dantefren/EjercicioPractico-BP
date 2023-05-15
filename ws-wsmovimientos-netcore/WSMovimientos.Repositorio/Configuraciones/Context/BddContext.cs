#region Using

using Microsoft.EntityFrameworkCore;
using WSMovimientos.Entidades.Modelo;

#endregion Using

namespace WSMovimientos.Repositorio.Configuraciones.Context
{
    public partial class BddContext : DbContext
    {
        public BddContext()
        {
        }

        public BddContext(DbContextOptions<BddContext> options)
            : base(options)
        {
        }

        public virtual DbSet<BmCliente> BmClientes { get; set; } = null!;
        public virtual DbSet<BmCuentum> BmCuenta { get; set; } = null!;
        public virtual DbSet<BmMovimiento> BmMovimientos { get; set; } = null!;
        public virtual DbSet<BmPersona> BmPersonas { get; set; } = null!;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Entidades.EConstantes.ConeccionBdd);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BmCliente>(entity =>
            {
                entity.HasKey(e => e.IdCliente)
                    .HasName("PK__BM_CLIEN__23A341302628D4CC");

                entity.ToTable("BM_CLIENTE");

                entity.Property(e => e.IdCliente).HasColumnName("ID_CLIENTE");

                entity.Property(e => e.Contrasenia)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("CONTRASENIA");

                entity.Property(e => e.Estado)
                    .IsRequired()
                    .HasColumnName("ESTADO")
                    .HasDefaultValueSql("('0')");

                entity.Property(e => e.IdPersona).HasColumnName("ID_PERSONA");

                entity.HasOne(d => d.IdPersonaNavigation)
                    .WithMany(p => p.BmClientes)
                    .HasForeignKey(d => d.IdPersona)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BM_CLIENTE_BM_PERSONA");
            });

            modelBuilder.Entity<BmCuentum>(entity =>
            {
                entity.HasKey(e => e.IdCuenta)
                    .HasName("PK__BM_CUENT__ADEAD61A8242302E");

                entity.ToTable("BM_CUENTA");

                entity.Property(e => e.IdCuenta).HasColumnName("ID_CUENTA");

                entity.Property(e => e.Estado)
                    .IsRequired()
                    .HasColumnName("ESTADO")
                    .HasDefaultValueSql("('1')");

                entity.Property(e => e.IdPersona).HasColumnName("ID_PERSONA");

                entity.Property(e => e.NumeroCuenta).HasColumnName("NUMERO_CUENTA");

                entity.Property(e => e.SaldoInicial)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("SALDO_INICIAL")
                    .HasDefaultValueSql("('0')");

                entity.Property(e => e.TipoCuenta)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("TIPO_CUENTA");

                entity.HasOne(d => d.IdPersonaNavigation)
                    .WithMany(p => p.BmCuenta)
                    .HasForeignKey(d => d.IdPersona)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BM_CUENTA_BM_PERSONA");
            });

            modelBuilder.Entity<BmMovimiento>(entity =>
            {
                entity.HasKey(e => e.IdMovimientos)
                    .HasName("PK__BM_MOVIM__CEB714B3CE4B5901");

                entity.ToTable("BM_MOVIMIENTOS");

                entity.Property(e => e.IdMovimientos).HasColumnName("ID_MOVIMIENTOS");

                entity.Property(e => e.Fecha)
                    .HasColumnType("datetime")
                    .HasColumnName("FECHA")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IdCuenta).HasColumnName("ID_CUENTA");

                entity.Property(e => e.Saldo)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("SALDO");

                entity.Property(e => e.Tipo)
                    .HasMaxLength(3)
                    .HasColumnName("TIPO");

                entity.Property(e => e.Valor)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("VALOR");

                entity.HasOne(d => d.IdCuentaNavigation)
                    .WithMany(p => p.BmMovimientos)
                    .HasForeignKey(d => d.IdCuenta)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BM_MOVIMIENTOS_BM_CUENTA");
            });

            modelBuilder.Entity<BmPersona>(entity =>
            {
                entity.HasKey(e => e.IdPersona)
                    .HasName("PK__BM_PERSO__782441495FA6ABB8");

                entity.ToTable("BM_PERSONA");

                entity.Property(e => e.IdPersona).HasColumnName("ID_PERSONA");

                entity.Property(e => e.Direccion)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("DIRECCION");

                entity.Property(e => e.Edad).HasColumnName("EDAD");

                entity.Property(e => e.Genero)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("GENERO")
                    .IsFixedLength();

                entity.Property(e => e.Identificacion)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("IDENTIFICACION");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("NOMBRE");

                entity.Property(e => e.Telefono).HasColumnName("TELEFONO");
            });

            OnModelCreatingPartial(modelBuilder);
        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}