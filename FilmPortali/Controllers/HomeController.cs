using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLayer.Concrete;
using DataAccessLayer.Concrete.EntityFramework;
using EntityLayer.Concrete;
using FilmPortali.Models;
using PagedList;
using PagedList.Mvc;
namespace FilmPortali.Controllers
{

    public class HomeController : Controller
    {
        FilmManager filmManager = new FilmManager(new EfFilmDal());
        FilmCategoryManager filmCategoryManager = new FilmCategoryManager(new EfFilmCategoryDal());
        CategoryManager categoryManager = new CategoryManager(new EfCategoryDal());
        CommentManager commentManager = new CommentManager(new EfCommentDal());
        HitManager hitManager = new HitManager(new EfHitDal());
        DirectorManager directorManager = new DirectorManager(new EfDirectorDal());
        ScoreManager scoreManager = new ScoreManager(new EfScoreDal());
        MemberManager memberManager = new MemberManager(new EfMemberDal());
        #region Metotlar
        private List<Category> GetCategories(int FilmId)
        {
            List<Category> categories = new List<Category>();
            List<FilmCategory> filmCategories = filmCategoryManager.GetAll().Where(x => x.FilmId == FilmId).ToList();
            for (int i = 0; i < filmCategories.Count; i++)
            {
                int categoryId = filmCategories[i].CategoryId;
                categories.Add(categoryManager.Get(x => x.CategoryId == categoryId));
            }

            return categories;
        }

        private int GetCommentCount(int filmId)
        {
            int count = commentManager.GetAll().Where(x => x.FilmId == filmId && x.IsApproved == true).ToList().Count;
            return count;
        }
        #endregion

        public ActionResult Index()
        {
            HomeScreenModel model = new HomeScreenModel();
            model.PopularFilms = filmManager.GetPopularFilms();
            for (int i = 0; i < model.PopularFilms.Count; i++)
            {
                model.PopularCategory.Add(GetCategories(model.PopularFilms[i].FilmId));
                model.PopularComments.Add(GetCommentCount(model.PopularFilms[i].FilmId));
            }
            model.RecentlyAddedFilms = filmManager.GetRecentlyAddedFilms();
            for (int i = 0; i < model.RecentlyAddedFilms.Count; i++)
            {
                model.RecentlyAddedCategory.Add(GetCategories(model.RecentlyAddedFilms[i].FilmId));
                model.RecentlyComments.Add(GetCommentCount(model.RecentlyAddedFilms[i].FilmId));
            }
            model.TrendingFilms = filmManager.GetTrendingFilms();
            for (int i = 0; i < model.TrendingFilms.Count; i++)
            {
                model.TrendingCategory.Add(GetCategories(model.TrendingFilms[i].FilmId));
                model.TrendingComments.Add(GetCommentCount(model.TrendingFilms[i].FilmId));
            }
            List<Film> topViewDay = filmManager.TopView(1);
            List<Film> topViewWeek = filmManager.TopView(2);
            List<Film> topViewMonth = filmManager.TopView(3);
            List<Film> topViewYear = filmManager.TopView(4);
            for (int i = 0; i < topViewDay.Count; i++)
            {
                model.Keys.Add(new TopViewKeys { Film = topViewDay[i], Key = "day" });
            }

            for (int i = 0; i < topViewWeek.Count; i++)
            {
                bool kontrol = false;
                for (int j = 0; j < model.Keys.Count; j++)
                {
                    if (model.Keys[j].Film.FilmId == topViewWeek[i].FilmId)
                    {
                        model.Keys[j].Key += " week";
                        kontrol = true;
                    }
                }
                if (kontrol == false)
                {
                    model.Keys.Add(new TopViewKeys { Film = topViewWeek[i], Key = "week" });
                }
            }
            for (int i = 0; i < topViewMonth.Count; i++)
            {
                bool kontrol = false;
                for (int j = 0; j < model.Keys.Count; j++)
                {
                    if (model.Keys[j].Film.FilmId == topViewMonth[i].FilmId)
                    {
                        model.Keys[j].Key += " month";
                        kontrol = true;
                    }

                }
                if (kontrol == false)
                {
                    model.Keys.Add(new TopViewKeys { Film = topViewMonth[i], Key = "month" });
                }
            }
            for (int i = 0; i < topViewYear.Count; i++)
            {
                bool kontrol = false;
                for (int j = 0; j < model.Keys.Count; j++)
                {
                    if (model.Keys[j].Film.FilmId == topViewYear[i].FilmId)
                    {
                        model.Keys[j].Key += " years";
                        kontrol = true;
                    }

                }
                if (kontrol == false)
                {
                    model.Keys.Add(new TopViewKeys { Film = topViewYear[i], Key = "years" });
                }
            }

            model.LastComments = filmManager.LastComments();
            for (int i = 0; i < model.LastComments.Count; i++)
            {
                model.LastCommentsCategory.Add(GetCategories(model.LastComments[i].FilmId));

            }
            return View(model);
        }

