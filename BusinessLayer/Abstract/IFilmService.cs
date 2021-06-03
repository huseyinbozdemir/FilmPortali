using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IFilmService
    {
        List<Film> GetAll();
        Film GetLast();
        Film Get(Expression<Func<Film, bool>> filter);
        List<Film> GetDirectorFilms(Expression<Func<Film, bool>> filter);
        List<Film> GetTrendingFilms();
        List<Film> GetPopularFilms();
        List<Film> GetRecentlyAddedFilms();
        List<Film> TopView(int choose);
        List<Film> LastComments();
        void Add(Film p);
        void Update(Film p);
        void Delete(Film p);
    }
}
