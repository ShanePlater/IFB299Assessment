using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace GMS.Data.Models
{
    public class Instrument
    {
        [Key] public string InstrumentID { get; set; }

        public string Type { get; set; }

        public string HireCost { get; set; }

        public string Descriptionv { get; set; } = "Dat badass violin";


    }
}
