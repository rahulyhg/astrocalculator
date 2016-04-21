using astrocalc.api.models;
using astrocalc.api.Repos;
using Microsoft.AspNet.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace astrocalc.api.Controllers {
    [Route("cities")]
    public class CitiesController {
        protected Repo _repo = new Repo();
        [Route("")]
        public List<City> Index() {
            return _repo.QueryInterface<ICity>().Index().ToList<City>();
        }
    }
}
