using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class Hit
    {
        [Key] public int HitId { get; set; }
        public int FilmId { get; set; }
        public virtual Film Film { get; set; }
        public DateTime HitDate { get; set; }
    }
}
