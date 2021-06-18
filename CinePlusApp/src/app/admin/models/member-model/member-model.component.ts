import { Component, OnInit } from '@angular/core';
import { Router,ActivatedRoute} from '@angular/router';


interface Idata {
  id:string;
  nombre:string;
  puntos:string;
}
let data1:Idata,
  data2:Idata,
  data3:Idata;

data1= {
  id: '1',
  nombre : 'Mark',
  puntos:'50'
  }
data3={
  id: '2',
  nombre : 'Leo',
  puntos:'60'

}
@Component({
  selector: 'app-member-model',
  templateUrl: './member-model.component.html',
  styleUrls: ['./member-model.component.css']
})
export class MemberModelComponent implements OnInit {

  constructor(private router: Router, private route: ActivatedRoute) { }
  data :Idata[]=[data1,data2,data1];

  ngOnInit(): void {
  }
  createMember() {
    this.router.navigate(['create'], { relativeTo: this.route })}
}
