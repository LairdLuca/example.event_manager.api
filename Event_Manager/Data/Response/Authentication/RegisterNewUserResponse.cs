namespace Event_Manager.Data.Response.Authentication
{
    public class RegisterNewUserResponse
    {
#if DEBUG
        public required string RegistrationCode { get; set; }
#endif

        public required string RegisteredEmail { get; set; }
        public required string ExpirationDateTime { get; set; }
    }
}
