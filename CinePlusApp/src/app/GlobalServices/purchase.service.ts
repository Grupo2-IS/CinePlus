import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Purchase , User} from "./purchase.model";
import { Film} from "./film-model.model";

@Injectable()
export class PurchaseService {
    private dataPath = 'https://localhost:5001/api/purchases' ;
    private userPath = "https://localhost:5001/api/users/";
    private datapathSeat = "https://localhost:5001/api/purchases/byShowing";
    private dataPathFilm = "https://localhost:5001/api/films/";
    actualPurchases: Purchase[];
    constructor(private http: HttpClient) { }

    GetSoldSeats(FilmID: number, RoomID: number, ShowingStart: Date) {
        return this.http.get<Purchase[]>(this.datapathSeat + '/' + FilmID + '/' + RoomID + '/' + ShowingStart)
    }

    GetPurchase() {
        return this.http.get<Purchase[]>(this.dataPath);
    
      }
      GetUser(id:number){
        return this.http.get<User[]>(this.userPath + id);
    
      }

      GetFilm( id:number){
          return this.http.get<Film>(this.dataPathFilm + id);
      }

      
      
    

}