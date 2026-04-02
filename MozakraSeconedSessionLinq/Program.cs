using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MozakraSeconedSessionLinq.Data;
using MozakraSeconedSessionLinq.Models;
using System.Collections;
using System.Diagnostics.Metrics;
using System.Numerics;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MozakraSeconedSessionLinq
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ApplicationDbContext context = new ApplicationDbContext();
            #region Mozakra
            #region MyRegion
            //var customers = context.Customers.AsQueryable();
            //customers = customers.Where(c => c.State == "ny" || c.State == "TX").OrderBy(c=>c.FirstName).ThenBy(c=>c.LastName);
            //customers = customers.Skip(0).Take(10);
            //foreach (var customer in customers)
            //{


            //    Console.WriteLine($"{customer.CustomerId} , {customer.FirstName} , {customer.LastName} , {customer.State}");
            //} 
            //var customer = context.Customers.FirstOrDefault(c=>c.State == "NY");
            //if (customer != null) 
            //    Console.WriteLine($"{customer.CustomerId} , {customer.FirstName} , {customer.LastName} , {customer.State}");
            #endregion
            #region Filterd columns

            //var customer = context.Customers.Select(c => new
            //{
            //    c.CustomerId,
            //    c.FirstName,
            //    c.LastName,
            //    c.State,


            //}).AsQueryable();

            //foreach (var c in customer)
            //{
            //    Console.WriteLine($"{c.CustomerId} , {c.FirstName} , {c.LastName} , {c.State}");

            //}
            #endregion
            #region Join
            //var OrdersWithCustomers = context.Orders.Join(context.Customers,
            //         o => o.CustomerId,
            //         c => c.CustomerId,
            //         (o, c) => new
            //         {

            //             o.CustomerId,
            //             o.OrderId,
            //             o.OrderDate,
            //             c.FirstName,
            //             c.LastName,



            //         });

            //foreach (var item in OrdersWithCustomers)
            //{
            //    Console.WriteLine($"{item.CustomerId} , {item.OrderId} , {item.OrderDate} , {item.FirstName} , {item.LastName}");
            //}


            //var OrdesWithCustomers = context.Orders.Include(o => o.Customer).Select(o=> new
            //{
            //    o.CustomerId,
            //    o.OrderId,
            //    o.OrderDate,
            //    o.Customer.FirstName,
            //    o.Customer.LastName,

            //});

            //foreach (var item in OrdesWithCustomers)
            //{
            //    Console.WriteLine($"{item.CustomerId} , {item.OrderId} , {item.OrderDate} , {item.FirstName} , {item.LastName}");


            //}
            #endregion
            #region Groubs
            //var CustomerGroupby = context.Customers.GroupBy(c => c.State).Select(c => new
            //{
            //    c.Key,
            //    count = c.Count()

            //});

            //foreach (var customer in CustomerGroupby)
            //{ Console.WriteLine($"{customer.Key} , {customer.count}"); } 
            #endregion

            #endregion


            //////////////////////=============================================/////////////////////////
            ///حل الواجب
            #region SolveAssignment
            //1 - List all customers' first and last names along with their email addresses.

            var customers = context.Customers.Select(c => new
            {
                c.FirstName,
                c.LastName,
                c.Email
            }).AsQueryable();

            foreach (var customer in customers)
            {
                Console.WriteLine($"{customer.FirstName} , {customer.LastName} , {customer.Email}");
            }





            //2 - Retrieve all orders processed by a specific staff member(e.g., staff_id = 3).

            var ordersByStaff = context.Orders.Where(o => o.StaffId == 3).Select(o => new
            {
                o.OrderId,
                o.OrderDate,
                o.CustomerId,
                o.StaffId,
                o.Staff.FirstName,
                o.Staff.LastName,
            }).AsQueryable();
            foreach (var order in ordersByStaff)
            {
                Console.WriteLine($"{order.OrderId} , {order.OrderDate} , {order.CustomerId} , {order.StaffId} , {order.FirstName} , {order.LastName}");
            }




            //3 - Get all products that belong to a category named "Mountain Bikes".
            var mountainBikes = context.Products.Where(p => p.Category.CategoryName == "Mountain Bikes").Select(p => new
            {
                p.ProductId,
                p.ProductName,
                p.ListPrice,
                p.CategoryId,
                p.Category.CategoryName

            }).AsQueryable();
            foreach (var product in mountainBikes)
            {
                Console.WriteLine($"{product.ProductId} , {product.ProductName} , {product.ListPrice} , {product.CategoryId} , {product.CategoryName}");
            }



            //4 - Count the total number of orders per store.

            var OrdersPerStore = context.Orders.GroupBy(o => o.StoreId).Select(o => new
            {
                o.Key,
                count = o.Count(),



            }).AsQueryable();

            foreach (var order in OrdersPerStore)
            {
                Console.WriteLine($"{order.Key} , {order.count}");
            }





            //5 - List all orders that have not been shipped yet(shipped_date is null).

            var unshippedOrders = context.Orders.Where(o => o.ShippedDate == null).Select(o => new
            {
                o.OrderId,
                o.OrderDate,
                o.CustomerId,
                o.StaffId,
                o.StoreId,
                o.ShippedDate
            }).AsQueryable();

            foreach (var order in unshippedOrders)
            {
                Console.WriteLine($"{order.OrderId} , {order.OrderDate} , {order.CustomerId} , {order.StaffId} , {order.StoreId} , {order.ShippedDate}");
            }

            //6 - Display each customer’s full name and the number of orders they have placed.


            var CustomerOrdersCount = context.Customers.Where(c => c.Orders.Count() > 0).Select(c => new
            {
                FullName = $"{c.FirstName} {c.LastName}",
                OrderCount = c.Orders.Count()
            }).AsQueryable();

            foreach (var customer in CustomerOrdersCount)
            {
                Console.WriteLine($"{customer.FullName} , {customer.OrderCount}");
            }





            //7 - List all products that have never been ordered(not found in order_items).

            var neverOrderedProducts = context.Products.Where(p => !p.OrderItems.Any()).Select(p => new
            {
                p.ProductId,
                p.ProductName,
                p.ListPrice,
                p.CategoryId,
                p.Category.CategoryName
            }).AsQueryable();

            foreach (var product in neverOrderedProducts)
            {
                Console.WriteLine($"{product.ProductId} , {product.ProductName} , {product.ListPrice} , {product.CategoryId} , {product.CategoryName}");
            }




            //8 - Display products that have a quantity of less than 5 in any store stock.
            var lowStockProducts = context.Stocks.Where(s => s.Quantity < 5).Select(s => new
            {
                s.ProductId,
                s.StoreId,
                s.Quantity,
                s.Product.ProductName,
                s.Store.StoreName
            }).AsQueryable();

            foreach (var stock in lowStockProducts)
            {
                Console.WriteLine($"{stock.ProductId} , {stock.StoreId} , {stock.Quantity} , {stock.ProductName} , {stock.StoreName}");
            }


            //9 - Retrieve the first product from the products table.
            var firstProduct = context.Products.FirstOrDefault();

            if (firstProduct != null)
            {
                Console.WriteLine(firstProduct.ProductName);
            }



            //10 - Retrieve all products from the products table with a certain model year.
            var AllProuducts = context.Products.Select(p => new
            {
                p.ProductName,
                p.ModelYear

            }).AsQueryable();
            foreach (var p in AllProuducts)
            {
                Console.WriteLine($"{p.ProductName} , {p.ModelYear}");
            }


            //11 - Display each product with the number of times it was ordered.
            var productOrderCounts = context.Products.Select(p => new
            {
                p.ProductId,
                p.ProductName,
                OrderCount = p.OrderItems.Sum(oi => oi.Quantity)
            }).AsQueryable();

            foreach (var product in productOrderCounts)
            {
                Console.WriteLine($"{product.ProductId} , {product.ProductName} , {product.OrderCount}");
            }



            //12 - Count the number of products in a specific category.

            var categoryProductCounts = context.Categories.Select(c => new
            {
                c.CategoryId,
                c.CategoryName,
                ProductCount = c.Products.Count()
            }).AsQueryable();

            foreach (var category in categoryProductCounts)
            {
                Console.WriteLine($"{category.CategoryId} , {category.CategoryName} , {category.ProductCount}");
            }


            //13 - Calculate the average list price of products. 

            var averageListPrice = context.Products.GroupBy(P => P.ProductId).Select(p => new
            {
                p.Key,
                AveragePrice = p.Average(p => p.ListPrice)
            }).AsQueryable();

            foreach (var product in averageListPrice)
            {
                Console.WriteLine($"{product.Key} , {product.AveragePrice}");
            }

            var averageListPrice1 = context.Products.Average(p => p.ListPrice);

            Console.WriteLine($"{averageListPrice1}");






            //14 - Retrieve a specific product from the products table by ID 4 .

            var productById = context.Products.Where(p => p.ProductId == 4).Select(p => new
            {
                p.ProductId,
                p.ProductName,
                p.ListPrice,
                p.CategoryId,
                p.Category.CategoryName
            }).AsQueryable();

            foreach (var product in productById)
            {
                Console.WriteLine($"{product.ProductId} , {product.ProductName} , {product.ListPrice} , {product.CategoryId} , {product.CategoryName}");
            }



            //15 - List all products that were ordered with a quantity greater than 3 in any order.

            var productsOrderedWithQuantityGreaterThan3 = context.OrderItems.Where(oi => oi.Quantity > 3).Select(oi => new
            {
                oi.ProductId,
                oi.Product.ProductName,
                oi.Quantity,
                oi.OrderId
            }).AsQueryable();
            foreach (var item in productsOrderedWithQuantityGreaterThan3)
            {
                Console.WriteLine($"{item.ProductId} , {item.ProductName} , {item.Quantity} , {item.OrderId}");
            }


            //16 - Display each staff member’s name and how many orders they processed.

            var staffOrderCounts = context.Staffs.Select(s => new
            {
                s.StaffId,
                FullName = $"{s.FirstName} {s.LastName}",
                OrderCount = s.Orders.Count()
            }).AsQueryable();
            foreach (var staff in staffOrderCounts)
            {
                Console.WriteLine($"{staff.StaffId} , {staff.FullName} , {staff.OrderCount}");
            }


            //17 - List active staff members only(active = true) along with their phone numbers.
            var activeStaff = context.Staffs.Where(s => s.Active == 1).Select(s => new
            {

                s.StaffId,
                FullName = $"{s.FirstName} {s.LastName}",
                s.Phone
            }).AsQueryable();
            foreach (var staff in activeStaff)
            {
                Console.WriteLine($"{staff.StaffId} , {staff.FullName} , {staff.Phone}");
            }



            //18 - List all products with their brand name and category name.
            var productsWithBrandAndCategory = context.Products.Select(p => new
            {
                p.ProductId,
                p.ProductName,
                p.ListPrice,
                BrandName = p.Brand.BrandName,
                CategoryName = p.Category.CategoryName
            }).AsQueryable();
            foreach (var product in productsWithBrandAndCategory)
            {
                Console.WriteLine($"{product.ProductId} , {product.ProductName} , {product.ListPrice} , {product.BrandName} , {product.CategoryName}");
            }

            //19 - Retrieve orders that are completed.
            var completedOrders = context.Orders.Where(o => o.ShippedDate != null).Select(o => new
            {
                o.OrderId,
                o.OrderDate,
                o.CustomerId,
                o.StaffId,
                o.StoreId,
                o.ShippedDate
            }).AsQueryable();
            foreach (var order in completedOrders)
            {
                Console.WriteLine($"{order.OrderId} , {order.OrderDate} , {order.CustomerId} , {order.StaffId} , {order.StoreId} , {order.ShippedDate}");
            }


            //20 - List each product with the total quantity sold(sum of quantity from order_items).
            var productTotalQuantities = context.Products.Select(p => new
            {
                p.ProductId,
                p.ProductName,
                TotalQuantitySold = p.OrderItems.Sum(oi => oi.Quantity)
            }).AsQueryable();
            foreach (var product in productTotalQuantities)
            {
                Console.WriteLine($"{product.ProductId} , {product.ProductName} , {product.TotalQuantitySold}");
            }

            #endregion









        }
    }
}