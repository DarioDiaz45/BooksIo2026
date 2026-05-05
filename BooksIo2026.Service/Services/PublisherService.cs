using BooksIo2026.Data;
using BooksIo2026.Entities;
using BooksIo2026.Service.Common;
using BooksIo2026.Service.DTOs.Publisher;
using BooksIo2026.Service.Interfaces;
using BooksIo2026.Service.Mappers;
using FluentValidation;

namespace BooksIo2026.Service.Services
{
    public class PublisherService : IPublisherService
    {
        private readonly IUnitOfWork _uow;
        private readonly IValidator<Publisher> _validator;
        public PublisherService(IUnitOfWork unitOfWork, IValidator<Publisher> validator)
        {

            _uow = unitOfWork;
            _validator = validator;
        }
        public Result Add(PublisherCreateDto publisherDto, bool isActive)
        {
            var publisher = PublisherMapper.toEntity(publisherDto);
            var result = _validator.Validate(publisher);
            if (!result.IsValid)
            {
                return Result.Failure(result.Errors.Select(e => e.ErrorMessage).ToList());
            }
            if (_uow.Publishers.ExistSameName(publisher.Name, publisher.PublisherId))
            {
                return Result.Failure("Publisher already exist!!!");
            }
            try
            {
                _uow.Publishers.Add(publisher);
                _uow.Save();
                return Result.Success();
            }
            catch (Exception ex)
            {

                return Result.Failure(ex.Message);
            }

        }

        public Result Delete(int id)
        {
            var publisher = _uow.Publishers.GetById(id);
            if (publisher == null)
            {
                return Result.Failure("Publisher not found.");
            }
            if (_uow.Publishers.HasBooks(id))
            {
                return Result.Failure("Cannot delete publisher with associated books.");
            }
            try
            {
                _uow.Publishers.Delete(id);
                _uow.Save();
                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Failure(ex.Message);
            }
        }


        public List<PublisherListDto> GetAll()
        {
            return _uow.Publishers.GetAll()
                 .Select(p => PublisherMapper.ToPublisherListDto(p))
                 .ToList();
        }

        public PublisherDetailsDto GetById(int id)
        {
            var publisher = _uow.Publishers.GetById(id);
            if (publisher == null) return null!;

            return PublisherMapper.ToPublisherDetailsDto(publisher);
        }

        public PublisherUpdateDto? GetForUpdate(int id)
        {
            var publisher = _uow.Publishers.GetById(id);
            if (publisher == null) return null;

            return PublisherMapper.ToPublisherUpdateDto(publisher);
        }

        public Result Update(PublisherUpdateDto dto, bool isActive)
        {
            var publisherToValidate = PublisherMapper.ToEntity(dto);
            var result = _validator.Validate(publisherToValidate);
            if (!result.IsValid)
            {
                return Result.Failure(result.Errors.Select(e => e.ErrorMessage).ToList());

            }

            var publisher = _uow.Publishers.GetById(dto.PublisherId);
            if (publisher == null)
            {
                return Result.Failure("Publisher not found");

            }
            publisher.Name = dto.Name;
            publisher.Country = dto.Country;
            publisher.FoundedDate = dto.FoundedDate;
            publisher.Email = dto.Email;
            publisher.IsActive = dto.IsActive;
            if (_uow.Publishers.ExistSameName(publisher.Name, publisher.PublisherId))
            {
                return Result.Failure("Publisher already exist!!!");
            }
            try
            {
                _uow.Publishers.Update(publisher);
                _uow.Save();
                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Failure(ex.Message);
            }
        }
    }
}
