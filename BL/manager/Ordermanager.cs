using BL.DTOs.OrderDTO;
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
    public class Ordermanager: IOrdermanager
    {
        private readonly iUnitOfWork unitOfWork;

        public Ordermanager(iUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public OrderDetailsDTO? GetByid(int id)
        {
            var order = unitOfWork.OrderRepo
                .GetAll(q => q.Where(o => o.Id == id)
                .Include(o => o.productorder)
                .ThenInclude(po => po.Product))
                .FirstOrDefault();

            if (order == null) return null;

            return new OrderDetailsDTO
            {
                OrderId = order.Id,
                TotalPrice = order.TotalPrice,

                Products = order.productorder.Select(po => new ProductDTO
                {
                    Name = po.Product.name,
                    Price = po.Product.price,
                    ImageName = po.Product.imagename
                }).ToList()
            };
        }

        public List<Order> GetAll()
        {
            return unitOfWork.OrderRepo.GetAll();
        }

        public void Add(Order order)
        {
           unitOfWork.OrderRepo.Add(order);
            unitOfWork.save();
        }
        public void Update(Order order)
        {
            unitOfWork.OrderRepo.Update(order);
            unitOfWork.save();
        }
        public void Delete(int id)
        {
           unitOfWork.OrderRepo.Delete(unitOfWork.OrderRepo.GetById(id));
            unitOfWork.save();
        }
    }
}
