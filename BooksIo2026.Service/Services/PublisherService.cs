using BooksIo2026.Data;
using BooksIo2026.Data.Interfaces;
using BooksIo2026.Entities;
using BooksIo2026.Service.DTOs.Publisher;
using BooksIo2026.Service.Interfaces;

namespace BooksIo2026.Service.Services
{
    public class PublisherService : IPublisherService
    {
        private readonly IPublisherRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        public PublisherService(IPublisherRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }
        public (bool Success, List<string> Errors) Add(PublisherCreateDto dto, bool isActive)
        {
            try
            {
                var publisher = new Publisher
                {
                    Name = dto.Name,
                    Country = dto.Country,
                    FoundedDate = dto.FoundedDate,
                    Email = dto.Email,
                    IsActive = isActive
                };

                _repository.Add(publisher);
                _unitOfWork.Save();

                return (true, new List<string>());
            }
            catch
            {
                return (false, new List<string> { "Error creating publisher" });
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
            catch
            {
                return (false, new List<string> { "Error deleting publisher" });
            }
        }


        public List<PublisherListDto> GetAll()
        {
            return _repository.GetAll()
            .Select(p => new PublisherListDto
            {
                PublisherId = p.PublisherId,
                Name = p.Name,
                Country = p.Country
            }).ToList();
        }

        public PublisherDetailsDto GetById(int id)
        {
            var publisher = _repository.GetById(id);
            if (publisher == null) return null!;

            return new PublisherDetailsDto
            {
                PublisherId = publisher.PublisherId,
                Name = publisher.Name,
                Country = publisher.Country,
                FoundedDate = publisher.FoundedDate,
                Email = publisher.Email
            };
        }

        public PublisherUpdateDto? GetForUpdate(int id)
        {
            var p = _repository.GetById(id);
            if (p == null) return null;

            return new PublisherUpdateDto
            {
                PublisherId = p.PublisherId,
                Name = p.Name,
                Country = p.Country,
                FoundedDate = p.FoundedDate,
                Email = p.Email
            };
        }

        public (bool Success, List<string> Errors) Update(PublisherUpdateDto dto, bool isActive)
        {
            var publisher = _repository.GetById(dto.PublisherId);
            if (publisher == null)
                return (false, new List<string> { "Publisher not found" });

            publisher.Name = dto.Name;
            publisher.Country = dto.Country;
            publisher.FoundedDate = dto.FoundedDate;
            publisher.Email = dto.Email;
            publisher.IsActive = isActive;

            try
            {
                _unitOfWork.Save();
                return (true, new List<string>());
            }
            catch
            {
                return (false, new List<string> { "Error updating publisher" });
            }
        }
    }
}
