using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RegistroEstudiantes.Migrations
{
    /// <inheritdoc />
    public partial class SeedEstudiantes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Estudiantes",
                columns: new[] { "EstudianteId", "Email", "FechaRegistro", "Nombre", "PasswordHash" },
                values: new object[,]
                {
                    { 1, "est1@test.com", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Estudiante 1", "AQAAAAEAACcQAAAAEOnF8x0WUbM8YSc+i+fQUzdm5mhAzwg8JpjY6LzILLoZeriV0rS6zBPD+3s6wuaZ6g==" },
                    { 2, "est2@test.com", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Estudiante 2", "AQAAAAEAACcQAAAAEP/J8wn5a4N3b3JLaPZw7FnQrbvm7wstgqzIU6BKqe90mdjiAwqOz5/ajDUnpTU0yQ==" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Estudiantes",
                keyColumn: "EstudianteId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Estudiantes",
                keyColumn: "EstudianteId",
                keyValue: 2);
        }
    }
}
