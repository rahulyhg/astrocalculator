using Microsoft.AspNet.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace astrocalc.api.Controllers
{
    [Route("sunrises")]
    public class SunrisesController
    {
        [Route("")]
        public string Ping() {
            return "this is hi from inside the sunrises controller";
        }
    }
}
