import { Component, OnInit,  } from '@angular/core';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-film-form',
  templateUrl: './film-form.component.html',
  styleUrls: ['./film-form.component.css']
})
export class FilmFormComponent implements OnInit {
  
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
  onSignin(form: NgForm) {
    const filmName = form.value.name;
    const country = form.value.country;
    const rating = form.value.rating;
    const duration = form.value.duracion;
    const genre = form.value.genero;
    const sinopsis = form.value.sinopsis;

  }


}
