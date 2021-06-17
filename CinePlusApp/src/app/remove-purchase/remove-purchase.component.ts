import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-remove-purchase',
  templateUrl: './remove-purchase.component.html',
  styleUrls: ['./remove-purchase.component.css']
})
export class RemovePurchaseComponent implements OnInit {

  constructor() { }

  ngOnInit(): void {
  }

  onRefund(form: NgForm) {
    const code: string = form.value.code;
    alert("No hay vuelta atras." + "\n" + code);
  }

}
