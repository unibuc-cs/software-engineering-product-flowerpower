import { Routes } from '@angular/router';
import {RegisterComponent} from "./register/register.component";
import {LoginComponent} from "./login/login.component";
import {HomeComponent} from "./home/home.component";
import {AuthGuard} from "./auth.guard";
import { FriendsListComponent } from './friends-list/friends-list.component';

export const routes: Routes = [
    { path: 'register', component: RegisterComponent, title: 'Register | Blinq24/1' },
    { path: 'login', component: LoginComponent, title: 'Login | Blinq24/1' },
    { path: 'home', component: HomeComponent, title: 'Home | Blinq24/1', canActivate: [AuthGuard] }, // partea cu canActivate o puneti la rute doar pt useri logati
    {path: 'friends-list', component: FriendsListComponent, title: 'Friends List | Blinq24/1'}, //trebuie setat can activate
];
