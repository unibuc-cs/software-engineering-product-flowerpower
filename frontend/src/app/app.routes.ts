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
        path: '',
        component: HomeComponent,
        title: 'Home | Blinq24/1',
        canActivate: [AuthGuard],
        children: [
            { path: 'hero', component: HomeHeroComponent, title: 'Hero | Blinq24/1' } 
        ]
    },
    { path: 'search-users', component: SearchUserComponent },
    {path: 'user', component: UserComponent}
];
@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
  })
  export class AppRoutingModule { }