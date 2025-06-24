using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Dal.newModels;

public partial class dbcontext : DbContext
{

    public dbcontext()
    {
    }


    public dbcontext(DbContextOptions<dbcontext> options)
        : base(options)
    {
    }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderDetail> OrderDetails { get; set; }

    public virtual DbSet<ProductsSum> ProductsSums { get; set; }

    public virtual DbSet<Table> Tables { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=D:\\c#\\CPC#PROJECT\\data2\\dbProject.mdf;Integrated Security=True;Connect Timeout=30");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustId).HasName("PK__tmp_ms_x__049E3AA9C8D5AF3A");

            entity.Property(e => e.CustId).ValueGeneratedNever();
            entity.Property(e => e.CustAddress).HasMaxLength(50);
            entity.Property(e => e.CustEmail).HasMaxLength(50);
            entity.Property(e => e.CustName).HasMaxLength(50);
            entity.Property(e => e.CustNum).ValueGeneratedOnAdd();
            entity.Property(e => e.CustPhone)
                .HasMaxLength(10)
                .IsFixedLength();
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EmpId).HasName("PK__tmp_ms_x__AF2DBB99A44967FF");

            entity.Property(e => e.EmpId).ValueGeneratedNever();
            entity.Property(e => e.Egmail)
                .HasMaxLength(50)
                .HasColumnName("EGmail");
            entity.Property(e => e.EmpNum).ValueGeneratedOnAdd();
            entity.Property(e => e.Ename)
                .HasMaxLength(20)
                .HasColumnName("EName");
            entity.Property(e => e.Ephone)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("EPhone");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK__Orders__C3905BCFA3EC6A01");

            entity.Property(e => e.OrderDate).HasMaxLength(50);
            entity.Property(e => e.PaymentType)
                .HasMaxLength(50)
                .HasColumnName("paymentType");
            entity.Property(e => e.Pcc)
                .HasMaxLength(50)
                .HasColumnName("pcc");
            entity.Property(e => e.Sent)
                .HasDefaultValue(false)
                .HasColumnName("sent");

            entity.HasOne(d => d.Cust).WithMany(p => p.Orders)
                .HasForeignKey(d => d.CustId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Orders__CustId__2EDAF651");

            entity.HasOne(d => d.Emp).WithMany(p => p.Orders)
                .HasForeignKey(d => d.EmpId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Orders__EmpId__2FCF1A8A");
        });

        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__OrderDet__3214EC0753BEA1DE");

            entity.ToTable("OrderDetail");

            entity.HasOne(d => d.Prod).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.ProdId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__OrderDeta__ProdI__208CD6FA");
        });

        modelBuilder.Entity<ProductsSum>(entity =>
        {
            entity.HasKey(e => e.ProdId).HasName("PK__tmp_ms_x__319F67F1894DDDE3");

            entity.ToTable("ProductsSum");

            entity.Property(e => e.ProdId).HasColumnName("prodId");
            entity.Property(e => e.Pcompany).HasMaxLength(50);
            entity.Property(e => e.Pdescription).HasColumnName("PDescription");
            entity.Property(e => e.Pimporter)
                .HasMaxLength(50)
                .HasColumnName("PImporter");
            entity.Property(e => e.Pname)
                .HasMaxLength(50)
                .HasColumnName("PName");
            entity.Property(e => e.Ppicture)
                .HasMaxLength(50)
                .HasColumnName("PPicture");
            entity.Property(e => e.Psum).HasColumnName("PSum");
        });

        modelBuilder.Entity<Table>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Table__3214EC074619E7C4");

            entity.ToTable("Table");

            entity.HasOne(d => d.Prod).WithMany(p => p.Tables)
                .HasForeignKey(d => d.ProdId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Table__ProdId__1CBC4616");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
