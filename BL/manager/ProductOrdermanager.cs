using DAL.Entity;
using DAL.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.manager
{
    public class ProductOrdermanager: iProductOrdermanager
    {
        private readonly iUnitOfWork unitOfWork;

        public ProductOrdermanager(iUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public void Addproductorder(productorder productorder)
        {
            unitOfWork.ProductOrderRepo.Add(productorder);
            unitOfWork.save();
        }
        public void save()
        {
            unitOfWork.save();
        }
    }
}
