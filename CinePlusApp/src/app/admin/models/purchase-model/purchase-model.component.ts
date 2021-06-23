import { Component, OnInit } from '@angular/core';
import {Purchase,User} from 'app/GlobalServices/purchase.model';
import {PurchaseService } from 'app/GlobalServices/purchase.service';
import { Router,ActivatedRoute} from '@angular/router';
import {Film} from 'app/GlobalServices/film-model.model';



@Component({
  selector: 'app-purchase-model',
  templateUrl: './purchase-model.component.html',
  styleUrls: ['./purchase-model.component.css']
})
export class PurchaseModelComponent implements OnInit {

  purchaseList : Purchase[]= [];
  // user : User[] =[]; //poner solo User no User[]
  // films : Film[] = []
  // film: Film;
  // j:number = 0;

  constructor( private router:Router, private route:ActivatedRoute, private purchaseService: PurchaseService ){ }

  ngOnInit() {
     this.OnGet();
   
  }
    createPurchase() {
    this.router.navigate(['create'], { relativeTo: this.route })}
   
    OnGet(){
      this.purchaseService.GetPurchase().subscribe(
        (response) => {
          this.purchaseList = response["$values"];
          console.log(this.purchaseList);

        },
        (err) => console.log(err),
      );

     
    }

    OnDelete(seatId:number,filmId:number,roomId:number,showingStart: Date){
      this.purchaseService.DeletePurchase(seatId, filmId, roomId, showingStart).subscribe(
        (response) => {
          console.log(response);
        },
        (err) => console.log(err),
      )

    }
    
    

    // GetFilms(purchaseList : Purchase[]){
    //   // purchaseList.forEach(p => {
    //   //   this.OnGetFilm(p.filmID);
    //   //   console.log(this.film);
    //   //   this.films.push(this.film);


        
    //   // });
    //     for (let index = 0; index < this.purchaseList.length; index++) {
    //       this.j = purchaseList[index].filmID;
    //       console.log(this.film);
    //       this.films.push(this.film);

          
    //     }
    // }

    // OnGetUser(id:number){
    //   this.purchaseService.GetUser(id).subscribe(
    //     (response) =>{
    //       this.user = response;
    //       console.log(response);
    //       console.log(this.user);
    //       console.log(this.user['nick']);
    //       // this.users.push(this.user);
    //     },
    //     (err)=> console.log(err),
    //   );
    //   this.AddI();

    // }

    // AddI(){
  
    //   this.j+=1;
    //   console.log(this.j);
    // }
    // OnGetFilm(id:number){
    //   this.purchaseService.GetFilm(id).subscribe(
    //     (response) =>{
    //       this.film = response;
    //       console.log(response);
    //       console.log(this.film);
    //     },
    //     (err)=> console.log(err),
    //   );

    // }
   

   

}
