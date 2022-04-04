using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vidly.Models;
using System.Data.Entity; //Eager Loading

namespace Vidly.ViewModels
{
    public class NewCustomerViewModel
    {
            public IEnumerable<MembershipType> MembershipTypes{get; set;}
            public  Customer Customer{get; set;}

    }
}