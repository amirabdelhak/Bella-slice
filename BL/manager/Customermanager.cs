using DAL.Entity;
using DAL.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.manager
{
    public class Customermanager: iCustomermanager
    {
        private readonly iUnitOfWork unitOfWork;

        public Customermanager(iUnitOfWork unitOfWork) 
        {
            this.unitOfWork = unitOfWork;
        }

        public void Add(Customer customer)
        {
            unitOfWork.CustomerRepo.Add(customer);
            unitOfWork.save();
        }
        public Customer? GetById(string id)
        {
            return unitOfWork.CustomerRepo.GetById(id);
        }
    }
}
