import { Injectable } from "@angular/core";
import { HttpClient } from '@angular/common/http';
import { NormalPurchase } from './purchase-model.model';
import {MemberPurchase} from'./purchase-model.model';
import { Input } from "@angular/core";

@Injectable()
export class PurchaseService {
  private dataPath = 'https://localhost:5001/api/normalPurchases';
  private dataPath1 = 'https://localhost:5001/api/memberPurchases';

  @Input() id:string;
  private dataMember = 'https://localhost:5001/api/member/';

  constructor(private http: HttpClient) { }

 GetNormalPurchase() {
    return this.http.get<NormalPurchase[]>(this.dataPath);

  }

  GetMemberPurchase() {
    return this.http.get<MemberPurchase[]>(this.dataPath1);

  }

}