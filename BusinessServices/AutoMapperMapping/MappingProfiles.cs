using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BusinessDataModels;
using BusinessEntities;

namespace BusinessServices.AutoMapperMapping
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<tblPhoneBookApp, PhoneBookModel>().ReverseMap();
       
        }
    }
}
