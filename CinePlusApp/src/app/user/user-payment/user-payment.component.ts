import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { PurchaseService } from 'app/GlobalServices/purchase.service';
import { SeatsService } from 'app/GlobalServices/seats.service';
import { ShowingService } from 'app/GlobalServices/showing-model.service';

@Component({
  selector: 'app-user-payment',
  templateUrl: './user-payment.component.html',
  styleUrls: ['./user-payment.component.css']
})
export class UserPaymentComponent implements OnInit {
  costo: number = 30;
  reservadas: number = 3;
  pelicula: string = "Batman y dora";
  constructor(private seatsService: SeatsService, private showingService: ShowingService,
    private purchaseService: PurchaseService, private router: Router) { }

  ngOnInit(): void {
    if (this.showingService.selectedShow == null) {
      this.router.navigate(['/cartelera']);
    }
    console.log(this.seatsService.selectedSeats);
  }

}
