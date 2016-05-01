using astrocalc.app.repos;
using astrocalc.app.storemodels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;

namespace astrocalc.api.Controllers
{
    [RoutePrefix("useraccounts")]
    [EnableCors(origins:"*", headers:"*",methods:"*")]
    public class UserAccountsController : AnyController
    {
        [HttpGet]
        [Route("{username}/{pin}")]
        public async Task<IHttpActionResult> GetOfIdPin(string username, string pin) {
            try {
                UserAccount account =await (_repo.QueryInterface<IUserAccount>()).Login(username, pin);
                if (account != null) {
                    return Ok<UserAccount>(account);
                }
                else {
                    account = (await (_repo.QueryInterface<IUserAccount>()).OfId(username));
                    if (account == null) {
                        return NotFound();
                    }
                    else {
                        return BadRequest();
                    }
                }
            }
            catch (Exception) {
                return InternalServerError();
            }
            
        }

    }
}
