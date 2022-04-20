using System.ComponentModel.DataAnnotations;

namespace EasyWorkEntities.Usuario.Request
{
    public class RegistrarDispositivoRequest
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "El key del dispositivo es obligatorio.")]
        public string keyDispositivo { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "La versión de android es obligatorio.")]
        public string versionAndroid { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "La versión del app es obligatorio.")]
        public string versionApp { get; set; }
        public double latitud { get; set; }
        public double longitud { get; set; }
    }
}
