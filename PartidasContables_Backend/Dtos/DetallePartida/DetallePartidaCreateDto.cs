using PartidasContables.DataBase.Entities;
using PartidasContables.Dtos.Partida;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace PartidasContables.Dtos.DetallePartida
{
    public class DetallePartidaCreateDto
    {
        public Guid IdPartida { get; set; }
        public Guid IdCatalogoCuenta { get; set; }
        public string Descripcion { get; set; } // Ej: "Ventas de producto"
        public decimal Monto { get; set; }
        public string TipoMovimiento { get; set; }
    }
}
