import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-user-payment',
  templateUrl: './user-payment.component.html',
  styleUrls: ['./user-payment.component.css']
})
export class UserPaymentComponent implements OnInit {
  costo: number = 34;
  reservadas: number = 3;
  pelicula: string = "Batman y dora";
  constructor() { }

  ngOnInit(): void {
  }

}
