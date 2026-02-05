namespace RegistroEstudiantes.Infraestructura.Data
{
    using Microsoft.EntityFrameworkCore;
    using RegistroEstudiantes.Dominio.Entidades;

    /// <summary>
    /// DbContext principal de la aplicación
    /// </summary>
    public class RegistroEstudiantesDbContext : DbContext
    {
        public RegistroEstudiantesDbContext(
        DbContextOptions<RegistroEstudiantesDbContext> options)
        : base(options)
        {
        }

        public DbSet<Estudiante> Estudiantes => Set<Estudiante>();
        public DbSet<Materia> Materias => Set<Materia>();
        public DbSet<Profesor> Profesores => Set<Profesor>();
        public DbSet<ProfesorMateria> ProfesorMaterias => Set<ProfesorMateria>();
        public DbSet<EstudianteMateria> EstudianteMaterias => Set<EstudianteMateria>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            ConfigurarEstudiante(modelBuilder);
            ConfigurarMateria(modelBuilder);
            ConfigurarProfesor(modelBuilder);
            ConfigurarProfesorMateria(modelBuilder);
            ConfigurarEstudianteMateria(modelBuilder);

            SeedMaterias(modelBuilder);
            SeedProfesores(modelBuilder);
            SeedProfesorMaterias(modelBuilder);

            SeedEstudiantes(modelBuilder);
        }

        private static void ConfigurarEstudiante(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Estudiante>(entity =>
            {
                entity.HasKey(e => e.EstudianteId);

                entity.Property(e => e.Nombre)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(e => e.Email)
                      .IsRequired()
                      .HasMaxLength(150);

                entity.HasIndex(e => e.Email)
                      .IsUnique();

                entity.Property(e => e.PasswordHash)
                      .IsRequired();

                entity.Property(e => e.FechaRegistro)
                      .IsRequired();
            });
        }

        private static void ConfigurarMateria(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Materia>(entity =>
            {
                entity.HasKey(m => m.MateriaId);

                entity.Property(m => m.Nombre)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(m => m.Creditos)
                      .IsRequired();
            });
        }

        private static void ConfigurarProfesor(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Profesor>(entity =>
            {
                entity.HasKey(p => p.ProfesorId);

                entity.Property(p => p.Nombre)
                      .IsRequired()
                      .HasMaxLength(100);
            });
        }

        private static void ConfigurarProfesorMateria(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProfesorMateria>(entity =>
            {
                entity.HasKey(pm => pm.ProfesorMateriaId);

                entity.HasOne(pm => pm.Profesor)
                      .WithMany(p => p.ProfesorMaterias)
                      .HasForeignKey(pm => pm.ProfesorId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(pm => pm.Materia)
                      .WithMany(m => m.ProfesorMaterias)
                      .HasForeignKey(pm => pm.MateriaId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasIndex(pm => new { pm.ProfesorId, pm.MateriaId })
                      .IsUnique();
            });
        }


        private static void ConfigurarEstudianteMateria(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EstudianteMateria>(entity =>
            {
                entity.HasKey(em => em.EstudianteMateriaId);

                entity.Property(em => em.FechaRegistro)
                      .IsRequired();

                entity.HasOne(em => em.Estudiante)
                      .WithMany(e => e.EstudianteMaterias)
                      .HasForeignKey(em => em.EstudianteId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(em => em.Materia)
                      .WithMany(m => m.EstudianteMaterias)
                      .HasForeignKey(em => em.MateriaId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(em => em.Profesor)
                      .WithMany(m => m.EstudianteMaterias)
                      .HasForeignKey(em => em.ProfesorId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasIndex(em => new
                {
                    em.EstudianteId,
                    em.MateriaId,
                    em.ProfesorId
                }).IsUnique();
            });
        }

        /// <summary>
        /// Insercion de las materias
        /// </summary>
        /// <param name="modelBuilder">ModelBuilder</param>
        private static void SeedMaterias(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Materia>().HasData(
                new Materia { MateriaId = 1, Nombre = "Matemáticas", Creditos = 3 },
                new Materia { MateriaId = 2, Nombre = "Física", Creditos = 3 },
                new Materia { MateriaId = 3, Nombre = "Química", Creditos = 3 },
                new Materia { MateriaId = 4, Nombre = "Biología", Creditos = 3 },
                new Materia { MateriaId = 5, Nombre = "Historia", Creditos = 3 },
                new Materia { MateriaId = 6, Nombre = "Geografía", Creditos = 3 },
                new Materia { MateriaId = 7, Nombre = "Programación", Creditos = 3 },
                new Materia { MateriaId = 8, Nombre = "Bases de Datos", Creditos = 3 },
                new Materia { MateriaId = 9, Nombre = "Redes", Creditos = 3 },
                new Materia { MateriaId = 10, Nombre = "Ingeniería de Software", Creditos = 3 }
            );
        }

        /// <summary>
        /// Insersion de los profesores
        /// </summary>
        /// <param name="modelBuilder">Model builder</param>
        private static void SeedProfesores(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Profesor>().HasData(
                new Profesor { ProfesorId = 1, Nombre = "Profesor A" },
                new Profesor { ProfesorId = 2, Nombre = "Profesor B" },
                new Profesor { ProfesorId = 3, Nombre = "Profesor C" },
                new Profesor { ProfesorId = 4, Nombre = "Profesor D" },
                new Profesor { ProfesorId = 5, Nombre = "Profesor E" }
            );
        }

        /// <summary>
        /// Insercion de la relacion de profesor con sus materias
        /// </summary>
        /// <param name="modelBuilder">Model builder</param>
        private static void SeedProfesorMaterias(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProfesorMateria>().HasData(
                new ProfesorMateria { ProfesorMateriaId = 1, ProfesorId = 1, MateriaId = 1 },
                new ProfesorMateria { ProfesorMateriaId = 2, ProfesorId = 1, MateriaId = 2 },

                new ProfesorMateria { ProfesorMateriaId = 3, ProfesorId = 2, MateriaId = 3 },
                new ProfesorMateria { ProfesorMateriaId = 4, ProfesorId = 2, MateriaId = 4 },

                new ProfesorMateria { ProfesorMateriaId = 5, ProfesorId = 3, MateriaId = 5 },
                new ProfesorMateria { ProfesorMateriaId = 6, ProfesorId = 3, MateriaId = 6 },

                new ProfesorMateria { ProfesorMateriaId = 7, ProfesorId = 4, MateriaId = 7 },
                new ProfesorMateria { ProfesorMateriaId = 8, ProfesorId = 4, MateriaId = 8 },

                new ProfesorMateria { ProfesorMateriaId = 9, ProfesorId = 5, MateriaId = 9 },
                new ProfesorMateria { ProfesorMateriaId = 10, ProfesorId = 5, MateriaId = 10 }
            );
        }

        /// <summary>
        /// Insercion de los estudiantes
        /// </summary>
        /// <param name="modelBuilder">Model builder</param>
        private static void SeedEstudiantes(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Estudiante>().HasData(
                new Estudiante
                {
                    EstudianteId = 1,
                    Nombre = "Estudiante 1",
                    Email = "est1@test.com",
                    PasswordHash = "AQAAAAEAACcQAAAAEOnF8x0WUbM8YSc+i+fQUzdm5mhAzwg8JpjY6LzILLoZeriV0rS6zBPD+3s6wuaZ6g==",
                    FechaRegistro = new DateTime(2025, 01, 01)
                },
                new Estudiante
                {
                    EstudianteId = 2,
                    Nombre = "Estudiante 2",
                    Email = "est2@test.com",
                    PasswordHash = "AQAAAAEAACcQAAAAEP/J8wn5a4N3b3JLaPZw7FnQrbvm7wstgqzIU6BKqe90mdjiAwqOz5/ajDUnpTU0yQ==",
                    FechaRegistro = new DateTime(2025, 01, 01)
                }
            );
        }
    }
}
