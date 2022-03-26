using NguyenQuocDai_2118110097.ContactDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NguyenQuocDai_2118110097.Models
{
    public class HomeModel
    {
        public List<Product> ListProduct { get; set; }

        public List<Category> ListCategory { get; set; }
        public List<Brand> ListBrand { get; set; }
        public List<Slider> ListSlider { get; set; }
    }
}