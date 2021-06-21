import { Injectable } from "@angular/core";
import { HttpClient } from '@angular/common/http';
import { Showing } from './showing-model.model';

@Injectable()
export class ShowingService {
  private dataPath = 'https://localhost:5001/api/showings';
  private dataPathActive = 'https://localhost:5001/api/showings/active';

  constructor(private http: HttpClient) { }

  GetShowing() {
    return this.http.get<Showing[]>(this.dataPath);
  }

  GetActiveShowing() {
    return this.http.get<Showing[]>(this.dataPathActive);
  }
}