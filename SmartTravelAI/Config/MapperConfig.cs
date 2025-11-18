using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mapster;
using SmartTravelAI.Models;

namespace SmartTravelAI.Config
{
    public static class MapperConfig
    {
        public static WebApplicationBuilder ConfigureMapster(this WebApplicationBuilder builder)
        {
            ConfigureModelToModel();
            builder.Services.AddSingleton(TypeAdapterConfig.GlobalSettings);

            return builder;
        }

        private static void ConfigureModelToModel()
        {
            TypeAdapterConfig<User, User>.NewConfig().Ignore(dest => dest.Id);
            TypeAdapterConfig<TagKey, TagKey>.NewConfig().Ignore(dest => dest.Id);
            TypeAdapterConfig<TagValue, TagValue>.NewConfig().Ignore(dest => dest.Id);
            TypeAdapterConfig<UserTrip, UserTrip>.NewConfig().Ignore(dest => dest.Id);
            TypeAdapterConfig<UserTripRoute, UserTripRoute>.NewConfig().Ignore(dest => dest.Id);
            TypeAdapterConfig<UserTripWaypoint, UserTripWaypoint>.NewConfig().Ignore(dest => dest.Id);
        }
    }
}
