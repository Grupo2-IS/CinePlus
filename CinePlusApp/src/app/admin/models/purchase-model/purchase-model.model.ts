export class Purchase{
    userID : number;
    seatID: number;
    filmID : number;
    roomID : number;
    price : number;
    purchaseCode : string;
    payWithPoints : boolean;
    usedPoints : number;
    showingStart : Date;
    seatRow : number;
    seatColumn : number;


    constructor(userID:number,seatID:number, filmID:number, roomID:number, price:number, purchaseCode:string, payWithPoints:boolean, usedPoints:number, showingStart:Date, seatRow:number, seatColumn:number){
       this.userID = userID;
       this.seatID = seatID;
       this.filmID = filmID;
       this.roomID = roomID;
       this.price = price;
       this.purchaseCode = purchaseCode;
       this.payWithPoints = payWithPoints;
       this.usedPoints = usedPoints;
       this.showingStart = showingStart;
       this.seatRow = seatRow;
       this.seatColumn = seatColumn;

    }

}

export class User{
    userID : number;
    name : string;
    constructor(userID:number, name:string){
        this.userID = userID;
        this.name = name;
    }
    
}




