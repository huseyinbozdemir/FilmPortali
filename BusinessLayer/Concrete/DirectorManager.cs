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
    public class DirectorManager:IDirectorService
    {
        private IDirectorDal _directorDal;

        public DirectorManager(IDirectorDal directorDal)
        {
            _directorDal = directorDal;
        }

        public void Add(Director p)
        {
            _directorDal.Add(p);
        }

        public void Delete(Director p)
        {
            _directorDal.Delete(p);
        }

        public Director Get(Expression<Func<Director, bool>> filter)
        {
            return _directorDal.Get(filter);
        }

        public List<Director> GetAll()
        {
            return _directorDal.GetAll();
        }

        public void Update(Director p)
        {
            _directorDal.Update(p);
        }
    }
}
