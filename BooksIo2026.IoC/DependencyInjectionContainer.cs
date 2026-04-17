using BooksIo2026.Data;
using BooksIo2026.Data.Interfaces;
using BooksIo2026.Data.Repositories;
using BooksIo2026.Entities;
using BooksIo2026.Service.Interfaces;
using BooksIo2026.Service.Services;
using BooksIo2026.Service.Validator;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace BooksIo2026.IoC
{
    public static class DependencyInjectionContainer
    {
        public static IServiceProvider Configure()
        {
            var services = new ServiceCollection();
            services.AddDbContext<BooksDbContext>();
            services.AddScoped<IAuthorRepository, AuthorRepository>();
            // services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<IAuthorService, AuthorService>();
            services.AddScoped<IValidator<Author>, AuthorValidator>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            // services.AddScoped<IBookService, BookService>();
            return services.BuildServiceProvider();
        }
    }
}
