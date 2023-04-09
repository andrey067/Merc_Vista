﻿// <auto-generated />
using System;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infrastructure.Migrations
{
    [DbContext(typeof(MercVistaContext))]
    [Migration("20230409223825_FirtMigration")]
    partial class FirtMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.15");

            modelBuilder.Entity("Domain.Acao", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("Abertura")
                        .HasColumnType("TEXT");

                    b.Property<string>("Ativo")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("Data")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Fechamento")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Maximo")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Minimo")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Quantidade")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Volume")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Acaoes");
                });
#pragma warning restore 612, 618
        }
    }
}
