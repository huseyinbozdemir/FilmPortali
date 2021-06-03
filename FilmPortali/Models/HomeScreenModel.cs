using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EntityLayer.Concrete;

namespace FilmPortali.Models
{
    public class HomeScreenModel
    {
        public List<Film> TrendingFilms { get; set; }
        public List<List<Category>> TrendingCategory { get; set; }
        public List<int> TrendingComments { get; set; }

        public List<Film> PopularFilms { get; set; }
        public List<List<Category>> PopularCategory { get; set; }
        public List<int> PopularComments { get; set; }

        public List<Film> RecentlyAddedFilms { get; set; }
        public List<List<Category>> RecentlyAddedCategory { get; set; }
        public List<int> RecentlyComments { get; set; }

        public List<TopViewKeys> Keys { get; set; }

        public List<Film> LastComments { get; set; }
        public List<List<Category>> LastCommentsCategory { get; set; }
        public HomeScreenModel()
        {
            TrendingCategory=new List<List<Category>>();
            PopularCategory=new List<List<Category>>();
            RecentlyAddedCategory=new List<List<Category>>();

            TrendingComments=new List<int>();
            PopularComments=new List<int>();
            RecentlyComments=new List<int>();

            Keys=new List<TopViewKeys>();

            LastComments=new List<Film>();
            LastCommentsCategory=new List<List<Category>>();
        }
        
    }

    public class TopViewKeys
    {
        public Film Film { get; set; }
        public string Key { get; set; }
    }

}