export class Showing{
    filmID: number;
    roomID:number;
    showingStart:  string;
    showingEnd : any;

    constructor( filmID:number,roomID:number,showingStart:string, showingEnd:string){
      this.filmID = filmID;
      this.roomID = roomID;
      this.showingEnd = showingEnd;
      this.showingStart = showingStart;
    }
}