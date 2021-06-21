import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Film } from 'app/GlobalServices/film-model.model';
import { FilmService } from 'app/GlobalServices/film-model.service';
import { Showing } from 'app/GlobalServices/showing-model.model';
import { ShowingService } from 'app/GlobalServices/showing-model.service';

@Component({
  selector: 'app-projection-details',
  templateUrl: './projection-details.component.html',
  styleUrls: ['./projection-details.component.css']
})
export class ProjectionDetailsComponent implements OnInit {
  film: Film;
  showings: Showing[] = [];
  constructor(private router: Router, private route: ActivatedRoute, private showingService: ShowingService,
    private filmService: FilmService) { }

  ngOnInit(): void {
    const id = this.route.snapshot.params['id']
    this.onGetShowings(id);
    this.filmService.GetFilmId(id).subscribe
      (
        (response) => {
          this.film = response;
        },
        (err) => console.log(err)
      )
  }

  onReserve(show: Showing) {
    this.showingService.selectedShow = show;
    this.router.navigate(['reservar'], { relativeTo: this.route });
  }

  onGetShowings(id: number) {
    console.log(this.showingService.showingListActive);
    this.showingService.showingListActive
      .forEach(show => {
        if (show.filmID == id) {
          this.showings.push(show);
        }
      });
  }

}
