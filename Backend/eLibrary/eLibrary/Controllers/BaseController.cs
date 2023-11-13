using eLibrary.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace eLibrary.Controllers
{
    public class BaseController : ControllerBase
    {
        public BaseController(IJwtService jwtService)
        {
            JwtService = jwtService;
        }

        #region -- Protected properties --

        protected IJwtService JwtService { get; }

        #endregion

        #region -- Protected helpers --

        protected IActionResult CheckAuthorization(Func<IActionResult> func)
        {
            var isTokenVerified = false;

            try
            {
                var jwt = Request.Headers.Authorization[0]?.Split(' ')[1];

                var token = JwtService.Verify(jwt);

                isTokenVerified = token is not null;
            }
            catch (Exception ex)
            {
                isTokenVerified = false;
            }

            if (isTokenVerified)
            {
                return func();
            }
            else
            {
                return Unauthorized();
            }
        }

        #endregion
    }
}
