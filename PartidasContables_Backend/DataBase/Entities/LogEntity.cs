using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PartidasContables.DataBase.Entities
{
    [Table("logs", Schema = "dbo")]
    public class LogEntity : BaseEntity
    {
        public DateTime Fecha { get; set; }

        [Required]
        public string IdUsuario { get; set; }

        [Required]
        [MaxLength(100)]
        public string Accion { get; set; } // Ej: "Crear Partida" o "Eliminar Partida"

        public Guid? IdPartida { get; set; }

        [ForeignKey("IdPartida")]
        public virtual PartidaEntity Partida { get; set; }
    }
}
