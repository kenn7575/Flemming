using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace BL
{
    public class Speed
    {
        [Key]
        public int Id { get; set; }
        public double? Value { get; set; }
        public int? Rpm { get; set; }
    }
}
