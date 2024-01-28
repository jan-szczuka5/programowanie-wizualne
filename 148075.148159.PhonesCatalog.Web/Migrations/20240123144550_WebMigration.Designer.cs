﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using _148075._148159.PhonesCatalog.Web;

#nullable disable

namespace _148075._148159.PhonesCatalog.Web.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20240123144550_WebMigration")]
    partial class WebMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.1");

            modelBuilder.Entity("_148075._148159.PhonesCatalog.Web.Models.Phone", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("AlreadySold")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Price")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ProducerID")
                        .HasColumnType("INTEGER");

                    b.Property<int>("SoftwareType")
                        .HasColumnType("INTEGER");

                    b.Property<int>("YearOfProduction")
                        .HasColumnType("INTEGER");

                    b.HasKey("ID");

                    b.HasIndex("ProducerID");

                    b.ToTable("Phones");
                });

            modelBuilder.Entity("_148075._148159.PhonesCatalog.Web.Models.Producer", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.ToTable("Producers");
                });

            modelBuilder.Entity("_148075._148159.PhonesCatalog.Web.Models.Phone", b =>
                {
                    b.HasOne("_148075._148159.PhonesCatalog.Web.Models.Producer", "Producer")
                        .WithMany()
                        .HasForeignKey("ProducerID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Producer");
                });
#pragma warning restore 612, 618
        }
    }
}
