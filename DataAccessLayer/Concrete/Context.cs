using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Concrete
{
    public class Context : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Director> Directors { get; set; }
        public DbSet<Film> Films { get; set; }
        public DbSet<FilmCategory> FilmCategories { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<Score> Scores { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Hit> Hits { get; set; }

    }
}
