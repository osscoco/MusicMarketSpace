import { Component, HostListener, OnInit } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import { NgIf } from '@angular/common';
import { Router } from '@angular/router';

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
  constructor(protected authService: AuthService, private router: Router) {}

  /// ---
  /// --- Cette partie est diédiée au bon affichage des éléments du menu navbar en fonction du support (PC, Tablette, Mobile)
  /// ---
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
  /// ---
  /// --- Fin de la partie dédiée
  /// ---

  admin() {
    this.router.navigateByUrl('/admin');
  }
    
  // Une fois le click sur le bouton de déconnexion
  async logout() {
    await this.authService.logout();
  }
}
