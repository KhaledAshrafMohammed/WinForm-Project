using Autofac;
using AutoMapper;
using ELDOKKAN.Context;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IContainer = Autofac.IContainer;
using ELDOKKAN.Repositories;
using ELDOKKAN.Application.Services;
using ELDOKKAN.Application.Mapper;
using ELDOKKAN.Context;
using ELDOKKAN.Repositories;

namespace Eldokkan.pl
{
    public class AutoFac
    {
        public static Autofac.IContainer Inject()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<ProductService>().As<IProductService>();

            builder.RegisterType<ProductRepository>().As<IProductRepository>();
            builder.RegisterType<AdminService>().As<IAdminService>();
            builder.RegisterType<AdminRepository>().As<IAdminRepository>();
             builder.RegisterType<CustomerService>().As<ICustomerService>();
             builder.RegisterType<CustomerRepository>().As<ICustomerRepository>();
            builder.RegisterType<CategoryService>().As<ICategoryService>();
            builder.RegisterType<CategoryRepository>().As<ICategoryRepository>();
            builder.RegisterType<OrderDetailsService>().As<IOrderDetailsService>();
            builder.RegisterType<OrderDetailsRepository>().As<IOrderDetailsRepository>();
            builder.RegisterType<OrderService>().As<IOrderService>();
            builder.RegisterType<OrderRepository>().As<IOrderRepository>();
            builder.RegisterType<AppDbContext>().As<AppDbContext>();

            builder.RegisterType<ProductService>().As<IProductService>().InstancePerLifetimeScope();
            builder.RegisterType<ProductRepository>().As<IProductRepository>();
            builder.RegisterType<AppDbContext>().As<AppDbContext>();

            builder.Register(c => new MapperConfiguration(cfg =>
            {
                // Register AutoMapper profile
                cfg.AddProfile<AutoMapperProfile>();
            }));//.AsSelf().SingleInstance();

            builder.Register(c => c.Resolve<MapperConfiguration>().CreateMapper(c.Resolve)).As<IMapper>().InstancePerLifetimeScope();
            builder.Register(c => c.Resolve<MapperConfiguration>().CreateMapper(c.Resolve)).As<IMapper>();//.InstancePerLifetimeScope();

            return builder.Build();

        }
    }

 
}
