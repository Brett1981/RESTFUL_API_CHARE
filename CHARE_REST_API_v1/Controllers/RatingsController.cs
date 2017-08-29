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
    public class RatingsController : ApiController
    {
        private CHARE_DBEntities db = new CHARE_DBEntities();

        // GET: api/Ratings
        public IQueryable<Rating> GetRatings()
        {
            return db.Ratings;
        }

        // GET: api/Ratings/5
        [ResponseType(typeof(Rating))]
        public IHttpActionResult GetRating(int id)
        {
            Rating rating = db.Ratings.Find(id);
            if (rating == null)
            {
                return NotFound();
            }

            return Ok(rating);
        }

        [ResponseType(typeof(Rating))]
        public IHttpActionResult GetRating(int id, string type)
        {
            List<Rating> rating = null;
            
            if (type.Equals("List"))
              rating  = db.Ratings.Where(t => t.MemberID == id).Take(5).ToList();

            if (rating == null)
            {
                return NotFound();
            }

            return Ok(rating);
        }

        // PUT: api/Ratings/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutRating(int id, Rating rating)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != rating.RateID)
            {
                return BadRequest();
            }

            db.Entry(rating).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RatingExists(id))
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

        // POST: api/Ratings
        [ResponseType(typeof(Rating))]
        public IHttpActionResult PostRating(Rating rating)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Ratings.Add(rating);
            
            var fiveStars = db.Ratings.Count(t => t.MemberID == rating.MemberID && t.rate == 5);
            var fourStars = db.Ratings.Count(t => t.MemberID == rating.MemberID && t.rate == 4);
            var threeStars = db.Ratings.Count(t => t.MemberID == rating.MemberID && t.rate == 3);
            var twoStars = db.Ratings.Count(t => t.MemberID == rating.MemberID && t.rate == 2);
            var oneStars = db.Ratings.Count(t => t.MemberID == rating.MemberID && t.rate == 1);
            int totalNoOfRate = fiveStars + fourStars + threeStars + twoStars + oneStars;
            if (totalNoOfRate != 0)
            {
                var averageRate = (5 * fiveStars + 4 * fourStars + 3 * threeStars + 2 * twoStars + 1 * oneStars) / totalNoOfRate;
                Member m = (from t in db.Members
                            where t.MemberID == rating.MemberID
                            select t).First();
                m.rate = averageRate;
            }
            db.SaveChanges();            
            return CreatedAtRoute("DefaultApi", new { id = rating.RateID }, rating);
        }

        // DELETE: api/Ratings/5
        [ResponseType(typeof(Rating))]
        public IHttpActionResult DeleteRating(int id)
        {
            Rating rating = db.Ratings.Find(id);
            if (rating == null)
            {
                return NotFound();
            }

            db.Ratings.Remove(rating);
            db.SaveChanges();

            return Ok(rating);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RatingExists(int id)
        {
            return db.Ratings.Count(e => e.RateID == id) > 0;
        }
    }
}