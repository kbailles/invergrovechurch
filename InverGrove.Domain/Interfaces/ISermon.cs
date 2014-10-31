using System;

namespace InverGrove.Domain.Interfaces
{
    public interface ISermon
    {
        int SermonId { get; set; }

        DateTime Date { get; set; }

        string Tags { get; set; }

        string Title { get; set; }

        int SoundCloudId { get; set; }
    }
}