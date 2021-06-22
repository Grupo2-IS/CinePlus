import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { Member } from './member.model';
import { MemberService } from './member-model.service';


// interface Idata {
//   id: string;
//   nombre: string;
//   puntos: string;
// }
// let data1: Idata,
//   data2: Idata,
//   data3: Idata;

// data1 = {
//   id: '1',
//   nombre: 'Mark',
//   puntos: '50'
// };
// data2 = {
//   id: '2',
//   nombre: 'Rolo',
//   puntos: '2'
// };
// data3 = {
//   id: '2',
//   nombre: 'Leo',
//   puntos: '60'
// };

@Component({
  selector: 'app-member-model',
  templateUrl: './member-model.component.html',
  styleUrls: ['./member-model.component.css']
})
export class MemberModelComponent implements OnInit {

  memberList: Member[] = [];
  //memberTest: Idata[] = [data1, data2, data3];

  constructor(private router: Router, private route: ActivatedRoute, private memberService: MemberService) { }

  ngOnInit() {
    this.OnGet();
  }
  createMember() {
    this.router.navigate(['create'], { relativeTo: this.route })
  }



  OnGet() {
    this.memberService.GetMember().subscribe(
      (response) => {
        this.memberList = response["$values"];
        console.log(response);
      },
      (err) => console.log(err),
    );
  }
}
