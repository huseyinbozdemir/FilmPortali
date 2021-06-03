using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FilmPortali.Models
{
    public class MemberCommentModel
    {
        public string MemberName { get; set; }
        public string MemberEmail { get; set; }
        public string Comment { get; set; }
        public int FilmId { get; set; }
        public int GivenScore { get; set; }
    }
}