using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace VentaXYZ.Modelo;

public partial class DbventaXyzContext : DbContext
{
    public DbventaXyzContext()
    {
    }

    public DbventaXyzContext(DbContextOptions<DbventaXyzContext> options)
        : base(options)
    {
    }

    public virtual DbSet<DetallePedido> DetallePedidos { get; set; }

    public virtual DbSet<Opcion> Opcions { get; set; }

    public virtual DbSet<OpcionRol> OpcionRols { get; set; }

    public virtual DbSet<Pedido> Pedidos { get; set; }

    public virtual DbSet<Producto> Productos { get; set; }

    public virtual DbSet<Rol> Rols { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder){}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DetallePedido>(entity =>
        {
            entity.HasKey(e => e.CodDetalle).HasName("PK__DetalleP__FC181D4585459982");

            entity.ToTable("DetallePedido");

            entity.Property(e => e.CodDetalle).HasColumnName("codDetalle");
            entity.Property(e => e.Cantidad)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("cantidad");
            entity.Property(e => e.CodPedido).HasColumnName("codPedido");
            entity.Property(e => e.CodSku)
                .HasMaxLength(15)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("codSku");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fechaRegistro");
            entity.Property(e => e.Item).HasColumnName("item");
            entity.Property(e => e.MarcaBorrado)
                .HasDefaultValue(false)
                .HasColumnName("marcaBorrado");

            entity.HasOne(d => d.CodPedidoNavigation).WithMany(p => p.DetallePedidos)
                .HasForeignKey(d => d.CodPedido)
                .HasConstraintName("FK__DetallePe__codPe__5441852A");

            entity.HasOne(d => d.CodSkuNavigation).WithMany(p => p.DetallePedidos)
                .HasForeignKey(d => d.CodSku)
                .HasConstraintName("FK__DetallePe__codSk__5535A963");
        });

        modelBuilder.Entity<Opcion>(entity =>
        {
            entity.HasKey(e => e.CodOpcion).HasName("PK__Opcion__78F78DA733A29A9A");

            entity.ToTable("Opcion");

            entity.Property(e => e.CodOpcion).HasColumnName("codOpcion");
            entity.Property(e => e.CodControlador)
                .HasMaxLength(20)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("codControlador");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("descripcion");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fechaRegistro");
            entity.Property(e => e.MarcaBorrado)
                .HasDefaultValue(false)
                .HasColumnName("marcaBorrado");
            entity.Property(e => e.Padre).HasColumnName("padre");
            entity.Property(e => e.Rutapagina)
                .HasMaxLength(50)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("rutapagina");
        });

        modelBuilder.Entity<OpcionRol>(entity =>
        {
            entity.HasKey(e => e.CodOpcionRol).HasName("PK__OpcionRo__80D61FB892B9DAA4");

            entity.ToTable("OpcionRol");

            entity.Property(e => e.CodOpcionRol).HasColumnName("codOpcionRol");
            entity.Property(e => e.CodOpcion).HasColumnName("codOpcion");
            entity.Property(e => e.CodRol).HasColumnName("codRol");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fechaRegistro");
            entity.Property(e => e.MarcaBorrado)
                .HasDefaultValue(false)
                .HasColumnName("marcaBorrado");
            entity.Property(e => e.Tipo).HasColumnName("tipo");

            entity.HasOne(d => d.CodOpcionNavigation).WithMany(p => p.OpcionRols)
                .HasForeignKey(d => d.CodOpcion)
                .HasConstraintName("FK__OpcionRol__codOp__3F466844");

            entity.HasOne(d => d.CodRolNavigation).WithMany(p => p.OpcionRols)
                .HasForeignKey(d => d.CodRol)
                .HasConstraintName("FK__OpcionRol__codRo__403A8C7D");
        });

        modelBuilder.Entity<Pedido>(entity =>
        {
            entity.HasKey(e => e.CodPedido).HasName("PK__Pedido__A0902DBD114CAB4D");

            entity.ToTable("Pedido");

            entity.Property(e => e.CodPedido).HasColumnName("codPedido");
            entity.Property(e => e.CodRepartidor)
                .HasMaxLength(15)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("codRepartidor");
            entity.Property(e => e.CodVendedor)
                .HasMaxLength(15)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("codVendedor");
            entity.Property(e => e.Estado)
                .HasDefaultValue(0)
                .HasColumnName("estado");
            entity.Property(e => e.FechaDespacho)
                .HasColumnType("datetime")
                .HasColumnName("fechaDespacho");
            entity.Property(e => e.FechaEntrega)
                .HasColumnType("datetime")
                .HasColumnName("fechaEntrega");
            entity.Property(e => e.FechaPedido)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fechaPedido");
            entity.Property(e => e.FechaRecepcion)
                .HasColumnType("datetime")
                .HasColumnName("fechaRecepcion");
            entity.Property(e => e.MarcaBorrado)
                .HasDefaultValue(false)
                .HasColumnName("marcaBorrado");

            entity.HasOne(d => d.CodRepartidorNavigation).WithMany(p => p.PedidoCodRepartidorNavigations)
                .HasForeignKey(d => d.CodRepartidor)
                .HasConstraintName("FK__Pedido__codRepar__4F7CD00D");

            entity.HasOne(d => d.CodVendedorNavigation).WithMany(p => p.PedidoCodVendedorNavigations)
                .HasForeignKey(d => d.CodVendedor)
                .HasConstraintName("FK__Pedido__codVende__4E88ABD4");
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.CodSku).HasName("PK__Producto__980301A101E6D1AC");

            entity.ToTable("Producto");

            entity.Property(e => e.CodSku)
                .HasMaxLength(15)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("codSku");
            entity.Property(e => e.Etiqueta)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("etiqueta");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fechaRegistro");
            entity.Property(e => e.MarcaBorrado)
                .HasDefaultValue(false)
                .HasColumnName("marcaBorrado");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.Precio)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("precio");
            entity.Property(e => e.Stock)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("stock");
            entity.Property(e => e.Tipo)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("tipo");
            entity.Property(e => e.UndMedida)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("und_medida");
        });

        modelBuilder.Entity<Rol>(entity =>
        {
            entity.HasKey(e => e.CodRol).HasName("PK__Rol__984C6140E2403359");

            entity.ToTable("Rol");

            entity.Property(e => e.CodRol).HasColumnName("codRol");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("descripcion");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fechaRegistro");
            entity.Property(e => e.MarcaBorrado)
                .HasDefaultValue(false)
                .HasColumnName("marcaBorrado");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.CodUsuario).HasName("PK__Usuario__52198B998A0ECC92");

            entity.ToTable("Usuario");

            entity.Property(e => e.CodUsuario)
                .HasMaxLength(15)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("codUsuario");
            entity.Property(e => e.Clave)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("clave");
            entity.Property(e => e.CodRol).HasColumnName("codRol");
            entity.Property(e => e.Correo)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("correo");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fechaRegistro");
            entity.Property(e => e.MarcaBorrado)
                .HasDefaultValue(false)
                .HasColumnName("marcaBorrado");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.Puesto)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("puesto");
            entity.Property(e => e.Telefono)
                .HasMaxLength(12)
                .IsUnicode(false)
                .HasColumnName("telefono");

            entity.HasOne(d => d.CodRolNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.CodRol)
                .HasConstraintName("FK__Usuario__codRol__44FF419A");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
