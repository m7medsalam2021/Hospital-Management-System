using AdminPanal.Models;
using AutoMapper;
using Hospital.Core.Dtos;
using Hospital.Core.Entities;

namespace AdminPanal.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Doctor, DoctorsDto>().ReverseMap();
        }
    }
}
