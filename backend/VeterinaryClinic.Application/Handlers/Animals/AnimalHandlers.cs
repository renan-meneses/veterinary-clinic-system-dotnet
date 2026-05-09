using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VeterinaryClinic.Domain.Dtos;
using VeterinaryClinic.Domain.Entities;
using VeterinaryClinic.Domain.Contracts.Repositories;
using VeterinaryClinic.Domain.Exceptions;
using VeterinaryClinic.Application.Commands.Animals;
using VeterinaryClinic.Application.Queries.Animals;

namespace VeterinaryClinic.Application.Handlers.Animals;

public class CreateAnimalCommandHandler : IRequestHandler<CreateAnimalCommand, ApiResponse<AnimalDto>>
{
    private readonly IRepository<Animal> _animalRepository;
    private readonly IRepository<Tutor> _tutorRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateAnimalCommandHandler> _logger;

    public CreateAnimalCommandHandler(
        IRepository<Animal> animalRepository,
        IRepository<Tutor> tutorRepository,
        IMapper mapper,
        ILogger<CreateAnimalCommandHandler> logger)
    {
        _animalRepository = animalRepository;
        _tutorRepository = tutorRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<ApiResponse<AnimalDto>> Handle(CreateAnimalCommand request, CancellationToken cancellationToken)
    {
        var animal = new Animal
        {
            Name = request.Name,
            Species = request.Species,
            Breed = request.Breed,
            Sex = request.Sex,
            BirthDate = request.BirthDate,
            ApproximateAge = request.ApproximateAge,
            Weight = request.Weight,
            Color = request.Color,
            Microchip = request.Microchip,
            Observations = request.Observations,
            PhotoUrl = request.PhotoUrl,
            Status = Enums.AnimalStatus.Active
        };

        foreach (var tutorId in request.TutorIds)
        {
            var tutorExists = await _tutorRepository.GetByIdAsync(tutorId, cancellationToken);
            if (tutorExists == null)
            {
                return ApiResponse<AnimalDto>.Fail($"Tutor with ID {tutorId} not found.");
            }

            animal.AnimalTutors.Add(new AnimalTutor
            {
                AnimalId = animal.Id,
                TutorId = tutorId,
                IsOwner = true,
                IsPrimary = animal.AnimalTutors.Count == 0
            });
        }

        await _animalRepository.AddAsync(animal, cancellationToken);
        _logger.LogInformation("Animal {AnimalId} created successfully", animal.Id);

        return ApiResponse<AnimalDto>.Ok(_mapper.Map<AnimalDto>(animal));
    }
}

public class UpdateAnimalCommandHandler : IRequestHandler<UpdateAnimalCommand, ApiResponse<AnimalDto>>
{
    private readonly IRepository<Animal> _animalRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<UpdateAnimalCommandHandler> _logger;

    public UpdateAnimalCommandHandler(
        IRepository<Animal> animalRepository,
        IMapper mapper,
        ILogger<UpdateAnimalCommandHandler> logger)
    {
        _animalRepository = animalRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<ApiResponse<AnimalDto>> Handle(UpdateAnimalCommand request, CancellationToken cancellationToken)
    {
        var animal = await _animalRepository.GetByIdAsync(request.Id, cancellationToken);
        if (animal == null)
        {
            return ApiResponse<AnimalDto>.Fail("Animal not found.");
        }

        animal.Name = request.Name;
        animal.Species = request.Species;
        animal.Breed = request.Breed;
        animal.Sex = request.Sex;
        animal.BirthDate = request.BirthDate;
        animal.ApproximateAge = request.ApproximateAge;
        animal.Weight = request.Weight;
        animal.Color = request.Color;
        animal.Microchip = request.Microchip;
        animal.Observations = request.Observations;
        animal.PhotoUrl = request.PhotoUrl;
        animal.UpdatedAt = DateTime.UtcNow;

        await _animalRepository.UpdateAsync(animal, cancellationToken);
        _logger.LogInformation("Animal {AnimalId} updated successfully", animal.Id);

        return ApiResponse<AnimalDto>.Ok(_mapper.Map<AnimalDto>(animal));
    }
}

public class GetAnimalsQueryHandler : IRequestHandler<GetAnimalsQuery, PaginatedApiResponse<AnimalDto>>
{
    private readonly IRepository<Animal> _animalRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<GetAnimalsQueryHandler> _logger;

    public GetAnimalsQueryHandler(
        IRepository<Animal> animalRepository,
        IMapper mapper,
        ILogger<GetAnimalsQueryHandler> logger)
    {
        _animalRepository = animalRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<PaginatedApiResponse<AnimalDto>> Handle(GetAnimalsQuery request, CancellationToken cancellationToken)
    {
        var filter = new AnimalFilter
        {
            SearchTerm = request.SearchTerm,
            Species = request.Species,
            Status = request.Status,
            Page = request.Page,
            PageSize = request.PageSize
        };

        var (animals, totalCount) = await _animalRepository.GetFilteredAsync(filter, cancellationToken);

        return PaginatedApiResponse<AnimalDto>.Ok(
            _mapper.Map<List<AnimalDto>>(animals),
            request.Page,
            request.PageSize,
            totalCount);
    }
}

public class GetAnimalByIdQueryHandler : IRequestHandler<GetAnimalByIdQuery, ApiResponse<AnimalDto>>
{
    private readonly IRepository<Animal> _animalRepository;
    private readonly IMapper _mapper;

    public GetAnimalByIdQueryHandler(IRepository<Animal> animalRepository, IMapper mapper)
    {
        _animalRepository = animalRepository;
        _mapper = mapper;
    }

    public async Task<ApiResponse<AnimalDto>> Handle(GetAnimalByIdQuery request, CancellationToken cancellationToken)
    {
        var animal = await _animalRepository.GetByIdAsync(request.Id, cancellationToken);
        if (animal == null)
        {
            return ApiResponse<AnimalDto>.Fail("Animal not found.");
        }

        return ApiResponse<AnimalDto>.Ok(_mapper.Map<AnimalDto>(animal));
    }
}

public class AnimalFilter
{
    public string? SearchTerm { get; set; }
    public Guid? Species { get; set; }
    public Guid? Status { get; set; }
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}