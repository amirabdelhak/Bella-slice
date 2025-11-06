using DAL.context;
using DAL.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.repository
{
    public class productrepository : genericrepository<Product>
    {
        private readonly Restaurantcontext dbcontext;
        public productrepository(Restaurantcontext dbcontext) : base(dbcontext)
        {
            this.dbcontext = dbcontext;
        }

        public List<Product>? GetByName(string name)
        {
          return dbcontext.Products.Where(p => p.name.Contains(name)).ToList();
        }
        public List<Category> getCategorys()
        {
            return dbcontext.Categories.ToList();
        }

        // New: get products by category id
        public List<Product> GetProductsByCategory(int categoryId)
        {
            return dbcontext.Products
                .Include(p => p.Category)
                .Where(p => p.Categoryid == categoryId).AsNoTracking()
                .ToList();
        }

        public List<Product> cartproducts( List<int> cart)
        {
            return dbcontext.Products
                            .Where(p => cart.Contains(p.id))
                            .ToList();
        }
    }
}
