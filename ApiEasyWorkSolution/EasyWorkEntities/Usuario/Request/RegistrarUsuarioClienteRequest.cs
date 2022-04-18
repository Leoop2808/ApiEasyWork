using System.ComponentModel.DataAnnotations;

namespace EasyWorkEntities.Usuario.Request
{
    public class RegistrarUsuarioClienteRequest
    {
        public DataPersona datosPersona { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "La contraseña es obligatoria.")]
        [MinLength(8, ErrorMessage = "La contraseña debe contener un minimo de 8 caracteres.")]
        public string contrasenia { get; set; }
    }

    public class DataPersona
    {
        private string _correo;
        [Required(AllowEmptyStrings = false, ErrorMessage = "El correo es obligatorio.")]
        [MaxLength(150, ErrorMessage = "El correo excede los caracteres permitidos. (200 caract.)")]
        [EmailAddress(ErrorMessage = "Debe ingresar un correo válido.")]
        public string correo { get { return _correo; } set { _correo = string.IsNullOrWhiteSpace(value) ? null : value; } }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El número de celular es obligatorio.")]
        public string celular { get; set; }
        public string nombre1 { get; set; }
        public string nombre2 { get; set; }
        public string apellido1 { get; set; }
        public string apellido2 { get; set; }
        public string codTipoDocumento { get; set; }
        public string documento { get; set; }
        public string genero { get; set; }
        public double latitud { get; set; }
        public double longitud { get; set; }
        public string codDistrito { get; set; }
    }
}
