using LiveChat.Application.Commands;
using LiveChat.Application.Handlers;
using LiveChat.Application.Mappers;
using LiveChat.Application.Models;
using LiveChat.Application.Queries;
using LiveChat.Domain.Models;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LiveChat.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddSingleton(new Dictionary<(int, int), Queue<Message>>());
            services.AddSingleton(new Dictionary<int, IEnumerable<UserResponse>>());
            services.AddSingleton(new object());

            services.AddAutoMapper(typeof(UserMapper));

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

            return services;
        }
    }
}
