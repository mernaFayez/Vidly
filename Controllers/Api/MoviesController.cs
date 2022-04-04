using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Entity;
using Vidly.Models;

namespace Vidly.Controllers.Api
{
    public class MoviesController : ApiController
    {
        // Intilaize _context instant
        private ApplicationDbContext _context;
        MoviesController()
        {
            var _context = new ApplicationDbContext();
        }

        //GET api/movies
        public IEnumerable<Movie> GetMovies(){
            return _context.Movies.Include(m=>m.Genre).ToList();
        }
        /*
        //GET api/movies/1
        public 
        //POSt api/customers
        [HttpPost]
        //PUT api/customers/1
        [HttpPut]
        //DELETE api/customers/1
        [HttpDelete]*/

    }
}
