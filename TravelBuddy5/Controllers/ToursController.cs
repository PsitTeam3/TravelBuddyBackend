using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using TravelBuddy5.DAL;

namespace TravelBuddy5.Controllers
{
    public class ToursController : ApiController
    {

        ITourRepo _repo = null;
        TravelBuddyEntities _db = null;

        public ToursController(ITourRepo tourRepo)
        {
            _repo = tourRepo;
            _db = _repo.DB;
        }

        // GET: api/Tours
        public IQueryable<Tour> GetTour()
        {
            return _db.Tour;
        }

        // GET: api/Tours/5
        [ResponseType(typeof(Tour))]
        public async Task<IHttpActionResult> GetTour(int id)
        {
            Tour tour = await _db.Tour.FindAsync(id);
            if (tour == null)
            {
                return NotFound();
            }

            return Ok(tour);
        }

        // PUT: api/Tours/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutTour(int id, Tour tour)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tour.Id)
            {
                return BadRequest();
            }

            _db.Entry(tour).State = EntityState.Modified;

            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TourExists(id))
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

        // POST: api/Tours
        [ResponseType(typeof(Tour))]
        public async Task<IHttpActionResult> PostTour(Tour tour)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _db.Tour.Add(tour);

            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (TourExists(tour.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = tour.Id }, tour);
        }

        // DELETE: api/Tours/5
        [ResponseType(typeof(Tour))]
        public async Task<IHttpActionResult> DeleteTour(int id)
        {
            Tour tour = await _db.Tour.FindAsync(id);
            if (tour == null)
            {
                return NotFound();
            }

            _db.Tour.Remove(tour);
            await _db.SaveChangesAsync();

            return Ok(tour);
        }

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        _db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        private bool TourExists(int id)
        {
            return _db.Tour.Any(e => e.Id == id);
        }
    }
}