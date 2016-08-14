using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// new...
using AutoMapper;

namespace Assignment9
{
    public static class AutoMapperConfig
    {
        public static void RegisterMappings()
        {
            // Add map creation statements here
            // Mapper.CreateMap< FROM , TO >();

            // Disable AutoMapper v4.2.x warnings
#pragma warning disable CS0618

            // AutoMapper create map statements

            Mapper.CreateMap<Models.RegisterViewModel, Models.RegisterViewModelForm>();

            // Add more below...

            //map players
            Mapper.CreateMap<Models.Player, Controllers.PlayerBase>();
            Mapper.CreateMap<Controllers.PlayerAdd, Models.Player>();

            //Map teams
            Mapper.CreateMap<Models.Team, Controllers.TeamBase>();
            Mapper.CreateMap<Controllers.TeamAdd, Models.Team>();

            //Coaches 
            Mapper.CreateMap<Models.Coach, Controllers.CoachBase>();
            Mapper.CreateMap<Controllers.CoachAdd, Models.Coach>();

#pragma warning restore CS0618
        }
    }
}