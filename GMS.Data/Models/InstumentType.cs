using System;
using System.ComponentModel.DataAnnotations;

namespace GMS.Data.Models
{
    public class InstumentType
    {
        [Key]
        public string Type { get; set; }

        public override string ToString()
        {
            return Type;
        }
    }
}