using IntranetWebApi.Data;
using IntranetWebApi.Repository;
using IntranetWebApi.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

#region Add services to the container
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton(typeof(IGenericRepository<>), typeof(GenericRepository<>));
#endregion Add services to the container

#region Database
builder.Services.AddDbContext<IntranetDbContext>(x =>
    x.UseSqlServer(builder.Configuration.GetConnectionString(nameof(ConnectionString.JDPIntranet))));
#endregion Database

var app = builder.Build();

#region Swagger
app.UseSwaggerUI();
app.UseSwagger(x => x.SerializeAsV2 = true);
#endregion Swagger

#region Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwagger();
}
#endregion Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.Run();