        public ActionResult TrendingFilms(int page = 1)
        {
            HomeSelectionFilmsModel model = new HomeSelectionFilmsModel();
            List<Film> films = filmManager.GetTrendingFilms();
            List<List<Category>> categories = new List<List<Category>>();
            List<int> comments = new List<int>();
            for (int i = 0; i < films.Count; i++)
            {
                categories.Add(GetCategories(films[i].FilmId));
                comments.Add(GetCommentCount(films[i].FilmId));
            }
            model.films = films.ToPagedList(page, 16);
            model.categories = categories.ToPagedList(page, 16);
            model.comments = comments.ToPagedList(page, 16);
            return View(model);
        }

        public ActionResult PopularFilms(int page = 1)
        {
            HomeSelectionFilmsModel model = new HomeSelectionFilmsModel();
            List<Film> films = filmManager.GetPopularFilms();
            List<List<Category>> categories = new List<List<Category>>();
            List<int> comments = new List<int>();
            for (int i = 0; i < films.Count; i++)
            {
                categories.Add(GetCategories(films[i].FilmId));
                comments.Add(GetCommentCount(films[i].FilmId));
            }
            model.films = films.ToPagedList(page, 16);
            model.categories = categories.ToPagedList(page, 16);
            model.comments = comments.ToPagedList(page, 16);
            return View(model);
        }
        public ActionResult RecentlyAddedFilms(int page = 1)
        {
            HomeSelectionFilmsModel model = new HomeSelectionFilmsModel();
            List<Film> films = filmManager.GetRecentlyAddedFilms();
            List<List<Category>> categories = new List<List<Category>>();
            List<int> comments = new List<int>();
            for (int i = 0; i < films.Count; i++)
            {
                categories.Add(GetCategories(films[i].FilmId));
                comments.Add(GetCommentCount(films[i].FilmId));
            }
            model.films = films.ToPagedList(page, 16);
            model.categories = categories.ToPagedList(page, 16);
            model.comments = comments.ToPagedList(page, 16);
            return View(model);
        }

        public ActionResult Films(int filter = 1, int page = 1)// AZ - ZA - IMDB ART - IMDB AZ - SKOR ART - SKOR AZ
        {
            HomeSelectionFilmsModel model = new HomeSelectionFilmsModel();
            List<Film> films = null;
            if (filter == 1)
                films = filmManager.GetRecentlyAddedFilms().OrderBy(x => x.FilmName).ToList();
            else if (filter == 2)
                films = filmManager.GetRecentlyAddedFilms().OrderByDescending(x => x.FilmName).ToList();
            else if (filter == 3)
                films = filmManager.GetRecentlyAddedFilms().OrderBy(x => x.FilmImdb).ToList();
            else if (filter == 4)
                films = filmManager.GetRecentlyAddedFilms().OrderByDescending(x => x.FilmImdb).ToList();
            else if (filter == 5)
                films = filmManager.GetRecentlyAddedFilms().OrderBy(x => x.FilmScore).ToList();
            else if (filter == 6)
                films = filmManager.GetRecentlyAddedFilms().OrderByDescending(x => x.FilmScore).ToList();
            List<List<Category>> categories = new List<List<Category>>();
            List<int> comments = new List<int>();
            for (int i = 0; i < films.Count; i++)
            {
                categories.Add(GetCategories(films[i].FilmId));
                comments.Add(GetCommentCount(films[i].FilmId));
            }
            model.films = films.ToPagedList(page, 16);
            model.categories = categories.ToPagedList(page, 16);
            model.comments = comments.ToPagedList(page, 16);
            return View(model);
        }
        public ActionResult FilmDetail(int filmId = 0)
        {
            Film film = filmManager.Get(x => x.FilmId == filmId);
            if (film != null)
            {
                film.FilmHitCount++;
                filmManager.Update(film);
                hitManager.Add(new Hit { FilmId = film.FilmId, HitDate = DateTime.Now });
                FilmDetailModel model = new FilmDetailModel();
                model.Film = film;
                model.Film.Director = directorManager.Get(x => x.DirectorId == model.Film.DirectorId);
                model.Categories = GetCategories(filmId);
                model.Comments = commentManager.GetAll().Where(x => x.FilmId == filmId && x.IsApproved == true).ToList();
                for (int i = 0; i < model.Comments.Count; i++)
                {
                    int memberId = model.Comments[i].MemberId;
                    model.Comments[i].Member = memberManager.Get(x => x.MemberId == memberId);
                }
                model.ScoreCount = scoreManager.GetAll().Where(x => x.FilmId == filmId).ToList().Count;
                return View(model);
            }
            return RedirectToAction("Index");
        }

