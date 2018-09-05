using CoolBooks.Models;

namespace CoolBooks.Repositories
{
    public interface IAuthorsRepository
    {
        Authors CreateAuthor(Authors author);
    }
}