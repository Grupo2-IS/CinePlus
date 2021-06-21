import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { PurchaseService } from 'app/GlobalServices/purchase.service';
import { Seat } from 'app/GlobalServices/seats.model';
import { SeatsService } from 'app/GlobalServices/seats.service';
import { Showing } from 'app/GlobalServices/showing-model.model';
import { ShowingService } from 'app/GlobalServices/showing-model.service';

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
    [2, 2, 2, 2, 2, 2, 2, 2],
    [2, 2, 2, 2, 2, 2, 2, 2]];
  reservadas: number = 0;
  costo: number = 0;
  selectedSeats: Seat[] = [];
  show: Showing;
  // ocuppied: boolean[][] = [
  //   [false, false, false, false, false, false, false, false],
  //   [true, true, false, false, false, false, false, false],
  //   [false, false, false, false, true, true, false, false],
  //   [false, false, true, true, true, false, true, false],
  //   [false, false, false, false, false, false, false, false],
  //   [false, false, false, false, false, false, false, false]];
  constructor(private router: Router, private route: ActivatedRoute, private showingService: ShowingService,
    private seatsService: SeatsService, private purchaseService: PurchaseService) { }

  ngOnInit(): void {

    if (this.showingService.selectedShow == null) {
      this.router.navigate([''], { relativeTo: this.route.root });
    }

    this.show = this.showingService.selectedShow;

    if (this.seatsService.seats.length == 0) {
      this.seatsService.GetSeats().subscribe(
        (response) => this.seatsService.seats = response['$values'],
        (err) => console.log(err)
      );
    }

    this.purchaseService.GetSoldSeats(this.show.filmID, this.show.roomID, this.show.startDate)
      .subscribe((response) => {
        this.purchaseService.actualPurchases = response['$values'];
      },
        (err) => console.log(err)
      );

    this.SetSeatValues();
  }

  selectSeat(row: number, col: number) {
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

    this.costo = this.reservadas * this.show.price;

  }

  onPay() {
    alert("OnPay");
    for (let i = 0; i < this.seats.length; i++) {
      const row = this.seats[i];
      for (let j = 0; j < row.length; j++) {
        const seat = row[j];
        if (seat == 1) {
          const id: number = 8 * i + j + 1;
          this.selectedSeats.push(new Seat(id, i, j, this.showingService.selectedShow.roomID));
        }
      }

    }
    // alert(this.seatsService.selectedSeats);
    this.router.navigate(['pagar'], { relativeTo: this.route });
  }

  private SetSeatValues() {
    for (let i = 0; i < this.purchaseService.actualPurchases.length; i++) {
      const purchase = this.purchaseService.actualPurchases[i];
      this.seats[purchase.seatRow][purchase.seatColumn] = 0;

    }

  }
}
