using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;
using System.Data.Entity;
using Vidly.Migrations;
namespace Vidly.Controllers
{
    public class MoviesController : Controller

    {
        private ApplicationDbContext _context;

        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        // GET: Movies/Random
        /*public ActionResult Random()
        {
            var movie = new Movie() { Name="Shrek"};

            var customers = new List<Customer>{
                new Customer{Name="customer1"},
                new Customer{Name="customer2"},
                                new Customer{Name="customer1"},
                new Customer{Name="customer1"},
                new Customer{Name="customer1"},
                new Customer{Name="customer1"}



            };

            var viewModel = new RandomMovieViewModel { 
                Movie=movie,
                Customers=customers
            
            };

            return View(viewModel);

            // pass data to the view to render it
            //return View(movie);

            //return new EmptyResult();
            //return HttpNotFound();
            //return RedirectToAction("Index", "Home");
            
        }*/
        /*public ActionResult Edit(int id){

            return Content("id = " + id);
        }*/

        /*public ActionResult Index(int? pageIndex, string sortBy)
        {
            if (!pageIndex.HasValue)
                pageIndex = 5;
            if (string.IsNullOrWhiteSpace(sortBy))
                sortBy = "Name";

            return Content(string.Format("pageIndx={0}&sortBy={1}", pageIndex, sortBy));
        }*/

        [Route("movies/released/{year},{month:regex(\\d{4}):range(1,12)}")]
       /* public ActionResult ByReleasedDate(int  year, int month)
        {
            return Content(year + "/" + month);
        }*/

        public ViewResult Index()
        {
            if (User.IsInRole(RoleName.CanManageMovies))
                return View(_context.Movies.Include(m => m.Genre).ToList());

            return View("ReadOnlyList", _context.Movies.Include(m => m.Genre).ToList());
        }

        /*private IEnumerable<Movie> GetMovies()
        {

            return new List<Movie>{
                new Movie{ Id=1 ,Name="Miss Potter"},
                new Movie{ Id=2 ,Name="Agora"},
                new Movie{ Id=3  ,Name="La Vita e Bella"}
            };
        }*/

        public ActionResult Details(int id)
        {
            var movie = _context.Movies.Include(m => m.Genre).SingleOrDefault(m => m.Id == id);
            if (movie == null)
                return HttpNotFound();

            return View(movie);
        }

        // Initialize the view model then send it to the MovieForm to be filled then the view will Post the data through SaveMovie
        [Authorize(Roles=RoleName.CanManageMovies)]
        public ViewResult AddNewMovie()
        {
            var genres= _context.Genres.ToList();
            var ViewModel = new MovieFormViewModel()
            {
                //Movie=new Movie(), //create instant of movie Model
                Genres = genres
            };
            return View("MovieForm",ViewModel);
        }

        public ActionResult Edit(int id)
        {
            var movie = _context.Movies.SingleOrDefault(c => c.Id == id); //get movie instant from db 
            if (movie == null)
                return HttpNotFound();
            var viewModel = new MovieFormViewModel(movie)
            {
      

                Genres = _context.Genres.ToList()
            };
            return View("MovieForm", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Movie movie) //For adding new and editting movies
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new MovieFormViewModel(movie) //this to populate the form with the same data
                {
                    Genres = _context.Genres.ToList()
                };
                return View("MovieForm", viewModel);
            }

            if (movie.Id == 0) //Id = 0 means it's a new movie, it's id is not generated yet
            {
                movie.DateAdded = DateTime.Now;
                _context.Movies.Add(movie);
            }

            else // Existing movie it will be edited
            {
                var movieInDb=_context.Movies.Single(m=>m.Id == movie.Id); //fetch this movie from the database
                movieInDb.Name=movie.Name;
                movieInDb.GenreId=movie.GenreId;
                movieInDb.NumInStock=movie.NumInStock;
                movieInDb.ReleaseDate=movie.ReleaseDate;

            }

            _context.SaveChanges();

            return RedirectToAction("Index", "Movies");
        }
        
    }
}