﻿// <auto-generated />
using System;
using CVhub.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CVhub.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20190125034130_StorageMigration")]
    partial class StorageMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CVhub.Models.Company", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Companies");
                });

            modelBuilder.Entity("CVhub.Models.JobApplication", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CoverLetter");

                    b.Property<DateTime?>("DateOfBirth")
                        .IsRequired();

                    b.Property<string>("EmailAddress")
                        .IsRequired();

                    b.Property<string>("FirstName")
                        .IsRequired();

                    b.Property<int>("JobOfferId");

                    b.Property<string>("LastName")
                        .IsRequired();

                    b.Property<string>("PhoneNumber")
                        .IsRequired();

                    b.Property<string>("PhotoFileName");

                    b.HasKey("Id");

                    b.HasIndex("JobOfferId");

                    b.ToTable("JobApplications");
                });

            modelBuilder.Entity("CVhub.Models.JobOffer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CompanyId");

                    b.Property<string>("CompanyName")
                        .IsRequired();

                    b.Property<DateTime>("Created");

                    b.Property<string>("Description")
                        .IsRequired();

                    b.Property<string>("JobTitle")
                        .IsRequired();

                    b.Property<string>("Location")
                        .IsRequired();

                    b.Property<decimal?>("SalaryFrom")
                        .IsRequired()
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("SalaryTo")
                        .IsRequired()
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime?>("ValidUntil")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.ToTable("JobOffers");
                });

            modelBuilder.Entity("CVhub.Models.JobApplication", b =>
                {
                    b.HasOne("CVhub.Models.JobOffer")
                        .WithMany("JobApplications")
                        .HasForeignKey("JobOfferId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CVhub.Models.JobOffer", b =>
                {
                    b.HasOne("CVhub.Models.Company", "Company")
                        .WithMany()
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
