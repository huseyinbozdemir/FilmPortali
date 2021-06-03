using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
   public interface IFilmCategoryService
    {
        List<FilmCategory> GetAll();
        void UpdateFilmsCategory(Film p, List<Category> pCategories);
        void Add(FilmCategory p);
        void Update(FilmCategory p);
        void Delete(FilmCategory p);
    }
}
