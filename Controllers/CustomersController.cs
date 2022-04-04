using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;
using System.Data.Entity; //Eager Loading
namespace Vidly.Controllers
{
    [Authorize]
    public class CustomersController : Controller
    {
        private ApplicationDbContext _context;
        
        public CustomersController()
        {
            _context =new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: Customers
        public ViewResult Index()
        {
           // return View((_context.Customers.Include(c=> c.MembershipType)).ToList()); //NOTICE THE CONNECTION BETWEEN 2 Classes customer and membershiptype this is how this include is written
            return View();
        }

       /* private IEnumerable<Customer> GetCustomers()
        {
            return new List<Customer>
            {
                new Customer{Id=1, Name= "Merna"},
                new Customer{Id=2, Name= "Sally"},
                new Customer{Id=3, Name= "Jojo"}

            };
        }*/

        public ActionResult Details(int id)
        {
            var customer = _context.Customers.Include(c=>c.MembershipType).SingleOrDefault(c => c.Id == id);
            if (customer == null)
                return HttpNotFound();
            return View(customer);
        }
        //Send data to the view
        public ActionResult New() 
        {
            var membershipTypes = _context.MembershipTypes.ToList();
            var ViewModel=new NewCustomerViewModel{
                Customer=new Customer(), //this force the customer object to be intialized with its default values
                MembershipTypes=membershipTypes
            };
            return View(ViewModel);
        }
        
        public ActionResult Edit(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);
            if (customer == null)
                return HttpNotFound();
            var viewModel = new NewCustomerViewModel
            {
                Customer=customer,
                MembershipTypes=_context.MembershipTypes.ToList()
            };
            return View("New",viewModel); //new => customer form 
        }

        //Adding HttpPost Attribute to make sure it can only be called using httpPost not httPGet
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Customer customer)
        //Model binding Happens here // Create => Save (add new and update customers) 
        //instead of updating the whole class you can create custom class if don't want certain properties to be changed
        {
            /*if (!ModelState.IsValid)
            {
                var viewModel = new NewCustomerViewModel //this to populate the form with the same data
                {
                    Customer = customer,
                    MembershipTypes = _context.MembershipTypes.ToList()
                };
                return View("New", viewModel);
            }*/
            if (customer.Id == 0) //Id= 0 means this is a new movie otherwise we are edittig the movie 
                _context.Customers.Add(customer); //Now data is not saved to the database it is just in the memory //DBcontext is a change tracker, it markes objects as added, modified or deleted
            else
            {
                var customerInDb = _context.Customers.Single(c => c.Id == customer.Id);
                // tryUpdateModel is bad don't use it //add them manually or use AutoMapper library
                //Mapper.Map(customer,customerInDB)
                customerInDb.Name = customer.Name;
                customerInDb.BirthDate = customer.BirthDate;
                customerInDb.MembershipTypeId = customer.MembershipTypeId;
                customerInDb.IsSubscribedToNewsletter = customer.IsSubscribedToNewsletter;


            }
            _context.SaveChanges(); //DBcontext will go check all modified objects and based on the kinf of the modification it will generate SQL Statements at runtime and then it will run them on the database
            return RedirectToAction("Index", "Customers");
        }
    }
}