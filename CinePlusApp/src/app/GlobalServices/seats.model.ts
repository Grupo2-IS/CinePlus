export class Seat {
    row: number;
    column: number;
    seatID: number;
    roomID: number;

    constructor(seatID: number, row: number, column: number, roomID: number) {
        this.seatID = seatID;
        this.row = row;
        this.column = column;
        this.roomID = roomID;
    }
}


// "seatID": 35,
// "roomID": 1,
// "row": 5,
// "column": 3,