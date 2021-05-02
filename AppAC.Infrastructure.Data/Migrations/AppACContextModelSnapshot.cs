﻿// <auto-generated />
using System;
using AppAC.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AppAC.Infrastructure.Data.Migrations
{
    [DbContext(typeof(AppACContext))]
    partial class AppACContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.5");

            modelBuilder.Entity("AppAC.Domain.Actividad", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("DocenteId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Estado")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("FechaAsignacion")
                        .HasColumnType("TEXT");

                    b.Property<int>("HorasAsignadas")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("TipoActividadId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("DocenteId");

                    b.HasIndex("TipoActividadId");

                    b.ToTable("Actividades");
                });

            modelBuilder.Entity("AppAC.Domain.Departamento", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("CodigoDpto")
                        .HasColumnType("TEXT");

                    b.Property<string>("NombreDpto")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Departamentos");
                });

            modelBuilder.Entity("AppAC.Domain.ItemPlan", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("PlanAccionId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("PlanAccionId");

                    b.ToTable("ItemsPlanes");
                });

            modelBuilder.Entity("AppAC.Domain.PlanAccion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("ActividadId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Fecha")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("ActividadId");

                    b.ToTable("Planes");
                });

            modelBuilder.Entity("AppAC.Domain.PlazoApertura", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Activo")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("FechaFin")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("FechaInicio")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("PlazosApertura");
                });

            modelBuilder.Entity("AppAC.Domain.TipoActividad", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("NombreActividad")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasAlternateKey("NombreActividad");

                    b.ToTable("TiposActividades");
                });

            modelBuilder.Entity("AppAC.Domain.Usuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Apellidos")
                        .HasColumnType("TEXT");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .HasColumnType("TEXT");

                    b.Property<string>("Identificacion")
                        .HasColumnType("TEXT");

                    b.Property<string>("Nombres")
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .HasColumnType("TEXT");

                    b.Property<string>("Sexo")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserName")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Usuarios");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Usuario");
                });

            modelBuilder.Entity("AppAC.Domain.Administrador", b =>
                {
                    b.HasBaseType("AppAC.Domain.Usuario");

                    b.HasDiscriminator().HasValue("Administrador");
                });

            modelBuilder.Entity("AppAC.Domain.Docente", b =>
                {
                    b.HasBaseType("AppAC.Domain.Usuario");

                    b.Property<int?>("DepartamentoId")
                        .HasColumnType("INTEGER")
                        .HasColumnName("Docente_DepartamentoId");

                    b.HasIndex("DepartamentoId");

                    b.HasDiscriminator().HasValue("Docente");
                });

            modelBuilder.Entity("AppAC.Domain.JefeDpto", b =>
                {
                    b.HasBaseType("AppAC.Domain.Usuario");

                    b.Property<int?>("DepartamentoId")
                        .HasColumnType("INTEGER");

                    b.HasIndex("DepartamentoId");

                    b.HasDiscriminator().HasValue("JefeDpto");
                });

            modelBuilder.Entity("AppAC.Domain.Actividad", b =>
                {
                    b.HasOne("AppAC.Domain.Docente", "Docente")
                        .WithMany()
                        .HasForeignKey("DocenteId");

                    b.HasOne("AppAC.Domain.TipoActividad", "TipoActividad")
                        .WithMany()
                        .HasForeignKey("TipoActividadId");

                    b.Navigation("Docente");

                    b.Navigation("TipoActividad");
                });

            modelBuilder.Entity("AppAC.Domain.ItemPlan", b =>
                {
                    b.HasOne("AppAC.Domain.PlanAccion", null)
                        .WithMany("Items")
                        .HasForeignKey("PlanAccionId");

                    b.OwnsOne("AppAC.Domain.AccionPlaneada", "AccionPlaneada", b1 =>
                        {
                            b1.Property<int>("ItemPlanId")
                                .HasColumnType("INTEGER");

                            b1.Property<string>("Descripcion")
                                .HasColumnType("TEXT");

                            b1.HasKey("ItemPlanId");

                            b1.ToTable("ItemsPlanes");

                            b1.WithOwner()
                                .HasForeignKey("ItemPlanId");
                        });

                    b.OwnsOne("AppAC.Domain.AccionRealizada", "AccionRealizada", b1 =>
                        {
                            b1.Property<int>("ItemPlanId")
                                .HasColumnType("INTEGER");

                            b1.Property<string>("Descripcion")
                                .HasColumnType("TEXT");

                            b1.HasKey("ItemPlanId");

                            b1.ToTable("ItemsPlanes");

                            b1.WithOwner()
                                .HasForeignKey("ItemPlanId");

                            b1.OwnsOne("AppAC.Domain.Evidencia", "Evidencia", b2 =>
                                {
                                    b2.Property<int>("AccionRealizadaItemPlanId")
                                        .HasColumnType("INTEGER");

                                    b2.Property<DateTime>("FechaCarga")
                                        .HasColumnType("TEXT");

                                    b2.Property<string>("Ruta")
                                        .HasColumnType("TEXT");

                                    b2.HasKey("AccionRealizadaItemPlanId");

                                    b2.ToTable("ItemsPlanes");

                                    b2.WithOwner()
                                        .HasForeignKey("AccionRealizadaItemPlanId");
                                });

                            b1.Navigation("Evidencia");
                        });

                    b.Navigation("AccionPlaneada");

                    b.Navigation("AccionRealizada");
                });

            modelBuilder.Entity("AppAC.Domain.PlanAccion", b =>
                {
                    b.HasOne("AppAC.Domain.Actividad", "Actividad")
                        .WithMany()
                        .HasForeignKey("ActividadId");

                    b.Navigation("Actividad");
                });

            modelBuilder.Entity("AppAC.Domain.Docente", b =>
                {
                    b.HasOne("AppAC.Domain.Departamento", "Departamento")
                        .WithMany()
                        .HasForeignKey("DepartamentoId");

                    b.Navigation("Departamento");
                });

            modelBuilder.Entity("AppAC.Domain.JefeDpto", b =>
                {
                    b.HasOne("AppAC.Domain.Departamento", "Departamento")
                        .WithMany()
                        .HasForeignKey("DepartamentoId");

                    b.Navigation("Departamento");
                });

            modelBuilder.Entity("AppAC.Domain.PlanAccion", b =>
                {
                    b.Navigation("Items");
                });
#pragma warning restore 612, 618
        }
    }
}
