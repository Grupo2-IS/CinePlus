import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-projection-details',
  templateUrl: './projection-details.component.html',
  styleUrls: ['./projection-details.component.css']
})
export class ProjectionDetailsComponent implements OnInit {

  constructor(private router: Router, private route: ActivatedRoute) { }

  ngOnInit(): void {
  }

  onReserve() {
    this.router.navigate(['reservar'], { relativeTo: this.route });
  }

}
