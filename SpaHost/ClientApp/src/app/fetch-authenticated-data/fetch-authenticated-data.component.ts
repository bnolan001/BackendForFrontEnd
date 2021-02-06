import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { AuthenticationService } from '../services/authentication.service';

@Component({
  selector: 'app-fetch-authenticated-data',
  templateUrl: './fetch-authenticated-data.component.html'
})
export class FetchAuthenticatedDataComponent {
  public userName: string;
  public claims: Claim[];

  constructor(private http: HttpClient,
    private authService: AuthenticationService) {
  }

  async ngOnInit(): Promise<void> {
    // Attempt to get the authenticated user's name
    if (this.authService.isAuthenticated()) {
      this.userName = this.authService.UserName;

      // Request the claims for the user from the API endpoint
      this.http.get<Claim[]>('api/test/identity').subscribe(result => {
        this.claims = result;
      }, error => console.error(error));
    }
    else {
      // Authenticate then return back to this page
      this.authService.authenticateUser("fetch-authenticated-data");
    }
  }
}

interface UserInfo {
  name: string;
}

interface Claim {
  type: string;
  value: string;
}
