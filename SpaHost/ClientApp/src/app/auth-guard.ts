import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { AuthenticationService } from './services/authentication.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
  constructor(private authService: AuthenticationService) { }

  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
    // If the user isn't authenticted then call the authentication request from the authService and pass the
    // route the user was trying to go to so they can be properly redirected.
    if (!this.authService.isAuthenticated()) {
      this.authService.authenticateUser(next.routeConfig.path);
    }
    else {
      return true;
    }
  }
}
