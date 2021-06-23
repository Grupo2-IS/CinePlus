import { Time } from "@angular/common";

export class Showing {
    filmID: number;
    roomID: number;
    filmName: string;
    roomName: string;
    startDate: Date;
    filmLength: number;
    filmSynopsis: string;
    filmGenre: string;
    filmCountry: string;
    price: number;

    constructor(filmID: number, roomID: number, filmName: string, roomName: string, startDate: Date,
        filmLength: number, filmSynopsis: string, filmGenre: string, filmCountry: string, price: number) {
        this.filmID = filmID;
        this.roomID = roomID;
        this.filmName = filmName;
        this.roomName = roomName;
        this.startDate = startDate;
        this.filmLength = filmLength;
        this.filmSynopsis = filmSynopsis;
        this.filmGenre = filmGenre;
        this.filmCountry = filmCountry;
        this.price = price;
    }
}


// Example response
// "$values": [
    // {
    //     "$id": "2",
    //     "filmName": "The Notebook",
    //     "roomName": "A1",
    //     "startDate": "2021-07-28T10:00:00",
    //     "filmLength": 124,
    //     "roomID": 1,
    //     "filmID": 1,
    //     "filmSynopsis": "En un hogar de retiro un hombre le lee a una mujer, que sufre de Alzheimer, la historia de dos jóvenes de distintas clases sociales que se enamoraron durante la convulsionada década del 40, y de cómo fueron separados por terceros, y por la guerra",
    //     "filmGenre": "Romance/Drama",
    //     "filmCountry": "Estados Unidos"
    // }]