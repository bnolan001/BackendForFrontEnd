import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {
  public UserName: string = null;

  constructor(private http: HttpClient) {
  }

  public checkStatus(): Promise<void> {
    console.log('AuthenticationService:checkStatus');
    return this.http.get<UserInfo>(`api/users`).toPromise()
      .then((user: UserInfo) => {
        console.log('AuthenticationService:checkStatus: user ' + user.name);
        this.UserName = user.name;
      }).catch(error => {
        console.error(error)
      });
  }

  public isAuthenticated(): boolean {
    return this.UserName !== null;
  }

  public authenticateUser(redirect: string): void {
    window.location.href = `accounts?redirect=${redirect}`;
  }
}

interface UserInfo {
  name: string;
}
