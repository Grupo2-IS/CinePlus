import { formatCurrency } from '@angular/common';
import { stringify } from '@angular/compiler/src/util';
import { NgModule } from '@angular/core';
import { Component, OnInit, ViewChild ,Input} from '@angular/core';
import { NgForm } from '@angular/forms';
import {Film}  from 'app/GlobalServices/film-model.model';
import {FilmService} from 'app/GlobalServices/film-model.service';

@Component({
  selector: 'app-film-form',
  templateUrl: './film-form.component.html',
  styleUrls: ['./film-form.component.css']
})
export class FilmFormComponent implements OnInit {
  
  @ViewChild('f') filmForm: NgForm;
  genres :string[] = ["Drama","Accion", "Romance","Suspenso", "Comedia", "Crimen","Melodrama","Independiente", "Terror", "Ciencia Ficcion" ];

  selectedGenres = [];
  selectedGenre = null;
 
  film:Film = new Film( 14," filmName", 54, "country"," this.genre", "5" ," sinopsis");
  id:number = 2;
  inEditMode: boolean =true;
  genre:string = "";
  filmsList: Film[]=[];

  
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

    if (this.inEditMode){
      form.value.name = this.film.name;
      form.value.country = this.film.country;
      form.value.rating = this.film.rating;
      form.value.duracion = this.film.filmLength;
      form.value.genre = this.film.genre;
      form.value.sinopsis = this.film.synopsis;
      form.value.id = this.film.filmID;
      console.log(form);
      
    }

    const filmName = form.value.name;
    const country = form.value.country;
    const rating = form.value.rating;
    const duration = form.value.duracion;
    this.genre = "";
    this.selectedGenres.forEach(g => {
      this.genre =  this.genre + "/" + g
      
    });
    const sinopsis = form.value.sinopsis;
    const id = form.value.id;  //se puede calcular segun la cant d usuarios en BD

    if (this.ValidId(id)){

      console.log("valid");
      this.film = new Film( id, filmName, duration, country, this.genre, rating, sinopsis);
      console.log(this.film);
    }
    else{
      console.log("Invalid Id");
    }
    form.reset();
  }


  submit(){
    console.log("llego a submit");
    if(!this.inEditMode){
      
    this.filmService.CreateFilm(this.film).subscribe(
      (response:Response)=>{
        console.log(response);
        this.filmForm.setValue({

        })
      }
    )  
     
    }

    else{
      this.filmService.UpdateFilm(this.id,this.film).subscribe(
        (response:Response)=>{
          console.log(response);
          this.filmForm.setValue({
  
          })
        }
      )  
      
    }

    
  }

  //TODAVIA NO FUNCIONA VALIDAR ID
  OnGet() {
    this.filmService.GetFilm().subscribe(
      (response) => {
        this.filmsList = response["$values"];
        // console.log(response);
      },
      (err) => console.log(err),
    );

  }
  ValidId(id:number):boolean{
    this.OnGet();
    console.log(this.filmsList);
    
    this.filmsList.forEach(f => { console.log(f.filmID);
      if( f.filmID === id){
      return false;
    }
    });
    return true;

  };

}
