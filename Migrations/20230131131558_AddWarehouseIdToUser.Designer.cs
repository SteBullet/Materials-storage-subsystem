﻿// <auto-generated />
using Materials_storage_subsystem.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Materials_storage_subsystem.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20230131131558_AddWarehouseIdToUser")]
    partial class AddWarehouseIdToUser
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.17")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Materials_storage_subsystem.Models.ExpenseSheet", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Date")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("WarehouseId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("WarehouseId");

                    b.ToTable("ExpenseSheets");
                });

            modelBuilder.Entity("Materials_storage_subsystem.Models.Material", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Volume")
                        .HasColumnType("int");

                    b.Property<int>("Weight")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Materials");
                });

            modelBuilder.Entity("Materials_storage_subsystem.Models.MaterialMovement", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("ExpenseSheetId")
                        .HasColumnType("bigint");

                    b.Property<int>("MaterialId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ExpenseSheetId");

                    b.HasIndex("MaterialId");

                    b.ToTable("MaterialMovements");
                });

            modelBuilder.Entity("Materials_storage_subsystem.Models.MaterialRemaining", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("MaterialId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<int>("WarehouseId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("MaterialId");

                    b.HasIndex("WarehouseId");

                    b.ToTable("MaterialRemainings");
                });

            modelBuilder.Entity("Materials_storage_subsystem.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Login")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("WarehouseId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("WarehouseId");

                    b.ToTable("Users");

                    b.HasDiscriminator<string>("Discriminator").HasValue("User");
                });

            modelBuilder.Entity("Materials_storage_subsystem.Models.Warehouse", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Adress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Capacity")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Warehouses");
                });

            modelBuilder.Entity("Materials_storage_subsystem.Models.Roles.Admin", b =>
                {
                    b.HasBaseType("Materials_storage_subsystem.Models.User");

                    b.HasDiscriminator().HasValue("Admin");
                });

            modelBuilder.Entity("Materials_storage_subsystem.Models.Roles.Checkman", b =>
                {
                    b.HasBaseType("Materials_storage_subsystem.Models.User");

                    b.HasDiscriminator().HasValue("Checkman");
                });

            modelBuilder.Entity("Materials_storage_subsystem.Models.Roles.Manager", b =>
                {
                    b.HasBaseType("Materials_storage_subsystem.Models.User");

                    b.HasDiscriminator().HasValue("Manager");
                });

            modelBuilder.Entity("Materials_storage_subsystem.Models.Roles.WarehouseManager", b =>
                {
                    b.HasBaseType("Materials_storage_subsystem.Models.User");

                    b.HasDiscriminator().HasValue("WarehouseManager");
                });

            modelBuilder.Entity("Materials_storage_subsystem.Models.ExpenseSheet", b =>
                {
                    b.HasOne("Materials_storage_subsystem.Models.Warehouse", "Warehouse")
                        .WithMany()
                        .HasForeignKey("WarehouseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Warehouse");
                });

            modelBuilder.Entity("Materials_storage_subsystem.Models.MaterialMovement", b =>
                {
                    b.HasOne("Materials_storage_subsystem.Models.ExpenseSheet", "ExpenseSheet")
                        .WithMany("Expenses")
                        .HasForeignKey("ExpenseSheetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Materials_storage_subsystem.Models.Material", "Material")
                        .WithMany()
                        .HasForeignKey("MaterialId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ExpenseSheet");

                    b.Navigation("Material");
                });

            modelBuilder.Entity("Materials_storage_subsystem.Models.MaterialRemaining", b =>
                {
                    b.HasOne("Materials_storage_subsystem.Models.Material", "Material")
                        .WithMany()
                        .HasForeignKey("MaterialId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Materials_storage_subsystem.Models.Warehouse", "Warehouse")
                        .WithMany()
                        .HasForeignKey("WarehouseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Material");

                    b.Navigation("Warehouse");
                });

            modelBuilder.Entity("Materials_storage_subsystem.Models.User", b =>
                {
                    b.HasOne("Materials_storage_subsystem.Models.Warehouse", "Warehouse")
                        .WithMany("Users")
                        .HasForeignKey("WarehouseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Warehouse");
                });

            modelBuilder.Entity("Materials_storage_subsystem.Models.ExpenseSheet", b =>
                {
                    b.Navigation("Expenses");
                });

            modelBuilder.Entity("Materials_storage_subsystem.Models.Warehouse", b =>
                {
                    b.Navigation("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
