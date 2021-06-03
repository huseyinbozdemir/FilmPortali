using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface IFilmDal : IEntityRepository<Film>
    {
        Film GetLast();
        List<Film> GetDirectorFilms(Expression<Func<Film, bool>> filter);
        List<Film> GetTrendingFilms();
        List<Film> GetPopularFilms();
        List<Film> GetRecentlyAddedFilms();
        List<Film> TopView(int choose);
        List<Film> LastComments();
    }
}
