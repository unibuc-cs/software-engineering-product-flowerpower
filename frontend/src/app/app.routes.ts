import { Routes, RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';
import {RegisterComponent} from "./register/register.component";
import {LoginComponent} from "./login/login.component";
import {HomeComponent} from "./home/home.component";
import {AuthGuard} from "./auth.guard";
import { SearchUserComponent } from './search-user/search-user.component';

import { UserComponent } from './user/user.component';
import {HomeHeroComponent} from "./home-hero/home-hero.component";
export const routes: Routes = [
    { path: 'register', component: RegisterComponent, title: 'Register | Blinq24/1' },
    { path: 'login', component: LoginComponent, title: 'Login | Blinq24/1' },
    {
        path: 'home',
        component: HomeComponent,
        title: 'Home | Blinq24/1',
        canActivate: [AuthGuard],
        children: [
            { path: '', redirectTo: 'hero', pathMatch: 'full' },
            { path: 'hero', component: HomeHeroComponent, title: 'Hero | Blinq24/1' } ,
            { path: 'user', component: UserComponent , title:'User | Blinq24/1'},
        ]
    },
    { path: 'search-users', component: SearchUserComponent },
    { path: '**', redirectTo: '/home/hero', pathMatch: 'full' }
];
@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
  })
  export class AppRoutingModule { }