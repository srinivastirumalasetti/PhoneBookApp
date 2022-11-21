using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using BusinessServices.AutoMapperMapping;

namespace GoodExample.Mappings
{
    public class AutoMapperConfiguration
    {
        public static void InitializeAutoMapper()
        {
            Mapper.Initialize(x => { x.AddProfile<MappingProfiles>(); });
            Mapper.Configuration.CompileMappings();
        }
    }
}