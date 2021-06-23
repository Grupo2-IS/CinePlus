import { Component, OnInit } from '@angular/core';
import {NgForm} from '@angular/forms';
import{ Film} from 'app/GlobalServices/film-model.model';
import {FilmService } from 'app/GlobalServices/film-model.service';
import{ShowingS} from 'app/admin/models/showing-model/showing-model.model';
import { ShowingService } from 'app/GlobalServices/showing-model.service';

@Component({
  selector: 'app-showing-form',
  templateUrl: './showing-form.component.html',
  styleUrls: ['./showing-form.component.css']
})
export class ShowingFormComponent implements OnInit {
 filmsList:Film[] =[];
 film=null;
 showing:ShowingS;

 genres :string[] = ["Drama","Accionlshbdufurfyygu", "Romantica","Suspenso" ];
  constructor(private filmService: FilmService, private showingService:ShowingService) { }

  ngOnInit() {
    this.OnGet();

  }
  onSignin(form: NgForm) {
    const film = form.value.film;
    const room = form.value.room;
    const start = form.value.start;
    const end = form.value.end;
    const price = form.value.price;

    this.showing = new ShowingS(film, room,start, end, price);

  }

  OnGet() {
    this.filmService.GetFilm().subscribe(
      (response) => {
        this.filmsList = response["$values"];
        console.log(this.filmsList);
      },
      (err) => console.log(err),
    );

  }

  addFilm(){

      if (this.film !== null && !this.filmsList.includes(this.film)) 
        this.filmsList.push(this.film);
  }

  submit(){
    this.showingService.CreateShowing(this.showing).subscribe(
      (response)=>{
        console.log(response);
      },
      (err) => console.log(err),
    );
  }
  
}
