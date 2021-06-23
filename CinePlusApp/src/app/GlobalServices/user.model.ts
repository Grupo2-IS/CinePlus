import { stringify } from "@angular/compiler/src/util";

export class User{
    UserID: number;
    Nick : string;
    Name : string;
    Role : string;
    Member : any;
     constructor( UserId:number, Nick:string, Name:string, Role:string){
         this.UserID = UserId;
         this.Nick = Nick;
         this.Name = Name;
         this.Role = Role;
        // this.Member =Member;

     }
}