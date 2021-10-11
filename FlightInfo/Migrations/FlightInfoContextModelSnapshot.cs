﻿// <auto-generated />
using System;
using FlightInfo.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FlightInfo.Migrations
{
    [DbContext(typeof(FlightInfoContext))]
    partial class FlightInfoContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.10")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("FlightInfo.Models.Airport", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CityId")
                        .HasColumnType("int");

                    b.Property<double>("Latitude")
                        .HasColumnType("float");

                    b.Property<double>("Longtitude")
                        .HasColumnType("float");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("CityId")
                        .IsUnique();

                    b.ToTable("Airport");
                });

            modelBuilder.Entity("FlightInfo.Models.City", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CountryId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CountryId");

                    b.ToTable("City");
                });

            modelBuilder.Entity("FlightInfo.Models.Country", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Country");
                });

            modelBuilder.Entity("FlightInfo.Models.Flight", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AirportId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DepartureTime")
                        .HasColumnType("datetime2");

                    b.Property<int?>("DestinationId")
                        .HasColumnType("int");

                    b.Property<string>("FlightNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("OriginId")
                        .HasColumnType("int");

                    b.Property<int?>("PilotId")
                        .HasColumnType("int");

                    b.Property<int?>("PlaneId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AirportId");

                    b.HasIndex("DestinationId");

                    b.HasIndex("OriginId");

                    b.HasIndex("PilotId");

                    b.HasIndex("PlaneId");

                    b.ToTable("Flight");
                });

            modelBuilder.Entity("FlightInfo.Models.Person", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Birthdate")
                        .HasColumnType("date");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("LastName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Person");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Person");
                });

            modelBuilder.Entity("FlightInfo.Models.Plane", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Capacity")
                        .HasColumnType("int");

                    b.Property<int>("CruiseSpeed")
                        .HasColumnType("int");

                    b.Property<string>("Manufacturer")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Model")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Plane");
                });

            modelBuilder.Entity("FlightPassenger", b =>
                {
                    b.Property<int>("FlightHistoryId")
                        .HasColumnType("int");

                    b.Property<int>("PassengerManifestId")
                        .HasColumnType("int");

                    b.HasKey("FlightHistoryId", "PassengerManifestId");

                    b.HasIndex("PassengerManifestId");

                    b.ToTable("FlightPassenger");
                });

            modelBuilder.Entity("PilotPlane", b =>
                {
                    b.Property<int>("PilotsId")
                        .HasColumnType("int");

                    b.Property<int>("QualificationId")
                        .HasColumnType("int");

                    b.HasKey("PilotsId", "QualificationId");

                    b.HasIndex("QualificationId");

                    b.ToTable("PilotPlane");
                });

            modelBuilder.Entity("FlightInfo.Models.Passenger", b =>
                {
                    b.HasBaseType("FlightInfo.Models.Person");

                    b.HasDiscriminator().HasValue("Passenger");
                });

            modelBuilder.Entity("FlightInfo.Models.Pilot", b =>
                {
                    b.HasBaseType("FlightInfo.Models.Person");

                    b.HasDiscriminator().HasValue("Pilot");
                });

            modelBuilder.Entity("FlightInfo.Models.Airport", b =>
                {
                    b.HasOne("FlightInfo.Models.City", "City")
                        .WithOne("Airport")
                        .HasForeignKey("FlightInfo.Models.Airport", "CityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("City");
                });

            modelBuilder.Entity("FlightInfo.Models.City", b =>
                {
                    b.HasOne("FlightInfo.Models.Country", "Country")
                        .WithMany()
                        .HasForeignKey("CountryId");

                    b.Navigation("Country");
                });

            modelBuilder.Entity("FlightInfo.Models.Flight", b =>
                {
                    b.HasOne("FlightInfo.Models.Airport", null)
                        .WithMany("FlightTable")
                        .HasForeignKey("AirportId");

                    b.HasOne("FlightInfo.Models.Airport", "Destination")
                        .WithMany()
                        .HasForeignKey("DestinationId");

                    b.HasOne("FlightInfo.Models.Airport", "Origin")
                        .WithMany()
                        .HasForeignKey("OriginId");

                    b.HasOne("FlightInfo.Models.Pilot", "Pilot")
                        .WithMany()
                        .HasForeignKey("PilotId");

                    b.HasOne("FlightInfo.Models.Plane", "Plane")
                        .WithMany()
                        .HasForeignKey("PlaneId");

                    b.Navigation("Destination");

                    b.Navigation("Origin");

                    b.Navigation("Pilot");

                    b.Navigation("Plane");
                });

            modelBuilder.Entity("FlightPassenger", b =>
                {
                    b.HasOne("FlightInfo.Models.Flight", null)
                        .WithMany()
                        .HasForeignKey("FlightHistoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FlightInfo.Models.Passenger", null)
                        .WithMany()
                        .HasForeignKey("PassengerManifestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PilotPlane", b =>
                {
                    b.HasOne("FlightInfo.Models.Pilot", null)
                        .WithMany()
                        .HasForeignKey("PilotsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FlightInfo.Models.Plane", null)
                        .WithMany()
                        .HasForeignKey("QualificationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("FlightInfo.Models.Airport", b =>
                {
                    b.Navigation("FlightTable");
                });

            modelBuilder.Entity("FlightInfo.Models.City", b =>
                {
                    b.Navigation("Airport");
                });
#pragma warning restore 612, 618
        }
    }
}
