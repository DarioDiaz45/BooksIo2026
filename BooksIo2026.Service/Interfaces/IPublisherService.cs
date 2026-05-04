using BooksIo2026.Service.Common;
using BooksIo2026.Service.DTOs.Publisher;


namespace BooksIo2026.Service.Interfaces
{
    public interface IPublisherService
    {
        List<PublisherListDto> GetAll();
        PublisherDetailsDto GetById(int id);
        PublisherUpdateDto? GetForUpdate(int id);
        Result Add(PublisherCreateDto dto, bool isActive);
        Result Update(PublisherUpdateDto dto, bool isActive);
        Result Delete(int id);
    }
}
