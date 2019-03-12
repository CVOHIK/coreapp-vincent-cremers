using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Buisiness.ViewModel
{
    public class DetailBagVM
    {
        public Customer Customer { get; set; }
        public ShoppingBag ShoppingBag { get; set; }

        public SelectList Products { get; set; }
        [Range(0,99)]
        public int AddProductQty { get; set; }
        public int AddProductId { get; set; }
    }
}
