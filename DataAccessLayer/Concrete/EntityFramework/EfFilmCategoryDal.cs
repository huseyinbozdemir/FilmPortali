using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete.Repositories;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Concrete.EntityFramework
{
    public class EfFilmCategoryDal : EfEntityRepositoryBase<FilmCategory, Context>, IFilmCategoryDal
    {
        public virtual void UpdateFilmsCategory(Film p, List<Category> pCategories)
        {
            using (Context c = new Context())
            {
                c.FilmCategories.RemoveRange(c.FilmCategories.Where(x => x.FilmId == p.FilmId));
                for (int i = 0; i < pCategories.Count; i++)
                {
                    int filmId = p.FilmId;
                    int categoryId = pCategories[i].CategoryId;
                    c.FilmCategories.Add(new FilmCategory {CategoryId = categoryId, FilmId = filmId});
                }
                c.SaveChanges();
            }
        }
    }
}
