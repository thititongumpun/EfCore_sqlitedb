using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace linq
{
    class Program
    {
        static void Main(string[] args)
        {
            // QueryProducts();

            // JoinCategoriesAndProducts();

            GroupJoinCategoriesAndProducts();

            LinqWithSet.setOfLinq();
        }

        static void QueryProducts()
        {
            using (var db = new Northwind())
            {
                var query = db.Products.Where(p => p.UnitPrice < 10M).OrderByDescending(p => p.UnitPrice);
                foreach (var item in query)
                {
                    Console.WriteLine($"{item.ProductID}:{item.ProductName} costs {item.UnitPrice}");
                }
            }
        }

        static void JoinCategoriesAndProducts()
        {
            using (var db = new Northwind())
            {
                var queryJoin = db.Categories.Join(
                    inner: db.Products,
                    outerKeySelector: c => c.CategoryID,
                    innerKeySelector: p => p.CategoryID,
                    resultSelector: (c, p) =>
                        new { c.CategoryName, p.ProductName, p.ProductID }
                ).OrderBy(c => c.CategoryName);

                foreach (var item in queryJoin)
                {
                    Console.WriteLine($"{item.ProductID}: {item.ProductName} is in  {item.CategoryName}");
                }
            }
        }

        static void GroupJoinCategoriesAndProducts()
        {
            using (var db = new Northwind())
            {
                var queryGroup = db.Categories.AsEnumerable().GroupJoin(
                inner: db.Products,
                outerKeySelector: category => category.CategoryID,
                innerKeySelector: product => product.CategoryID,
                resultSelector: (c, matchingProducts) => new
                {
                    c.CategoryName,
                    Products = matchingProducts.OrderBy(p => p.ProductName)
                });
                foreach (var item in queryGroup)
                {
                    Console.WriteLine("{0} has {1} products.",
                    arg0: item.CategoryName,
                    arg1: item.Products.Count());
                    foreach (var product in item.Products)
                    {
                        Console.WriteLine($" {product.ProductName}");
                    }
                }
            }
        }
    }
}
