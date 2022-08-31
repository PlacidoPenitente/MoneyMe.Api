﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MoneyMe.Infrastructure.Database;

namespace MoneyMe.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20220829155621_UpdatatedStructure")]
    partial class UpdatatedStructure
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.17")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MoneyMe.Domain.ApplicationAggregate.Loan", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CustomerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DateAdded")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateModified")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("LoanAmount")
                        .HasColumnType("decimal(5,4)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Loans");
                });

            modelBuilder.Entity("MoneyMe.Domain.CustomerAggregate.Customer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DateAdded")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateModified")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("EmailAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MobileNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("MoneyMe.Domain.ProductAggregate.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DateAdded")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateModified")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("InterestRate")
                        .HasColumnType("decimal(5,4)");

                    b.Property<int>("MaximumDuration")
                        .HasColumnType("int");

                    b.Property<int>("MinimumDuration")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Rule")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            Id = new Guid("26e4541a-2ee6-4834-8f93-ed7ddfcffedb"),
                            DateAdded = new DateTime(2022, 8, 29, 15, 56, 21, 412, DateTimeKind.Utc).AddTicks(3360),
                            DateModified = new DateTime(2022, 8, 29, 15, 56, 21, 412, DateTimeKind.Utc).AddTicks(3380),
                            InterestRate = 0m,
                            MaximumDuration = 3,
                            MinimumDuration = 1,
                            Name = "Product A",
                            Rule = "InterestFreeRule"
                        },
                        new
                        {
                            Id = new Guid("d9ad31d7-83c6-4085-a2d3-233037a27eb1"),
                            DateAdded = new DateTime(2022, 8, 29, 15, 56, 21, 412, DateTimeKind.Utc).AddTicks(9214),
                            DateModified = new DateTime(2022, 8, 29, 15, 56, 21, 412, DateTimeKind.Utc).AddTicks(9236),
                            InterestRate = 0.0949m,
                            MaximumDuration = 12,
                            MinimumDuration = 6,
                            Name = "Product B",
                            Rule = "FirstTwoMonthsInterestFreeRule"
                        },
                        new
                        {
                            Id = new Guid("50ffc053-1243-4aee-b5cc-5bf8f9946c85"),
                            DateAdded = new DateTime(2022, 8, 29, 15, 56, 21, 412, DateTimeKind.Utc).AddTicks(9457),
                            DateModified = new DateTime(2022, 8, 29, 15, 56, 21, 412, DateTimeKind.Utc).AddTicks(9459),
                            InterestRate = 0.0949m,
                            MaximumDuration = 36,
                            MinimumDuration = 18,
                            Name = "Product C",
                            Rule = "NoInterestFreeRule"
                        });
                });

            modelBuilder.Entity("MoneyMe.Domain.QuoteAggregate.Quote", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CustomerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DateAdded")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateModified")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("InterestRate")
                        .HasColumnType("decimal(5,4)");

                    b.Property<decimal>("LoanAmount")
                        .HasColumnType("decimal(5,4)");

                    b.Property<int>("Term")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Quotes");
                });

            modelBuilder.Entity("MoneyMe.Domain.ApplicationAggregate.Loan", b =>
                {
                    b.OwnsMany("MoneyMe.Domain.LoanAggregate.Term", "Terms", b1 =>
                        {
                            b1.Property<Guid>("LoanId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int")
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.Property<decimal>("Interest")
                                .HasColumnType("decimal(5,4)");

                            b1.Property<int>("Period")
                                .HasColumnType("int");

                            b1.Property<decimal>("Principal")
                                .HasColumnType("decimal(5,4)");

                            b1.HasKey("LoanId", "Id");

                            b1.ToTable("Loans_Terms");

                            b1.WithOwner()
                                .HasForeignKey("LoanId");
                        });

                    b.Navigation("Terms");
                });

            modelBuilder.Entity("MoneyMe.Domain.QuoteAggregate.Quote", b =>
                {
                    b.OwnsMany("MoneyMe.Domain.LoanAggregate.Term", "MonthlyAmotization", b1 =>
                        {
                            b1.Property<Guid>("QuoteId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int")
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.Property<decimal>("Interest")
                                .HasColumnType("decimal(5,4)");

                            b1.Property<int>("Period")
                                .HasColumnType("int");

                            b1.Property<decimal>("Principal")
                                .HasColumnType("decimal(5,4)");

                            b1.HasKey("QuoteId", "Id");

                            b1.ToTable("Quotes_MonthlyAmotization");

                            b1.WithOwner()
                                .HasForeignKey("QuoteId");
                        });

                    b.Navigation("MonthlyAmotization");
                });
#pragma warning restore 612, 618
        }
    }
}