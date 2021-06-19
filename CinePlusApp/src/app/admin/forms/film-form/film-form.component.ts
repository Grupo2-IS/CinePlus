import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-film-form',
  templateUrl: './film-form.component.html',
  styleUrls: ['./film-form.component.css']
})
export class FilmFormComponent implements OnInit {
  // generos = [
  //   { id: 1, name: "Emma Lalaland" },
  //   { id: 2, name: "Jack Depp" },
  //   { id: 3, name: "Tom Gump" },
  //   { id: 4, name: "Teresa Binoche" },
  //   { id: 5, name: "Audrey Poulan" },
  // ];
  genres :string[] = ["Drama","Accion", "Romantica","Suspenso" ];

  selectedGenres = [];
  selectedGenre = null;
  sinopsis = "";
  constructor() { }

  ngOnInit(): void {
  }

  addGenre() {

    if (this.selectedGenre !== null && !this.selectedGenres.includes(this.selectedGenre)) {
      this.selectedGenres.push(this.selectedGenre);
    }
  }

  removeGenre(genre: string) {
    const index = this.selectedGenres.indexOf(genre);
    if (index > -1) {
      this.selectedGenres.splice(index, 1);
    }
  }
}
