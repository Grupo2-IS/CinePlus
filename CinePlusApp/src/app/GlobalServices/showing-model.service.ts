import { Injectable, OnInit } from "@angular/core";
import { HttpClient } from '@angular/common/http';
import { Showing } from './showing-model.model';

@Injectable()
export class ShowingService {
  private dataPath = 'https://localhost:5001/api/showings';
  private dataPathActive = 'https://localhost:5001/api/showings/active';
  showingList: Showing[] = [];
  showingListActive: Showing[] = [];
  selectedShow: Showing;
  constructor(private http: HttpClient) { }


  GetShowing() {
    return this.http.get<Showing[]>(this.dataPath);
  }

  GetActiveShowing() {
    return this.http.get<Showing[]>(this.dataPathActive);
  }

  GetShowingSubs() {
    this.GetShowing().subscribe(
      (response) => {
        this.showingList = response['$values'];
      },
      (err) => console.log(err)
    );
  }

  GetActiveShowingSubs() {
    this.GetActiveShowing().subscribe(
      (response) => {
        this.showingListActive = response['$values'];
        console.log(response);
        console.log(this.showingListActive);
      },
      (err) => console.log(err)
    );
  }

}