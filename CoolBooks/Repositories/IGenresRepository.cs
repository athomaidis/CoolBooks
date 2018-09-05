using CoolBooks.Models;

namespace CoolBooks.Repositories
{
    public interface IGenresRepository
    {
        Genres CreateGenre(Genres genre); 
    }
}
