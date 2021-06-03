using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface IFilmCategoryDal : IEntityRepository<FilmCategory>
    {
        void UpdateFilmsCategory(Film p, List<Category> pCategories);
    }
}
