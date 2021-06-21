import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Showing } from './showing-model.model';
import { ShowingService} from './showing-model.service';


@Component({
  selector: 'app-showing-model',
  templateUrl: './showing-model.component.html',
  styleUrls: ['./showing-model.component.css']
})
export class ShowingModelComponent implements OnInit {
  
  showingList: Showing[]= [];
  constructor(private router: Router, private route: ActivatedRoute, private showingService:ShowingService) { }

  ngOnInit() {
    this.OnGet();
  }
  createShowing() {
    this.router.navigate(['create'], { relativeTo: this.route })
  }

  OnGet(){
    this.showingService.GetShowing().subscribe(
      (response) => {
        this.showingList = response["$values"];
        console.log(response);
      },
      (err) => console.log(err),
    );

  }

}
