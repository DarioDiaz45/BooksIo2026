using BooksIo2026.Data;
using BooksIo2026.Entities;
using BooksIo2026.IoC;
using BooksIo2026.Service.DTOs.Author;
using BooksIo2026.Service.DTOs.Book;
using BooksIo2026.Service.DTOs.Publisher;
using BooksIo2026.Service.Interfaces;
using Microsoft.Extensions.DependencyInjection;

internal class Program
{
    static IServiceProvider provider = DependencyInjectionContainer.Configure();
    static void Main(string[] args)
    {

        do
        {
            Console.WriteLine("Library Manager");
            Console.WriteLine("1. Authors");
            Console.WriteLine("2. Books");
            Console.WriteLine("3. Publishers");
            Console.WriteLine("0. Exit");
            Console.Write("Select an option:");
            var option = Console.ReadLine();
            switch (option)
            {
                case "1":
                    AuthorsMenu();
                    break;
                case "2":
                    BooksMenu();
                    break;
                case "3":
                    PublishersMenu();
                    break;
                case "0":
                    return;
                default:
                    break;
            }
        } while (true);

    }

    private static void BooksMenu()
    {
        using (var scoped = provider.CreateScope())
        {
            var bookService = scoped.ServiceProvider.GetRequiredService<IBookService>();
            var authorService = scoped.ServiceProvider.GetRequiredService<IAuthorService>();
            var publisherService = scoped.ServiceProvider.GetRequiredService<IPublisherService>();

            do
            {
                Console.Clear();
                Console.WriteLine("Book's Manager");
                Console.WriteLine("1. List of Books");
                Console.WriteLine("2. Add a Book");
                Console.WriteLine("3. Delete a Book");
                Console.WriteLine("4. Update a Book");
                Console.WriteLine("5. View Book Details");
                Console.WriteLine("0. Back to Main Menu");

                var op = Console.ReadLine();

                switch (op)
                {
                    case "1":
                        ListBooks(bookService);
                        break;
                    case "2":
                        AddBook(bookService, authorService, publisherService);
                        break;
                    case "3":
                        DeleteBook(bookService);
                        break;
                    case "4":
                        UpdateBook(bookService, authorService, publisherService);
                        break;
                    case "5":
                        ShowBookDetails(bookService);
                        break;
                    case "0":
                        return;
                }

            } while (true);
        }
    }

    private static void ShowBookDetails(IBookService bookService)
    {
        Console.Clear();
        Console.WriteLine("Book Details");
        ShowBooks(bookService);
        Console.Write("Select Book ID to view details: ");
        if (!int.TryParse(Console.ReadLine(), out int bookId))
        {
            Console.WriteLine("Invalid Book ID");
            Console.ReadLine();
            return;
        }
        var resultT = bookService.GetDetails(bookId);
        if (resultT.IsFailure)
        {
            ShowErrors(resultT.Errors);
            return;
        }
        var book = resultT.Value;
        Console.WriteLine($"ID: {book!.BookId}");
        Console.WriteLine($"Title: {book.Title}");
        Console.WriteLine($"Author: {book.AuthorName}");
        Console.WriteLine($"Publisher: {book.PublisherName}");
        Console.WriteLine($"Published Date: {book.PublishedDate.ToShortDateString()}");
        Console.WriteLine($"Price: {book.Price}");
        Console.WriteLine("Press any key to continue");
        Console.ReadLine();

    }

    private static void UpdateBook(IBookService service ,IAuthorService authorService, IPublisherService publisherService)
    {
        Console.Clear();
        Console.WriteLine("Update a Book");

        ShowBooks(service);
        Console.WriteLine("-----------------------------");


        Console.Write("Select an ID of the Book to update:");
        var id = int.Parse(Console.ReadLine()!);

        var bookResult = service.GetForUpdate(id);

        if (bookResult.IsFailure)
        {
            ShowErrors(bookResult.Errors);
            return;
        }

        var bookToUpdate = bookResult.Value;
        Console.WriteLine($"Book to update: {bookToUpdate!.Title}");
        Console.Write("New Title (ENTER to keep the same): ");
        var inputTitle = Console.ReadLine();
        var newTitle = !string.IsNullOrWhiteSpace(inputTitle) ? inputTitle : bookToUpdate.Title;

        Console.Write("Is Active? (y/n): ");
        bool isActive = Console.ReadLine()!.ToLower() == "y";

        Console.Write("Confirm the changes: (y/n): ");
        var response = Console.ReadLine();

        if (response!.ToLower() == "y")
        {
            bookToUpdate.Title = newTitle;

            var result = service.Update(bookToUpdate, isActive);

            if (result.IsFailure)
            {
                ShowErrors(result.Errors);
            }
            else
            {
                Console.WriteLine("Book updated successfully.");
            }
        }
        else
        {
            Console.WriteLine("Update cancelled.");
        }

        Console.ReadLine();
    }

