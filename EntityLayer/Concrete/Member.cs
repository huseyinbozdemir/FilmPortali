using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class Member
    {
        [Key]
        public int MemberId { get; set; }
        [StringLength(40)]
        public string MemberName { get; set; }
        [StringLength(40)]
        public string MemberEmail { get; set; }

        public ICollection<Score> Scores { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}
