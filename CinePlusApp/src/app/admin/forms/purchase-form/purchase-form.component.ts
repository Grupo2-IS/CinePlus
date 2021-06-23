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
 month:number;
 year:number;
 entradas:number;
  constructor( private purchaseService:PurchaseService) { }

  ngOnInit(): void {
  }
  onSignin(form: NgForm) {
     this.month = form.value.month;
     this.year = form.value.year;
    
  }

  submit(year:number,month:number)
  { this.entradas=10;
    this.purchaseService.GetEntradasPorMes(year, month).subscribe(
    (response)=>{
      this.entradas =response["$value"];
      console.log(response);
    },
    (err) => console.log(err),
   );
}



}




