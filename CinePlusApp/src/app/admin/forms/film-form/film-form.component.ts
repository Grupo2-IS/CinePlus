import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-film-form',
  templateUrl: './film-form.component.html',
  styleUrls: ['./film-form.component.css']
})
export class FilmFormComponent implements OnInit {
  artists = [
    { id: 1, name: "Emma Lalaland" },
    { id: 2, name: "Jack Depp" },
    { id: 3, name: "Tom Gump" },
    { id: 4, name: "Teresa Binoche" },
    { id: 5, name: "Audrey Poulan" },
  ];
  selectedArtists = [];
  selectedArtist = null;
  sinopsis = "";
  constructor() { }

  ngOnInit(): void {
  }

  addArtist() {

    if (this.selectedArtist !== null && !this.selectedArtists.includes(this.selectedArtist)) {
      this.selectedArtists.push(this.selectedArtist);
    }
  }

  removeArtist(artist: { id: number, name: string }) {
    const index = this.selectedArtists.indexOf(artist);
    if (index > -1) {
      this.selectedArtists.splice(index, 1);
    }
  }
}
