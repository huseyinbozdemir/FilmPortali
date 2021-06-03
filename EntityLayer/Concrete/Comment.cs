using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class Comment
    {
        [Key]
        public int CommentId { get; set; }

        public int MemberId { get; set; }
        public virtual Member Member { get; set; }

        public int FilmId { get; set; }
        public virtual Film Film { get; set; }
        [StringLength(300)]
        public string CommentDetail { get; set; }
        public DateTime CommentTime { get; set; }
        public bool IsApproved { get; set; }

    }
}
