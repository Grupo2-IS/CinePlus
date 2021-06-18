import { Injectable } from "@angular/core";
import { HttpClient } from '@angular/common/http';
import { Artist } from './artist-model';
import { Observable } from "rxjs";

@Injectable()
export class ArtistService {
  private dataPath = 'https://localhost:5001/api/request/hotelInPackage';

  constructor(private http: HttpClient) { }

  GetHotel() {
    return this.http.get<Hotel[]>(this.dataPath);
  }

}