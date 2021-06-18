import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';


interface Idata {
  id:string;
  nombre:string;
}
let data1:Idata,
  data2:Idata,
  data3:Idata;

data1= {
  id: '1',
  nombre : 'Mark',
  }
data3={
  id: '2',
  nombre : 'Leo',
}
@Component({
  selector: 'app-artist-model',
  templateUrl: './artist-model.component.html',
  styleUrls: ['./artist-model.component.css']
})
export class ArtistModelComponent implements OnInit {

  constructor(private router: Router, private route: ActivatedRoute) { }
  data:Idata[] =[data1, data2, data1];


  ngOnInit(): void {
  }
  createArtist() {
    this.router.navigate(['create'], { relativeTo: this.route })
  }

}
