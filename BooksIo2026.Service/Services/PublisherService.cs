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
        public (bool Success, List<string> Errors) Add(PublisherCreateDto dto)
        {
            try
            {
                var publisher = new Publisher
                {
                    Name = dto.Name,
                    Country = dto.Country,
                    FoundedDate = dto.FoundedDate,
                    Email = dto.Email,
                    IsActive = true
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
    }
}
