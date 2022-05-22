namespace EasyWorkEntities.Cliente.Request
{
    public class RegistrarSolicitudServicioRequest
    {
        public string codUsuarioTecnicoCategoria { get; set; }
        public string codDistrito { get; set; }
        public string codMedioPago { get; set; }
        public string codCategoriaServicio { get; set; }
        public string codTipoBusqueda { get; set; }
        public string codPerfilTecnico { get; set; }
        public string direccion { get; set; }
        public string descripcionProblema { get; set; }
        public double latitud { get; set; }
        public double longitud { get; set; }
    }
}
