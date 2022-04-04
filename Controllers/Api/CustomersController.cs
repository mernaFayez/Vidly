using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Entity;
using Vidly.Models;
using Vidly.Dtos;
using AutoMapper;

namespace Vidly.Controllers.Api
{
    public class CustomersController : ApiController
    {
        private ApplicationDbContext _context;

        CustomersController()
        {
            _context = new ApplicationDbContext();
        }
        // Get/Api/Customers
        public IHttpActionResult GetCustomers()
        {
            var customerDtos = _context.Customers
                .Include(c => c.MembershipType)
                .ToList()
                .Select(Mapper.Map<Customer, CustomerDto>);

            return Ok(customerDtos);
        }

        // Get/Api/Customers/1
        public IHttpActionResult GetCustomer(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);
            if (customer == null)
                return NotFound();
                //throw new HttpResponseException(HttpStatusCode.NotFound);

            return Ok(Mapper.Map<Customer, CustomerDto>(customer));
            //return customer ;

        }

       /* public Customer CreateCustomer(Customer customer)
        {
            // Validate Input
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            _context.Customers.Add(customer);
            _context.SaveChanges();
            return customer;
        }*/

        // POST/Api/Csutomers
        [HttpPost]
        public IHttpActionResult CreateCustomer(CustomerDto customerDto)
        {
            // Validate Input
            if (!ModelState.IsValid)
                return BadRequest();
            var customer = Mapper.Map<CustomerDto, Customer>(customerDto);

            _context.Customers.Add(customer);
            _context.SaveChanges();
            customerDto.Id = customer.Id;
            return Created(new Uri(Request.RequestUri+"/"+ customer.Id),customerDto);
        }

        /*public CustomerDto CreateCustomer(CustomerDto customerDto)
        {
            // Validate Input
            if(!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

               var customer = Mapper.Map<CustomerDto, Customer>(customerDto);
            
                _context.Customers.Add(customer);
                _context.SaveChanges();
                customerDto.Id = customer.Id;
                return customerDto;
        }*/

        // PUT /Api/Customers/1
        [HttpPut]
        public void UpdateCustomer(int id, CustomerDto customerDto) 
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var CustomerInDb = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (CustomerInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

             Mapper.Map(customerDto,CustomerInDb);
            //We used Automapper tool instead of this
            /*CustomerInDb.Name = customerDto.Name;
            CustomerInDb.BirthDate = customerDto.BirthDate;
            CustomerInDb.IsSubscribedToNewsletter = customerDto.IsSubscribedToNewsletter;
            CustomerInDb.MembershipType = customerDto.MembershipType;*/

            _context.SaveChanges();
        }

        // Delete Api/Customers/1
        [HttpDelete]
        public void DeleteCustomer (int id)
        {
            var CustomerInDb = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (CustomerInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            _context.Customers.Remove(CustomerInDb);

            _context.SaveChanges();

        }

    }
}
