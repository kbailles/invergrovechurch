using System;
using InverGrove.Domain.Interfaces;

namespace InverGrove.Domain.Models
{
    public class Sermon : ISermon
    {
        public int SermonId { get; set; }

        public DateTime Date { get; set; }

        public string Tags { get; set; }

        public string Title { get; set; }

        public int SoundCloudId { get; set; }
    }
}