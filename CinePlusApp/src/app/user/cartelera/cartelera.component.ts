import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ShowingService } from 'app/GlobalServices/showing-model.service';
import { Showing } from 'app/GlobalServices/showing-model.model';


@Component({
  selector: 'app-cartelera',
  templateUrl: './cartelera.component.html',
  styleUrls: ['./cartelera.component.css']
})
export class CarteleraComponent implements OnInit {

  showingList: Showing[] = [];
  constructor(private router: Router, private route: ActivatedRoute, private showingService: ShowingService) { }

  ngOnInit(): void {
    if (this.showingService.showingListActive.length === 0) {
      this.showingService.GetActiveShowingSubs(this.showingList);
    }
    else {
      this.showingList = this.showingService.showingListActive;
    }
  }

  seeDetails() {
    this.router.navigate(['detalles'], { relativeTo: this.route })
  }

  onGetActive() {
    this.showingService.GetActiveShowing().subscribe(
      (response) => this.showingList = response['$values'],
      (err) => console.log(err)
    );
  }
}

