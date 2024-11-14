
namespace PartidasContables.Dtos.DetallePartidaDto
{
    public class DetallesPartidaDto
    {
       public Guid Id { get; set; }
        public Guid IdPartida { get; set; }
        public Guid IdCatalogoCuenta { get; set; }
        public string Descripcion { get; set; } // Ej: "Ventas de producto"
        public decimal Monto { get; set; }
        public string TipoMovimiento { get; set; }
    }
}
