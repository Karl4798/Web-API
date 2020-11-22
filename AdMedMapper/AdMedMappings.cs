using AdMedAPI.Models;
using AdMedAPI.Models.Dtos;
using AutoMapper;

namespace AdMedAPI.AdMedMapper
{
    // Helper class used for auto mappings of all entities and Dtos
    public class AdMedMappings : Profile
    {
        // Auto mapper constructor
        public AdMedMappings()
        {
            // Auto mappings for all Dtos and database entities
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