using astrocalc.api.Models;
using Microsoft.AspNet.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace astrocalc.api.Controllers
{
    [Route("solar")]
    public class SunrisesController
    {
        [Route("")]
        public SolarConfig Index() {
            return new SolarConfig() {Month = DateTime.Today.Month, Year = DateTime.Today.Year};
        }
        
    }
}
