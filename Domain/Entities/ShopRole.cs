using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Buisiness
{
    public class ShopRole : IdentityRole
    {
        public ShopRole() : base()
        {

        }

        public ShopRole(string roleName) : base(roleName)
        {

        }

        public ShopRole(string roleName, string description, DateTime creationDate): base(roleName)
        {
            Description = description;
            CreationDate = creationDate;
        }
        public String Description { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
