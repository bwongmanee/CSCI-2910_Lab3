using WongmaneeB_QueryBuilder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WongmaneeB_QueryBuilder.Models
{
    public class BannedGame : IClassModel
    {
        // ======================== PROPERTIES ======================== //
        public int Id { get; set; }
        public string Title { get; set; }
        public string Series { get; set; }
        public string Country { get; set; }
        public string Details { get; set; }



        // ======================== PARAMETERIZED CONSTRUCTOR ======================== //
        public BannedGame (int id, string title, string series, string country, string details)
        {
            this.Id = id;
            this.Title = title;
            this.Series = series;
            this.Country = country;
            this.Details = details;
        }
    }
}
