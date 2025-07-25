﻿// <auto-generated />
using System;
using Dal.newModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Dal.Migrations
{
    [DbContext(typeof(dbcontext))]
    [Migration("20250608103502_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Dal.newModels.Customer", b =>
                {
                    b.Property<int>("CustId")
                        .HasColumnType("integer");

                    b.Property<string>("CustAddress")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("CustEmail")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("CustName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<int>("CustNum")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("CustNum"));

                    b.Property<string>("CustPhone")
                        .HasMaxLength(10)
                        .HasColumnType("character(10)")
                        .IsFixedLength();

                    b.HasKey("CustId")
                        .HasName("PK__tmp_ms_x__049E3AA9C8D5AF3A");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("Dal.newModels.Employee", b =>
                {
                    b.Property<int>("EmpId")
                        .HasColumnType("integer");

                    b.Property<string>("Egmail")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("EGmail");

                    b.Property<int>("EmpNum")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("EmpNum"));

                    b.Property<string>("Ename")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)")
                        .HasColumnName("EName");

                    b.Property<string>("Ephone")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("character(10)")
                        .HasColumnName("EPhone")
                        .IsFixedLength();

                    b.HasKey("EmpId")
                        .HasName("PK__tmp_ms_x__AF2DBB99A44967FF");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("Dal.newModels.Order", b =>
                {
                    b.Property<int>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("OrderId"));

                    b.Property<int>("CustId")
                        .HasColumnType("integer");

                    b.Property<int?>("EmpId")
                        .HasColumnType("integer");

                    b.Property<string>("OrderDate")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("PaymentType")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("paymentType");

                    b.Property<string>("Pcc")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("pcc");

                    b.Property<bool?>("Sent")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(false)
                        .HasColumnName("sent");

                    b.HasKey("OrderId")
                        .HasName("PK__Orders__C3905BCFA3EC6A01");

                    b.HasIndex("CustId");

                    b.HasIndex("EmpId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("Dal.newModels.OrderDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int?>("Cost")
                        .HasColumnType("integer");

                    b.Property<int>("Count")
                        .HasColumnType("integer");

                    b.Property<int>("OrderId")
                        .HasColumnType("integer");

                    b.Property<int>("ProdId")
                        .HasColumnType("integer");

                    b.HasKey("Id")
                        .HasName("PK__OrderDet__3214EC0753BEA1DE");

                    b.HasIndex("ProdId");

                    b.ToTable("OrderDetail", (string)null);
                });

            modelBuilder.Entity("Dal.newModels.ProductsSum", b =>
                {
                    b.Property<int>("ProdId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("prodId");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ProdId"));

                    b.Property<string>("Pcompany")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("Pdescription")
                        .HasColumnType("text")
                        .HasColumnName("PDescription");

                    b.Property<string>("Pimporter")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("PImporter");

                    b.Property<string>("Pname")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("PName");

                    b.Property<string>("Ppicture")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("PPicture");

                    b.Property<int>("Psum")
                        .HasColumnType("integer")
                        .HasColumnName("PSum");

                    b.HasKey("ProdId")
                        .HasName("PK__tmp_ms_x__319F67F1894DDDE3");

                    b.ToTable("ProductsSum", (string)null);
                });

            modelBuilder.Entity("Dal.newModels.Table", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int?>("Cost")
                        .HasColumnType("integer");

                    b.Property<int>("Count")
                        .HasColumnType("integer");

                    b.Property<int>("OrderId")
                        .HasColumnType("integer");

                    b.Property<int>("ProdId")
                        .HasColumnType("integer");

                    b.HasKey("Id")
                        .HasName("PK__Table__3214EC074619E7C4");

                    b.HasIndex("ProdId");

                    b.ToTable("Table", (string)null);
                });

            modelBuilder.Entity("Dal.newModels.Order", b =>
                {
                    b.HasOne("Dal.newModels.Customer", "Cust")
                        .WithMany("Orders")
                        .HasForeignKey("CustId")
                        .IsRequired()
                        .HasConstraintName("FK__Orders__CustId__2EDAF651");

                    b.HasOne("Dal.newModels.Employee", "Emp")
                        .WithMany("Orders")
                        .HasForeignKey("EmpId")
                        .HasConstraintName("FK__Orders__EmpId__2FCF1A8A");

                    b.Navigation("Cust");

                    b.Navigation("Emp");
                });

            modelBuilder.Entity("Dal.newModels.OrderDetail", b =>
                {
                    b.HasOne("Dal.newModels.ProductsSum", "Prod")
                        .WithMany("OrderDetails")
                        .HasForeignKey("ProdId")
                        .IsRequired()
                        .HasConstraintName("FK__OrderDeta__ProdI__208CD6FA");

                    b.Navigation("Prod");
                });

            modelBuilder.Entity("Dal.newModels.Table", b =>
                {
                    b.HasOne("Dal.newModels.ProductsSum", "Prod")
                        .WithMany("Tables")
                        .HasForeignKey("ProdId")
                        .IsRequired()
                        .HasConstraintName("FK__Table__ProdId__1CBC4616");

                    b.Navigation("Prod");
                });

            modelBuilder.Entity("Dal.newModels.Customer", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("Dal.newModels.Employee", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("Dal.newModels.ProductsSum", b =>
                {
                    b.Navigation("OrderDetails");

                    b.Navigation("Tables");
                });
#pragma warning restore 612, 618
        }
    }
}
