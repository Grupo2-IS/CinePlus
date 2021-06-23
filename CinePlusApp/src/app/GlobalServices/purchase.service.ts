import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Purchase , User} from "./purchase.model";
import { Film} from "./film-model.model";

@Injectable()
export class PurchaseService {
    private dataPath = 'https://localhost:5001/api/purchases' ;
    private datapathSeat = "https://localhost:5001/api/purchases/byShowing";
    actualPurchases: Purchase[];
    constructor(private http: HttpClient) { }

    GetSoldSeats(FilmID: number, RoomID: number, ShowingStart: Date) {
        return this.http.get<Purchase[]>(this.datapathSeat + '/' + FilmID + '/' + RoomID + '/' + ShowingStart)
    }

    GetPurchase() {
        return this.http.get<Purchase[]>(this.dataPath);
    
      }

    DeletePurchase(seatId:number,filmId:number,roomId:number,showingStart: Date ){
      return this.http.delete(this.dataPath + '/' + seatId +  '/' + filmId + '/' + roomId + '/' + showingStart);
    }
      
 
      
    

}