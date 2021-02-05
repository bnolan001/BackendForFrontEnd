import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { AuthenticationService } from '../services/authentication.service';

@Component({
  selector: 'app-guarded-route',
  templateUrl: './guarded-route.component.html'
})
export class GuardedRouteComponent {
  public userName: string;

  constructor(private http: HttpClient,
    private authService: AuthenticationService) {
  }

  async ngOnInit(): Promise<void> {
    this.userName = this.authService.UserName;
  }
}
