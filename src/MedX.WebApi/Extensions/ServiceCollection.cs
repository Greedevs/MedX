﻿using MedX.Data.IRepositories;
using MedX.Data.Repositories;
using MedX.Service.Interfaces;
using MedX.Service.Mappers;
using MedX.Service.Services;

namespace MedX.WebApi.Extensions;

public static class ServiceCollection
{
    public static void AddServices(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(MappingProfile));
        services.AddScoped<IRoomService, RoomService>();
        services.AddScoped<IDoctorService, DoctorService>();
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
    }
}