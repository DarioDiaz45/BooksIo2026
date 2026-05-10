using BooksIo2026.Service.Common;
using BooksIo2026.Service.DTOs.Publisher;


namespace BooksIo2026.Service.Interfaces
{
    public interface IPublisherService
    {
        Result<List<PublisherListDto>> GetAll();
        Result<PublisherListDto> GetById(int id);
        Result<PublisherUpdateDto> GetForUpdate(int id);
        Result Add(PublisherCreateDto dto, bool isActive);
        Result Update(PublisherUpdateDto dto, bool isActive);
        Result Delete(int id);
    }
}
