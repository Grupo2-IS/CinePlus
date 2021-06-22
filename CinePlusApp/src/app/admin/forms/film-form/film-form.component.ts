import { stringify } from '@angular/compiler/src/util';
import { Component, OnInit,  } from '@angular/core';
import { NgForm } from '@angular/forms';
import {Film}  from 'app/GlobalServices/film-model.model';
import {FilmService} from 'app/GlobalServices/film-model.service';

@Component({
  selector: 'app-film-form',
  templateUrl: './film-form.component.html',
  styleUrls: ['./film-form.component.css']
})
export class FilmFormComponent implements OnInit {
  
  genres :string[] = ["Drama","Accion", "Romance","Suspenso", "Comedia", "Crimen","Melodrama","Independiente", "Terror", "Ciencia Ficcion" ];

  selectedGenres = [];
  selectedGenre = null;
 
  film:Film;
  genre:string = "";

  
  constructor(private filmService:FilmService) { }

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
    this.genre = "";
    this.selectedGenres.forEach(g => {
      this.genre =  this.genre + "/" + g
      
    });
    const sinopsis = form.value.sinopsis;
    const id = form.value.id;  //se puede calcular segunla cant d usuarios en BD

    this.film = new Film( id, filmName, duration, country, this.genre, rating, sinopsis);
    console.log(this.film);

  }
  submit(){
    console.log("llego a submit");
    //this.filmService.CreateFilm
    this.filmService.CreateFilm(this.film).subscribe(
      (response:Response)=>{
        console.log(response);
        
      }

    )
    
    console.log("paso a create");
    
    
  }


}
