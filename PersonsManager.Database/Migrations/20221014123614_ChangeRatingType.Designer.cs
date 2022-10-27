﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PersonsManager.Database;

#nullable disable

namespace PersonsManager.Database.Migrations
{
    [DbContext(typeof(PersonsManagerDbContext))]
    [Migration("20221014123614_ChangeRatingType")]
    partial class ChangeRatingType
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("PersonsManager.Domain.Models.Person", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MiddleName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("People");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Person");
                });

            modelBuilder.Entity("PersonsManager.Domain.Models.Client", b =>
                {
                    b.HasBaseType("PersonsManager.Domain.Models.Person");

                    b.Property<bool>("IsBanned")
                        .HasColumnType("bit");

                    b.HasDiscriminator().HasValue("Client");
                });

            modelBuilder.Entity("PersonsManager.Domain.Models.Master", b =>
                {
                    b.HasBaseType("PersonsManager.Domain.Models.Person");

                    b.Property<Guid>("AddressId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<double>("Rating")
                        .HasColumnType("float");

                    b.HasDiscriminator().HasValue("Master");
                });
#pragma warning restore 612, 618
        }
    }
}
