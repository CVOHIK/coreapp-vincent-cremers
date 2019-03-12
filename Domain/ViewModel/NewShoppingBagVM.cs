using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Buisiness;

namespace Buisiness.ViewModel
{
    public class NewShoppingBagVM
    {
        public Customer Customer { get; set; }
        public DateTime Date { get; set; }
        public int ShoppingBagId { get; set; }
    }
}
