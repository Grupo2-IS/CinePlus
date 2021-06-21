export class Member{
    memberID : number;
    userID : number ;
    points : number;
    email : string;
    user : any;     //user:
                    //userID, nick, name, role

    constructor( memberID:number, userID:number, points:number,email:string , user:any){
        this.memberID = memberID;
        this.userID = userID;
        this.points = points;
        this.email = email;
        this.user = user;

    }
}