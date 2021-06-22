using System;

namespace CinePlus.Context.Repositories
{
    public class PurchaseWrapper
    {
        public int UserID { get; set; }
        public string UserName{ get; set; }
        public int SeatID { get; set; }
        public int FilmID { get; set; }
        public string FilmName{ get; set; }
        public int RoomID { get; set; }
        public DateTime ShowingStart { get; set; }
        public int Price { get; set; }
        public bool PayWithPoints { get; set; }
        public int UsedPoints { get; set; }
        public string PurchaseCode { get; set; }
        public int SeatRow { get; set; }
        public int SeatColumn { get; set; }

        public PurchaseWrapper(int UserID,string UserName, int SeatID, int FilmID,string FilmName, int RoomID, DateTime ShowingStart,
            int Price, bool PayWithPoints, int UsedPoints, string PurchaseCode, int SeatRow, int SeatColumn)
        {
            this.UserID = UserID;
            this.UserName = UserName;
            this.SeatID = SeatID;
            this.FilmID = FilmID;
            this.FilmName = FilmName;
            this.RoomID = RoomID;
            this.ShowingStart = ShowingStart;
            this.Price = Price; ;
            this.PayWithPoints = PayWithPoints;
            this.UsedPoints = UsedPoints;
            this.PurchaseCode = PurchaseCode;
            this.SeatRow = SeatRow;
            this.SeatColumn = SeatColumn;
        }
    }
}