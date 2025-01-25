import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { AuthService } from '../service/auth.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
  constructor(private auth: AuthService, private router: Router ) {}
  canActivate(): boolean {

    const role = this.auth.getUserDetail()?.roles;
    if(role[1] == "SupportAgent"){
      return true;
    }else{
      alert("You are not authorized to view this page");
      this.router.navigate(['/login']);
      return false;
    }
  }
  
}
