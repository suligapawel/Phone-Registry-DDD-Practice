﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using PhoneRegistryDDD.Helpdesk.Infrastructure.EntityFramework;

#nullable disable

namespace PhoneRegistryDDD.Helpdesk.Infrastructure.EntityFramework.Migrations
{
    [DbContext(typeof(HelpdeskDbContext))]
    partial class HelpdeskDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("PhoneRegistryDDD.Helpdesk.Core.Entities.Employee", b =>
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

                    b.ToTable("Employees", "helpdesk");
                });

            modelBuilder.Entity("PhoneRegistryDDD.Helpdesk.Core.Entities.SimCard", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("EmployeeId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId");

                    b.ToTable("Simcards", "helpdesk");
                });

            modelBuilder.Entity("PhoneRegistryDDD.Helpdesk.Core.Entities.SimCard", b =>
                {
                    b.HasOne("PhoneRegistryDDD.Helpdesk.Core.Entities.Employee", null)
                        .WithMany("SimCards")
                        .HasForeignKey("EmployeeId");

                    b.OwnsOne("PhoneRegistryDDD.Helpdesk.Core.Entities.Devices.Device", "Device", b1 =>
                        {
                            b1.Property<Guid>("SimCardId")
                                .HasColumnType("uuid");

                            b1.Property<Guid>("Id")
                                .HasColumnType("uuid");

                            b1.HasKey("SimCardId");

                            b1.ToTable("Devices", "helpdesk");

                            b1.WithOwner()
                                .HasForeignKey("SimCardId");
                        });

                    b.Navigation("Device");
                });

            modelBuilder.Entity("PhoneRegistryDDD.Helpdesk.Core.Entities.Employee", b =>
                {
                    b.Navigation("SimCards");
                });
#pragma warning restore 612, 618
        }
    }
}
