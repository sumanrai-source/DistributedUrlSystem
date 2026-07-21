import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NavigationEnd, Router, RouterOutlet } from '@angular/router';
import { filter } from 'rxjs/operators';
import { SystemFooterComponent } from './shared/ui/system-footer/system-footer.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule, RouterOutlet, SystemFooterComponent],
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  isGateRoute = false;
  profileMenuOpen = false;

  constructor(router: Router) {
    router.events.pipe(filter((event): event is NavigationEnd => event instanceof NavigationEnd)).subscribe(event => {
      this.isGateRoute = event.urlAfterRedirects === '/';
    });
  }

  toggleProfileMenu(): void {
    this.profileMenuOpen = !this.profileMenuOpen;
  }

  logout(): void {
    this.profileMenuOpen = false;
    console.log('Logging out...');
  }
}
``