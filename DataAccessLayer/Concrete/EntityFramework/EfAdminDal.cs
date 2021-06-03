using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete.Repositories;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Concrete.EntityFramework
{
    public class EfAdminDal : EfEntityRepositoryBase<Admin, Context>, IAdminDal
    {
        public bool Authentication(Admin p)
        {
            using (Context c = new Context())
            {
                var bilgi = c.Admins.FirstOrDefault(x => x.AdminUsername == p.AdminUsername && x.AdminPassword == p.AdminPassword);
                if (bilgi != null)
                {
                    return true;
                }
                return false;
            }
        }
        public Admin GetAdmin()
        {
            using (Context c = new Context())
            {
                return c.Admins.First();
            }
        }
    }
}
