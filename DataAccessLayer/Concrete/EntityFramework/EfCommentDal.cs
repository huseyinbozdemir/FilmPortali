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
    public class EfCommentDal : EfEntityRepositoryBase<Comment, Context>, ICommentDal
    {
        public List<Comment> NonApproved()
        {
            using (Context c = new Context())
            {
                var result = c.Comments.Where(x => x.IsApproved == false).ToList();
                return result;
            }
        }
    }
}
