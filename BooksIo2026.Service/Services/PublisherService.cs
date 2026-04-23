using BooksIo2026.Data;
using BooksIo2026.Data.Interfaces;
using BooksIo2026.Entities;
using BooksIo2026.Service.DTOs.Publisher;
using BooksIo2026.Service.Interfaces;
using BooksIo2026.Service.Mappers;
using FluentValidation;

namespace BooksIo2026.Service.Services
{
    public class PublisherService : IPublisherService
    {
        private readonly IPublisherRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<Publisher> _validator;
        public PublisherService(IPublisherRepository repository, IUnitOfWork unitOfWork, IValidator<Publisher> validator)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _validator = validator;
        }
        public (bool Success, List<string> Errors) Add(PublisherCreateDto dto, bool isActive)
        {
            var publisher = PublisherMapper.toEntity(dto);

            publisher.IsActive = isActive;

            var result = _validator.Validate(publisher);

            if (!result.IsValid)
            {
                var errors = result.Errors.Select(e => e.ErrorMessage).ToList();
                return (false, errors);
            }

            try
            {
                _repository.Add(publisher);
                _unitOfWork.Save();
                return (true, new List<string>());
            }
            catch (Exception)
            {
                return (false, new List<string> { "An error occurred while adding the publisher." });
            }
        }

        public (bool Success, List<string> Errors) Delete(int id)
        {
            try
            {
                _repository.Delete(id);
                _unitOfWork.Save();
                return (true, new List<string>());
            }
            catch (Exception)
            {
                return (false, new List<string> { "An error occurred while deleting the publisher." });
            }
        }


        public List<PublisherListDto> GetAll()
        {
            return _repository.GetAll()
                 .Select(p => PublisherMapper.ToPublisherListDto(p))
                 .ToList();
        }

        public PublisherDetailsDto GetById(int id)
        {
            var publisher = _repository.GetById(id);
            if (publisher == null) return null!;

            return PublisherMapper.ToPublisherDetailsDto(publisher);
        }

        public PublisherUpdateDto? GetForUpdate(int id)
        {
            var publisher = _repository.GetById(id);
            if (publisher == null) return null;

            return PublisherMapper.ToPublisherUpdateDto(publisher);
        }

        public (bool Success, List<string> Errors) Update(PublisherUpdateDto dto, bool isActive)
        {
            var publisher = _repository.GetById(dto.PublisherId);

            if (publisher == null)
            {
                return (false, new List<string> { "Publisher not found." });
            }

            publisher.Name = dto.Name;
            publisher.Country = dto.Country;
            publisher.FoundedDate = dto.FoundedDate;
            publisher.Email = dto.Email;
            publisher.IsActive = isActive;

            var result = _validator.Validate(publisher);

            if (!result.IsValid)
            {
                var errors = result.Errors.Select(e => e.ErrorMessage).ToList();
                return (false, errors);
            }

            try
            {
                _unitOfWork.Save();
                return (true, new List<string>());
            }
            catch (Exception)
            {
                return (false, new List<string> { "An error occurred while updating the publisher." });
            }
        }
    }
}
