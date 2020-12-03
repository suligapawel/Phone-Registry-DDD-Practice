﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PhoneRegistryDDD.Helpdesk.Infrastructure.EntityFramework;

namespace PhoneRegistryDDD.Helpdesk.Infrastructure.Migrations
{
    [DbContext(typeof(HelpdeskContext))]
    partial class HelpdeskContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("PhoneRegistryDDD.Helpdesk.Core.Entities.Employee", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETDATE()");

                    b.HasKey("Id");

                    b.ToTable("Employees", "helpdesk");
                });

            modelBuilder.Entity("PhoneRegistryDDD.Helpdesk.Core.Entities.SimCard", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("EmployeeId")
                        .HasColumnType("uniqueidentifier");

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
                                .HasColumnType("uniqueidentifier");

                            b1.Property<Guid>("Id")
                                .HasColumnType("uniqueidentifier");

                            b1.HasKey("SimCardId");

                            b1.ToTable("Devices");

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
