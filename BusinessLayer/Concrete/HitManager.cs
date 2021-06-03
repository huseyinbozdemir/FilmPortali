using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;

namespace BusinessLayer.Concrete
{
    public class HitManager:IHitService
    {
        private IHitDal _hitDal;

        public HitManager(IHitDal hitDal)
        {
            _hitDal = hitDal;
        }
        public void Add(Hit p)
        {
            _hitDal.Add(p);
        }
    }
}
