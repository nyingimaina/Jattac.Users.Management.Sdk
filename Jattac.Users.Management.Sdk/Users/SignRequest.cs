namespace Jattac.Users.Management.Sdk.Users
{
    public class SignRequest
    {
        public string Username { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public Guid Id { get; set; }
    }
}