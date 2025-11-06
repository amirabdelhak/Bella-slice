using DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.manager
{
    public interface iCustomermanager
    {
        void Add(Customer customer);
        Customer? GetById(string id);
    }
}
