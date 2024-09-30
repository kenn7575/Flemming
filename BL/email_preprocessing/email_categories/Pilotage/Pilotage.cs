using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace BL
{
    public class Pilotage
    {
        [Key]
        public int Id { get; set; }
        public DateTime? Eta { get; set; }
        public DateTime? DepartureTime { get; set; }
        public string? EmbarkationPoint { get; set; }
        public string? DisembarkationPoint { get; set; }
    }
}
