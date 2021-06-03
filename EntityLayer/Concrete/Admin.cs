using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class Admin
    {
        [Key]
        [StringLength(20)]
        public string AdminUsername { get; set; }
        [StringLength(20)]
        public string AdminPassword { get; set; }
    }
}
