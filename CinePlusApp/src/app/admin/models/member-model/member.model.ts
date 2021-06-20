export class Member{
    memberID : number;
    userID : number ;
    points : number;
    memberPurchases : any;

    constructor( memberID:number, userID:number, points:number, memberPurchases:any){
        this.memberID = memberID;
        this.userID = userID;
        this.points = points;
        this.memberPurchases = memberPurchases

    }
}