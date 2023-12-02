﻿// <auto-generated />
using BookShop;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BookShop.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20231202144252_Genrebook")]
    partial class Genrebook
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.14");

            modelBuilder.Entity("BookShop.Book", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("BookContinuation")
                        .HasColumnType("TEXT");

                    b.Property<double>("CostPrice")
                        .HasColumnType("REAL");

                    b.Property<string>("FIOAvtora")
                        .HasColumnType("TEXT");

                    b.Property<int>("GenreID")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("NameOfThePublisher")
                        .HasColumnType("TEXT");

                    b.Property<int>("PageNumber")
                        .HasColumnType("INTEGER");

                    b.Property<string>("PublicationYear")
                        .HasColumnType("TEXT");

                    b.Property<double>("SellingPrice")
                        .HasColumnType("REAL");

                    b.HasKey("Id");

                    b.HasIndex("GenreID");

                    b.ToTable("books");
                });

            modelBuilder.Entity("BookShop.Discounts", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("Discoute")
                        .HasColumnType("INTEGER");

                    b.Property<int>("GenreID")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("GenreID");

                    b.ToTable("discounts");
                });

            modelBuilder.Entity("BookShop.FavoritesPurchases", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("BookId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Status")
                        .HasColumnType("TEXT");

                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("BookId");

                    b.HasIndex("UserId");

                    b.ToTable("favoritesPurchases");
                });

            modelBuilder.Entity("BookShop.Genre", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("genres");
                });

            modelBuilder.Entity("BookShop.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Login")
                        .HasColumnType("TEXT");

                    b.Property<string>("Mail")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("Passowrd")
                        .HasColumnType("TEXT");

                    b.Property<string>("Status")
                        .HasColumnType("TEXT");

                    b.Property<string>("SurName")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("users");
                });

            modelBuilder.Entity("BookShop.Book", b =>
                {
                    b.HasOne("BookShop.Genre", "Genre")
                        .WithMany()
                        .HasForeignKey("GenreID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Genre");
                });

            modelBuilder.Entity("BookShop.Discounts", b =>
                {
                    b.HasOne("BookShop.Genre", "Genre")
                        .WithMany()
                        .HasForeignKey("GenreID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Genre");
                });

            modelBuilder.Entity("BookShop.FavoritesPurchases", b =>
                {
                    b.HasOne("BookShop.Book", "Book")
                        .WithMany()
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BookShop.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Book");

                    b.Navigation("User");
                });
#pragma warning restore 612, 618
        }
    }
}
