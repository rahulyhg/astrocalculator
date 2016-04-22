using astrocalc.api.models;
using astrocalc.api.Models;
using astrocalc.api.Repos;
using Microsoft.AspNet.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Globalization;
using astrocalc.api.services.usnautical;
namespace astrocalc.api.Controllers {
    [Route("sunrises")]
    public class SunrisesController : Controller {
        protected Repo _repo = new Repo();
        [Route("")]
        [HttpGet]
        public ObjectResult Index() {
            var result = new SolarConfig() {
                Month = DateTime.Today.Month,
                Year = DateTime.Today.Year,
                CityChoices = _repo
                .QueryInterface<ICity>().Index().ToList<City>()
            };
            return new ObjectResult(result);
        }
        [HttpPost]
        [Route("")]
        public ObjectResult SunriseForMonth([FromBody]SolarConfig config) {
            List<SolarDetail> details = new List<SolarDetail>();
            for (int i = 1; i < DateTime.DaysInMonth(config.Year, config.Month); i++) {
                DateTime dt = new DateTime(config.Year, config.Month, i, 0, 0, 0); // this is the date to start with
                DateTime dtSunrise= dt.LocalSunrise(config.City.Longitude, config.City.Latitude, 82.00);
                details.Add(new SolarDetail() {
                    Sunrise = dtSunrise
                }); 
            }
            return new ObjectResult(details);
        }
    }
}
