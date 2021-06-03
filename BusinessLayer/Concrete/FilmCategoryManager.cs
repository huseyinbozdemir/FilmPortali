using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class FilmCategoryManager:IFilmCategoryService
    {
        private IFilmCategoryDal _filmCategoryDal;

        public FilmCategoryManager(IFilmCategoryDal filmCategoryDal)
        {
            _filmCategoryDal = filmCategoryDal;
        }

        public void Add(FilmCategory p)
        {
            _filmCategoryDal.Add(p);
        }

        public void Delete(FilmCategory p)
        {
            _filmCategoryDal.Delete(p);
        }

        public List<FilmCategory> GetAll()
        {
            return _filmCategoryDal.GetAll();
        }

        public void Update(FilmCategory p)
        {
            _filmCategoryDal.Update(p);
        }

        public void UpdateFilmsCategory(Film p, List<Category> pCategories)
        {
            _filmCategoryDal.UpdateFilmsCategory(p,pCategories);
        }
    }
}
