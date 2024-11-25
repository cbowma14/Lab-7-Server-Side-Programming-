using Lab5.Models;

namespace Lab5.Services
{
    public interface ILibraryService
    {

        public List<Book> ReadBooks();
        public List<User> ReadUsers();
        public void AddBook(Book book);
        public void EditBook(int id, string newTitle, string newAuthor, string newISBN);
        public void DeleteBook(int id);
        public void AddUser(User user);
        public void EditUser(int id, string newName, string newEmail);
        public void DeleteUser(int id);




    }
}
