using DAL.context;
using DAL.Entity;
using DAL.repository;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.UnitOfWork
{
    public interface iUnitOfWork
    {
        productrepository ProductRepo { get; }
        igenericrepository<Category> CategoryRepo { get; }
        igenericrepository<Order> OrderRepo { get; }
        igenericrepository<Customer> CustomerRepo { get; }
        igenericrepository<productorder> ProductOrderRepo { get; }
        void save();
    }
}
