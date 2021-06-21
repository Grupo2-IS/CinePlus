import { Injectable } from "@angular/core";
import { HttpClient, HttpParams } from '@angular/common/http';
import { Purchase, User } from './purchase-model.model';
import { Input } from "@angular/core";

@Injectable()
export class PurchaseService {
  private dataPath = 'https://localhost:5001/api/purchases' ;
  

  //id:string="1";
 // @Input() id:string;

  private userPath = "https://localhost:5001/api/users/";

  constructor(private http: HttpClient) { 
    //this.id ="5";
  }


 GetPurchase() {
    return this.http.get<Purchase[]>(this.dataPath);

  }
  GetUser(id:number){
    return this.http.get<User[]>(this.userPath + id);

  }
  

  

}