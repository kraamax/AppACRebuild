using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AppAC.Infrastructure.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Departamentos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CodigoDpto = table.Column<string>(type: "TEXT", nullable: true),
                    NombreDpto = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departamentos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TiposActividades",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NombreActividad = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiposActividades", x => x.Id);
                    table.UniqueConstraint("AK_TiposActividades_NombreActividad", x => x.NombreActividad);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Identificacion = table.Column<string>(type: "TEXT", nullable: true),
                    Nombres = table.Column<string>(type: "TEXT", nullable: true),
                    Apellidos = table.Column<string>(type: "TEXT", nullable: true),
                    UserName = table.Column<string>(type: "TEXT", nullable: true),
                    Password = table.Column<string>(type: "TEXT", nullable: true),
                    Email = table.Column<string>(type: "TEXT", nullable: true),
                    Sexo = table.Column<string>(type: "TEXT", nullable: true),
                    Discriminator = table.Column<string>(type: "TEXT", nullable: false),
                    Docente_DepartamentoId = table.Column<int>(type: "INTEGER", nullable: true),
                    DepartamentoId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Usuarios_Departamentos_DepartamentoId",
                        column: x => x.DepartamentoId,
                        principalTable: "Departamentos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Usuarios_Departamentos_Docente_DepartamentoId",
                        column: x => x.Docente_DepartamentoId,
                        principalTable: "Departamentos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Actividades",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TipoActividadId = table.Column<int>(type: "INTEGER", nullable: true),
                    ResponsableId = table.Column<int>(type: "INTEGER", nullable: true),
                    AsignadorId = table.Column<int>(type: "INTEGER", nullable: true),
                    HorasAsignadas = table.Column<int>(type: "INTEGER", nullable: false),
                    Estado = table.Column<string>(type: "TEXT", nullable: true),
                    FechaAsignacion = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Actividades", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Actividades_TiposActividades_TipoActividadId",
                        column: x => x.TipoActividadId,
                        principalTable: "TiposActividades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Actividades_Usuarios_AsignadorId",
                        column: x => x.AsignadorId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Actividades_Usuarios_ResponsableId",
                        column: x => x.ResponsableId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PlazosApertura",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CreadorId = table.Column<int>(type: "INTEGER", nullable: true),
                    FechaInicio = table.Column<DateTime>(type: "TEXT", nullable: false),
                    FechaFin = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Activo = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlazosApertura", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlazosApertura_Usuarios_CreadorId",
                        column: x => x.CreadorId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Planes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Fecha = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ActividadId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Planes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Planes_Actividades_ActividadId",
                        column: x => x.ActividadId,
                        principalTable: "Actividades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ItemsPlanes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false),
                    AccionPlaneada_Descripcion = table.Column<string>(type: "TEXT", nullable: true),
                    AccionRealizada_Evidencia_Ruta = table.Column<string>(type: "TEXT", nullable: true),
                    AccionRealizada_Evidencia_FechaCarga = table.Column<DateTime>(type: "TEXT", nullable: true),
                    AccionRealizada_Descripcion = table.Column<string>(type: "TEXT", nullable: true),
                    PlanAccionId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemsPlanes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemsPlanes_Planes_PlanAccionId",
                        column: x => x.PlanAccionId,
                        principalTable: "Planes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Departamentos",
                columns: new[] { "Id", "CodigoDpto", "NombreDpto" },
                values: new object[] { 1, "ss232", "Prueba" });

            migrationBuilder.InsertData(
                table: "Departamentos",
                columns: new[] { "Id", "CodigoDpto", "NombreDpto" },
                values: new object[] { 2, "ss432", "Ingeniería de Sistemas" });

            migrationBuilder.InsertData(
                table: "Departamentos",
                columns: new[] { "Id", "CodigoDpto", "NombreDpto" },
                values: new object[] { 3, "ss1233", "Ingeniería ambiental" });

            migrationBuilder.InsertData(
                table: "Departamentos",
                columns: new[] { "Id", "CodigoDpto", "NombreDpto" },
                values: new object[] { 4, "s165", "Licenciatura en Lenguas" });

            migrationBuilder.InsertData(
                table: "Departamentos",
                columns: new[] { "Id", "CodigoDpto", "NombreDpto" },
                values: new object[] { 5, "re342", "Ingeniería Electronica" });

            migrationBuilder.InsertData(
                table: "Departamentos",
                columns: new[] { "Id", "CodigoDpto", "NombreDpto" },
                values: new object[] { 6, "fs482", "Licenciatura en ciencias" });

            migrationBuilder.InsertData(
                table: "Departamentos",
                columns: new[] { "Id", "CodigoDpto", "NombreDpto" },
                values: new object[] { 7, "kg213", "Matematicas y Fisica" });

            migrationBuilder.InsertData(
                table: "TiposActividades",
                columns: new[] { "Id", "NombreActividad" },
                values: new object[] { 1, "Extensión" });

            migrationBuilder.InsertData(
                table: "TiposActividades",
                columns: new[] { "Id", "NombreActividad" },
                values: new object[] { 2, "Investigación" });

            migrationBuilder.InsertData(
                table: "TiposActividades",
                columns: new[] { "Id", "NombreActividad" },
                values: new object[] { 3, "Tutorias" });

            migrationBuilder.InsertData(
                table: "TiposActividades",
                columns: new[] { "Id", "NombreActividad" },
                values: new object[] { 4, "Asesorias" });

            migrationBuilder.InsertData(
                table: "Usuarios",
                columns: new[] { "Id", "Apellidos", "Docente_DepartamentoId", "Discriminator", "Email", "Identificacion", "Nombres", "Password", "Sexo", "UserName" },
                values: new object[] { 1, "Prueba p", 1, "Docente", "prueba2@prueba", "123prueba", "UsuarioD Doc", "777", "Masculino", "prueba2@prueba" });

            migrationBuilder.InsertData(
                table: "Usuarios",
                columns: new[] { "Id", "Apellidos", "DepartamentoId", "Discriminator", "Email", "Identificacion", "Nombres", "Password", "Sexo", "UserName" },
                values: new object[] { 2, "Prueba p", 1, "JefeDpto", "prueba1@prueba", "1233prueba", "UsuarioJ Jefe", "777", "Masculino", "prueba1@prueba" });

            migrationBuilder.CreateIndex(
                name: "IX_Actividades_AsignadorId",
                table: "Actividades",
                column: "AsignadorId");

            migrationBuilder.CreateIndex(
                name: "IX_Actividades_ResponsableId",
                table: "Actividades",
                column: "ResponsableId");

            migrationBuilder.CreateIndex(
                name: "IX_Actividades_TipoActividadId",
                table: "Actividades",
                column: "TipoActividadId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemsPlanes_PlanAccionId",
                table: "ItemsPlanes",
                column: "PlanAccionId");

            migrationBuilder.CreateIndex(
                name: "IX_Planes_ActividadId",
                table: "Planes",
                column: "ActividadId");

            migrationBuilder.CreateIndex(
                name: "IX_PlazosApertura_CreadorId",
                table: "PlazosApertura",
                column: "CreadorId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_DepartamentoId",
                table: "Usuarios",
                column: "DepartamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_Docente_DepartamentoId",
                table: "Usuarios",
                column: "Docente_DepartamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_Email",
                table: "Usuarios",
                column: "Email",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ItemsPlanes");

            migrationBuilder.DropTable(
                name: "PlazosApertura");

            migrationBuilder.DropTable(
                name: "Planes");

            migrationBuilder.DropTable(
                name: "Actividades");

            migrationBuilder.DropTable(
                name: "TiposActividades");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Departamentos");
        }
    }
}
