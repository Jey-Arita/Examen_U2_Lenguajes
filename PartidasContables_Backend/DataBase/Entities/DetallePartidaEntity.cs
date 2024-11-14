using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PartidasContables.DataBase.Entities
{
    [Table("detalle_partidas", Schema = "dbo")]
    public class DetallePartidaEntity : BaseEntity
    {
        [Column("id_partida")]
        [Required]
        public Guid IdPartida { get; set; }

        [ForeignKey(nameof(IdPartida))]
        public virtual PartidaEntity Partida { get; set; }

        [Column("id_catalogo_cuenta")]
        [Required]
        public Guid IdCatalogoCuenta { get; set; }

        [ForeignKey(nameof(IdCatalogoCuenta))]
        public CatalogoCuentaEntity CatalogoCuenta { get; set; }

        [Column("descripcion")]
        [Required]
        public string Descripcion { get; set; } // Ej: "Ventas de producto"

        [Column("monto", TypeName = "decimal(18,2)")]
        public decimal Monto { get; set; }

        [Column("tipo_movimiento")]
        public string TipoMovimiento { get; set; }

    }
}
