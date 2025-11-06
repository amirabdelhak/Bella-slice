using DAL.Entity;
using DAL.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.manager
{
    public class categorymanager: Icategorymanager
    {
        private readonly iUnitOfWork unitOfWork;
        public categorymanager(iUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public List<Category>? getallcategory()
        {
            return unitOfWork.CategoryRepo.GetAll().ToList();
        }
        public Category? getcategorybyid(int id)
        {
            return unitOfWork.CategoryRepo.GetById(id);
        }
        public void createcategory(Category category)
        {
            unitOfWork.CategoryRepo.Add(category);
            unitOfWork.save();
        }
        public void updatecategory(Category category)
        {
            unitOfWork.CategoryRepo.Update(category);
            unitOfWork.save();
        }
        public void deletecategory(int id)
        {
            var category = unitOfWork.CategoryRepo.GetById(id);
            if (category != null)
            {
                unitOfWork.CategoryRepo.Delete(category);
                unitOfWork.save();
            }
        }

    }
}
