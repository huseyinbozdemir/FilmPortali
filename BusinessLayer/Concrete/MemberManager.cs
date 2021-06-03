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
    public class MemberManager:IMemberService
    {
        private IMemberDal _memberDal;

        public MemberManager(IMemberDal memberDal)
        {
            _memberDal = memberDal;
        }

        public void Add(Member p)
        {
            _memberDal.Add(p);
        }

        public void Delete(Member p)
        {
            _memberDal.Delete(p);
        }

        public Member Get(Expression<Func<Member, bool>> filter)
        {
            return _memberDal.Get(filter);
        }

        public List<Member> GetAll()
        {
            return _memberDal.GetAll();
        }

        public void Update(Member p)
        {
            _memberDal.Update(p);
        }
    }
}
