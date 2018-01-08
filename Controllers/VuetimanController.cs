using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using vuetiman_c.Models;
using vuetiman_c.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace vuetiman_c.Controllers
{
    [Route("api/[controller]")]
    public class VuetimanController : Controller
    {
        private readonly VuetimanRepository db;
        public VuetimanController(VuetimanRepository VuetimanRepo)
        {
            db = VuetimanRepo;
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<Vuetiman> Get()
        {
            return db.GetAll();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public Vuetiman Get(int id)
        {
            return db.GetById(id);
        }

        // POST api/values
        [HttpPost]
        public Vuetiman Post([FromBody]Vuetiman Vuetiman)
        {
            return db.Add(Vuetiman);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public Vuetiman Put(int id, [FromBody]Vuetiman Vuetiman)
        {
            if (ModelState.IsValid)
            {
                return db.GetOneByIdAndUpdate(id, Vuetiman);
            }
            return null;
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public string Delete(int id)
        {
            return db.FindByIdAndRemove(id);
        }
    }
}
