using AdMedAPI.Models;
using AdMedAPI.Models.Dtos;
using AutoMapper;

namespace AdMedAPI.AdMedMapper
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