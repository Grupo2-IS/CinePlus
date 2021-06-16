import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-set-purchase',
  templateUrl: './set-purchase.component.html',
  styleUrls: ['./set-purchase.component.css']
})
export class SetPurchaseComponent implements OnInit {
  // 0: occupied, 1: selected, 2: available
  seats: number[][] = [
    [2, 2, 2, 2, 2, 2, 2, 2],
    [2, 2, 2, 2, 2, 2, 2, 2],
    [2, 2, 2, 2, 2, 2, 2, 2],
    [2, 2, 2, 2, 2, 2, 2, 2],
    [2, 2, 2, 2, 0, 2, 2, 2],
    [2, 2, 2, 2, 2, 2, 2, 2]];
  reservadas: number = 0;
  precio: number = 5;
  costo: number = 0;
  ocuppied: boolean[][] = [
    [false, false, false, false, false, false, false, false],
    [true, true, false, false, false, false, false, false],
    [false, false, false, false, true, true, false, false],
    [false, false, true, true, true, false, true, false],
    [false, false, false, false, false, false, false, false],
    [false, false, false, false, false, false, false, false]];
  constructor() { }

  ngOnInit(): void {
  }

  selectSeat(row: number, col: number) {
    debugger;
    if (this.seats[row][col] === 0) {
      return;
    }

    if (this.seats[row][col] === 1) {
      this.seats[row][col] = 2;
      this.reservadas--;
    }
    else {
      this.seats[row][col] = 1;
      this.reservadas++;
    }

    this.costo = this.reservadas * this.precio;

  }

}
