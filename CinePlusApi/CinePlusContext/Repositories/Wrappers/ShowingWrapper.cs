using System;

namespace CinePlus.Context.Repositories
{
    public class ShowingWrapper
    {
        public string FilmName { get; set; }
        public string RoomName { get; set; }
        public DateTime StartDate { get; set; }
        public double FilmLength { get; set; }
        public int RoomID { get; set; }
        public int FilmID { get; set; }
        public string FilmSynopsis { get; set; }
        public string FilmGenre { get; set; }
        public string FilmCountry { get; set; }

        public ShowingWrapper(string filmName, string roomName, DateTime startDate, double filmLength
                            , int roomID, int filmID, string filmSynopsis, string FilmGenre, string filmCountry)
        {
            this.FilmName = filmName;
            this.RoomName = roomName;
            this.StartDate = startDate;
            this.FilmLength = filmLength;
            this.RoomID = roomID;
            this.FilmID = filmID;
            this.FilmSynopsis = filmSynopsis;
            this.FilmGenre = FilmGenre;
            this.FilmCountry = filmCountry;
        }
    }
}