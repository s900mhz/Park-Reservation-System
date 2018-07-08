using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class CampSite
    {
        #region Properties
        public int Id { get; set; }
        public int CampgroundId { get; set; }
        public int SiteNumber { get; set; }
        public int MaxOccupancy { get; set; }
        public bool Accesible { get; set; }
        public int MaxRvLength { get; set; }
        public bool Utilities { get; set; }
        public decimal Cost { get; set; }
        #endregion
    }
}
