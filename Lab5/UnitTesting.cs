using Lab5.Models;
using Lab5.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;


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
        Assert.AreEqual(2, books.Count); // Verify the count matches
        Assert.AreEqual("Book 1", books.First().Title); // Verify the title of the first book
        Assert.AreEqual("Author 1", books.First().Author); // Verify the author of the first book
        Assert.AreEqual("ISBN001", books.First().ISBN); // Verify the ISBN of the first book
    }


    // Test for AddBook();
    [TestMethod]
    public async Task AddBook_ShouldAddBookToList()
    {
        // Arrange
        LibraryService libraryService = new LibraryService();
        var initialBooks = new List<Book>
        {
            new Book { Id = 1, Title = "Book 1", Author = "Author 1", ISBN = "ISBN001" }
        };

        var newBook = new Book { Title = "New Book", Author = "New Author", ISBN = "NEWISBN001" };

        // Act
        libraryService.AddBook(newBook);
        List<Book> updatedBooks = await Task.Run(() => libraryService.ReadBooks()); // Simulate async for ReadBooks

        // Assert
        Assert.IsNotNull(updatedBooks);
        Assert.AreEqual(2, updatedBooks.Count); // Verify a new book was added
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
        libraryService.EditBook(1, updatedTitle, updatedAuthor, updatedISBN);
        List<Book> updatedBooks = await Task.Run(() => libraryService.ReadBooks());

        // Assert
        Assert.IsNotNull(updatedBooks);
        var editedBook = updatedBooks.FirstOrDefault(b => b.Id == 1);
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

        // Act
        libraryService.DeleteBook(1);
        List<Book> updatedBooks = await Task.Run(() => libraryService.ReadBooks());

        // Assert
        Assert.IsNotNull(updatedBooks);
        Assert.AreEqual(1, updatedBooks.Count);
        Assert.IsFalse(updatedBooks.Any(b => b.Id == 1));
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
        Assert.AreEqual(2, users.Count);
        Assert.AreEqual("User 1", users.First().Name);
    }


    // Test for AddUser();
    [TestMethod]
    public async Task AddUser_ShouldAddUserToList()
    {
        // Arrange
        LibraryService libraryService = new LibraryService();

        var newUser = new User { Name = "New User", Email = "newuser@example.com" };

        // Act
        libraryService.AddUser(newUser);
        List<User> updatedUsers = await Task.Run(() => libraryService.ReadUsers());

        // Assert
        Assert.IsNotNull(updatedUsers);
        Assert.AreEqual(2, updatedUsers.Count);
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
        libraryService.EditUser(1, updatedName, updatedEmail);
        List<User> updatedUsers = await Task.Run(() => libraryService.ReadUsers());

        // Assert
        Assert.IsNotNull(updatedUsers);
        var editedUser = updatedUsers.FirstOrDefault(u => u.Id == 1);
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

        // Act
        libraryService.DeleteUser(1);
        List<User> updatedUsers = await Task.Run(() => libraryService.ReadUsers());

        // Assert
        Assert.IsNotNull(updatedUsers);
        Assert.AreEqual(1, updatedUsers.Count);
        Assert.IsFalse(updatedUsers.Any(u => u.Id == 1));
    }


}