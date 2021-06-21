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
      this.onGetActive();
    }
    else {
      this.showingList = this.showingService.showingListActive;
    }
  }

  seeDetails(id: number) {
    this.router.navigate(['detalles', id], { relativeTo: this.route })
  }

  onGetActive() {
    this.showingService.GetActiveShowing().subscribe(
      (response) => {
        this.showingList = response['$values'];
        this.showingService.showingListActive = response['$values'];
      },
      (err) => console.log(err)
    );
  }
}

