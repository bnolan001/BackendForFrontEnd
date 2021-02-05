import { Injectable } from '@angular/core';
import { AuthenticationService } from './authentication.service';

@Injectable({
  providedIn: 'root'
})

/**
 * Pre-loads all data required for core application functions
 */
export class AppInitService {
  constructor(private authService: AuthenticationService) { }

  /**
   * Executes all data fetches before the app renders
   */
  public load(): Promise<any[]> {
    console.log(`AppInitService:load:Start`)
    return Promise.all([
      this.authService.checkStatus()
    ]);
  }
}
