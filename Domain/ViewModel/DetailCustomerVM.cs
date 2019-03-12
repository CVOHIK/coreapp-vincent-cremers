using System;
using System.Collections.Generic;
using System.Text;
using Buisiness.Interfaces;

namespace Buisiness.ViewModel
{
    public class DetailCustomerVM
    {
        public Customer Customer { get; set; }
        public ICollection<ShoppingBag> ShoppingBags { get; set; }


        public void recalculateBags()
        {
            foreach (ShoppingBag item in ShoppingBags)
            {
                item.Recalculate();
            }
        }
    

    }
}
