using PartidasContables.DataBase.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using PartidasContables.Dtos.DetallePartidaDto;

namespace PartidasContables.Dtos.Partida
{
    public class PartidaDto
    {
        public Guid Id { get; set; }
        public DateTime Fecha { get; set; }
        public string Descripcion { get; set; } // Descripción de la partida o sinopsis
        public bool Desactivada { get; set; } = false; // Estado de la partida
        public string IdUsuario { get; set; }
        public List<DetallesPartidaDto> Detalles { get; set; }

    }
}
