export class ShowingS {
    filmID: number;
    roomID:number;
    showingStart:  Date;
    showingEnd : Date;
    price : number;
    // film:any;
    // room:any;

    constructor( filmID:number,roomID:number,showingStart:Date, showingEnd:Date, price:number){
      this.filmID = filmID;
      this.roomID = roomID;
      this.showingEnd = showingEnd;
      this.showingStart = showingStart;
      this.price=price;
      // this.film = film;
      // this.room = room;
    }
}



// Example response
// "$values": [
//     {
//         "$id": "2",
//         "filmName": "Rain Man",
//         "roomName": "B1",
//         "startDate": "2021-05-28T11:00:00",
//         "filmLength": 134,
//         "roomID": 2,
//         "filmID": 2
  
