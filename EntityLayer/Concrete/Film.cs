using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class Film
    {
        [Key]
        public int FilmId { get; set; }
        [StringLength(50)]
        public string FilmName { get; set; }
        [StringLength(500)]
        public string FilmDescription { get; set; }

        public int DirectorId { get; set; }
        public virtual Director Director { get; set; }

        public float FilmImdb { get; set; }
        public int FilmYear { get; set; }
        [StringLength(500)]
        public string FilmTrailer { get; set; }
        public bool IsSuitable { get; set; }

        [StringLength(500)]
        public string FilmImage { get; set; }
        public float FilmScore { get; set; }
        public DateTime FilmAddedTime { get; set; }
        public int FilmHitCount { get; set; }
    }
}