        public ActionResult AddComment(MemberCommentModel p)
        {
            if(p.MemberEmail!=null && p.MemberName != null)
            {
                if(p.MemberName.Trim().Length>0 && p.MemberEmail.Trim().Length > 0)
                {
                    Member control = memberManager.Get(x => x.MemberEmail == p.MemberEmail);
                    Member member = new Member { MemberName = p.MemberName, MemberEmail = p.MemberEmail };
                    int memberId = -1;
                    if (control == null)
                    {
                        memberManager.Add(member);
                        memberId = memberManager.GetAll().Last().MemberId;
                    }
                    else
                    {
                        member.MemberId = control.MemberId;
                        memberManager.Update(member);
                        memberId = control.MemberId;
                    }

                    Comment comment = new Comment { FilmId = p.FilmId, CommentDetail = p.Comment, CommentTime = DateTime.Now, IsApproved = false, MemberId = memberId };
                    Score score = new Score { FilmId = p.FilmId, MemberId = memberId, Point = p.GivenScore };
                    if (comment.CommentDetail != null)
                        commentManager.Add(comment);
                    if (score.Point != 0)
                        scoreManager.Add(score);
                }
               
            }
            return RedirectToAction("FilmDetail", "Home", new { filmId = p.FilmId });
        }

        public ActionResult RandomFilm()
        {
            Random rnd = new Random();
            List<Film> films = filmManager.GetAll();
            int filmId = films[rnd.Next(0, films.Count)].FilmId;
            return RedirectToAction("FilmDetail", "Home", new { filmId = filmId });
        }


        public ActionResult Search(string searchKey = null, int page = 1)
        {
            if (searchKey != null)
            {
                HomeSelectionFilmsModel model = new HomeSelectionFilmsModel();
                List<Film> films = filmManager.GetAll().Where(x => x.FilmName.ToLower().Contains(searchKey.ToLower())).ToList();
                List<List<Category>> categories = new List<List<Category>>();
                List<int> comments = new List<int>();
                for (int i = 0; i < films.Count; i++)
                {
                    categories.Add(GetCategories(films[i].FilmId));
                    comments.Add(GetCommentCount(films[i].FilmId));
                }
                model.films = films.ToPagedList(page, 16);
                model.categories = categories.ToPagedList(page, 16);
                model.comments = comments.ToPagedList(page, 16);
                return View(model);
            }
            return RedirectToAction("Index");
        }

        public ActionResult Category(int categoryId = 0, int page = 1)
        {
            Category category = categoryManager.Get(x => x.CategoryId == categoryId);
            if (category != null)
            {
                List<FilmCategory> filmCategories = filmCategoryManager.GetAll().Where(x => x.CategoryId == categoryId).ToList();
                HomeSelectionFilmsModel model = new HomeSelectionFilmsModel();
                List<Film> films = new List<Film>();
                for (int i = 0; i < filmCategories.Count; i++)
                {
                    int filmId = filmCategories[i].FilmId;

                    films.Add(filmManager.Get(x => x.FilmId == filmId));
                }
                List<List<Category>> categories = new List<List<Category>>();
                List<int> comments = new List<int>();
                for (int i = 0; i < films.Count; i++)
                {
                    categories.Add(GetCategories(films[i].FilmId));
                    comments.Add(GetCommentCount(films[i].FilmId));
                }
                model.films = films.ToPagedList(page, 16);
                model.categories = categories.ToPagedList(page, 16);
                model.comments = comments.ToPagedList(page, 16);
                return View(model);
            }
            return RedirectToAction("Index");
        }
        public ActionResult Contact()
        {
            return View();
        }
    }
}