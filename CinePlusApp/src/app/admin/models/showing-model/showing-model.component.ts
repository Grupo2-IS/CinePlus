import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';


@Component({
  selector: 'app-showing-model',
  templateUrl: './showing-model.component.html',
  styleUrls: ['./showing-model.component.css']
})
export class ShowingModelComponent implements OnInit {

  constructor(private router: Router, private route: ActivatedRoute) { }

  ngOnInit(): void {
  }
  createShowing() {
    this.router.navigate(['create'], { relativeTo: this.route })
  }

}
