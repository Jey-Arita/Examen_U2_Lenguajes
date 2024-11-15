using System.ComponentModel.DataAnnotations;

namespace PartidasContables.Dtos.LogDatabase
{
    public class LogDatabaseCreateDto
    {
        public DateTime Fecha {  get; set; }
        [Required]
        public string IdUsuario { get; set; }
        [Required]
        public TipoAccion Accion { get; set; }
        public Guid? IdPartida { get; set; }
        public Guid? IdCatalogo { get; set; }
        public Dictionary<string, object> Detalles { get; set; }
    }

    public enum TipoAccion
    {
        Login,
        Logout,
        CreacionPartida,
        ModificacionPartida,
        CreacionCatalogo,
        ModificacionCatalogo,
        EliminacionCatalogo
    }
}
