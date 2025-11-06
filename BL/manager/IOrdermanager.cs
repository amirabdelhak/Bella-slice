using BL.DTOs.OrderDTO;
using DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.manager
{
    public interface IOrdermanager
    {
        OrderDetailsDTO? GetByid(int id);
        List<Order> GetAll();
        void Add(Order order);
        void Update(Order order);
        void Delete(int id);
    }
}
