﻿// <auto-generated />
using System;
using DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DAL.Migrations
{
    [DbContext(typeof(DBContext))]
    [Migration("20241107113234_InitialMigration")]
    partial class InitialMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("DAL.Models.Articulos", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<decimal>("Costo")
                        .HasColumnType("decimal(10,2)");

                    b.Property<DateTime>("Fecha")
                        .HasColumnType("datetime2");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("PreciosEspecialidadesId")
                        .HasColumnType("bigint");

                    b.Property<long>("TiposSegurosId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("PreciosEspecialidadesId");

                    b.HasIndex("TiposSegurosId");

                    b.ToTable("Articulos");
                });

            modelBuilder.Entity("DAL.Models.Citas", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<long>("FacturasId")
                        .HasColumnType("bigint");

                    b.Property<long>("GruposCitasId")
                        .HasColumnType("bigint");

                    b.Property<TimeSpan>("Hora")
                        .HasColumnType("time");

                    b.Property<long>("PacienteId")
                        .HasColumnType("bigint");

                    b.Property<long?>("PacientesId")
                        .HasColumnType("bigint");

                    b.Property<long>("PreciosEspecialidadesId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("FacturasId");

                    b.HasIndex("GruposCitasId");

                    b.HasIndex("PacienteId");

                    b.HasIndex("PacientesId");

                    b.HasIndex("PreciosEspecialidadesId");

                    b.ToTable("Citas");
                });

            modelBuilder.Entity("DAL.Models.ContratosSeguros", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Estado")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("FechaInicio")
                        .HasColumnType("datetime2");

                    b.Property<long>("TiposSegurosId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("TiposSegurosId");

                    b.ToTable("ContratosSeguros");
                });

            modelBuilder.Entity("DAL.Models.Diagnosticos", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Fecha")
                        .HasColumnType("datetime2");

                    b.Property<long>("HistoriasClinicasId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("HistoriasClinicasId");

                    b.ToTable("Diagnosticos");
                });

            modelBuilder.Entity("DAL.Models.Especialidades", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<TimeSpan>("tiempoCita")
                        .HasColumnType("time");

                    b.HasKey("Id");

                    b.ToTable("Especialidades");
                });

            modelBuilder.Entity("DAL.Models.Facturas", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<long>("ContratosSegurosId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("FechaEmision")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Pago")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("ContratosSegurosId");

                    b.ToTable("Facturas");
                });

            modelBuilder.Entity("DAL.Models.GruposCitas", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<long>("EspecialidadesId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("Fecha")
                        .HasColumnType("datetime2");

                    b.Property<string>("Lugar")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("MedicosId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("EspecialidadesId");

                    b.HasIndex("MedicosId");

                    b.ToTable("GruposCitas");
                });

            modelBuilder.Entity("DAL.Models.HistoriasClinicas", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("FechaCreacion")
                        .HasColumnType("datetime2");

                    b.Property<long>("PacientesId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.ToTable("HistoriasClinicas");
                });

            modelBuilder.Entity("DAL.Models.Medicamentos", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Dosis")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("RecetasId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("RecetasId");

                    b.ToTable("Medicamentos");
                });

            modelBuilder.Entity("DAL.Models.Personas", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Apellidos")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasMaxLength(13)
                        .HasColumnType("nvarchar(13)");

                    b.Property<string>("Documento")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombres")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Personas");

                    b.HasDiscriminator().HasValue("Personas");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("DAL.Models.PreciosEspecialidades", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<long>("EspecialidadesId")
                        .HasColumnType("bigint");

                    b.Property<long>("TiposSegurosId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("EspecialidadesId");

                    b.HasIndex("TiposSegurosId");

                    b.ToTable("PreciosEspecialidades");
                });

            modelBuilder.Entity("DAL.Models.Recetas", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("Fecha")
                        .HasColumnType("datetime2");

                    b.Property<long>("HistoriasClinicasId")
                        .HasColumnType("bigint");

                    b.Property<string>("Tipo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("HistoriasClinicasId");

                    b.ToTable("Recetas");
                });

            modelBuilder.Entity("DAL.Models.ResultadosEstudios", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Fecha")
                        .HasColumnType("datetime2");

                    b.Property<long>("HistoriasClinicasId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("HistoriasClinicasId");

                    b.ToTable("ResultadosEstudios");
                });

            modelBuilder.Entity("DAL.Models.TiposSeguros", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("TiposSeguros");
                });

            modelBuilder.Entity("DAL.Models.Users", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<bool>("Activo")
                        .HasColumnType("bit");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("PersonasId")
                        .HasColumnType("bigint");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.HasIndex("PersonasId");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("EspecialidadesMedicos", b =>
                {
                    b.Property<long>("EspecialidadesId")
                        .HasColumnType("bigint");

                    b.Property<long>("MedicosId")
                        .HasColumnType("bigint");

                    b.HasKey("EspecialidadesId", "MedicosId");

                    b.HasIndex("MedicosId");

                    b.ToTable("EspecialidadesMedicos");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("Shared.Entities.Articulo", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<decimal>("Costo")
                        .HasPrecision(10, 2)
                        .HasColumnType("decimal(10,2)");

                    b.Property<DateTime>("Fecha")
                        .HasColumnType("datetime2");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Articulo");
                });

            modelBuilder.Entity("DAL.Models.Medicos", b =>
                {
                    b.HasBaseType("DAL.Models.Personas");

                    b.Property<string>("Matricula")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasDiscriminator().HasValue("Medicos");
                });

            modelBuilder.Entity("DAL.Models.Pacientes", b =>
                {
                    b.HasBaseType("DAL.Models.Personas");

                    b.Property<long>("ContratosSegurosId")
                        .HasColumnType("bigint");

                    b.Property<long>("HistoriasClinicasId")
                        .HasColumnType("bigint");

                    b.Property<string>("Telefono")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasIndex("ContratosSegurosId")
                        .IsUnique()
                        .HasFilter("[ContratosSegurosId] IS NOT NULL");

                    b.HasIndex("HistoriasClinicasId")
                        .IsUnique()
                        .HasFilter("[HistoriasClinicasId] IS NOT NULL");

                    b.HasDiscriminator().HasValue("Pacientes");
                });

            modelBuilder.Entity("DAL.Models.Articulos", b =>
                {
                    b.HasOne("DAL.Models.PreciosEspecialidades", "PreciosEspecialidades")
                        .WithMany()
                        .HasForeignKey("PreciosEspecialidadesId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("DAL.Models.TiposSeguros", "TiposSeguros")
                        .WithMany("Articulos")
                        .HasForeignKey("TiposSegurosId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("PreciosEspecialidades");

                    b.Navigation("TiposSeguros");
                });

            modelBuilder.Entity("DAL.Models.Citas", b =>
                {
                    b.HasOne("DAL.Models.Facturas", "Facturas")
                        .WithMany("Citas")
                        .HasForeignKey("FacturasId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("DAL.Models.GruposCitas", "GruposCitas")
                        .WithMany("Citas")
                        .HasForeignKey("GruposCitasId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("DAL.Models.Pacientes", "Pacientes")
                        .WithMany()
                        .HasForeignKey("PacienteId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("DAL.Models.Pacientes", null)
                        .WithMany("Citas")
                        .HasForeignKey("PacientesId");

                    b.HasOne("DAL.Models.PreciosEspecialidades", "PreciosEspecialidades")
                        .WithMany()
                        .HasForeignKey("PreciosEspecialidadesId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Facturas");

                    b.Navigation("GruposCitas");

                    b.Navigation("Pacientes");

                    b.Navigation("PreciosEspecialidades");
                });

            modelBuilder.Entity("DAL.Models.ContratosSeguros", b =>
                {
                    b.HasOne("DAL.Models.TiposSeguros", "TiposSeguros")
                        .WithMany()
                        .HasForeignKey("TiposSegurosId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TiposSeguros");
                });

            modelBuilder.Entity("DAL.Models.Diagnosticos", b =>
                {
                    b.HasOne("DAL.Models.HistoriasClinicas", "HistoriasClinicas")
                        .WithMany("Diagnosticos")
                        .HasForeignKey("HistoriasClinicasId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("HistoriasClinicas");
                });

            modelBuilder.Entity("DAL.Models.Facturas", b =>
                {
                    b.HasOne("DAL.Models.ContratosSeguros", "ContratosSeguros")
                        .WithMany()
                        .HasForeignKey("ContratosSegurosId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ContratosSeguros");
                });

            modelBuilder.Entity("DAL.Models.GruposCitas", b =>
                {
                    b.HasOne("DAL.Models.Especialidades", "Especialidades")
                        .WithMany()
                        .HasForeignKey("EspecialidadesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DAL.Models.Medicos", "Medicos")
                        .WithMany("GruposCitas")
                        .HasForeignKey("MedicosId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Especialidades");

                    b.Navigation("Medicos");
                });

            modelBuilder.Entity("DAL.Models.Medicamentos", b =>
                {
                    b.HasOne("DAL.Models.Recetas", "Recetas")
                        .WithMany("Medicamentos")
                        .HasForeignKey("RecetasId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Recetas");
                });

            modelBuilder.Entity("DAL.Models.PreciosEspecialidades", b =>
                {
                    b.HasOne("DAL.Models.Especialidades", "Especialidades")
                        .WithMany()
                        .HasForeignKey("EspecialidadesId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("DAL.Models.TiposSeguros", "TiposSeguros")
                        .WithMany()
                        .HasForeignKey("TiposSegurosId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Especialidades");

                    b.Navigation("TiposSeguros");
                });

            modelBuilder.Entity("DAL.Models.Recetas", b =>
                {
                    b.HasOne("DAL.Models.HistoriasClinicas", "historiaClinica")
                        .WithMany("Recetas")
                        .HasForeignKey("HistoriasClinicasId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("historiaClinica");
                });

            modelBuilder.Entity("DAL.Models.ResultadosEstudios", b =>
                {
                    b.HasOne("DAL.Models.HistoriasClinicas", null)
                        .WithMany("ResultadosEstudios")
                        .HasForeignKey("HistoriasClinicasId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DAL.Models.Users", b =>
                {
                    b.HasOne("DAL.Models.Personas", "Personas")
                        .WithMany()
                        .HasForeignKey("PersonasId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Personas");
                });

            modelBuilder.Entity("EspecialidadesMedicos", b =>
                {
                    b.HasOne("DAL.Models.Especialidades", null)
                        .WithMany()
                        .HasForeignKey("EspecialidadesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DAL.Models.Medicos", null)
                        .WithMany()
                        .HasForeignKey("MedicosId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("DAL.Models.Users", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("DAL.Models.Users", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DAL.Models.Users", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("DAL.Models.Users", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DAL.Models.Pacientes", b =>
                {
                    b.HasOne("DAL.Models.ContratosSeguros", "ContratosSeguros")
                        .WithOne("Pacientes")
                        .HasForeignKey("DAL.Models.Pacientes", "ContratosSegurosId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DAL.Models.HistoriasClinicas", "HistoriasClinicas")
                        .WithOne("Pacientes")
                        .HasForeignKey("DAL.Models.Pacientes", "HistoriasClinicasId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ContratosSeguros");

                    b.Navigation("HistoriasClinicas");
                });

            modelBuilder.Entity("DAL.Models.ContratosSeguros", b =>
                {
                    b.Navigation("Pacientes")
                        .IsRequired();
                });

            modelBuilder.Entity("DAL.Models.Facturas", b =>
                {
                    b.Navigation("Citas");
                });

            modelBuilder.Entity("DAL.Models.GruposCitas", b =>
                {
                    b.Navigation("Citas");
                });

            modelBuilder.Entity("DAL.Models.HistoriasClinicas", b =>
                {
                    b.Navigation("Diagnosticos");

                    b.Navigation("Pacientes")
                        .IsRequired();

                    b.Navigation("Recetas");

                    b.Navigation("ResultadosEstudios");
                });

            modelBuilder.Entity("DAL.Models.Recetas", b =>
                {
                    b.Navigation("Medicamentos");
                });

            modelBuilder.Entity("DAL.Models.TiposSeguros", b =>
                {
                    b.Navigation("Articulos");
                });

            modelBuilder.Entity("DAL.Models.Medicos", b =>
                {
                    b.Navigation("GruposCitas");
                });

            modelBuilder.Entity("DAL.Models.Pacientes", b =>
                {
                    b.Navigation("Citas");
                });
#pragma warning restore 612, 618
        }
    }
}
