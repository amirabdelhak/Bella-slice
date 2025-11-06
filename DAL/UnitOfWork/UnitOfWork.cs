using DAL.context;
using DAL.Entity;
using DAL.repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.UnitOfWork
{
    public class UnitOfWork:iUnitOfWork
    {
        private readonly IServiceProvider serviceProvider;
        private readonly Restaurantcontext dbcontext;
        private productrepository productRepo;
        private igenericrepository<Category> categoryRepo;
        private igenericrepository<Order> orderRepo;
        private igenericrepository<Customer> customerRepo;
        private igenericrepository<productorder> productorderRepo;


        public UnitOfWork(Restaurantcontext dbcontext, IServiceProvider serviceProvider)
        {
            this.dbcontext = dbcontext;
            this.serviceProvider = serviceProvider;
        }

        public productrepository ProductRepo
                     => productRepo ??= serviceProvider.GetRequiredService<productrepository>();
        public igenericrepository<Category> CategoryRepo
            => categoryRepo ??= serviceProvider.GetRequiredService<igenericrepository<Category>>();

        public igenericrepository<Order> OrderRepo 
            => orderRepo ??= serviceProvider.GetRequiredService<igenericrepository<Order>>();

        public igenericrepository<Customer> CustomerRepo
             => customerRepo ??= serviceProvider.GetRequiredService<igenericrepository<Customer>>();

        public igenericrepository<productorder> ProductOrderRepo
            => productorderRepo ??=serviceProvider.GetRequiredService<igenericrepository<productorder>>();

        public void save() => dbcontext.SaveChanges();

       
    }
}
