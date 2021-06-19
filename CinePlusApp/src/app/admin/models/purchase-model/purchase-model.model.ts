export class Purchase{
    usuario : string;
    pelicula : string;
    sala : string;
    precio : string;
    codigo : string;

    constructor(usuario:string, pelicula:string, sala:string, precio:string, codigo:string){
        this.usuario = usuario;
        this.pelicula = pelicula;
        this.sala = sala;
        this.precio = precio;
        this.codigo = codigo;
    }

}

