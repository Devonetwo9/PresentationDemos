﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RoomWithAVue.Data;

namespace RoomWithAVue
{
  public class Startup
  {
    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddDbContext<AccomodationContext>();
      services.AddControllersWithViews()
        .AddNewtonsoftJson(j => j.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
        app.UseNodeModules();
      }

      app.UseStaticFiles();

      app.UseRouting();

      app.UseEndpoints(cfg =>
      {
        cfg.MapControllers();
      });
    }
  }
}
