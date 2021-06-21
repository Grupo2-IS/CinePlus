import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Seat } from "./seats.model";

@Injectable()
export class SeatsService {
    private dataPath = "https://localhost:5001/api/seats";
    seats: Seat[] = [];
    selectedSeats: Seat[] = [];
    constructor(private http: HttpClient) { }

    GetSeats() {
        return this.http.get<Seat[]>(this.dataPath);
    }

    GetSeatsSub() {
        this.GetSeats().subscribe(
            (response) => this.seats = response['$values'],
            (err) => console.log(err)
        );
    }
}