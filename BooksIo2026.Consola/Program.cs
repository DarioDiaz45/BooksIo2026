using BooksIo2026.Data;
using BooksIo2026.IoC;
using BooksIo2026.Service.DTOs.Author;
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
                    // BooksMenu();
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
                    case "0":
                        return;
                }

            } while (true);
        }
    }

    private static void AddPublisher(IPublisherService service)
    {
        Console.Clear();
        Console.WriteLine("Add Publisher");

        var dto = new PublisherCreateDto();

        Console.Write("Name: ");
        dto.Name = Console.ReadLine()!;

        Console.Write("Country: ");
        dto.Country = Console.ReadLine()!;

        Console.Write("Founded Date (yyyy-mm-dd): ");
        dto.FoundedDate = DateTime.Parse(Console.ReadLine()!);

        Console.Write("Email: ");
        dto.Email = Console.ReadLine();

        var result = service.Add(dto);

        if (!result.Success)
        {
            foreach (var error in result.Errors)
            {
                Console.WriteLine(error);
            }
        }
        else
        {
            Console.WriteLine("Publisher added successfully");
        }

        Console.WriteLine("Press any key...");
        Console.ReadLine();
    }

    private static void ListPublishers(IPublisherService service)
    {
        Console.Clear();
        Console.WriteLine("List of Publishers");

        var publishers = service.GetAll();

        foreach (var p in publishers)
        {
            Console.WriteLine($"{p.PublisherId} - {p.Name} - {p.Country}");
        }

        Console.WriteLine("Press any key...");
        Console.ReadLine();
    }

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