    private static void DeleteBook(IBookService service)
    {
        Console.Clear();
        Console.WriteLine("Delete a Book");
        Console.WriteLine("List of Available Books");

        ShowBooks(service);

        Console.Write("Select Id of the Book to delete:");
        var id = int.Parse(Console.ReadLine()!);

        var bookResult = service.GetById(id);

        if (bookResult.IsFailure)
        {
            ShowErrors(bookResult.Errors);
            return;
        }

        var book = bookResult.Value;

        Console.Write($"Are you sure to delete {book!.Title}? (y/n): ");
        var response = Console.ReadLine();

        if (response!.ToLower() == "y")
        {
            var result = service.Delete(id);

            if (result.IsFailure)
            {
                ShowErrors(result.Errors);
            }
            else
            {
                Console.WriteLine("Book deleted successfully.");
            }
        }
        else
        {
            Console.WriteLine("Deletion cancelled.");
        }

        Console.ReadLine();
    }

    private static void AddBook(IBookService bookService, IAuthorService authorService, IPublisherService publisherService)
    {
        Console.Clear();
        Console.WriteLine("--- Add New Book ---");

        var dto = new BookCreateDto();

        Console.Write("Title: ");
        dto.Title = Console.ReadLine() ?? "";


        Console.WriteLine("\nAvailable Authors:");
        ShowAuthors(authorService);

        Console.Write("Select Author ID: ");
        if (!int.TryParse(Console.ReadLine(), out int authorId))
        {
            Console.WriteLine("Invalid Author ID");
            Console.ReadLine();
            return;
        }
        dto.AuthorId = authorId;


        Console.WriteLine("\nAvailable Publishers:");
        ShowPublishers(publisherService);

        Console.Write("Select Publisher ID: ");
        if (!int.TryParse(Console.ReadLine(), out int publisherId))
        {
            Console.WriteLine("Invalid Publisher ID");
            Console.ReadLine();
            return;
        }
        dto.PublisherId = publisherId;

        Console.Write("Published Date (yyyy-mm-dd): ");
        if (DateTime.TryParse(Console.ReadLine(), out DateTime date))
        {
            dto.PublishedDate = date;
        }

        Console.Write("Price: ");
        if (decimal.TryParse(Console.ReadLine(), out decimal price))
        {
            dto.Price = price;
        }


        var result = bookService.Add(dto);

        if (result.IsFailure)
        {
            ShowErrors(result.Errors);
        }
        else
        {
            Console.WriteLine("Book added successfully!!!");
        }

        Console.ReadLine();
    }
    private static void ListBooks(IBookService service)
    {
        Console.Clear();
        Console.WriteLine("List of Books");
        ShowBooks(service);
        Console.WriteLine("Press any key to continue");
        Console.ReadLine();
    }



    private static void ShowBooks(IBookService service)
    {
        var booksResult = service.GetAll();
        if (booksResult.IsFailure)
        {
            ShowErrors(booksResult.Errors);

        }
        var books = booksResult.Value;
        foreach (var b in books!)
        {
            Console.WriteLine($"Id: {b.BookId,4} Title: {b.Title,-25} Author: {b.AuthorName,-25} Publisher: {b.PublisherName,-25}");
        }
    }
    //Aca empieza publisher y termina book

