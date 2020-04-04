using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NationalCookies.Data
{
    [Serializable ]
    public class Cookie
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public double Price { get; set; }
    }
}
