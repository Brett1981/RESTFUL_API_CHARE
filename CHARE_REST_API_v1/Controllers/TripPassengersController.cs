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
    public class TripPassengersController : ApiController
    {
        private CHARE_DBEntities db = new CHARE_DBEntities();

        // GET: api/TripPassengers
        public IQueryable<TripPassenger> GetTripPassengers()
        {
            return db.TripPassengers;
        }

        // GET: api/TripPassengers/5
        [ResponseType(typeof(TripPassenger))]
        public IHttpActionResult GetTripPassenger(int id)
        {
            TripPassenger tripPassenger = db.TripPassengers.Find(id);
            if (tripPassenger == null)
            {
                return NotFound();
            }

            return Ok(tripPassenger);
        }

        // Custom GET: api/TripPassengers/5
        [ResponseType(typeof(TripPassenger))]
        public IHttpActionResult GetTripPassenger(int id, string type)
        {
            List<TripPassenger> tripPassenger = null;
            // Get all trip details belong to the passenger with ID = id
            if (type.Equals("List"))            
                tripPassenger = db.TripPassengers.Where(t => t.PassengerID == id).ToList();

            if (tripPassenger == null)
            {
                return NotFound();
            }
            return Ok(tripPassenger);
        }

        // PUT: api/TripPassengers/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTripPassenger(int id, TripPassenger tripPassenger)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tripPassenger.TripPassengerID)
            {
                return BadRequest();
            }

            db.Entry(tripPassenger).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TripPassengerExists(id))
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

        // POST: api/TripPassengers
        [ResponseType(typeof(TripPassenger))]
        public IHttpActionResult PostTripPassenger(TripPassenger tripPassenger)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.TripPassengers.Add(tripPassenger);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = tripPassenger.TripPassengerID }, tripPassenger);
        }

        // DELETE: api/TripPassengers/5
        [ResponseType(typeof(TripPassenger))]
        public IHttpActionResult DeleteTripPassenger(int id)
        {
            TripPassenger tripPassenger = db.TripPassengers.Find(id);
            if (tripPassenger == null)
            {
                return NotFound();
            }

            db.TripPassengers.Remove(tripPassenger);
            db.SaveChanges();

            return Ok(tripPassenger);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TripPassengerExists(int id)
        {
            return db.TripPassengers.Count(e => e.TripPassengerID == id) > 0;
        }
    }
}