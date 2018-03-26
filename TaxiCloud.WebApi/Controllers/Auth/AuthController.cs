using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TaxiCloud.Core.IoC;
using TaxiCloud.Core.Repository;
using TaxiCloud.Core.Repository.Model;
using TaxiCloud.WebApi.Controllers.Auth.Model;
using TaxiCloud.WebApi.Utils;

namespace TaxiCloud.WebApi.Controllers.Auth
{
    public class AuthController : ApiController
    {

        [HttpPost]
        //[EnableCors(origins: "*", headers: "*", methods: "POST")]
        public object Login(LoginRequestModel credentials)
        {
            var sql = IocKernel.Get<SqlRepository>();
            var user = sql.Users.FirstOrDefault(x => x.Email == credentials.Credentials.Email && x.Password == credentials.Credentials.Password);
            if (user == null)
                ThrowResponseException(HttpStatusCode.BadRequest, "Такой пары email и пароля не существует!");
            return Json(new { success = true, user = new { email = user.Email, token = user.GetToken(), type = user.Type } });
        }

        [HttpPost]
        //[EnableCors(origins: "*", headers: "*", methods: "POST")]
        public object Signup(SignupRequestModel user)
        {
            var sql = IocKernel.Get<SqlRepository>();
            if (sql.Users.FirstOrDefault(x => x.Email == user.User.Email) != null)
                ThrowResponseException(HttpStatusCode.BadRequest, "Этот email уже используется в системе!");
            var newUser = new User()
            {
                Email = user.User.Email,
                Password = user.User.Password,
                Fio = "",
                Type = -1,
                Login = "",
                Phone = ""
            };
            sql.Users.Add(newUser);
            sql.SaveChanges();
            var res = sql.Users.ToList();
            if (newUser.Id <= 0)
                ThrowResponseException(HttpStatusCode.BadRequest, "Внутрення ошибка, попробуйте позже!");
            return Json(new { success = true, user = new { email = newUser.Email, token = newUser.GetToken(), type = newUser.Type } });
        }

        private void ThrowResponseException(HttpStatusCode statusCode, string message)
        {
            var errorResponse = Request.CreateErrorResponse(statusCode, message);
            throw new HttpResponseException(errorResponse);
        }
    }
}