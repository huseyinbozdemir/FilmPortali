using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class FilmCategory
    {
        [Key]
        public int FilmCategoryId { get; set; }

        public int FilmId { get; set; }
        public virtual Film Film { get; set; }

        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
    }
}
