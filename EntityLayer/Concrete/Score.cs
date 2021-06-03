using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class Score
    {
        [Key]
        public int ScoreId { get; set; }

        public int MemberId { get; set; }
        public virtual Member Member { get; set; }

        public int FilmId { get; set; }
        public virtual Film Film { get; set; }

        public int Point { get; set; }
    }
}
