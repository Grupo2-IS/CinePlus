import { Injectable } from "@angular/core";
import { HttpClient } from '@angular/common/http';
import { Film } from './film-model.model';

@Injectable()
export class FilmService {
  private dataPath = 'https://localhost:5001/api/films';
  private requestPath = 'https://localhost:5001/api/request/';


  constructor(private http: HttpClient) { }

  GetFilm() {
    return this.http.get<Film[]>(this.dataPath);
  }
  GetFilmId(id: number) {
    return this.http.get<Film>(this.dataPath + '/' + id);
  }

  CreateFilm(film:Film){

  return this.http.post(this.dataPath,film);
  }

  DeleteFilm(id:number){
    return this.http.delete(this.dataPath + '/' + id);
  }
  UpdateFilm(id:number, film:Film){
    return this.http.put(this.dataPath +'/'+ id ,film);

  }


  GetFilmsOrderByRating(){
    return this.http.get(this.requestPath + 'filmsByRating' );

  }

  
  GetFilmsByCountry( country:string){
    return this.http.get(this.requestPath + 'filmsByCountry' + country);

  }
}