﻿// <auto-generated />
using GDLevels.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace GDLevels.Migrations
{
    [DbContext(typeof(GDLevelsDataContext))]
    [Migration("20220202152519_LevelID_to_LevelId")]
    partial class LevelID_to_LevelId
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.9");

            modelBuilder.Entity("GDLevels.Models.Level", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("LevelId")
                        .HasColumnType("INTEGER");

                    b.Property<double>("RequestTime")
                        .HasColumnType("REAL");

                    b.HasKey("Id");

                    b.HasAlternateKey("LevelId");

                    b.ToTable("Levels");
                });
#pragma warning restore 612, 618
        }
    }
}
