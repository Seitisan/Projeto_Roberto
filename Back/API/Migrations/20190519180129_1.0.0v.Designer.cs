﻿// <auto-generated />
using System;
using API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace API.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20190519180129_1.0.0v")]
    partial class _100v
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062");

            modelBuilder.Entity("API.Models.CadastroPesquisa", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Coordenador");

                    b.Property<string>("Curso");

                    b.Property<DateTime>("PesquisaCriada");

                    b.Property<int>("QuantidadeAluno");

                    b.Property<string>("TipoCurso");

                    b.Property<string>("Turma");

                    b.HasKey("Id");

                    b.ToTable("CadastroPesquisas");
                });

            modelBuilder.Entity("API.Models.Docente", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("CadastroPesquisaId");

                    b.Property<string>("Materia");

                    b.Property<string>("NomeDocente");

                    b.HasKey("Id");

                    b.HasIndex("CadastroPesquisaId");

                    b.ToTable("Docentes");
                });

            modelBuilder.Entity("API.Models.Pergunta", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Texto");

                    b.HasKey("Id");

                    b.ToTable("Perguntas");
                });

            modelBuilder.Entity("API.Models.Pesquisa", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Comentario");

                    b.Property<int>("IdCadastroPesquisa");

                    b.Property<string>("NomeAluno");

                    b.Property<DateTime>("Respondido");

                    b.HasKey("Id");

                    b.HasIndex("IdCadastroPesquisa");

                    b.ToTable("Pesquisas");
                });

            modelBuilder.Entity("API.Models.Resposta", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Coordenador");

                    b.Property<string>("DocenteMateria");

                    b.Property<int>("IdDocente");

                    b.Property<int>("IdPergunta");

                    b.Property<int>("IdPesquisa");

                    b.Property<int>("ValorImportancia");

                    b.Property<int>("ValorResposta");

                    b.HasKey("Id");

                    b.HasIndex("IdPesquisa");

                    b.ToTable("Resposta");
                });

            modelBuilder.Entity("API.Models.Docente", b =>
                {
                    b.HasOne("API.Models.CadastroPesquisa")
                        .WithMany("Docentes")
                        .HasForeignKey("CadastroPesquisaId");
                });

            modelBuilder.Entity("API.Models.Pesquisa", b =>
                {
                    b.HasOne("API.Models.CadastroPesquisa", "CadastroPesquisa")
                        .WithMany("Pesquisas")
                        .HasForeignKey("IdCadastroPesquisa")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("API.Models.Resposta", b =>
                {
                    b.HasOne("API.Models.Pesquisa", "Pesquisa")
                        .WithMany("Respostas")
                        .HasForeignKey("IdPesquisa")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
