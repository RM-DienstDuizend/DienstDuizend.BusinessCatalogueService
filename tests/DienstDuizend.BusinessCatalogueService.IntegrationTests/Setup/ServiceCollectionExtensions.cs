﻿using Microsoft.Extensions.DependencyInjection;

namespace DienstDuizend.BusinessCatalogueService.IntegrationTests.Setup;

internal static class ServiceCollectionExtensions
{
    public static void RemoveByType<T>(this IServiceCollection services)
    {
        var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(T));
        if (descriptor != null) services.Remove(descriptor);
    }
    
}