using AutoMapper;
using VeterinaryClinic.Application.Handlers.Animals;
using VeterinaryClinic.Application.Commands.Animals;
using VeterinaryClinic.Application.Queries.Animals;
using VeterinaryClinic.Domain.Entities;

namespace VeterinaryClinic.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Animal, AnimalDto>()
            .ForMember(dest => dest.Tutors, opt => opt.MapFrom(src => src.AnimalTutors.Select(at => new TutorDto
            {
                Id = at.Tutor.Id,
                Name = at.Tutor.Name,
                Email = at.Tutor.Email,
                Phone = at.Tutor.Phone,
                IsOwner = at.IsOwner,
                IsPrimary = at.IsPrimary
            })));

        CreateMap<CreateAnimalCommand, Animal>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow))
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.AnimalTutors, opt => opt.Ignore());

        CreateMap<UpdateAnimalCommand, Animal>()
            .ForMember(dest => dest.AnimalTutors, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore());

        CreateMap<Animal, AnimalCompleteHistoryDto>()
            .ForMember(dest => dest.Vaccines, opt => opt.MapFrom(src => src.Vaccines))
            .ForMember(dest => dest.Consultations, opt => opt.MapFrom(src => src.Consultations))
            .ForMember(dest => dest.Hospitalizations, opt => opt.MapFrom(src => src.Hospitalizations))
            .ForMember(dest => dest.MedicalRecords, opt => opt.MapFrom(src => src.MedicalRecords))
            .ForMember(dest => dest.PetshopAttendances, opt => opt.MapFrom(src => src.PetshopAttendances));

        CreateMap<Vaccine, VaccineDto>();
        CreateMap<Consultation, ConsultationDto>()
            .ForMember(dest => dest.VeterinarianName, opt => opt.MapFrom(src => src.Veterinarian != null ? src.Veterinarian.Name : null));
        CreateMap<Hospitalization, HospitalizationDto>();
        CreateMap<MedicalRecord, MedicalRecordDto>();
        CreateMap<PetshopAttendance, PetshopAttendanceDto>()
            .ForMember(dest => dest.ServiceName, opt => opt.MapFrom(src => src.Service != null ? src.Service.Name : null));
    }
}