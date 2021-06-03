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
    public class AdminManager : IAdminService
    {
        IAdminDal _adminDal;

        public AdminManager(IAdminDal adminDal)
        {
            _adminDal = adminDal;
        }

  

        public bool Authentication(Admin p)
        {
            return _adminDal.Authentication(p);
        }

        public Admin GetAdmin()
        {
            return _adminDal.GetAdmin();
        }

        public void Update(Admin p)
        {
            _adminDal.Update(p);
        }
    }
}
