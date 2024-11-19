﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CartolaApi.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20241119191046_dtoupdateautoincrement")]
    partial class dtoupdateautoincrement
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            // Mantendo apenas a parte relacionada à chave autoincremento na tabela Seasons
            modelBuilder.Entity("CartolaApi.Data.DTOs.Season", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int") // Garantindo que o campo Id seja autoincremento
                        .UseMySqlIdentityColumn(); // Certificando-se de que é configurado corretamente como AUTO_INCREMENT no MySQL

                    b.Property<DateTime>("FinalDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.ToTable("Seasons");
                });

#pragma warning restore 612, 618
        }
    }
}