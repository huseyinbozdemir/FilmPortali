using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete.Repositories;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class CategoryManager : ICategoryService
    {
        private ICategoryDal _categoryDal;

        public CategoryManager(ICategoryDal categorydal)
        {
            _categoryDal = categorydal;
        }
        public void Add(Category p)
        {
            _categoryDal.Add(p);
        }

        public void Delete(Category p)
        {
            _categoryDal.Delete(p);
        }

        public Category Get(Expression<Func<Category, bool>> filter)
        {
            return _categoryDal.Get(filter);
        }

        public List<Category> GetAll()
        {
           return _categoryDal.GetAll();
        }

        public void Update(Category p)
        {
            _categoryDal.Update(p);
        }
    }
}
