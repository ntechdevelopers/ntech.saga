using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Ntech.Saga.Contracts;
using Ntech.Saga.Service.Api.Commands;

namespace Ntech.Saga.Service.Api
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            var bus = BusConfigurator.ConfigureBus();
            var sendToUri = new Uri($"{RabbitMqConstants.RabbitMqUri}{RabbitMqConstants.SagaQueue}");
            var endPoint = bus.GetSendEndpoint(sendToUri).Result;

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World!");
                });

                endpoints.MapGet("/booking", async context =>
                {
                    await endPoint.Send<IBookingRequestCommand>(new BookingRequestCommand
                    {
                        CustomerId = "Ntech => Booking Successfully",
                        BookingId = "booking-1234",
                        RequestTime = DateTime.Now
                    });
                    await context.Response.WriteAsync("Send booking...");
                });

                endpoints.MapGet("/booking/cancel", async context =>
                {
                    await endPoint.Send<IBookingRequestCommand>(new BookingRequestCommand
                    {
                        CustomerId = "Ntech => Booking Cancelled",
                        BookingId = "cancel-1234",
                        RequestTime = DateTime.Now
                    });
                    await context.Response.WriteAsync("Send booking...");
                });

                endpoints.MapGet("/booking/fail", async context =>
                {
                    await endPoint.Send<IBookingRequestCommand>(new BookingRequestCommand
                    {
                        CustomerId = "Ntech => Booking Failure",
                        BookingId = "null",
                        RequestTime = DateTime.Now
                    });
                    await context.Response.WriteAsync("Send booking...");
                });
            });
        }
    }
}
