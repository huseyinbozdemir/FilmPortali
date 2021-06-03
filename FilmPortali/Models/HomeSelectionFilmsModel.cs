using EntityLayer.Concrete;
using PagedList;
using PagedList.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FilmPortali.Models
{
    public class HomeSelectionFilmsModel
    {
        public IPagedList<Film> films { get; set; }
        public IPagedList<List<Category>> categories { get; set; }
        public IPagedList<int> comments { get; set; }
    }
}