using Lab5.Models;
using Lab5.Services;


[TestClass]
public class LibraryServiceTests
{

    // Test for ReadBooks();
    [TestMethod]
    public async Task ReadBooks_ShouldReturnListOfBooks()
    {
        // Arrange
        LibraryService libraryService = new LibraryService();

        // Act
        List<Book> books = await Task.Run(() => libraryService.ReadBooks()); // Simulate async for ReadBooks 

        // Assert
        Assert.IsNotNull(books);
        Assert.AreEqual(libraryService.ReadBooks().Count, books.Count); // Verify the count matches

    }


    // Test for AddBook();
    [TestMethod]
    public async Task AddBook_ShouldAddBookToList()
    {
        // Arrange
        LibraryService libraryService = new LibraryService();
        List<Book> initialBooks = await Task.Run(() => libraryService.ReadBooks());
        var bookToBeAdded = new List<Book>
        {
            new Book { Id = 1, Title = "Book 1", Author = "Author 1", ISBN = "ISBN001" }
        };
        var newBook = new Book { Title = "New Book", Author = "New Author", ISBN = "NEWISBN001" };

        // Act
        libraryService.AddBook(newBook);
        List<Book> updatedBooks = await Task.Run(() => libraryService.ReadBooks()); // Simulate async for ReadBooks

        // Assert
        Assert.IsNotNull(updatedBooks);
        Assert.AreEqual(initialBooks.Count + 1, updatedBooks.Count); // Verify a new book was added
        Assert.IsTrue(updatedBooks.Any(b => b.Title == "New Book")); // Check the new book exists
    }


    // Test for EditBook();
    [TestMethod]
    public async Task EditBook_ShouldUpdateBookDetails()
    {
        // Arrange
        LibraryService libraryService = new LibraryService();
        var updatedTitle = "Updated Book";
        var updatedAuthor = "Updated Author";
        var updatedISBN = "UPDATEDISBN001";

        // Act
        libraryService.EditBook(100, updatedTitle, updatedAuthor, updatedISBN);
        List<Book> updatedBooks = await Task.Run(() => libraryService.ReadBooks());

        // Assert
        Assert.IsNotNull(updatedBooks);
        var editedBook = updatedBooks.FirstOrDefault(b => b.Id == 100);
        Assert.IsNotNull(editedBook);
        Assert.AreEqual(updatedTitle, editedBook.Title);
        Assert.AreEqual(updatedAuthor, editedBook.Author);
        Assert.AreEqual(updatedISBN, editedBook.ISBN);
    }



    // Test for DeleteBook();
    [TestMethod]
    public async Task DeleteBook_ShouldRemoveBookFromList()
    {
        // Arrange
        LibraryService libraryService = new LibraryService();
        List<Book> initialBooks = await Task.Run(() => libraryService.ReadBooks());

        // Act
        libraryService.DeleteBook(100);
        List<Book> updatedBooks = await Task.Run(() => libraryService.ReadBooks());

        // Assert
        Assert.IsNotNull(updatedBooks);
        Assert.AreEqual(initialBooks.Count - 1, updatedBooks.Count);
        Assert.IsFalse(updatedBooks.Any(b => b.Id == 100));
    }


    // Test for ReadUsers();
    [TestMethod]
    public async Task ReadUsers_ShouldReturnListOfUsers()
    {
        // Arrange
        LibraryService libraryService = new LibraryService();

        // Act
        List<User> users = await Task.Run(() => libraryService.ReadUsers());

        // Assert
        Assert.IsNotNull(users);
        Assert.AreEqual(libraryService.ReadUsers().Count, users.Count);
    }


    // Test for AddUser();
    [TestMethod]
    public async Task AddUser_ShouldAddUserToList()
    {
        // Arrange
        LibraryService libraryService = new LibraryService();
        List<User> initialUsers = await Task.Run(() => libraryService.ReadUsers());
        var newUser = new User { Name = "New User", Email = "newuser@example.com" };

        // Act
        libraryService.AddUser(newUser);
        List<User> updatedUsers = await Task.Run(() => libraryService.ReadUsers());

        // Assert
        Assert.IsNotNull(updatedUsers);
        Assert.AreEqual(initialUsers.Count + 1, updatedUsers.Count);
        Assert.IsTrue(updatedUsers.Any(u => u.Name == "New User" && u.Email == "newuser@example.com"));
    }


    // Test for EditUser();
    [TestMethod]
    public async Task EditUser_ShouldUpdateUserDetails()
    {
        // Arrange
        LibraryService libraryService = new LibraryService();

        var updatedName = "Updated User";
        var updatedEmail = "updateduser@example.com";

        // Act
        libraryService.EditUser(10, updatedName, updatedEmail);
        List<User> updatedUsers = await Task.Run(() => libraryService.ReadUsers());

        // Assert
        Assert.IsNotNull(updatedUsers);
        var editedUser = updatedUsers.FirstOrDefault(u => u.Id == 10);
        Assert.IsNotNull(editedUser);
        Assert.AreEqual(updatedName, editedUser.Name);
        Assert.AreEqual(updatedEmail, editedUser.Email);
    }


    // Test for DeleteUser();
    [TestMethod]
    public async Task DeleteUser_ShouldRemoveUserFromList()
    {
        // Arrange
        LibraryService libraryService = new LibraryService();
        List<User> initialUsers = await Task.Run(() => libraryService.ReadUsers());

        // Act
        libraryService.DeleteUser(10);
        List<User> updatedUsers = await Task.Run(() => libraryService.ReadUsers());

        // Assert
        Assert.IsNotNull(updatedUsers);
        Assert.AreEqual(initialUsers.Count - 1, updatedUsers.Count);
        Assert.IsFalse(updatedUsers.Any(u => u.Id == 10));
    }


}
