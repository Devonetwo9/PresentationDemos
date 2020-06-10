﻿using Aversion.Controllers;
using Aversion.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.Mvc.Versioning.Conventions;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace Aversion
{
  public class Startup
  {
    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddDbContext<AversionContext>();
      services.AddTransient<AversionSeeder>();

      services.AddControllers()
        .AddNewtonsoftJson(cfg =>
        {
          cfg.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
        });

      services.AddApiVersioning(cfg =>
      {
        cfg.DefaultApiVersion = new ApiVersion(1, 0);
        cfg.AssumeDefaultVersionWhenUnspecified = true;
        cfg.ReportApiVersions = true;
        cfg.ApiVersionReader = ApiVersionReader.Combine(
          new HeaderApiVersionReader("X-Version"),
          new QueryStringApiVersionReader("ver"));

        cfg.Conventions
          .Controller<CustomersController>()
          .HasApiVersion(new ApiVersion(1, 0));

        cfg.Conventions
          .Controller<CustomersController>()
          .HasApiVersion(new ApiVersion(1, 1));

        cfg.Conventions
          .Controller<CustomersController>()
          .Action(c => c.Get(1))
          .HasApiVersion(new ApiVersion(1,0));

        cfg.Conventions
          .Controller<CustomersController>()
          .Action(c => c.Get11(1))
          .HasApiVersion(new ApiVersion(1, 1));
      });
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      app.UseRouting();

      app.UseEndpoints(cfg =>
      {
        cfg.MapControllers();
      });
    }
  }
}
