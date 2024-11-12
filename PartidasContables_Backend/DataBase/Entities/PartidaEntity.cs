using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PartidasContables.DataBase.Entities
{
    public class PartidaEntity : BaseEntity
    {
        [Required]
        public DateTime Fecha { get; set; }

        [MaxLength(500)]
        public string Descripcion { get; set; }

        // Estado de la partida: si está activa o eliminada
        public bool EstaEliminada { get; set; } = false;

        [Required]
        public string IdUsuario { get; set; }
        [ForeignKey(nameof(IdUsuario))]
        public virtual UserEntity Usuario { get; set; }

        // Relacion con DetallePartidaEntity
        public ICollection<DetallePartidaEntity> Detalles { get; set; } = new List<DetallePartidaEntity>();
    }

}
