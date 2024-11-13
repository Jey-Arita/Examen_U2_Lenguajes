using PartidasContables.DataBase.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PartidasContables.Dtos.DetallePartidaDto
{
    public class DetallePartidaDto
    {
       public Guid Id { get; set; }
        public Guid IdPartida { get; set; }
        public Guid IdCatalogoCuenta { get; set; }
        public string Descripcion { get; set; } // Ej: "Ventas de producto"
        public decimal Monto { get; set; }
    }
}
