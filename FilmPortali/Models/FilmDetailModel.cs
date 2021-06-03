using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FilmPortali.Models
{
    public class FilmDetailModel
    {
        public Film Film { get; set; }
        public List<Category> Categories { get; set; }
        public List<Comment> Comments { get; set; }
        public int ScoreCount { get; set; }
    }
}