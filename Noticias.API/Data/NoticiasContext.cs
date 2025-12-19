using Microsoft.EntityFrameworkCore;
using Noticias.API.Models;

namespace Noticias.API.Data
{
    public class NoticiasContext : DbContext
    {
        public NoticiasContext(DbContextOptions<NoticiasContext> options) : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Pais> Paises { get; set; }
        public DbSet<Noticia> Noticias { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Seed de Usuarios
            modelBuilder.Entity<Usuario>().HasData(
                new Usuario { Id = 1, Nombre = "Admin Principal", Email = "admin@noticias.com", Password = "admin123", Rol = "Administrador" },
                new Usuario { Id = 2, Nombre = "Carlos Editor", Email = "carlos@noticias.com", Password = "carlos123", Rol = "Editor" },
                new Usuario { Id = 3, Nombre = "María López", Email = "maria@noticias.com", Password = "maria123", Rol = "Editor" },
                new Usuario { Id = 4, Nombre = "Juan Pérez", Email = "juan@noticias.com", Password = "juan123", Rol = "Administrador" }
            );

            // Seed de Categorías
            modelBuilder.Entity<Categoria>().HasData(
                new Categoria { Id = 1, Nombre = "Política" },
                new Categoria { Id = 2, Nombre = "Deportes" },
                new Categoria { Id = 3, Nombre = "Tecnología" },
                new Categoria { Id = 4, Nombre = "Economía" },
                new Categoria { Id = 5, Nombre = "Entretenimiento" }
            );

            // Seed de Países
            modelBuilder.Entity<Pais>().HasData(
                new Pais { Id = 1, Nombre = "República Dominicana" },
                new Pais { Id = 2, Nombre = "México" },
                new Pais { Id = 3, Nombre = "España" },
                new Pais { Id = 4, Nombre = "Argentina" },
                new Pais { Id = 5, Nombre = "Colombia" }
            );

            // Seed de Noticias
            modelBuilder.Entity<Noticia>().HasData(
                new Noticia
                {
                    Id = 1,
                    Titulo = "Nueva inversión tecnológica en RD",
                    Contenido = "El gobierno anuncia una importante inversión en infraestructura tecnológica para impulsar la transformación digital del país.",
                    FechaPublicacion = DateTime.Now.AddDays(-5),
                    CategoriaId = 3,
                    PaisId = 1,
                    UsuarioId = 2
                },
                new Noticia
                {
                    Id = 2,
                    Titulo = "Campeonato de béisbol inicia próximo mes",
                    Contenido = "La liga dominicana de béisbol anuncia el inicio de la nueva temporada con grandes expectativas.",
                    FechaPublicacion = DateTime.Now.AddDays(-3),
                    CategoriaId = 2,
                    PaisId = 1,
                    UsuarioId = 2
                },
                new Noticia
                {
                    Id = 3,
                    Titulo = "Elecciones presidenciales en México",
                    Contenido = "Se aproximan las elecciones presidenciales con varios candidatos en la contienda electoral.",
                    FechaPublicacion = DateTime.Now.AddDays(-2),
                    CategoriaId = 1,
                    PaisId = 2,
                    UsuarioId = 3
                },
                new Noticia
                {
                    Id = 4,
                    Titulo = "Startup española revoluciona el mercado",
                    Contenido = "Una nueva empresa tecnológica española lanza una innovadora plataforma de inteligencia artificial.",
                    FechaPublicacion = DateTime.Now.AddDays(-1),
                    CategoriaId = 3,
                    PaisId = 3,
                    UsuarioId = 3
                },
                new Noticia
                {
                    Id = 5,
                    Titulo = "Economía argentina muestra signos de recuperación",
                    Contenido = "Los indicadores económicos muestran una tendencia positiva en el último trimestre del año.",
                    FechaPublicacion = DateTime.Now.AddHours(-12),
                    CategoriaId = 4,
                    PaisId = 4,
                    UsuarioId = 2
                },
                new Noticia
                {
                    Id = 6,
                    Titulo = "Festival de cine en Colombia",
                    Contenido = "Bogotá se prepara para recibir el festival internacional de cine con películas de todo el mundo.",
                    FechaPublicacion = DateTime.Now.AddHours(-6),
                    CategoriaId = 5,
                    PaisId = 5,
                    UsuarioId = 3
                },
                new Noticia
                {
                    Id = 7,
                    Titulo = "Inteligencia artificial en la medicina dominicana",
                    Contenido = "Hospitales dominicanos implementan sistemas de IA para mejorar diagnósticos y atención al paciente.",
                    FechaPublicacion = DateTime.Now.AddHours(-2),
                    CategoriaId = 3,
                    PaisId = 1,
                    UsuarioId = 2
                }
            );
        }
    }
}