export class Showing{
    pelicula: string;
    sala:string;
    horaInicio :  any;
    horaFinalizacion : any;

    constructor( pelicula:string, sala: string, horaInicio:any, horaFinalizacion:any ){
        this.pelicula = pelicula;
        this.sala = sala;
        this.horaInicio = horaInicio;
        this.horaFinalizacion = horaFinalizacion;
    }
}