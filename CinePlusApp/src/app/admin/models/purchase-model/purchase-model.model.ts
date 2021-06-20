export class NormalPurchase{
    userId : number;
    filmID : number;
    roomID : number;
    price : number;
    purchaseCode : string;
    //showingStart : string;


    constructor(userId:number, filmID:number, roomID:number, price:number, purchaseCode:string){
       this.userId = userId;
       this.filmID = filmID;
       this.roomID = roomID;
       this.price = price;
       this.purchaseCode = purchaseCode;
    }

}

export class MemberPurchase{
    memberId : number;
    filmID : number;
    roomID : number;
    price : number;
    purchaseCode : string;
    payWithPoints : boolean;
    usedPoints : number;
    //showingStart : string;
    //seatID : number


    constructor(memberId:number, filmID:number, roomID:number, price:number, purchaseCode:string, payWithPoints:boolean, usedPoints:number){
       this.memberId = memberId;
       this.filmID = filmID;
       this.roomID = roomID;
       this.price = price;
       this.purchaseCode = purchaseCode;
       this.payWithPoints = payWithPoints;
       this.usedPoints = usedPoints;

    }

}



