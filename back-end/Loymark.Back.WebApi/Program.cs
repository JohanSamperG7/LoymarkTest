using Loaymark.Back.Api.Filters;
using Loymark.Back.Domain.Ports;
using Loymark.Back.Domain.Services;
using Loymark.Back.Infraestructure.Context;
using Loymark.Back.Infrastructure.Adapters;
using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager config = builder.Configuration;

builder.Services.AddMediatR(Assembly.Load("Loymark.Back.Application"), typeof(Program).Assembly);
builder.Services.AddAutoMapper(Assembly.Load("Loymark.Back.Application"));

builder.Services.AddControllers(opts =>
{
    opts.Filters.Add(typeof(AppExceptionFilterAttribute));
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<PersistenceContext>(opt =>
{
    opt.UseSqlServer(config.GetConnectionString("Database")!, sqlopts =>
    {
        sqlopts.MigrationsHistoryTable("_MigrationHistory", config.GetValue<string>("SchemaName"));
    });
});

builder.Services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddTransient<IDbConnection>(_ => new SqlConnection(config.GetConnectionString("Database")));
builder.Services.AddTransient(typeof(UserService));
builder.Services.AddTransient(typeof(ActivityService));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
