using System;
using System.ComponentModel.DataAnnotations;

namespace GMS.Data.Models
{
    public class Instrument
    {
        [Key] public Guid InstrumentID { get; set; }

        public string Type { get; set; }

        public string HireCost { get; set; }

        public string Descriptionv { get; set; } = "Dat badass violin";


    }
}
