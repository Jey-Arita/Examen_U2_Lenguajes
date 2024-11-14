using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace PartidasContables.DataBase.Entities
{
    [Table("partidas", Schema = "dbo")]
    public class PartidaEntity : BaseEntity
    {
        [Column("fecha")]
        [Required]
        public DateTime Fecha { get; set; }

        [Column("descripcion")]
        [MaxLength(500)]
        [Required] // Solo si la descripción es obligatoria
        public string Descripcion { get; set; } // Descripción de la partida o sinopsis

        [Column("desactivada")]
        public bool Desactivada { get; set; } = false; // Estado de la partida

        [Column("id_usuario")]
        [Required]
        public string IdUsuario { get; set; }

        [ForeignKey(nameof(IdUsuario))]
        public virtual UserEntity Usuario { get; set; }

        // Relación con DetallePartidaEntity
        public List<DetallePartidaEntity> Detalles { get; set; } = new List<DetallePartidaEntity>();
    }
}
