
using System;
using System.ComponentModel.DataAnnotations;

namespace Pharmacy.Models
{
    public class GenericName
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Create_by { get; set; }

        public DateTime Create_date { get; set; }

        public string Update_by { get; set; }

        public DateTime Update_date { get; set; }

    }
}