﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using backend.DataAccess;

#nullable disable

namespace backend.DataAccess.Migrations
{
    [DbContext(typeof(EmailContext))]
    partial class EmailContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("backend.DataAccess.Entities.Subscriber", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Subscribers");

                    b.HasData(
                        new
                        {
                            Id = new Guid("e4066b77-977f-40af-94e1-a65ef4033061"),
                            Email = "razvan-andrei.canuci@student.tuiasi.ro",
                            Name = "Andrei",
                            PhoneNumber = "0707070707"
                        },
                        new
                        {
                            Id = new Guid("fb17d1d3-e1e3-4cfc-924c-61219a1faa57"),
                            Email = "alex_alexutz@niezz.com",
                            Name = "Alexandru",
                            PhoneNumber = "0712345678"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}