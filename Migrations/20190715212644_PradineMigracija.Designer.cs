﻿// <auto-generated />
using System;
using FilmaiDB.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FilmaiDB.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20190715212644_PradineMigracija")]
    partial class PradineMigracija
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("FilmaiDB.Models.Filmas", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Aktoriai")
                        .IsRequired();

                    b.Property<int?>("IsleidimoData")
                        .IsRequired();

                    b.Property<string>("Pavadinimas")
                        .IsRequired();

                    b.Property<int>("Zanras");

                    b.HasKey("Id");

                    b.ToTable("Filmai");
                });
#pragma warning restore 612, 618
        }
    }
}
