import { Component, OnInit } from '@angular/core';
import {NgForm} from '@angular/forms';
@Component({
  selector: 'app-purchase-form',
  templateUrl: './purchase-form.component.html',
  styleUrls: ['./purchase-form.component.css']
})
export class PurchaseFormComponent implements OnInit {

  constructor() { }

  ngOnInit(): void {
  }
  onSignin(form: NgForm) {
    const userID = form.value.usuario;
    const seatID = form.value.asiento;
  

  }

}
