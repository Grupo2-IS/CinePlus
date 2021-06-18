import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

interface Idata {
  id:string;
  nombre:string;
  duracion: string;
  pais: string;
  genero:string;
  rating: string;
}
let data1:Idata,
  data2:Idata,
  data3:Idata;

data1= {
  id: '1',
  nombre : 'The Notebook',
  duracion: '2:04:00',
  pais: 'Estados Unidos',
  genero:'Romance/Drama',
  rating:'6',

}
data3={
  id: '2',
  nombre : 'Rain Man',
  duracion: '2:14:00',
  pais: 'Estados Unidos',
  genero:'Drama/Melodrama',
  rating:'4',

}



@Component({
  selector: 'app-film-model',
  templateUrl: './film-model.component.html',
  styleUrls: ['./film-model.component.css']
})
export class FilmModelComponent implements OnInit {

  constructor(private router: Router, private route: ActivatedRoute) { }

  data:Idata[] =[data1, data2, data1];

  ngOnInit(): void {
  }

  createFilm() {
    this.router.navigate(['create'], { relativeTo: this.route })
  }
}
