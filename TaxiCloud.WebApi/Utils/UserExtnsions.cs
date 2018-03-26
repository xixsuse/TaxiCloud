using TaxiCloud.Core.Repository.Model;

namespace TaxiCloud.WebApi.Utils
{
    public static class UserExtnsions
    {
        public static string GetToken(this User user)
        {
            return StringCipher.Encrypt(user.Email, "mysupersecretword");
        }

        public static string GetEmailFromToken(this string token)
        {
            return StringCipher.Decrypt(token, "mysupersecretword");
        }
    }
}