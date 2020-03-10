﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nethereum.eShop.ApplicationCore.Interfaces;
using Nethereum.eShop.Infrastructure.Identity;
using System;
using System.Threading.Tasks;

namespace Nethereum.eShop.SqlServer.Infrastructure.Data.Config
{
    public class SqlServerEShopAppIdentityDbBootstrapper : IEShopIdentityDbBootstrapper
    {
        public void AddDbContext(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppIdentityDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("IdentityConnection")));
        }

        public Task EnsureCreatedAsync(IServiceProvider serviceProvider)
        {
            //TODO: implement migrations
            return Task.CompletedTask;
        }
    }
}
