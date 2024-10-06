using Loymark.Back.Infraestructure.Context;
using Loymark.Domain.Exceptions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Loymark.Back.WebApi.Tests
{
    [ExcludeFromCodeCoverage]
    public class IntegrationTestBuilder<TProgram>
        : WebApplicationFactory<TProgram> where TProgram : class
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public IntegrationTestBuilder()
        {
            _scopeFactory = Services.GetService<IServiceScopeFactory>()!;
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            InMemoryDatabaseRoot rootDb = new();

            builder.ConfigureServices(services =>
            {
                ServiceDescriptor descriptor = services.SingleOrDefault(
                    descriptors => descriptors.ServiceType == typeof(DbContextOptions<PersistenceContext>)
                )!;

                if (descriptor != null)
                    services.Remove(descriptor);

                services.AddDbContext<PersistenceContext>(options =>
                {
                    options.UseInMemoryDatabase("InMemoryLoymarkPersistenceContext", rootDb);
                    options.EnableServiceProviderCaching(false);
                });

                ServiceProvider sp = services.BuildServiceProvider();
                using IServiceScope scope = sp.CreateScope();
                using PersistenceContext appContext = scope.ServiceProvider.GetRequiredService<PersistenceContext>();

                try
                {
                    appContext.Database.EnsureCreated();
                    
                    #region seed
                    appContext.Add(new ActivityBuilder().Build());
                    #endregion
                    
                    appContext.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw new AppException(ex.Message);
                }
            });

            builder.UseEnvironment("Development");
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
