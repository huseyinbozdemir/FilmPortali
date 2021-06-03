using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EntityLayer.Concrete;
namespace FilmPortali.Models
{
    public class FilmModel : Film
    {
        public HttpPostedFileBase filmGorsel { get; set; }
        public string SecilenKategoriler { get; set; }
        public string FilmDirectorName { get; set; }
    }
}