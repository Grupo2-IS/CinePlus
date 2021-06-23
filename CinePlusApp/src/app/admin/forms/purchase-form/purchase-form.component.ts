import { Component, OnInit } from '@angular/core';
import {NgForm} from '@angular/forms';
import {Purchase} from 'app/GlobalServices/purchase.model';
import{PurchaseService } from'app/GlobalServices/purchase.service';

@Component({
  selector: 'app-purchase-form',
  templateUrl: './purchase-form.component.html',
  styleUrls: ['./purchase-form.component.css']
})
export class PurchaseFormComponent implements OnInit {
 purchase:Purchase;
  constructor() { }

  ngOnInit(): void {
  }
  onSignin(form: NgForm) {
    const userID = form.value.usuario;
    const seatID = form.value.asiento;
    
  

  }

}

// constructor(userID: number, userName:string, filmID:number, filmName:string, seatID: number, roomID: number, showingStart: Date, price: number,
//   payWithPoints: boolean, usedPoints: number, purchaseCode: string, seatRow: number, seatColumn: number) {
