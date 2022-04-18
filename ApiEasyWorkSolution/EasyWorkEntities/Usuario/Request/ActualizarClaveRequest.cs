namespace EasyWorkEntities.Usuario.Request
{
    public class ActualizarClaveRequest
    {
        public string recoveryCode { get; set; }
        public string username { get; set; }
        public string newPassword { get; set; }
    }
}
