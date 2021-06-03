using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete.Repositories;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Concrete.EntityFramework
{
    public class EfFilmDal : EfEntityRepositoryBase<Film, Context>, IFilmDal
    {
        public List<Film> GetDirectorFilms(Expression<Func<Film, bool>> filter)
        {
            using (Context c = new Context())
            {
                return c.Films.Where(filter).ToList();
            }
        }

        public Film GetLast()
        {
            using (Context c = new Context())
            {
                return c.Films.OrderByDescending(x => x.FilmId).FirstOrDefault();
            }
        }

        public List<Film> GetPopularFilms()
        {
            using (Context c = new Context())
            {
                return c.Films.OrderByDescending(x => x.FilmHitCount).ToList();
            }
        }
        public List<Film> GetRecentlyAddedFilms()
        {
            using (Context c = new Context())
            {
                return c.Films.OrderByDescending(x => x.FilmAddedTime).ToList();
            }
        }

        public List<Film> GetTrendingFilms()
        {
            using (Context c = new Context())
            {
                return c.Films.OrderByDescending(x => x.FilmScore).ThenByDescending(x => x.FilmAddedTime).ToList();
            }
        }

        public List<Film> TopView(int choose)
        {
            using (Context c = new Context())// 1: Gün ~~ 2: Hafta ~~ 3: Ay ~~ 4: Yıl
            {
                List<Film> films = new List<Film>();
                DateTime start = DateTime.Today;
                DateTime end = DateTime.Today;

                if (choose == 1)
                {
                    start = DateTime.Today;
                    end = start.AddDays(1).AddSeconds(-1);//SUNDAY MONDAY THU... 0 1 2
                }
                else if (choose == 2)
                {
                    start = DateTime.Today.AddDays(-((DateTime.Today.DayOfWeek == 0) ? 6 : (int)DateTime.Today.DayOfWeek - 1));
                    end = start.AddDays(7).AddSeconds(-1);
                }
                else if (choose == 3)
                {
                    start = DateTime.Today.AddDays(1 - DateTime.Today.Day);
                    end = start.AddMonths(1).AddSeconds(-1);
                }
                else
                {
                    start = new DateTime(DateTime.Now.Year, 1, 1);
                    end = start.AddYears(1).AddSeconds(-1);
                }
                var query = c.Hits.Where(b => b.HitDate >= start && b.HitDate <= end).GroupBy(x => x.FilmId).Select(y => new { filmId = y.Key, count = y.Count() }).ToDictionary(z => z.filmId, i => i.count).OrderByDescending(a => a.Value).Take(5);
                foreach (var item in query)
                {
                    films.Add(c.Films.Find(item.Key));
                }
                return films;
            }

        }

        public List<Film> LastComments()
        {
            using (Context c = new Context())
            {
                List<Film> films = new List<Film>();
                var comments = c.Comments.OrderByDescending(x => x.CommentTime).Where(y=>y.IsApproved==true).ToList();
                int j = 0;
                for (int i = 0; i < comments.Count && j<5; i++)
                {
                    int filmId = comments[i].FilmId;

                    if (!films.Any(x => x.FilmId == filmId))
                    {
                        films.Add(c.Films.Where(x => x.FilmId == filmId).FirstOrDefault());
                        j++;
                    }
                }
                return films;
            }
        }
    }
}
