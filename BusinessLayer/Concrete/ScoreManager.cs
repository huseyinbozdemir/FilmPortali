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
    public class ScoreManager : IScoreService
    {
        private IScoreDal _scoreDal;

        public ScoreManager(IScoreDal scoreDal)
        {
            _scoreDal = scoreDal;
        }

        public void Add(Score p)
        {
            _scoreDal.Add(p);
        }

        public void Delete(Score p)
        {
            _scoreDal.Delete(p);
        }

        public List<Score> GetAll()
        {
            return _scoreDal.GetAll();
        }

        public void Update(Score p)
        {
            _scoreDal.Update(p);
        }
    }
}
