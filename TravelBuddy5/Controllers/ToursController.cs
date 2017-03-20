using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Http;
using TravelBuddy5.DAL;
using TravelBuddy5.DAL.Interfaces;
using TravelBuddy5.DAL.Repositories;
using TravelBuddy5.Models;

namespace TravelBuddy5.Controllers
{
    public class ToursController : ApiController
    {
        private readonly ITourRepo _repo;

        public ToursController(ITourRepo tourRepo)
        {
            _repo = tourRepo;
        }

        public IQueryable<TourDTO> GetTours()
        {
            return _repo.GetTours().Select(CreateTourDTO());
        }

        // GET: api/Tours
        public IQueryable<TourDTO> GetToursByCity(int id)
        {
            return _repo.GetToursByCity(id).Include(tour => tour.City).Include(tour => tour.City.Country).Select(CreateTourDTO());
        }

        private Expression<Func<Tour, TourDTO>> CreateTourDTO()
        {
            return tour => new TourDTO
            {
                Id = tour.Id,
                Name = tour.Name,
                City = tour.City.Name,
                Country = tour.City.Country.Name,
                Description = tour.Description,
                DetailDescription = tour.DetailDescription
            };
        }

        // GET: api/Tours/5
       /* [ResponseType(typeof(Tour))]
        public async Task<IHttpActionResult> GetTour(int id)
        {
            Tour tour = await _repo.GetToursByCity(id);
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

            //_db.Entry(tour).State = EntityState.Modified;

            try
            {
               // await _db.SaveChangesAsync();
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
        *
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
        */
    }
}