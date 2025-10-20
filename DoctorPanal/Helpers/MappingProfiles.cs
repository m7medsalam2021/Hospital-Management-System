using AutoMapper;
using Hospital.Core.Dtos;
using Hospital.Core.Entities;

namespace DoctorPanal.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<PrescriptionDetail, PrescriptionDetailDto>().ReverseMap();
        }
    }
}
