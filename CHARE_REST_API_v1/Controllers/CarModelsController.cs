using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using CHARE_REST_API_v1.Models;

namespace CHARE_REST_API_v1.Controllers
{
    public class CarModelsController : ApiController
    {
        private CHARE_DBEntities db = new CHARE_DBEntities();

        // GET: api/CarModels
        public IQueryable<string> GetCarModels()
        {
            var carmodels = db.CarModels;

            var m = (from c in carmodels
                     orderby c.make
                     select c.make).Distinct();
            return m;
        }

        // GET: api/CarModels        
        public IQueryable<string> GetCarModels(string make)
        {
            var carmodels = db.CarModels;

            var m = (from c in carmodels
                     where c.make == make
                     orderby c.model
                     select c.model).Distinct();
            return m;

        }
        // GET: api/CarModels/5
        [ResponseType(typeof(CarModel))]
        public IHttpActionResult GetCarModel(int id)
        {
            CarModel carModel = db.CarModels.Find(id);
            if (carModel == null)
            {
                return NotFound();
            }

            return Ok(carModel);
        }

        // PUT: api/CarModels/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCarModel(int id, CarModel carModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != carModel.CarModelID)
            {
                return BadRequest();
            }

            db.Entry(carModel).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarModelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/CarModels
        [ResponseType(typeof(CarModel))]
        public IHttpActionResult PostCarModel(CarModel carModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.CarModels.Add(carModel);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = carModel.CarModelID }, carModel);
        }

        // DELETE: api/CarModels/5
        [ResponseType(typeof(CarModel))]
        public IHttpActionResult DeleteCarModel(int id)
        {
            CarModel carModel = db.CarModels.Find(id);
            if (carModel == null)
            {
                return NotFound();
            }

            db.CarModels.Remove(carModel);
            db.SaveChanges();

            return Ok(carModel);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CarModelExists(int id)
        {
            return db.CarModels.Count(e => e.CarModelID == id) > 0;
        }
    }
}