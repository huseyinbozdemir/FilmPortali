using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class FilmManager : IFilmService
    {
        private IFilmDal _filmDal;

        public FilmManager(IFilmDal filmDal)
        {
            _filmDal = filmDal;
        }

        public void Add(Film p)
        {
            _filmDal.Add(p);
        }

        public void Delete(Film p)
        {
            _filmDal.Delete(p);
        }

        public Film Get(Expression<Func<Film, bool>> filter)
        {
            return _filmDal.Get(filter);
        }

        public List<Film> GetAll()
        {
            return _filmDal.GetAll();
        }

        public List<Film> GetDirectorFilms(Expression<Func<Film, bool>> filter)
        {
            return _filmDal.GetDirectorFilms(filter);
        }

        public Film GetLast()
        {
            return _filmDal.GetLast();
        }

        public List<Film> GetPopularFilms()
        {
            return _filmDal.GetPopularFilms();
        }

        public List<Film> GetRecentlyAddedFilms()
        {
            return _filmDal.GetRecentlyAddedFilms();
        }

        public List<Film> GetTrendingFilms()
        {
            return _filmDal.GetTrendingFilms();
        }

        public List<Film> LastComments()
        {
            return _filmDal.LastComments();
        }

        public List<Film> TopView(int choose)
        {
            return _filmDal.TopView(choose);
        }

        public void Update(Film p)
        {
            _filmDal.Update(p);
        }
    }
}
