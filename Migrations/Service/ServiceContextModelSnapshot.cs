﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MowManager.Models;

namespace MowManager.Migrations.Service
{
    [DbContext(typeof(ServiceContext))]
    partial class ServiceContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MowManager.Models.Crew", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("LeadContactCell");

                    b.Property<int>("NumCrewMembers");

                    b.HasKey("ID");

                    b.ToTable("Crew");
                });

            modelBuilder.Entity("MowManager.Models.Pricing", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.Property<decimal>("Rate");

                    b.Property<decimal>("RateTaxIncluded");

                    b.Property<decimal>("TaxValue");

                    b.HasKey("ID");

                    b.ToTable("Pricing");
                });

            modelBuilder.Entity("MowManager.Models.Service", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address");

                    b.Property<string>("AreaToMow");

                    b.Property<bool>("BagMow");

                    b.Property<int?>("CrewID");

                    b.Property<string>("Day");

                    b.Property<int>("DogRedos");

                    b.Property<string>("Frequency");

                    b.Property<int>("FutureSkipWeekNum");

                    b.Property<string>("Market");

                    b.Property<int?>("PricingID");

                    b.Property<int>("Restarts");

                    b.Property<int>("SendScheduleWeekNum");

                    b.Property<int>("Skips");

                    b.Property<DateTime>("StartDate");

                    b.HasKey("ID");

                    b.HasIndex("CrewID");

                    b.HasIndex("PricingID");

                    b.ToTable("ServiceItems");
                });

            modelBuilder.Entity("MowManager.Models.Service", b =>
                {
                    b.HasOne("MowManager.Models.Crew", "Crew")
                        .WithMany()
                        .HasForeignKey("CrewID");

                    b.HasOne("MowManager.Models.Pricing", "Pricing")
                        .WithMany()
                        .HasForeignKey("PricingID");
                });
#pragma warning restore 612, 618
        }
    }
}
