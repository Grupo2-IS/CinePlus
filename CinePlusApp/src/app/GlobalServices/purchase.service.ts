import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Film } from "./film-model.model";
import { Purchase } from "./purchase.model";

@Injectable()
export class PurchaseService {
    private datapath = "https://localhost:5001/api/purchases/byShowing";
    actualPurchases: Purchase[];
    constructor(private http: HttpClient) { }

    GetSoldSeats(FilmID: number, RoomID: number, ShowingStart: Date) {
        return this.http.get<Purchase[]>(this.datapath + '/' + FilmID + '/' + RoomID + '/' + ShowingStart)
    }

}