using BusinessLayer.Concrete;
using DataAccessLayer.Concrete.EntityFramework;
using EntityLayer.Concrete;
using FilmPortali.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FilmPortali.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        CategoryManager categoryManager = new CategoryManager(new EfCategoryDal());
        FilmManager filmManager = new FilmManager(new EfFilmDal());
        FilmCategoryManager filmCategoryManager = new FilmCategoryManager(new EfFilmCategoryDal());
        DirectorManager directorManager = new DirectorManager(new EfDirectorDal());
        AdminManager adminManager = new AdminManager(new EfAdminDal());
        ScoreManager scoreManager = new ScoreManager(new EfScoreDal());
        MemberManager memberManager = new MemberManager(new EfMemberDal());
        CommentManager commentManager = new CommentManager(new EfCommentDal());
        // GET: Admin


        public ActionResult Index()
        {
            List<int> counter = new List<int>();
            counter.Add(filmManager.GetAll().Count);
            counter.Add(commentManager.GetAll().Count);
            counter.Add(commentManager.NonApproved().Count);
            counter.Add(counter[1] - counter[2]);
            counter.Add(memberManager.GetAll().Count);
            counter.Add(categoryManager.GetAll().Count);
            counter.Add(scoreManager.GetAll().Count);
            return View(counter);
        }

        #region Kategori İşlemleri
        public ActionResult Category()
        {
            var result = categoryManager.GetAll();
            return View(result);
        }
        public ActionResult CategoryAdd()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CategoryAdd(Category p)
        {
            categoryManager.Add(p);
            return RedirectToAction("Category", "Admin");
        }
        public ActionResult CategoryRemove(int id)
        {
            categoryManager.Delete(new Category() { CategoryId = id });
            return RedirectToAction("Category", "Admin");
        }

        public ActionResult CategoryUpdate(int id)
        {
            var result = categoryManager.Get(x => x.CategoryId == id);
            return View(result);
        }

        [HttpPost]
        public ActionResult CategoryUpdate(Category p)
        {
            categoryManager.Update(p);
            return RedirectToAction("Category", "Admin");
        }

        [HttpGet]
        public ActionResult CategoryFilm(int categoryId = 0)
        {
            if (categoryId != 0)
            {
                List<Film> films = new List<Film>();
                var categoryFilm = filmCategoryManager.GetAll();
                for (int i = 0; i < categoryFilm.Count; i++)
                {
                    int filmId = categoryFilm[i].FilmId;
                    if (categoryFilm[i].CategoryId == categoryId)
                    {
                        Film aranan = filmManager.Get(x => x.FilmId == filmId);
                        aranan.Director = directorManager.Get(x => x.DirectorId == aranan.DirectorId);
                        films.Add(aranan);

                    }

                }
                var category = categoryManager.Get(x => x.CategoryId == categoryId);
                var result = new List<Object>() { films, category };
                return View(result);
            }
            else return RedirectToAction("Category", "Admin");

        }

        public ActionResult CategoryList()
        {
            var result = categoryManager.GetAll();
            return View(result);
        }

        public ActionResult FilmsTable()
        {
            var result = filmManager.GetAll();
            return PartialView(result);
        }

        [HttpGet]
        public ActionResult FilmsTable(int categoryId)
        {
            List<Film> films = new List<Film>();
            var categoryFilm = filmCategoryManager.GetAll();
            for (int i = 0; i < categoryFilm.Count; i++)
            {
                int filmId = categoryFilm[i].FilmId;
                if (categoryFilm[i].CategoryId == categoryId)
                {
                    Film aranan = filmManager.Get(x => x.FilmId == filmId);
                    aranan.Director = directorManager.Get(x => x.DirectorId == aranan.DirectorId);
                    films.Add(aranan);

                }

            }
            return PartialView(films);
        }
        #endregion

        #region Film İşlemleri

        public ActionResult Film()
        {
            var result = filmManager.GetAll();
            for (int i = 0; i < result.Count; i++)
            {
                int directorId = result[i].DirectorId;
                result[i].Director = directorManager.Get(x => x.DirectorId == directorId);
            }
            return View(result);
        }
        public ActionResult FilmAdd()
        {
            return View();
        }
        [HttpPost]
        public ActionResult FilmAdd(FilmModel f)
        {
            var fileName = Path.GetFileName(f.filmGorsel.FileName);
            if (f.FilmDescription.Length > 500) f.FilmDescription = f.FilmDescription.Substring(0, 500);
            if (fileName.Length > 0)
            {
                var folderPath = Path.Combine(Server.MapPath("~/filmImages/"), fileName);
                f.filmGorsel.SaveAs(folderPath);
                Film p = new Film()
                {
                    FilmName = f.FilmName,
                    FilmDescription = f.FilmDescription,
                    FilmImdb = f.FilmImdb,
                    FilmYear = f.FilmYear,
                    FilmTrailer = f.FilmTrailer,
                    IsSuitable = f.IsSuitable,
                    FilmScore = 0,
                    FilmHitCount = 0,
                    FilmImage = fileName,
                    FilmAddedTime = DateTime.Now
                };
                string[] categories = f.SecilenKategoriler.Split(';');
                Director director = directorManager.Get(x => x.DirectorName == f.FilmDirectorName);
                if (director == null) 
                {
                    directorManager.Add(new Director { DirectorName = f.FilmDirectorName });
                    director = directorManager.Get(x => x.DirectorName == f.FilmDirectorName);
                }
                p.DirectorId = director.DirectorId;
                filmManager.Add(p);
                for (int i = 0; i < categories.Length - 1; i++)
                {
                    FilmCategory categoryFilm = new FilmCategory() { CategoryId = int.Parse(categories[i]), FilmId = filmManager.GetLast().FilmId };
                    filmCategoryManager.Add(categoryFilm);
                }
            }

            return RedirectToAction("Film", "Admin");
        }

        public ActionResult FilmRemove(int filmId = 0)
        {
            if (filmId != 0)
            {
                filmManager.Delete(new Film { FilmId = filmId });
            }
            return RedirectToAction("Film", "Admin");
        }

        public ActionResult FilmUpdate(int filmId = 0)
        {
            if (filmId != 0)
            {
                var film = filmManager.Get(x => x.FilmId == filmId);
                int directorId = film.DirectorId;
                film.Director = directorManager.Get(x => x.DirectorId == directorId);
                var filmCategories = filmCategoryManager.GetAll().Where(x => x.FilmId == filmId).ToList();
                var categories = new List<Category>();
                for (int i = 0; i < filmCategories.Count; i++)
                {
                    int categoryId = filmCategories[i].CategoryId;
                    categories.Add(categoryManager.Get(x => x.CategoryId == categoryId));
                }
                List<object> model = new List<object>();
                model.Add(film);
                model.Add(categories);
                return View(model);
            }
            return RedirectToAction("Film", "Admin");
        }

        [HttpPost]
        public ActionResult FilmUpdate(FilmModel f)
        {
            if (f.FilmDescription.Length > 500) f.FilmDescription = f.FilmDescription.Substring(0, 500);
            string oldFolder = filmManager.Get(x => x.FilmId == f.FilmId).FilmImage; 

            Film p = new Film()
            {
                FilmId = f.FilmId,
                FilmName = f.FilmName,
                FilmDescription = f.FilmDescription,
                FilmImdb = f.FilmImdb,
                FilmYear = f.FilmYear,
                FilmTrailer = f.FilmTrailer,
                IsSuitable = f.IsSuitable,
                FilmAddedTime = filmManager.Get(x => x.FilmId == f.FilmId).FilmAddedTime
            };
            if (f.filmGorsel == null)
            {
                p.FilmImage = oldFolder;
            }
            else
            {
                var fileName = Path.GetFileName(f.filmGorsel.FileName);
                var folderPath = Path.Combine(Server.MapPath("~/filmImages/"), fileName);
                f.filmGorsel.SaveAs(folderPath);
                p.FilmImage = fileName;
                FileInfo file = new FileInfo(Path.Combine(Server.MapPath("~/filmImages/"), oldFolder));
                file.Delete();
            }

            p.FilmScore = filmManager.Get(x => x.FilmId == f.FilmId).FilmScore;
            p.FilmHitCount = filmManager.Get(x => x.FilmId == f.FilmId).FilmHitCount;
            string[] categories = f.SecilenKategoriler.Split(';');

            List<Category> pCategories = new List<Category>();
            for (int i = 0; i < categories.Length - 1; i++)
            {
                int categoryId = int.Parse(categories[i]);
                pCategories.Add(categoryManager.Get(x => x.CategoryId == categoryId));
            }
            Director director = directorManager.Get(x => x.DirectorName == f.FilmDirectorName);
            if (director == null)
            {
                directorManager.Add(new Director { DirectorName = f.FilmDirectorName });
                director = directorManager.Get(x => x.DirectorName == f.FilmDirectorName);
            }
            p.DirectorId = director.DirectorId;
            filmManager.Update(p);
            filmCategoryManager.UpdateFilmsCategory(p, pCategories);
            return RedirectToAction("Film", "Admin");
        }

        public ActionResult FilmDetail(int filmId = 0)
        {
            if (filmId != 0)
            {
                var film = filmManager.Get(x => x.FilmId == filmId);
                int directorId = film.DirectorId;
                film.Director = directorManager.Get(x => x.DirectorId == directorId);
                var filmCategories = filmCategoryManager.GetAll().Where(x => x.FilmId == filmId).ToList();
                var categories = new List<Category>();
                for (int i = 0; i < filmCategories.Count; i++)
                {
                    int categoryId = filmCategories[i].CategoryId;
                    categories.Add(categoryManager.Get(x => x.CategoryId == categoryId));
                }
                List<object> model = new List<object>();
                model.Add(film);
                model.Add(categories);
                return View(model);
            }
            return RedirectToAction("Film", "Admin");
        }
        #endregion

        #region Yönetmen İşlemleri
        public ActionResult Director()
        {
            var result = directorManager.GetAll();
            return View(result);
        }


        public ActionResult DirectorAdd()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DirectorAdd(Director p)
        {
            directorManager.Add(p);
            return RedirectToAction("Director", "Admin");
        }

        public ActionResult DirectorRemove(int DirectorId)
        {
            directorManager.Delete(new Director() { DirectorId = DirectorId });
            return RedirectToAction("Director", "Admin");
        }

        public ActionResult DirectorUpdate(int DirectorId)
        {
            var director = directorManager.Get(x => x.DirectorId == DirectorId);
            return View(director);
        }

        [HttpPost]
        public ActionResult DirectorUpdate(Director p)
        {
            directorManager.Update(p);
            return RedirectToAction("Director", "Admin");
        }

        public ActionResult DirectorList(int DirectorId)
        {
            var films = filmManager.GetDirectorFilms(x => x.DirectorId == DirectorId);
            return View(films);
        }
        #endregion

        #region Yorumlar
        public ActionResult Comments()
        {
            var result = commentManager.NonApproved();
            for (int i = 0; i < result.Count; i++)
            {
                int memberId = result[i].MemberId;
                result[i].Member = memberManager.Get(x => x.MemberId == memberId);
            }
            return View(result);
        }

        public ActionResult Approve(int commentId)
        {
            Comment comment = commentManager.GetAll().Where(x => x.CommentId == commentId).FirstOrDefault();
            comment.IsApproved = true;
            commentManager.Update(comment);
            return RedirectToAction("Comments", "Admin");
        }

        public ActionResult NonAprove(int commentId)
        {
            Comment comment = commentManager.GetAll().Where(x => x.CommentId == commentId).FirstOrDefault();
            commentManager.Delete(comment);
            return RedirectToAction("Comments", "Admin");
        }
        #endregion

        #region Skorlar
        public ActionResult Scores()
        {
            var result = scoreManager.GetAll();
            for (int i = 0; i < result.Count; i++)
            {
                int memberId = result[i].MemberId;
                int filmId = result[i].FilmId;
                result[i].Member = memberManager.Get(x => x.MemberId == memberId);
                result[i].Film = filmManager.Get(x => x.FilmId == filmId);
            }
            return View(result);
        }
        #endregion

        #region Ayarlar
        public ActionResult Settings()
        {
            var current = adminManager.GetAdmin();
            return View(current);
        }

        [HttpPost]

        public ActionResult Settings(string oldPw, string newPw1, string newPw2)
        {
            var current = adminManager.GetAdmin();
            if (current.AdminPassword == oldPw && newPw1 == newPw2)
            {
                current.AdminPassword = newPw1;
                adminManager.Update(current);
            }
            return View(current);
        }
        #endregion


    }
}