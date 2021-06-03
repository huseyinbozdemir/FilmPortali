using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class CommentManager : ICommentService
    {
        private ICommentDal _commentDal;

        public CommentManager(ICommentDal commentDal)
        {
            _commentDal = commentDal;
        }

        public void Add(Comment p)
        {
            _commentDal.Add(p);
        }

        public void Delete(Comment p)
        {
            _commentDal.Delete(p);
        }

        public List<Comment> GetAll()
        {
            return _commentDal.GetAll();
        }

        public List<Comment> NonApproved()
        {
            return _commentDal.NonApproved();
        }

        public void Update(Comment p)
        {
            _commentDal.Update(p);
        }
    }
}
