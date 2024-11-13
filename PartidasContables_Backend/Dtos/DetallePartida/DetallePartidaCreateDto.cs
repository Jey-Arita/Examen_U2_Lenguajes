using System.ComponentModel.DataAnnotations;

namespace PartidasContables.Dtos.DetallePartida
{
    public class DetallePartidaCreateDto
    {
        [Required]
        public string Descripcion { get; set; } // Ej: "Ventas de producto"
        [Required]
        public decimal Monto { get; set; }
    }
}
