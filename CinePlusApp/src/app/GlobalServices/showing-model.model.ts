export class Showing {
    filmID: number;
    roomID: number;
    filmName: string;
    roomName: string;
    startDate: Date;
    filmLength: number;

    constructor(filmID: number, roomID: number, filmName: string, roomName: string, startDate: Date, filmLength: number) {
        this.filmID = filmID;
        this.roomID = roomID;
        this.filmName = filmName;
        this.roomName = roomName;
        this.startDate = startDate;
        this.filmLength = filmLength;
    }
}


// Example response
// "$values": [
//     {
//         "$id": "2",
//         "filmName": "Rain Man",
//         "roomName": "B1",
//         "startDate": "2021-05-28T11:00:00",
//         "filmLength": 134,
//         "roomID": 2,
//         "filmID": 2
//     },]