import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink, RouterLinkActive } from '@angular/router';

import { MatToolbarModule } from '@angular/material/toolbar';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { MatListModule } from '@angular/material/list';

@Component({
  selector: 'app-system-footer',
  standalone: true,
  imports: [
    CommonModule,
    RouterLink,
    RouterLinkActive,
    MatToolbarModule,
    MatIconModule,
    MatButtonModule,
    MatSlideToggleModule,
    MatListModule
  ],
  templateUrl: './system-footer.component.html',
  styleUrls: ['./system-footer.component.scss']
})
export class SystemFooterComponent {

  profileOpen = false;
  darkMode = false;

  profileName = 'Admin User';

  profileRoles = [
    'Administrator',
    'Manager'
  ];

  navItems = [
  { icon: 'home', label: 'Home', route: '/' },
  { icon: 'link', label: 'Urls', route: '/url-shortner' },
  { icon: 'sell', label: 'Slugs', route: '/all-slugs' },
];

  actions = [
    { icon: 'settings', label: 'Settings', action: 'settings' },
    { icon: 'build', label: 'Configuration', action: 'configuration' },
    { icon: 'exit_to_app', label: 'Log Out', action: 'logout', danger: true }
  ];

  get profileRolesText(): string {
    return this.profileRoles.join(' • ');
  }

  toggleProfile(): void {
    this.profileOpen = !this.profileOpen;
    this.updateSidebarState();
  }

  toggleTheme(): void {
    this.darkMode = !this.darkMode;
    document.body.classList.toggle('dark-theme', this.darkMode);
  }

  private updateSidebarState(): void {
    document.body.classList.toggle('sidebar-open', this.profileOpen);
  }

  onAction(action: string): void {
    this.profileOpen = false;

    switch (action) {
      case 'settings':
        console.log('Open settings');
        break;

      case 'configuration':
        console.log('Open configuration');
        break;

      case 'logout':
        console.log('User requested logout');
        break;
    }
  }
}