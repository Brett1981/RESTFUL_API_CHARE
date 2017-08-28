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
using CHARE_REST_API.Class;

namespace CHARE_REST_API_v1.Controllers
{
    public class TripDriversController : ApiController
    {
        private CHARE_DBEntities db = new CHARE_DBEntities();

        // GET: api/TripDrivers
        public IQueryable<TripDriver> GetTripDrivers()
        {
            return db.TripDrivers;
        }

        // GET: api/TripDrivers/5
        [ResponseType(typeof(TripDriver))]
        public IHttpActionResult GetTripDriver(int id)
        {
            TripDriver tripDriver = db.TripDrivers.Find(id);
            if (tripDriver == null)
            {
                return NotFound();
            }

            return Ok(tripDriver);
        }

        [ResponseType(typeof(TripDriver))]
        public IHttpActionResult GetTripDriver(int id, string type)
        {
            List<TripDriver> tripDriver = null;
            if (type.Equals("List"))
                tripDriver = db.TripDrivers.Where(t => t.DriverID == id).ToList();
            else if (type.Equals("Search"))
            {
                var passTrip = db.TripPassengers.Where(t => t.TripPassengerID == id).Select(t => t).FirstOrDefault();
                string femaleOnly = passTrip.femaleOnly;
                string originLatLng = passTrip.originLatLng;
                string destLatLng = passTrip.destinationLatLng;
                TimeSpan time = passTrip.arriveTime;
                Haversine h = new Haversine();
                tripDriver = db.TripDrivers.Where(t => t.femaleOnly == femaleOnly && t.availableSeat >= 1 && (TimeSpan.Compare(time, t.arriveTime) >= 0)).AsEnumerable()
                    .Where(t => (h.GetDistance(destLatLng.ToString(), t.destinationLatLng.ToString()) < 1)).ToList();
            }
            if (tripDriver == null)
            {
                return NotFound();
            }        
            return Ok(tripDriver);
        }       

        // PUT: api/TripDrivers/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTripDriver(int id, TripDriver tripDriver)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tripDriver.TripDriverID)
            {
                return BadRequest();
            }

            db.Entry(tripDriver).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TripDriverExists(id))
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

        // POST: api/TripDrivers
        [ResponseType(typeof(TripDriver))]
        public IHttpActionResult PostTripDriver(TripDriver tripDriver)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.TripDrivers.Add(tripDriver);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = tripDriver.TripDriverID }, tripDriver);
        }

        // DELETE: api/TripDrivers/5
        [ResponseType(typeof(TripDriver))]
        public IHttpActionResult DeleteTripDriver(int id)
        {
            TripDriver tripDriver = db.TripDrivers.Find(id);
            if (tripDriver == null)
            {
                return NotFound();
            }

            db.TripDrivers.Remove(tripDriver);
            db.SaveChanges();

            return Ok(tripDriver);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TripDriverExists(int id)
        {
            return db.TripDrivers.Count(e => e.TripDriverID == id) > 0;
        }
    }
}