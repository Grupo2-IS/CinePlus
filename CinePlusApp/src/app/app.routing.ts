import { NgModule } from '@angular/core';
import { CommonModule, } from '@angular/common';
import { BrowserModule } from '@angular/platform-browser';
import { Routes, RouterModule } from '@angular/router';

import { MainComponent } from './main/main.component';

import { CarteleraComponent } from './user/cartelera/cartelera.component';
import { ProjectionDetailsComponent } from './user/projection-details/projection-details.component';
import { SetPurchaseComponent } from './user/set-purchase/set-purchase.component';

import { AdminMainComponent } from './admin/admin-main/admin-main.component';
import { ArtistModelComponent } from './admin/models/artist-model/artist-model.component';
import { FilmModelComponent } from './admin/models/film-model/film-model.component';
import { MemberModelComponent } from './admin/models/member-model/member-model.component';
import { PurchaseModelComponent } from './admin/models/purchase-model/purchase-model.component';
import { ShowingModelComponent } from './admin/models/showing-model/showing-model.component';
import { FilmFormComponent } from './admin/forms/film-form/film-form.component';
import { ArtistFormComponent } from './admin/forms/artist-form/artist-form.component';
import { MemberFormComponent } from './admin/forms/member-form/member-form.component';
import { PurchaseFormComponent } from './admin/forms/purchase-form/purchase-form.component';
import { ShowingFormComponent } from './admin/forms/showing-form/showing-form.component';

const routes: Routes = [
    { path: '', redirectTo: 'index', pathMatch: 'full' },
    { path: 'index', component: MainComponent },
    // User paths.
    { path: 'cartelera', component: CarteleraComponent },
    { path: 'cartelera/detalles', component: ProjectionDetailsComponent },
    { path: 'reservar', component: SetPurchaseComponent },
    // Admin paths.
    { path: 'admin', component: AdminMainComponent },
    { path: 'admin/artists', component: ArtistModelComponent },
    { path: 'admin/artists/create', component: ArtistFormComponent },
    { path: 'admin/films', component: FilmModelComponent },
    { path: 'admin/films/create', component: FilmFormComponent },
    { path: 'admin/members', component: MemberModelComponent },
    { path: 'admin/members/create', component: MemberFormComponent },
    { path: 'admin/purhcases', component: PurchaseModelComponent },
    { path: 'admin/purhcases/create', component: PurchaseFormComponent },
    { path: 'admin/showings', component: ShowingModelComponent },
    { path: 'admin/showings/create', component: ShowingFormComponent },
    { path: 'test', component: SetPurchaseComponent }
];

@NgModule({
    imports: [
        CommonModule,
        BrowserModule,
        RouterModule.forRoot(routes)
    ],
    exports: [
    ],
})
export class AppRoutingModule { }
