using DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.manager
{
    public interface IProductmanager
    {
         List<Product>? getallproduct();
         List<Category>? getallcategories();
        List<Product>? GetProductsByCategory(int categoryid);
         Product? getproductbyid(int id);
         List<Product>? getproductbyname(string name);
         void createproduct(Product product);
         void updateproduct(Product product);
         void deleteproduct(int id);
         List<Category> getCategorys();
        List<Product> cartproducts(List<int> cart);


    }
}
