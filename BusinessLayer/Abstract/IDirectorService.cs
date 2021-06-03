using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IDirectorService
    {
        List<Director> GetAll();
        Director Get(Expression<Func<Director, bool>> filter);
        void Add(Director p);
        void Update(Director p);
        void Delete(Director p);
    }
}
