import { Output } from '@angular/core';
import { Component, OnInit, } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Film} from 'app/GlobalServices/film-model.model';
import { FilmService } from 'app/GlobalServices/film-model.service';

// interface Idata {
//   id: string;
//   nombre: string;
//   duracion: string;
//   pais: string;
//   genero: string;
//   rating: string;
// }
// let data1: Idata,
//   data2: Idata,
//   data3: Idata;

// data1 = {
//   id: '1',
//   nombre: 'The Notebook',
//   duracion: '2:04:00',
//   pais: 'Estados Unidos',
//   genero: 'Romance/Drama',
//   rating: '6',

// };
// data2 = {
//   id: '2',
//   nombre: 'Rain Man',
//   duracion: '2:14:00',
//   pais: 'Estados Unidos',
//   genero: 'Drama/Melodrama',
//   rating: '4',

// };
// data3 = {
//   id: '3',
//   nombre: 'Coco avant Channel',
//   duracion: '2:14:00',
//   pais: 'Francia',
//   genero: 'Drama/Bibliografico',
//   rating: '4',

// };


@Component({
  selector: 'app-film-model',
  templateUrl: './film-model.component.html',
  styleUrls: ['./film-model.component.css']
})
export class FilmModelComponent implements OnInit {
  //data: Idata[] = [data1, data2, data3];

  filmsList: Film[] = []

  constructor(private router: Router, private route: ActivatedRoute, private filmService: FilmService) { }

  ngOnInit() {
    this.OnGet();
  }

  createFilm() {
    this.router.navigate(['create'], { relativeTo: this.route })
  }


  OnGet() {
    this.filmService.GetFilm().subscribe(
      (response) => {
        this.filmsList = response["$values"];
        console.log(response);
      },
      (err) => console.log(err),
    );

  }

  OnDelete(id:number){
    this.filmService.DeleteFilm(id).subscribe(
      (response:Response)=>{
        console.log(response);
        
      }
    ) 
    this.ngOnInit();


  }
}
