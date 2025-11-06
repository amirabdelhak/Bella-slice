using DAL.Entity;
using DAL.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.manager
{
    public class productmanger:IProductmanager
    {
        private readonly iUnitOfWork unitOfWork;


        public productmanger(iUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }


        public List<Product>? getallproduct()
        {
            return unitOfWork.ProductRepo.GetAll();
        }

        public List<Product>? GetProductsByCategory(int categoryid)
        {
            return unitOfWork.ProductRepo.GetProductsByCategory(categoryid);
        }

        public List<Category>? getallcategories()
        {
            return unitOfWork.CategoryRepo.GetAll();
        }
        public Product? getproductbyid(int id)
        {
            return unitOfWork.ProductRepo.GetById(id);
        }

        public List<Product>? getproductbyname(string name)
        {
            return unitOfWork.ProductRepo.GetByName(name);
        }

        public void createproduct(Product product)
        {
            unitOfWork.ProductRepo.Add(product);
            unitOfWork.save();
        }
        public List<Category> getCategorys()
        {
            return unitOfWork.ProductRepo.getCategorys();
        }
        public void updateproduct(Product product)
        {

            unitOfWork.ProductRepo.Update(product);
            unitOfWork.save();
        }
        public void deleteproduct(int id)
        {
            var product = unitOfWork.ProductRepo.GetById(id);
            if (product != null)
            {
                unitOfWork.ProductRepo.Delete(product);
                unitOfWork.save();
            }
        }

        public List<Product> cartproducts(List<int> cart)
        {
            return unitOfWork.ProductRepo.cartproducts(cart);
        }

    }
}
