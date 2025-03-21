import { Component, HostListener, OnInit } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import { NgIf } from '@angular/common';

@Component({
  selector: 'app-navbar-authenticated',
  standalone: true,
  imports: [NgIf],
  templateUrl: './navbar-authenticated.component.html',
  styleUrl: './navbar-authenticated.component.scss'
})
export class NavbarAuthenticatedComponent implements OnInit {

  // Responsive pour les Tooltips
  isMobileOrTablet = false;

  // Constructeur
  constructor(protected authService: AuthService) {}

  ngOnInit(): void {
    this.checkViewport();
  }

  @HostListener('window:resize')
  onResize() {
    this.checkViewport();
  }

  checkViewport() {
    const width = window.innerWidth;
    this.isMobileOrTablet = width >= 320 && width <= 480;
  }
    
  // Une fois le click sur le bouton de déconnexion
  async logout() {
    await this.authService.logout();
    window.location.reload();
  }
}
