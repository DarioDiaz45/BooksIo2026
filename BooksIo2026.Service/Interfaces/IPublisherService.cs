using BooksIo2026.Service.DTOs.Publisher;
using System;
using System.Collections.Generic;
using System.Text;

namespace BooksIo2026.Service.Interfaces
{
    public interface IPublisherService
    {
        List<PublisherListDto> GetAll();
        (bool Success, List<string> Errors) Add(PublisherCreateDto dto);
    }
}
