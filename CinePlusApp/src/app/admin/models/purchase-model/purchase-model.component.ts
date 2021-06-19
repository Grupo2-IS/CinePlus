import { Component, OnInit } from '@angular/core';
import {Purchase } from './purchase-model.model';
import {PurchaseService } from './purchase-model.service';
import { Router,ActivatedRoute} from '@angular/router';



@Component({
  selector: 'app-purchase-model',
  templateUrl: './purchase-model.component.html',
  styleUrls: ['./purchase-model.component.css']
})
export class PurchaseModelComponent implements OnInit {

  normalPurchaseList : Purchase[]= [];
  memberPurchaseList : Purchase[]= [];

  constructor( private router:Router, private route:ActivatedRoute, private purchaseService: PurchaseService ){ }

  ngOnInit() {
     this.OnGet1();
     this.OnGet2();
  }
  createPurchase() {
    this.router.navigate(['create'], { relativeTo: this.route })}
   
    OnGet1(){
      this.purchaseService.GetNormalPurchase().subscribe(
        (response) => {
          this.normalPurchaseList = response;
          console.log(this.normalPurchaseList);
        },
        (err) => console.log(err),
      );
    }

    OnGet2(){
      this.purchaseService.GetMemberPurchase().subscribe(
        (response) => {
          this.memberPurchaseList = response;
          console.log(this.memberPurchaseList);
        },
        (err) => console.log(err),
      );
    }

}
