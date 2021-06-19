import { Component, OnInit } from '@angular/core';
import {NgForm} from '@angular/forms';
import{ Film} from 'app/admin/models/film-model/film-model.model';
import {FilmService } from 'app/admin/models/film-model/film-model.service';

@Component({
  selector: 'app-showing-form',
  templateUrl: './showing-form.component.html',
  styleUrls: ['./showing-form.component.css']
})
export class ShowingFormComponent implements OnInit {
 filmsList:Film[] =[];
 film=null;

 genres :string[] = ["Drama","Accionlshbdufurfyygu", "Romantica","Suspenso" ];
  constructor(private filmService: FilmService) { }

  ngOnInit() {
    this.OnGet();
  }
  onSignin(form: NgForm) {
    const film = form.value.film;
    const romm = form.value.room;
    const start = form.value.start;
    const end = form.value.end;

  }

  OnGet() {
    this.filmService.GetFilm().subscribe(
      (response) => {
        this.filmsList = response;
        console.log(this.filmsList);
      },
      (err) => console.log(err),
    );

  }

  addFilm(){

      if (this.film !== null && !this.filmsList.includes(this.film)) 
        this.filmsList.push(this.film);
      
  }
  
}
