﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using PhoneRegistryDDD.Availability.Infrastructure.EntityFramework;

#nullable disable

namespace PhoneRegistryDDD.Availability.Infrastructure.EntityFramework.Migrations
{
    [DbContext(typeof(AvailabilityDbContext))]
    partial class AvailabilityDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("PhoneRegistryDDD.Availability.Core.Entities.Assortment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasDefaultValueSql("now()");

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("timestamp with time zone")
                        .HasDefaultValueSql("now()");

                    b.HasKey("Id");

                    b.ToTable("Assortments", "availability");
                });

            modelBuilder.Entity("PhoneRegistryDDD.Availability.Core.ValueObjects.Block", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("AssortmentId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("AssortmentId");

                    b.ToTable("Blocks", "availability");
                });

            modelBuilder.Entity("PhoneRegistryDDD.Availability.Core.ValueObjects.Block", b =>
                {
                    b.HasOne("PhoneRegistryDDD.Availability.Core.Entities.Assortment", null)
                        .WithMany("Blocks")
                        .HasForeignKey("AssortmentId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.OwnsOne("PhoneRegistryDDD.Availability.Core.Entities.Owner", "Owner", b1 =>
                        {
                            b1.Property<Guid>("BlockId")
                                .HasColumnType("uuid");

                            b1.Property<Guid>("Id")
                                .HasColumnType("uuid")
                                .HasColumnName("OwnerId");

                            b1.HasKey("BlockId");

                            b1.ToTable("Blocks", "availability");

                            b1.WithOwner()
                                .HasForeignKey("BlockId");
                        });

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("PhoneRegistryDDD.Availability.Core.Entities.Assortment", b =>
                {
                    b.Navigation("Blocks");
                });
#pragma warning restore 612, 618
        }
    }
}
