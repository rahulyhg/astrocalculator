using astrocalc.app.repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace astrocalc.api.Controllers
{
    public class AnyController : ApiController
    {
        protected Repo _repo = new Repo();
    }
}
