using System;
using System.Collections.Generic;

namespace CinePlus.Entities
{
    public class Director
    {
        public int FilmID { get; set; }
        public int ArtistID { get; set; }
        public virtual Film Film { get; set; }
        public virtual Artist Artist { get; set; }

    }
}