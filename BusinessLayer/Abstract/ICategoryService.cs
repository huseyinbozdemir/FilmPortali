using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface ICategoryService
    {
        List<Category> GetAll();
        Category Get(Expression<Func<Category, bool>> filter);
        void Add(Category p);
        void Update(Category p);
        void Delete(Category p);

    }
}
