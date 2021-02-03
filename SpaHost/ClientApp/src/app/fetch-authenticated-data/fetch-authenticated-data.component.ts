import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-fetch-authenticated-data',
  templateUrl: './fetch-authenticated-data.component.html'
})
export class FetchAuthenticatedDataComponent {
  public userInfo: UserInfo;
  public claims: Claim[];

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    // Attempt to get the authenticated user's name
    http.get<UserInfo>(baseUrl + 'api/users').subscribe(result => {
      this.userInfo = result;
      // If the user is not authenticated then redirect them to an MVC controller
      // that requires authentication
      if (!this.userInfo.name) {
        let currentLocation = window.location.href;
        window.location.href = `${baseUrl}accounts?redirect=${currentLocation}`;
      }
      // Request the claims for the user from the API endpoint
      http.get<Claim[]>(baseUrl + 'api/test/identity').subscribe(result => {
        this.claims = result;
      }, error => console.error(error));
    }, error => console.error(error));
  }
}

interface UserInfo {
  name: string;
}

interface Claim {
  type: string;
  value: string;
}
