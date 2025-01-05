using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toplearn.DataLayer.Entities.Courses;

namespace Toplearn.Core.Convertors
{
    public static class PriceConvertor
    {
        public static decimal GetPriceWithDiscount(this Cart cart, bool isDollar = false)
        {
            var totalPrice = cart.GetPrice(isDollar);
            if (cart.Discount == null)
                return totalPrice;
            decimal finalPrice = totalPrice * (100 - cart.Discount.Percent) / 100;
            if (isDollar)
            {
                finalPrice = Math.Round(finalPrice);
            }
            else
            {
                finalPrice = Math.Floor(finalPrice / 1000) * 1000;
            }
            return finalPrice;
        }
        public static decimal GetPrice(this Cart cart, bool isDollar = false)
        {
            decimal finalPrice = cart.CourseCarts.Sum(x => (isDollar ? x.Course.PriceDollar : x.Course.Price) * x.Count);
            if (isDollar)
            {
                finalPrice = Math.Round(finalPrice);
            }
            else
            {
                finalPrice = Math.Floor(finalPrice / 1000) * 1000;
            }
            return finalPrice;
        }
    }
}
