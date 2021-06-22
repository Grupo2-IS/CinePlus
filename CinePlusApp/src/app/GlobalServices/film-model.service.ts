import { Injectable } from "@angular/core";
import { HttpClient } from '@angular/common/http';
import { Film } from './film-model.model';

@Injectable()
export class FilmService {
  private dataPath = 'https://localhost:5001/api/films';
  private dataPath2 = 'https://localhost:5001/api/films';

  private dataPathId = 'https://localhost:5001/api/films/';

  constructor(private http: HttpClient) { }

  GetFilm() {
    return this.http.get<Film[]>(this.dataPath);
  }
  GetFilmId(id: number) {
    return this.http.get<Film>(this.dataPathId + id);
  }

  CreateFilm(film:Film){

  return this.http.post(this.dataPath2,film);
  }

}