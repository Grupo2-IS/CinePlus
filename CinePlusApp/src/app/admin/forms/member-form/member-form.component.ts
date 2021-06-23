import { Component, OnInit } from '@angular/core';
import {NgForm} from '@angular/forms';
import{User} from '../../../GlobalServices/user.model';
import{MemberService } from 'app/GlobalServices/member-model.service';
import{ Member} from '../../../GlobalServices/member.model';

@Component({
  selector: 'app-member-form',
  templateUrl: './member-form.component.html',
  styleUrls: ['./member-form.component.css']
})
export class MemberFormComponent implements OnInit {

  member:Member;
  user:User;


  constructor( private memberService:MemberService ) { }

  ngOnInit(): void {
  }

  onSignin(form: NgForm) {
    const name = form.value.name;
    const nick = form.value.nick;
    const userID = form.value.userID;
    const memberID = form.value.memberID;
    const points = form.value.points;
    const role = form.value.role;
    const email = form.value.email;
    const password = form.value.password;

    this.member = new Member(memberID, userID,points,email);
    this.user = new User(userID,nick,name,role);
    this.member.user=this.user;
    this.user.Member = this.member;
    console.log(this.member);
    console.log(this.user);

  }
  submit(){
    this.memberService.CreateMember(this.member).subscribe(
      (response:Response)=>{
        console.log(response);
      }
    )

    this.memberService.CreateUser(this.user).subscribe(
      (response:Response)=>{
        console.log(response);
      }
    )



    
  
  }

}