    private static void PublishersMenu()
    {
        using (var scoped = provider.CreateScope())
        {
            var service = scoped.ServiceProvider.GetRequiredService<IPublisherService>();

            do
            {
                Console.Clear();
                Console.WriteLine("Publisher Manager");
                Console.WriteLine("1. List Publishers");
                Console.WriteLine("2. Add Publisher");
                Console.WriteLine("3. Delete an Publisher");
                Console.WriteLine("4. Update an Publisher");
                Console.WriteLine("5. View Publisher Details");
                Console.WriteLine("0. Back");
                Console.Write("Select an option:");

                var option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        ListPublishers(service);
                        break;
                    case "2":
                        AddPublisher(service);
                        break;
                    case "3":
                        DeletePublisher(service);
                        break;
                    case "4":
                        UpdatePublisher(service);
                        break;
                    case "5":
                        ShowDetailsPublisher(service);
                        break;
                    case "0":
                        return;
                }

            } while (true);
        }
    }

    private static void ShowDetailsPublisher(IPublisherService service)
    {
        Console.Clear();
        Console.WriteLine("=== Publisher Details ===\n");

        var publishersResult = service.GetAll();

        if (publishersResult.IsFailure)
        {
            foreach (var error in publishersResult.Errors)
            {
                Console.WriteLine(error);
            }
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
            return;
        }

        foreach (var publisher in publishersResult.Value!)
        {
            Console.WriteLine($"{publisher.PublisherId} - {publisher.Name}");
        }

        Console.WriteLine();

        int publisherId;
        while (true)
        {
            Console.Write("Select a Publisher ID (0 to quit): ");
            var input = Console.ReadLine();

            if (!int.TryParse(input, out publisherId))
            {
                Console.WriteLine("You must enter a number.");
                continue;
            }
            if (publisherId == 0) return;
            var exists = publishersResult.Value!.Any(p => p.PublisherId == publisherId);

            if (!exists)
            {
                Console.WriteLine("The ID does not correspond to a listed publisher.");
                continue;
            }

            break;
        }

        var result = service.GetPublisherDetails(publisherId);

        Console.Clear();
        Console.WriteLine("=== Publisher Details ===\n");

        if (result.IsFailure)
        {
            ShowErrors(result.Errors);
            Console.ReadLine();
        }
        else
        {
            var publisher = result.Value!;

            Console.WriteLine($"Id: {publisher.PublisherId}");
            Console.WriteLine($"Name: {publisher.Name}");
            Console.WriteLine($"Country: {publisher.Country}");
            Console.WriteLine($"Founded Date: {publisher.FoundedDate:dd/MM/yyyy}");
            Console.WriteLine($"Email: {publisher.Email ?? "Not provided"}");
            Console.WriteLine();

            Console.WriteLine("--- BOOKS ---");

            if (!publisher.Books.Any())
            {
                Console.WriteLine("No associated books.");
            }
            else
            {
                foreach (var book in publisher.Books)
                {
                    Console.WriteLine($"{book.BookId} - {book.Title}");
                }
            }


        }
        Console.WriteLine("\nPress any key to continue...");
        Console.ReadKey();
    }

    private static void ShowPublishers(IPublisherService service)
    {
        var publishersResult = service.GetAll();
        if (publishersResult.IsFailure)
        {
            ShowErrors(publishersResult.Errors);
            return;
        }
        var publishers = publishersResult.Value;
        foreach (var publisher in publishers!)
        {
            Console.WriteLine($"Id: {publisher.PublisherId,4} Name: {publisher.Name,-25} Country: {publisher.Country,-25}");
        }
    }

    private static void UpdatePublisher(IPublisherService service)
    {
        Console.Clear();
        Console.WriteLine("Update a Publisher");

        ShowPublishers(service);

        Console.Write("Select an ID of the Publisher to update:");
        var id = int.Parse(Console.ReadLine()!);

        var publisherResult = service.GetForUpdate(id);
        if (publisherResult.IsFailure)
        {
            ShowErrors(publisherResult.Errors);
            return;
        }

        var publisherToUpdate = publisherResult.Value;

        Console.WriteLine($"Publisher to update: {publisherToUpdate!.Name}");

        Console.Write("New Name (ENTER to keep the same): ");
        var inputName = Console.ReadLine();
        var newName = !string.IsNullOrWhiteSpace(inputName) ? inputName : publisherToUpdate.Name;

        Console.Write("New Country (ENTER to keep the same): ");
        var inputCountry = Console.ReadLine();
        var newCountry = !string.IsNullOrWhiteSpace(inputCountry) ? inputCountry : publisherToUpdate.Country;

        Console.Write("Is Active? (y/n): ");
        bool isActive = Console.ReadLine()!.ToLower() == "y";

        Console.Write("Confirm the changes: (y/n): ");
        var response = Console.ReadLine();

        if (response!.ToLower() == "y")
        {
            publisherToUpdate.Name = newName;
            publisherToUpdate.Country = newCountry;

            var result = service.Update(publisherToUpdate, isActive);

            if (result.IsFailure)
            {
                ShowErrors(result.Errors);
            }
            else
            {
                Console.WriteLine("Publisher updated successfully.");
            }
        }
        else
        {
            Console.WriteLine("Update cancelled.");
        }

        Console.ReadLine();
    }

    private static void DeletePublisher(IPublisherService service)
    {
        Console.Clear();
        Console.WriteLine("Delete a Publisher");
        Console.WriteLine("List of Available Publishers");

        ShowPublishers(service);

        Console.Write("Select Id of the Publisher to delete:");
        var id = int.Parse(Console.ReadLine()!);

        var publisherResult = service.GetById(id);
        if (publisherResult.IsFailure)
        {
            ShowErrors(publisherResult.Errors);
            return;
        }

        var publisher = publisherResult.Value;

        Console.Write($"Are you sure to delete {publisher!.Name}? (y/n): ");
        var response = Console.ReadLine();

        if (response!.ToLower() == "y")
        {
            var result = service.Delete(id);

            if (result.IsFailure)
            {
                foreach (var error in result.Errors)
                {
                    Console.WriteLine(error);
                }
            }
            else
            {
                Console.WriteLine("Publisher deleted successfully.");
            }
        }
        else
        {
            Console.WriteLine("Deletion cancelled.");
        }


        Console.ReadLine();
    }

    private static void AddPublisher(IPublisherService service)
    {
        Console.Clear();
        Console.WriteLine("--- Add New Publisher ---");

        var dto = new PublisherCreateDto();

        Console.Write("Name: ");
        dto.Name = Console.ReadLine() ?? "";

        Console.Write("Country: ");
        dto.Country = Console.ReadLine() ?? "";

        Console.Write("Founded Date (yyyy-mm-dd): ");
        if (DateTime.TryParse(Console.ReadLine(), out DateTime date))
        {
            dto.FoundedDate = date;
        }

        Console.Write("Email (optional): ");
        dto.Email = Console.ReadLine();
        var result = service.Add(dto, true);
        if (result.IsFailure)
        {
            ShowErrors(result.Errors);
        }
        else
        {
            Console.WriteLine("Publisher added succesfully!!!");

        }
        Console.ReadKey();
    }

    private static void ListPublishers(IPublisherService service)
    {
        Console.Clear();
        Console.WriteLine("List of Publishers");

        ShowPublishers(service);

        Console.WriteLine("Press any key to continue...");
        Console.ReadLine();
    }
    //Aca empieza author y termina publisher 
    private static void AuthorsMenu()
    {
        using (var scoped = provider.CreateScope())
        {
            var service = scoped.ServiceProvider.GetRequiredService<IAuthorService>();
            do
            {
                Console.Clear();
                Console.WriteLine("Author's Manager");
                Console.WriteLine("1. List of Authors");
                Console.WriteLine("2. Add an Author");
                Console.WriteLine("3. Delete an Author");
                Console.WriteLine("4. Update an Author");
                Console.WriteLine("5. View Author Details");

                Console.WriteLine("0. Back to Main Menu");
                Console.Write("Select an option:");
                var opcion = Console.ReadLine();
                switch (opcion)
                {
                    case "1":
                        ListAuthors(service);
                        break;
                    case "2":
                        AddAuthor(service);
                        break;
                    case "3":
                        DeleteAuthor(service);
                        break;
                    case "4":
                        UpdateAuthor(service);
                        break;
                    case "5":
                        ShowDetailsAuthors(service);
                        break;
                    case "0":
                        Console.WriteLine("Exiting...");
                        return;
                    default:
                        break;
                }


            } while (true);
        }



    }

    private static void ShowDetailsAuthors(IAuthorService service)
    {
        Console.Clear();
        Console.WriteLine("=== Author's Details ===\n");

        var authorsResult = service.GetAll();

        if (authorsResult.IsFailure)
        {
            ShowErrors(authorsResult.Errors);
        }

        foreach (var author in authorsResult.Value!)
        {
            Console.WriteLine($"{author.AuthorId,2} - {author.FullName}");
        }

        Console.WriteLine();

        int authorId;
        while (true)
        {
            Console.Write("Select An Author ID to view details (0 to quit): ");
            var input = Console.ReadLine();

            if (!int.TryParse(input, out authorId))
            {
                Console.WriteLine("You must enter a number.");
                continue;
            }
            if (authorId == 0) return;
            var exists = authorsResult.Value!.Any(a => a.AuthorId == authorId);

            if (!exists)
            {
                Console.WriteLine("The ID does not correspond to a listed author.");
                continue;
            }

            break;
        }

        var result = service.GetAuthorDetails(authorId);

        Console.Clear();
        Console.WriteLine("=== Author Details ===\n");

        if (result.IsFailure)
        {
            foreach (var error in result.Errors)
            {
                Console.WriteLine(error);
            }
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
            return;

        }
        else
        {
            var author = result.Value!;

            Console.WriteLine($"Id: {author.AuthorId}");
            Console.WriteLine($"FullName: {author.FirstName} {author.LastName}");
            Console.WriteLine();

            Console.WriteLine("--- Books ---");

            if (!author.Books.Any())
            {
                Console.WriteLine("No associated books.");
            }
            else
            {
                foreach (var book in author.Books)
                {
                    Console.WriteLine($"{book.BookId} - {book.Title}");
                }
            }
        }

        Console.WriteLine("\nPress any key to continue...");
        Console.ReadKey();
    }

    private static void UpdateAuthor(IAuthorService service)
    {
        Console.Clear();
        Console.WriteLine("Update an Author");
        ShowAuthors(service);
        using (var context = new BooksDbContext())
        {
            Console.Write("Select an ID of the Author to update:");
            var authorId = int.Parse(Console.ReadLine()!);
            var authorResult = service.GetForUpdate(authorId);
            if (authorResult.IsFailure)
            {
                ShowErrors(authorResult.Errors);
                return;
            }
            var authorToUpdate = authorResult.Value;
            Console.WriteLine($"Author to update: {authorToUpdate!.FirstName} {authorToUpdate.LastName}");
            Console.Write("New First Name: (ENTER to keep the same)");
            var inputFirstName = Console.ReadLine();
            var newFirstName = !string.IsNullOrWhiteSpace(inputFirstName) ? inputFirstName : authorToUpdate!.FirstName;

            Console.Write("New Last Name: (ENTER to keep the same)");
            var inputLastName = Console.ReadLine();
            var newLastName = !string.IsNullOrWhiteSpace(inputLastName) ? inputLastName : authorToUpdate!.LastName;
            Console.Write("Confirm the changes: (y/n)");
            var response = Console.ReadLine();
            if (response!.ToLower() == "y")
            {

                authorToUpdate!.FirstName = newFirstName;
                authorToUpdate.LastName = newLastName;
                var result = service.Update(authorToUpdate);
                if (result.IsFailure)
                {
                    foreach (var error in result.Errors)
                    {
                        Console.WriteLine(error);
                    }
                }
                else
                {
                    Console.WriteLine("Author updated successfully.");
                }


            }
            else
            {
                Console.WriteLine("Update cancelled.");
            }

            Console.ReadLine();
        }
    }

    private static void ShowErrors(List<string> errors)
    {
        foreach (var error in errors)
        {
            Console.WriteLine(error);
        }
        Console.WriteLine("Press any key to continue...");

    }

    private static void DeleteAuthor(IAuthorService service)
    {
        Console.Clear();
        Console.WriteLine("Delete an Author");
        Console.WriteLine("List of Available Authors");
        ShowAuthors(service);

        Console.Write("Select Id of the Author to delete:");
        var authorId = int.Parse(Console.ReadLine()!);
        var authorResult = service.GetById(authorId);
        if (authorResult.IsFailure)
        {
            ShowErrors(authorResult.Errors);
            return;
        }
        var authorToDelete = authorResult.Value;

        Console.Write($"Are you sure to delete {authorToDelete!.FullName}? (y/n): ");
        var response = Console.ReadLine();
        if (response!.ToLower() == "y")
        {
            var result = service.Delete(authorToDelete.AuthorId);
            if (result.IsFailure)
            {
                ShowErrors(result.Errors);
            }
            else
            {
                Console.WriteLine("Author deleted successfully.");
            }


        }
        else
        {
            Console.WriteLine("Deletion cancelled.");
        }

        Console.ReadLine();


    }

    private static void AddAuthor(IAuthorService service)
    {
        Console.Clear();
        Console.WriteLine("Add a New Author");
        Console.Write("First Name:");
        var firstName = Console.ReadLine();
        Console.Write("Last Name:");
        var lastName = Console.ReadLine();

        var authorDto = new AuthorCreateDto
        {
            FirstName = firstName!,
            LastName = lastName!
        };
        var result = service.Add(authorDto);
        if (result.IsFailure)
        {
            ShowErrors(result.Errors);
        }
        else
        {
            Console.WriteLine("Author added successfully.");
        }

        Console.ReadKey();


    }

    private static void ListAuthors(IAuthorService service)
    {
        Console.Clear();
        Console.WriteLine("List of Authors");
        ShowAuthors(service);
        Console.ReadLine();
    }

    private static void ShowAuthors(IAuthorService service)
    {
        var authorsResult = service.GetAll();
        if (authorsResult.IsFailure)
        {
            ShowErrors(authorsResult.Errors);
            return;
        }
        var authors = authorsResult.Value;
        foreach (var author in authors!)
        {
            Console.WriteLine($"Id: {author.AuthorId,4} Author:{author.FullName,-30}");
        }
    }
}
