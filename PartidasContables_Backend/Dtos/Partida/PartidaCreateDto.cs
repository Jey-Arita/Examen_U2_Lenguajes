using PartidasContables.Dtos.DetallePartida;
using PartidasContables.Dtos.DetallePartidaDto;
using System.ComponentModel.DataAnnotations;



namespace PartidasContables.Dtos.Partida
{
    public class PartidaCreateDto
    {
        public string Descripcion { get; set; } // Descripción de la partida o sinopsis
        public string IdUsuario { get; set; }
        public DateTime Fecha { get; set; }
        public List<DetallePartidaCreateDto> Detalles { get; set; }
    }
}
