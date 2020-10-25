using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdMedAPI.Models;
using AdMedAPI.Models.Dtos;
using AutoMapper;

namespace AdMedAPI.ParkyMapper
{

    public class AdMedMappings : Profile
    {

        public AdMedMappings()
        {

            CreateMap<Application, ApplicationCreateDto>().ReverseMap();
            CreateMap<Application, ApplicationUpdateDto>().ReverseMap();
            CreateMap<Resident, ResidentCreateDto>().ReverseMap();
            CreateMap<Resident, ResidentUpdateDto>().ReverseMap();
            CreateMap<Medication, MedicationCreateDto>().ReverseMap();
            CreateMap<Medication, MedicationUpdateDto>().ReverseMap();
            CreateMap<Post, PostCreateDto>().ReverseMap();
            CreateMap<Post, PostUpdateDto>().ReverseMap();
            CreateMap<User, UserUpdateDto>().ReverseMap();

        }

    }

}
