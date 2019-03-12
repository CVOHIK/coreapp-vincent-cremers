using Buisiness.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Buisiness
{

    public class ShoppingBag
    {

        public int Id { get; set; }
        public DateTime  Date { get; set; }
        [Display(Name="Total Price")]
        public Double TotalPrice { get; set; }

        public Customer Customer { get; set; }

        public ObservableCollection<ShoppingItem> Items { get; set; }

        [NotMapped]
        [Display(Name = "Total Items In Bag")]
        public int NrOfItemsInBag { get; set; }
        [NotMapped]
        public double Discount { get; set; }
        [NotMapped]
        [Display(Name = "Total - Discount")]
        public double TotalMinusDiscount { get { return TotalPrice - Discount; } }

        private double GetDiscount()
        {
            double DiscountPercentage = 0;
            if (NrOfItemsInBag < 3)
            {
                DiscountPercentage = 0;
            }
            else if (NrOfItemsInBag <= 6)
            {
                DiscountPercentage = 0.05;
            }
            else
            {
                DiscountPercentage = 0.1;
            }
            return TotalPrice * DiscountPercentage;
        }
        private int GetNrOfItems()
        {
            int NrOfItems = 0;
            foreach (ShoppingItem shoppingItem in Items)
            {
                NrOfItems += shoppingItem.Quantity;
            }
            this.NrOfItemsInBag = NrOfItems;
            return NrOfItems;
        }


        public void Recalculate()
        {
            NrOfItemsInBag = GetNrOfItems();
            Discount = GetDiscount();
        }








    }
}