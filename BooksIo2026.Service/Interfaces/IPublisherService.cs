using BooksIo2026.Service.DTOs.Publisher;


namespace BooksIo2026.Service.Interfaces
{
    public interface IPublisherService
    {
        List<PublisherListDto> GetAll();
        PublisherDetailsDto GetById(int id);
        PublisherUpdateDto? GetForUpdate(int id);
        (bool Success, List<string> Errors) Add(PublisherCreateDto dto, bool isActive);
        (bool Success, List<string> Errors) Update(PublisherUpdateDto dto, bool isActive);
        (bool Success, List<string> Errors) Delete(int id);
    }
}
