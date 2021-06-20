export class Film {
    filmID: number;
    name: string;
    filmLength: number;
    country: string;
    genre: string;
    rating: string;
    synopsis: string;
    constructor(filmID: number, name: string, filmLength: number, country: string,
        genre: string, rating: string, synopsis: string) {
        this.filmID = filmID;
        this.name = name;
        this.filmLength = filmLength;
        this.country = country;
        this.genre = genre;
        this.rating = rating;
        this.synopsis = synopsis;
    }
}


// Ejemplo de JSON response
// {
//     "filmID": 1,
//     "name": "The Notebook",
//     "filmLength": 124,
//     "country": "Estados Unidos",
//     "genre": "Romance/Drama",
//     "rating": 6,
//     "synopsis": "En un hogar de retiro un hombre le lee a una mujer, que sufre de Alzheimer,"
//                 + "la historia de dos jóvenes de distintas clases sociales que se enamoraron" 
//                 +"durante la convulsionada década del 40, y de cómo fueron separados por terceros," 
//                 +"y por la guerra",
// }









