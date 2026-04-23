using BooksIo2026.Entities;
using BooksIo2026.Service.DTOs.Publisher;

namespace BooksIo2026.Service.Mappers
{
    public static class PublisherMapper
    {
        public static PublisherListDto ToPublisherListDto(Publisher publisher)
        {
            return new PublisherListDto
            {
                PublisherId = publisher.PublisherId,
                Name = publisher.Name
            };
        }

        public static PublisherUpdateDto ToPublisherUpdateDto(Publisher publisher)
        {
            return new PublisherUpdateDto
            {
                PublisherId = publisher.PublisherId,
                Name = publisher.Name,
                Country = publisher.Country,
                FoundedDate = publisher.FoundedDate,
                Email = publisher.Email
            };
        }

        public static Publisher toEntity(PublisherUpdateDto dto)
        {
            return new Publisher
            {
                PublisherId = dto.PublisherId,
                Name = dto.Name,
                Country = dto.Country,
                FoundedDate = dto.FoundedDate,
                Email = dto.Email
            };
        }

        public static PublisherDetailsDto ToPublisherDetailsDto(Publisher publisher)
        {
            return new PublisherDetailsDto
            {
                PublisherId = publisher.PublisherId,
                Name = publisher.Name,
                Country = publisher.Country,
                FoundedDate = publisher.FoundedDate,
                Email = publisher.Email
            };
        }

        public static Publisher toEntity(PublisherCreateDto dto)
        {
            return new Publisher
            {
                Name = dto.Name,
                Country = dto.Country,
                FoundedDate = dto.FoundedDate,
                Email = dto.Email
            };
        }
    }
}
