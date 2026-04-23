using BooksIo2026.Data;
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
            var service = scoped.ServiceProvider.GetRequiredService<IBookService>();

            do
            {
                Console.Clear();
                Console.WriteLine("Book's Manager");
                Console.WriteLine("1. List of Books");
                Console.WriteLine("2. Add a Book");
                Console.WriteLine("3. Delete a Book");
                Console.WriteLine("4. Update a Book");
                Console.WriteLine("0. Back to Main Menu");

                var op = Console.ReadLine();

                switch (op)
                {
                    case "1":
                        ListBooks(service);
                        break;
                    case "2":
                        AddBook(service);
                        break;
                    case "3":
                        DeleteBook(service);
                        break;
                    case "4":
                        UpdateBook(service);
                        break;
                    case "0":
                        return;
                }

            } while (true);
        }
    }

    private static void UpdateBook(IBookService service)
    {
        Console.Clear();
        Console.WriteLine("Update a Book");

        ShowBooks(service);

        Console.Write("Select an ID of the Book to update:");
        var id = int.Parse(Console.ReadLine()!);

        var bookToUpdate = service.GetForUpdate(id);

        if (bookToUpdate != null)
        {
            Console.WriteLine($"Book to update: {bookToUpdate.Title}");

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

                if (!result.Success)
                {
                    foreach (var error in result.Errors)
                    {
                        Console.WriteLine(error);
                    }
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
        }
        else
        {
            Console.WriteLine("Book does not exist.");
        }

        Console.WriteLine("Press any key to continue...");
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

        var book = service.GetById(id);

        if (book != null)
        {
            Console.Write($"Are you sure to delete {book.Title}? (y/n): ");
            var response = Console.ReadLine();

            if (response!.ToLower() == "y")
            {
                var result = service.Delete(id);

                if (!result.Success)
                {
                    foreach (var error in result.Errors)
                    {
                        Console.WriteLine(error);
                    }
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
        }
        else
        {
            Console.WriteLine("Book not found.");
        }

        Console.WriteLine("Press any key to continue.");
        Console.ReadLine();
    }

    private static void AddBook(IBookService service)
    {
        Console.Clear();
        Console.WriteLine("Add a New Book");

        Console.Write("Title:");
        var title = Console.ReadLine();

        Console.Write("Author Id:");
        var authorId = int.Parse(Console.ReadLine()!);

        Console.Write("Publisher Id:");
        var publisherId = int.Parse(Console.ReadLine()!);

        Console.Write("Published Date:(yyyy-mm-dd): ");
        var publishedDate = DateTime.Parse(Console.ReadLine()!);

        Console.Write("Price:");
        var price = decimal.Parse(Console.ReadLine()!);

        Console.Write("Is Active? (y/n): ");
        var input = Console.ReadLine();
        bool isActive = input!.ToLower() == "y";

        var dto = new BookCreateDto
        {
            Title = title!,
            AuthorId = authorId,
            PublisherId = publisherId,
            PublishedDate = publishedDate,
            Price = price
        };

        var result = service.Add(dto, isActive);

        if (!result.Success)
        {
            foreach (var error in result.Errors)
            {
                Console.WriteLine(error);
            }
        }
        else
        {
            Console.WriteLine("Book added successfully.");
        }

        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }

    private static void ListBooks(IBookService service)
    {
        Console.Clear();
        Console.WriteLine("List of Books");

        ShowBooks(service);

        Console.WriteLine("Press any key to continue...");
        Console.ReadLine();
    }

    private static void ShowBooks(IBookService service)
    {
        var books = service.GetAll();

        foreach (var b in books)
        {
            Console.WriteLine($"Id: {b.BookId,4} Title: {b.Title,-30}");
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
                    case "0":
                        return;
                }

            } while (true);
        }
    }
    private static void ShowPublishers(IPublisherService service)
    {
        var publishers = service.GetAll();

        foreach (var p in publishers)
        {
            Console.WriteLine($"Id: {p.PublisherId,4} Publisher: {p.Name,-30}");
        }
    }

    private static void UpdatePublisher(IPublisherService service)
    {
        Console.Clear();
        Console.WriteLine("Update a Publisher");

        ShowPublishers(service);

        Console.Write("Select an ID of the Publisher to update:");
        var id = int.Parse(Console.ReadLine()!);

        var publisherToUpdate = service.GetForUpdate(id);

        if (publisherToUpdate != null)
        {
            Console.WriteLine($"Publisher to update: {publisherToUpdate.Name}");

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

                if (!result.Success)
                {
                    foreach (var error in result.Errors)
                    {
                        Console.WriteLine(error);
                    }
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
        }
        else
        {
            Console.WriteLine("Publisher does not exist.");
        }

        Console.WriteLine("Press any key to continue...");
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

        var publisher = service.GetById(id);

        if (publisher != null)
        {
            Console.Write($"Are you sure to delete {publisher.Name}? (y/n): ");
            var response = Console.ReadLine();

            if (response!.ToLower() == "y")
            {
                var result = service.Delete(id);

                if (!result.Success)
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
        }
        else
        {
            Console.WriteLine("Publisher not found.");
        }

        Console.WriteLine("Press any key to continue.");
        Console.ReadLine();
    }

    private static void AddPublisher(IPublisherService service)
    {
        Console.Clear();
        Console.WriteLine("Add a New Publisher");

        Console.Write("Name: ");
        var name = Console.ReadLine();

        Console.Write("Country: ");
        var country = Console.ReadLine();

        Console.Write("Founded Date (yyyy-mm-dd): ");
        var foundedDate = DateTime.Parse(Console.ReadLine()!);

        Console.Write("Email: ");
        var email = Console.ReadLine();

        Console.Write("Is Active? (y/n): ");
        bool isActive = Console.ReadLine()!.ToLower() == "y";

        var dto = new PublisherCreateDto
        {
            Name = name!,
            Country = country!,
            FoundedDate = foundedDate,
            Email = email
        };

        var result = service.Add(dto, isActive);

        if (!result.Success)
        {
            foreach (var error in result.Errors)
            {
                Console.WriteLine(error);
            }
        }
        else
        {
            Console.WriteLine("Publisher added successfully.");
        }

        Console.WriteLine("Press any key to continue...");
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
                    case "0":
                        Console.WriteLine("Exiting...");
                        return;
                    default:
                        break;
                }


            } while (true);
        }



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
            var authorToUpdate = service.GetForUpdate(authorId);
            if (authorToUpdate != null)
            {
                Console.WriteLine($"Author to update: {authorToUpdate.FirstName} {authorToUpdate.LastName}");
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
                    if (!result.Success)
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
            }
            else
            {
                Console.WriteLine("Author does not exist.");

            }
            Console.WriteLine("Key to continue...");
            Console.ReadLine();
        }
    }

    private static void DeleteAuthor(IAuthorService service)
    {
        Console.Clear();
        Console.WriteLine("Delete an Author");
        Console.WriteLine("List of Available Authors");
        ShowAuthors(service);

        Console.Write("Select Id of the Author to delete:");
        var authorId = int.Parse(Console.ReadLine()!);
        var authorToDelete = service.GetById(authorId);
        if (authorToDelete != null)
        {
            Console.Write($"Are you sure to delete {authorToDelete.FirstName} {authorToDelete.LastName}? (y/n): ");
            var response = Console.ReadLine();
            if (response!.ToLower() == "y")
            {
                var result = service.Delete(authorToDelete.AuthorId);
                if (!result.Success)
                {
                    foreach (var error in result.Errors)
                    {
                        Console.WriteLine(error);
                    }
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
        }
        else
        {
            Console.WriteLine("Author not found.");

        }
        Console.WriteLine("Key to continue.");
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
        if (!result.Success)
        {
            foreach (var error in result.Errors)
            {
                Console.WriteLine(error);
            }
        }
        else
        {
            Console.WriteLine("Author added successfully.");
        }



        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();


    }

    private static void ListAuthors(IAuthorService service)
    {
        Console.Clear();
        Console.WriteLine("List of Authors");
        ShowAuthors(service);
        Console.WriteLine("Press any key to continue...");
        Console.ReadLine();
    }

    private static void ShowAuthors(IAuthorService service)
    {
        var authors = service.GetAll();
        foreach (var author in authors)
        {
            Console.WriteLine($"Id: {author.AuthorId,4} Author:{author.FullName,-30}");
        }
    }
}
