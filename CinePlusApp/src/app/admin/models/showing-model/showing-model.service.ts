import { Injectable } from "@angular/core";
import { HttpClient } from '@angular/common/http';
import { Showing } from './showing-model.model';

@Injectable()
export class ShowingService {
  private dataPath = 'https://localhost:5001/api/showings';

  constructor(private http: HttpClient) { }

  GetShowing() {
    return this.http.get<Showing[]>(this.dataPath);
  }

}