import { Component, OnInit } from '@angular/core';
import {Purchase,User} from './purchase-model.model';
import {PurchaseService } from './purchase-model.service';
import { Router,ActivatedRoute} from '@angular/router';



@Component({
  selector: 'app-purchase-model',
  templateUrl: './purchase-model.component.html',
  styleUrls: ['./purchase-model.component.css']
})
export class PurchaseModelComponent implements OnInit {

  purchaseList : Purchase[]= [];
  user : User[] =[];

  constructor( private router:Router, private route:ActivatedRoute, private purchaseService: PurchaseService ){ }

  ngOnInit() {
     this.OnGet1();
     this.OnGet2();
  }
  createPurchase() {
    this.router.navigate(['create'], { relativeTo: this.route })}
   
    OnGet1(){
      this.purchaseService.GetPurchase().subscribe(
        (response) => {
          this.purchaseList = response["$values"];
          console.log(response);
        },
        (err) => console.log(err),
      );

     
    }
    

    OnGet2(id:number){
      this.purchaseService.GetUser(id).subscribe(
        (response) =>{
          this.user = response;
          console.log(response);
          console.log(this.user);
          console.log(this.user['nick']);
        },
        (err)=> console.log(err),
      );

    }
   

   

}
