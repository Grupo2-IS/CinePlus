export class Purchase {
    userID: number;
    seatID: number;
    roomID: number;
    showingStart: Date;
    price: number;
    payWithPoints: boolean;
    usedPoints: number;
    purchaseCode: string;
    seatRow: number;
    seatColumn: number;
    constructor(userID: number, seatID: number, roomID: number, showingStart: Date, price: number,
        payWithPoints: boolean, usedPoints: number, purchaseCode: string, seatRow: number, seatColumn: number) {
        this.userID = userID;
        this.seatID = seatID;
        this.roomID = roomID;
        this.showingStart = showingStart;
        this.price = price;
        this.payWithPoints = payWithPoints;
        this.usedPoints = usedPoints;
        this.purchaseCode = purchaseCode;
        this.seatRow = seatRow;
        this.seatColumn = seatColumn;
    }
}


// {
//     "userID": 3,
//     "seatID": 15,
//     "filmID": 3,
//     "roomID": 3,
//     "showingStart": "2021-07-28T10:00:00",
//     "price": 10,
//     "payWithPoints": false,
//     "usedPoints": 0,
//     "purchaseCode": "DEDFGRHA"
// }