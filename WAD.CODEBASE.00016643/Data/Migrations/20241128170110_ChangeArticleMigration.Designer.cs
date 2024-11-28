﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WAD.CODEBASE._00016643.Data;

#nullable disable

namespace WAD.CODEBASE._00016643.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20241128170110_ChangeArticleMigration")]
    partial class ChangeArticleMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("WAD.CODEBASE._00016643.Models.Article", b =>
                {
                    b.Property<int>("ArticleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ArticleId"));

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NewspaperId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ArticleId");

                    b.HasIndex("CategoryId");

                    b.HasIndex("NewspaperId");

                    b.ToTable("Articles");
                });

            modelBuilder.Entity("WAD.CODEBASE._00016643.Models.Category", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CategoryId"));

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CategoryId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("WAD.CODEBASE._00016643.Models.Newspaper", b =>
                {
                    b.Property<int>("NewspaperId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("NewspaperId"));

                    b.Property<string>("NewspaperDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NewspaperName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("NewspaperId");

                    b.ToTable("Newspapers");
                });

            modelBuilder.Entity("WAD.CODEBASE._00016643.Models.Article", b =>
                {
                    b.HasOne("WAD.CODEBASE._00016643.Models.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WAD.CODEBASE._00016643.Models.Newspaper", "Newspaper")
                        .WithMany("Articles")
                        .HasForeignKey("NewspaperId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("Newspaper");
                });

            modelBuilder.Entity("WAD.CODEBASE._00016643.Models.Newspaper", b =>
                {
                    b.Navigation("Articles");
                });
#pragma warning restore 612, 618
        }
    }
}
