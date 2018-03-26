namespace TaxiCloud.WebApi.Controllers.Auth.Model
{

    public class LoginRequestModel
    {
        public Credentials Credentials { get; set; }
    }

    public class Credentials
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}