using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;


namespace Buisiness
{
    public class Customer:IdentityUser
    {
        public Customer() : base(){ }
        public String Name { get; set; }
        public String FirstName { get; set; }


        public ICollection<ShoppingBag> Bags { get; set; }


    }
}
