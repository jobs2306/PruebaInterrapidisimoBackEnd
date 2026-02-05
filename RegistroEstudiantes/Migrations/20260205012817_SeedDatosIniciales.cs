using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RegistroEstudiantes.Migrations
{
    /// <inheritdoc />
    public partial class SeedDatosIniciales : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Materias",
                columns: new[] { "MateriaId", "Creditos", "Nombre" },
                values: new object[,]
                {
                    { 1, 3, "Matemáticas" },
                    { 2, 3, "Física" },
                    { 3, 3, "Química" },
                    { 4, 3, "Biología" },
                    { 5, 3, "Historia" },
                    { 6, 3, "Geografía" },
                    { 7, 3, "Programación" },
                    { 8, 3, "Bases de Datos" },
                    { 9, 3, "Redes" },
                    { 10, 3, "Ingeniería de Software" }
                });

            migrationBuilder.InsertData(
                table: "Profesores",
                columns: new[] { "ProfesorId", "Nombre" },
                values: new object[,]
                {
                    { 1, "Profesor A" },
                    { 2, "Profesor B" },
                    { 3, "Profesor C" },
                    { 4, "Profesor D" },
                    { 5, "Profesor E" }
                });

            migrationBuilder.InsertData(
                table: "ProfesorMaterias",
                columns: new[] { "ProfesorMateriaId", "MateriaId", "ProfesorId" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 2, 1 },
                    { 3, 3, 2 },
                    { 4, 4, 2 },
                    { 5, 5, 3 },
                    { 6, 6, 3 },
                    { 7, 7, 4 },
                    { 8, 8, 4 },
                    { 9, 9, 5 },
                    { 10, 10, 5 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ProfesorMaterias",
                keyColumn: "ProfesorMateriaId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ProfesorMaterias",
                keyColumn: "ProfesorMateriaId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ProfesorMaterias",
                keyColumn: "ProfesorMateriaId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ProfesorMaterias",
                keyColumn: "ProfesorMateriaId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "ProfesorMaterias",
                keyColumn: "ProfesorMateriaId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "ProfesorMaterias",
                keyColumn: "ProfesorMateriaId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "ProfesorMaterias",
                keyColumn: "ProfesorMateriaId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "ProfesorMaterias",
                keyColumn: "ProfesorMateriaId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "ProfesorMaterias",
                keyColumn: "ProfesorMateriaId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "ProfesorMaterias",
                keyColumn: "ProfesorMateriaId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Materias",
                keyColumn: "MateriaId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Materias",
                keyColumn: "MateriaId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Materias",
                keyColumn: "MateriaId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Materias",
                keyColumn: "MateriaId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Materias",
                keyColumn: "MateriaId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Materias",
                keyColumn: "MateriaId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Materias",
                keyColumn: "MateriaId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Materias",
                keyColumn: "MateriaId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Materias",
                keyColumn: "MateriaId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Materias",
                keyColumn: "MateriaId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Profesores",
                keyColumn: "ProfesorId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Profesores",
                keyColumn: "ProfesorId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Profesores",
                keyColumn: "ProfesorId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Profesores",
                keyColumn: "ProfesorId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Profesores",
                keyColumn: "ProfesorId",
                keyValue: 5);
        }
    }
}
