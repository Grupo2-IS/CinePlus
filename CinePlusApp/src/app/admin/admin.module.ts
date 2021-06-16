import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AdminRoutingModule } from './admin-routing.module';
import { ArtistModelComponent } from './models/artist-model/artist-model.component';
import { FilmModelComponent } from './models/film-model/film-model.component';
import { MemberModelComponent } from './models/member-model/member-model.component';
import { ShowingModelComponent } from './models/showing-model/showing-model.component';
import { PurchaseModelComponent } from './models/purchase-model/purchase-model.component';
import { AdminMainComponent } from './admin-main/admin-main.component';
import { AdminComponent } from './admin.component';
import { FilmFormComponent } from './forms/film-form/film-form.component';
import { ArtistFormComponent } from './forms/artist-form/artist-form.component';
import { MemberFormComponent } from './forms/member-form/member-form.component';
import { PurchaseFormComponent } from './forms/purchase-form/purchase-form.component';
import { ShowingFormComponent } from './forms/showing-form/showing-form.component';


@NgModule({
  declarations: [ArtistModelComponent,
    FilmModelComponent,
    MemberModelComponent,
    ShowingModelComponent,
    PurchaseModelComponent,
    AdminMainComponent,
    AdminComponent,
    FilmFormComponent,
    ArtistFormComponent,
    MemberFormComponent,
    PurchaseFormComponent,
    ShowingFormComponent
  ],
  imports: [
    CommonModule,
    AdminRoutingModule
  ]
})
export class AdminModule { }
