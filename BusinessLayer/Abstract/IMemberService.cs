using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IMemberService
    {
        List<Member> GetAll();
        Member Get(Expression<Func<Member, bool>> filter);
        void Add(Member p);
        void Update(Member p);
        void Delete(Member p);
    }
}
