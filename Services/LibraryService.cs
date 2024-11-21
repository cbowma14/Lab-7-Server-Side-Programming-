using Lab5.Models;
using static System.Reflection.Metadata.BlobBuilder;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Lab5.Services
{

    public class LibraryService : ILibraryService
    {

        // Reads the data from the Books.csv file
        public List<Book> ReadBooks()
        {
            var books = new List<Book>();

            try
            {
                using (var reader = new StreamReader("Data/Books.csv"))
                {
                    string? line;
                    bool isHeader = true;

                    while ((line = reader.ReadLine()) != null)
                    {
                        if (isHeader)
                        {
                            // Skip header row
                            isHeader = false;
                            continue;
                        }

                        var values = line.Split(',');

                        if (values.Length == 4)
                        {
                            try
                            {
                                var book = new Book
                                {
                                    Id = int.Parse(values[0]),
                                    Title = values[1],
                                    Author = values[2],
                                    ISBN = values[3]
                                };

                                books.Add(book);
                            }
                            catch (FormatException ex)
                            {
                                Console.WriteLine($"Error parsing line: {line}. Exception: {ex.Message}");
                            }
                        }
                        else
                        {
                            Console.WriteLine($"Skipping invalid line: {line}");
                        }
                    }
                }
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine($"File not found: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }

            return books;
        }


        // Reads the data from the Users.csv file
        public List<User> ReadUsers()
        {
            var users = new List<User>();

            try
            {
                using (var reader = new StreamReader("Data/Users.csv"))
                {
                    string? line;
                    bool isHeader = true;

                    while ((line = reader.ReadLine()) != null)
                    {
                        if (isHeader)
                        {
                            // Skip header row
                            isHeader = false;
                            continue;
                        }

                        var values = line.Split(',');

                        if (values.Length == 3) // Ensure there are exactly 3 columns
                        {
                            try
                            {
                                var user = new User
                                {
                                    Id = int.Parse(values[0]),
                                    Name = values[1],
                                    Email = values[2]
                                };

                                users.Add(user);
                            }
                            catch (FormatException ex)
                            {
                                Console.WriteLine($"Error parsing line: {line}. Exception: {ex.Message}");
                            }
                        }
                        else
                        {
                            Console.WriteLine($"Skipping invalid line: {line}");
                        }
                    }
                }
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine($"File not found: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }

            return users;
        }


        // Adds a book
        public void AddBook(Book book)
        {
            var books = ReadBooks();
            book.Id = books.Any() ? books.Max(b => b.Id) + 1 : 1;
            books.Add(book);
            WriteBookToCsv(books); // Pass the updated list directly
        }

        // Edits a book
        public void EditBook(int id, string newTitle, string newAuthor, string newISBN)
        {
            var books = ReadBooks();
            Book book = books.FirstOrDefault(b => b.Id == id);
            if (book != null)
            {
                book.Title = newTitle;
                book.Author = newAuthor;
                book.ISBN = newISBN;
                WriteBookToCsv(books); // Pass the updated list directly
            }
            else
            {
                Console.WriteLine("Book not found.");
            }
        }

        // Deletes a book
        public void DeleteBook(int id)
        {
            var books = ReadBooks();
            var book = books.FirstOrDefault(b => b.Id == id);
            if (book != null)
            {
                books.Remove(book);
                WriteBookToCsv(books); // Pass the updated list directly
            }
            else
            {
                Console.WriteLine("Book not found.");
            }
        }

        // Writes the changes to the Books.csv file
        private void WriteBookToCsv(List<Book> books)
        {
            try
            {
                using (var writer = new StreamWriter("Data/Books.csv"))
                {
                    // Write header
                    writer.WriteLine("Id,Title,Author,ISBN");

                    foreach (var book in books)
                    {
                        writer.WriteLine($"{book.Id},{book.Title},{book.Author},{book.ISBN}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while writing to file: {ex.Message}");
            }
        }



        // Adds a user
        public void AddUser(User user)
        {
            var users = ReadUsers();
            // Generate a new Id for the user
            user.Id = users.Any() ? users.Max(u => u.Id) + 1 : 1;
            users.Add(user);
            WriteUsersToCsv(users); // Pass the updated list directly
        }

        // Edits a user
        public void EditUser(int id, string newName, string newEmail)
        {
            var users = ReadUsers();
            var user = users.FirstOrDefault(u => u.Id == id);
            if (user != null)
            {
                user.Name = newName;
                user.Email = newEmail;
                WriteUsersToCsv(users); // Pass the updated list directly
            }
            else
            {
                Console.WriteLine("User not found.");
            }
        }

        // Deletes a user
        public void DeleteUser(int id)
        {
            var users = ReadUsers();
            var user = users.FirstOrDefault(u => u.Id == id);
            if (user != null)
            {
                users.Remove(user);
                WriteUsersToCsv(users); // Pass the updated list directly
            }
            else
            {
                Console.WriteLine("User not found.");
            }
        }

        // Writes the changes to the Users.csv file
        private void WriteUsersToCsv(List<User> users)
        {
            try
            {
                using (var writer = new StreamWriter("Data/Users.csv"))
                {
                    // Write header
                    writer.WriteLine("Id,Name,Email");

                    foreach (var user in users)
                    {
                        writer.WriteLine($"{user.Id},{user.Name},{user.Email}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while writing to file: {ex.Message}");
            }
        }
    }
}
