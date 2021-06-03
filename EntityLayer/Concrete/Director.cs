using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class Director
    {
        [Key]
        public int DirectorId { get; set; }
        [StringLength(50)]
        public string DirectorName { get; set; }

        public ICollection<Film> Films { get; set; }
    }
}
