using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IScoreService
    {
        List<Score> GetAll();
        void Add(Score p);
        void Update(Score p);
        void Delete(Score p);
    }
}
