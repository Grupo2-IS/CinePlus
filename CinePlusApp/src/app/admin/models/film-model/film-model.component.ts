import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-film-model',
  templateUrl: './film-model.component.html',
  styleUrls: ['./film-model.component.css']
})
export class FilmModelComponent implements OnInit {

  constructor(private router: Router, private route: ActivatedRoute) { }

  ngOnInit(): void {
  }

  createFilm() {
    this.router.navigate(['create'], { relativeTo: this.route })
  }
}
