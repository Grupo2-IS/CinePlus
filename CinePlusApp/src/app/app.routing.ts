import { NgModule } from '@angular/core';
import { CommonModule, } from '@angular/common';
import { BrowserModule } from '@angular/platform-browser';
import { Routes, RouterModule } from '@angular/router';

import { MainComponent } from './main/main.component';

import { CarteleraComponent } from './user/cartelera/cartelera.component';
import { ProjectionDetailsComponent } from './user/projection-details/projection-details.component';
import { SetPurchaseComponent } from './user/set-purchase/set-purchase.component';
import { UserComponent } from './user/user.component';
import { UserPaymentComponent } from './user/user-payment/user-payment.component';


import { AdminMainComponent } from './admin/admin-main/admin-main.component';
import { FilmModelComponent } from './admin/models/film-model/film-model.component';
import { MemberModelComponent } from './admin/models/member-model/member-model.component';
import { PurchaseModelComponent } from './admin/models/purchase-model/purchase-model.component';
import { ShowingModelComponent } from './admin/models/showing-model/showing-model.component';
import { FilmFormComponent } from './admin/forms/film-form/film-form.component';
import { MemberFormComponent } from './admin/forms/member-form/member-form.component';
import { PurchaseFormComponent } from './admin/forms/purchase-form/purchase-form.component';
import { ShowingFormComponent } from './admin/forms/showing-form/showing-form.component';
import { AdminComponent } from './admin/admin.component';

import { SignupComponent } from './auth/signup/signup.component';
import { SigninComponent } from './auth/signin/signin.component';
import { RemovePurchaseComponent } from './remove-purchase/remove-purchase.component';
// import { LandingComponent } from './examples/landing/landing.component';
// import { ProfileComponent } from './examples/profile/profile.component';
// import { ComponentsComponent } from './components/components.component';
// import { LoginComponent } from './examples/login/login.component';
// import { NucleoiconsComponent } from './components/nucleoicons/nucleoicons.component';


// import { ComponentsComponent } from './components/components.component'


const routes: Routes = [
    { path: '', redirectTo: 'index', pathMatch: 'full' },
    { path: 'index', component: MainComponent },
    // { path: 'index', component: ComponentsComponent },
    // { path: 'examples/login', component: LoginComponent },
    // { path: 'examples/landing', component: LandingComponent },
    // { path: 'examples/profile', component: ProfileComponent },
    // { path: 'nucleoicons', component: NucleoiconsComponent },
    { path: 'signup', component: SignupComponent },
    { path: 'signin', component: SigninComponent },
    { path: 'refund', component: RemovePurchaseComponent },

    // User paths.
    {
        path: 'user', component: UserComponent,
        children: [
            { path: 'cartelera', component: CarteleraComponent },
            { path: 'cartelera/detalles', component: ProjectionDetailsComponent },
            { path: 'cartelera/detalles/reservar', component: SetPurchaseComponent },
            { path: 'cartelera/detalles/reservar/pagar', component: UserPaymentComponent }
        ]
    },

    // {
    //     path: 'admin', loadChildren: () =>
    //         AdminModule
    // },
    // Admin paths.
    {
        path: 'admin', component: AdminComponent,
        children: [
            { path: '', redirectTo: 'main', pathMatch: 'full' },
            { path: 'main', component: AdminMainComponent },
            { path: 'films', component: FilmModelComponent },
            { path: 'films/create', component: FilmFormComponent },
            { path: 'members', component: MemberModelComponent },
            { path: 'members/create', component: MemberFormComponent },
            { path: 'purchases', component: PurchaseModelComponent },
            { path: 'purchases/create', component: PurchaseFormComponent },
            { path: 'showings', component: ShowingModelComponent },
            { path: 'showings/create', component: ShowingFormComponent },
        ]
    },
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
