import { Routes, RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';
import {RegisterComponent} from "./register/register.component";
import {LoginComponent} from "./login/login.component";
import {HomeComponent} from "./home/home.component";
import {AuthGuard} from "./auth.guard";
import { SearchUserComponent } from './search-user/search-user.component';
import { FriendRequestsComponent } from './friend-requests/friend-requests.component';
export const routes: Routes = [
    { path: 'register', component: RegisterComponent, title: 'Register | Blinq24/1' },
    { path: 'login', component: LoginComponent, title: 'Login | Blinq24/1' },
    { path: 'home', component: HomeComponent, title: 'Home | Blinq24/1', canActivate: [AuthGuard] }, // partea cu canActivate o puneti la rute doar pt useri logati
    { path: 'search-users', component: SearchUserComponent },
    {path: 'friend-requests', component: FriendRequestsComponent},
    
];
@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
  })
  export class AppRoutingModule { }