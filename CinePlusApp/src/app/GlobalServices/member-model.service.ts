import { Injectable } from "@angular/core";
import { HttpClient } from '@angular/common/http';
import { Member } from './member.model';
import{ User} from './user.model';
import { identifierModuleUrl } from "@angular/compiler";

@Injectable()
export class MemberService {
  private dataPath = 'https://localhost:5001/api/member';
  private dataPathUser = 'https://localhost:5001/api/users';

 

  constructor(private http: HttpClient) { }

  GetMember() {
    return this.http.get<Member[]>(this.dataPath);
  }

  DeleteMember(id:number){
    return this.http.delete(this.dataPath + '/' + id);
  }
  CreateMember(member:Member){
    return this.http.post(this.dataPath,member);

  }
  CreateUser( user:User){
    return this.http.post(this.dataPathUser,user);

  }
}