﻿// <auto-generated />
using System;
using GreatNews.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace GreatNews.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    partial class NewsContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.11-servicing-32099")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("GreatNews.Models.Comment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Comment_");

                    /*b.Property<Guid>("NewsId");*/

                    b.Property<Guid?>("NewsId");

                    b.HasKey("Id");

                    b.HasIndex("NewsId");

                    b.ToTable("Comments_");
                });

            modelBuilder.Entity("GreatNews.Models.News", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Content");

                    b.Property<DateTime>("Date");

                    b.Property<string>("Heading");

                    b.Property<int>("PositiveIndex");

                    b.Property<string>("Source");

                    b.HasKey("Id");

                    b.ToTable("News_");
                });

            modelBuilder.Entity("GreatNews.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<string>("Role");

                    b.Property<Guid?>("newsId");

                    b.HasKey("Id");

                    b.HasIndex("newsId");

                    b.ToTable("Users_");
                });

            modelBuilder.Entity("GreatNews.Models.Comment", b =>
                {
                    b.HasOne("GreatNews.Models.News", "News")
                        .WithMany()
                        .HasForeignKey("NewsId1");
                });

            modelBuilder.Entity("GreatNews.Models.User", b =>
                {
                    b.HasOne("GreatNews.Models.News", "news")
                        .WithMany()
                        .HasForeignKey("newsId");
                });
#pragma warning restore 612, 618
        }
    }
}
