using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InverGrove.Data.Entities
{
    [Table("Sermon")]
    public class Sermon
    {
        public int SermonId { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [StringLength(128)]
        public string Tags { get; set; }

        [Required]
        [StringLength(128)]
        public string Title { get; set; }

        [Required]
        public int SoundCloudId { get; set; }
    }
}