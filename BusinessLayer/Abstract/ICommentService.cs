using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface ICommentService
    {
        List<Comment> GetAll();
        List<Comment> NonApproved();
        void Add(Comment p);
        void Update(Comment p);
        void Delete(Comment p);
    }
}
