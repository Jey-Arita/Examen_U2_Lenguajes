using System.ComponentModel.DataAnnotations;

namespace PartidasContables.Dtos.Partida
{
    public class PartidaCreateDto
    {
        [Required]
        [MaxLength(500)]
        public string Descripcion { get; set; } // Descripción de la partida o sinopsis

        [Required]
        public string IdUsuario { get; set; }
    }
}
