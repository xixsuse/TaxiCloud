namespace TaxiCloud.WebApi.Controllers.Auth.Model
{
    public class SignupRequestModel
    {
        public SignupUser User { get; set; }
    }

    public class SignupUser
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Fio { get; set; }
        public string Phone { get; set; }
    }
}