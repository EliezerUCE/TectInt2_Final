namespace Noticias.API.Models
{
    public class Noticia
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Contenido { get; set; }
        public DateTime FechaPublicacion { get; set; }
        public int CategoriaId { get; set; }
        public Categoria Categoria { get; set; }
        public int PaisId { get; set; }
        public Pais Pais { get; set; }
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
    }
}