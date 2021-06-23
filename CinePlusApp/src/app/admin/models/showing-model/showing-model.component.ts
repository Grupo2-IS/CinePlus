import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import{Showing} from 'app/GlobalServices/showing-model.model';
import { ShowingService } from 'app/GlobalServices/showing-model.service';


@Component({
  selector: 'app-showing-model',
  templateUrl: './showing-model.component.html',
  styleUrls: ['./showing-model.component.css']
})
export class ShowingModelComponent implements OnInit {

  showingList: Showing[] = [];
  ShowingEnd: Date;


  constructor(private router: Router, private route: ActivatedRoute, private showingService: ShowingService) { }

  ngOnInit() {
    this.OnGet();
  }
  createShowing() {
    this.router.navigate(['create'], { relativeTo: this.route })
  }

  OnGet() {
    this.showingService.GetShowing().subscribe(
      (response) => {
        this.showingList = response["$values"];
        console.log(response);
      },
      (err) => console.log(err),
    );
  }

  OnDelete(FilmId:number, RoomID:number, ShowingStart:Date,duration:number){
    this.ShowingEnd=ShowingStart ;
    console.log(this.ShowingEnd);
    this.ShowingEnd.setMinutes(ShowingStart.getMinutes()+ duration);
    console.log(this.ShowingEnd);
    this,this.showingService.DeleteShowing(FilmId, RoomID, ShowingStart, this.ShowingEnd).subscribe(
      (response) =>{
        console.log(response)
      },
      (err) => console.log(err),
    );
  }

}
