﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MohaQuiz.Backend.DataBase;

#nullable disable

namespace MohaQuiz.Backend.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.9");

            modelBuilder.Entity("MohaQuiz.Backend.Models.CorrectAnswer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("CorrectAnswerText")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsCorrect")
                        .HasColumnType("INTEGER");

                    b.Property<int>("QuestionId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("QuestionId");

                    b.ToTable("CorrectAnswers");
                });

            modelBuilder.Entity("MohaQuiz.Backend.Models.Question", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("FullScore")
                        .HasColumnType("INTEGER");

                    b.Property<int>("QuestionNumber")
                        .HasColumnType("INTEGER");

                    b.Property<string>("QuestionText")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("RoundId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("RoundId");

                    b.ToTable("Questions");
                });

            modelBuilder.Entity("MohaQuiz.Backend.Models.Round", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("RoundName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("RoundNumber")
                        .HasColumnType("INTEGER");

                    b.Property<int>("RoundTypeId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("RoundTypeId");

                    b.ToTable("Rounds");
                });

            modelBuilder.Entity("MohaQuiz.Backend.Models.RoundType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("RoundTypeName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("RoundTypes");
                });

            modelBuilder.Entity("MohaQuiz.Backend.Models.Team", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("TeamName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Teams");
                });

            modelBuilder.Entity("MohaQuiz.Backend.Models.TeamAnswer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<double>("GivenScore")
                        .HasColumnType("REAL");

                    b.Property<int>("QuestionId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("TeamAnswerText")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("TeamId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("QuestionId");

                    b.HasIndex("TeamId");

                    b.ToTable("TeamAnswers");
                });

            modelBuilder.Entity("MohaQuiz.Backend.Models.CorrectAnswer", b =>
                {
                    b.HasOne("MohaQuiz.Backend.Models.Question", "Question")
                        .WithMany("CorrectAnswers")
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Question");
                });

            modelBuilder.Entity("MohaQuiz.Backend.Models.Question", b =>
                {
                    b.HasOne("MohaQuiz.Backend.Models.Round", "Round")
                        .WithMany("Questions")
                        .HasForeignKey("RoundId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Round");
                });

            modelBuilder.Entity("MohaQuiz.Backend.Models.Round", b =>
                {
                    b.HasOne("MohaQuiz.Backend.Models.RoundType", "RoundType")
                        .WithMany("Rounds")
                        .HasForeignKey("RoundTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("RoundType");
                });

            modelBuilder.Entity("MohaQuiz.Backend.Models.TeamAnswer", b =>
                {
                    b.HasOne("MohaQuiz.Backend.Models.Question", "Question")
                        .WithMany("TeamAnswers")
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MohaQuiz.Backend.Models.Team", "Team")
                        .WithMany("TeamAnswers")
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Question");

                    b.Navigation("Team");
                });

            modelBuilder.Entity("MohaQuiz.Backend.Models.Question", b =>
                {
                    b.Navigation("CorrectAnswers");

                    b.Navigation("TeamAnswers");
                });

            modelBuilder.Entity("MohaQuiz.Backend.Models.Round", b =>
                {
                    b.Navigation("Questions");
                });

            modelBuilder.Entity("MohaQuiz.Backend.Models.RoundType", b =>
                {
                    b.Navigation("Rounds");
                });

            modelBuilder.Entity("MohaQuiz.Backend.Models.Team", b =>
                {
                    b.Navigation("TeamAnswers");
                });
#pragma warning restore 612, 618
        }
    }
}
