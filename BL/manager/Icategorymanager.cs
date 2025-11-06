using DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.manager
{
    public interface Icategorymanager
    {
        List<Category>? getallcategory();
        Category? getcategorybyid(int id);
        void createcategory(Category category);
        void updatecategory(Category category);
        void deletecategory(int id);

    }
}
