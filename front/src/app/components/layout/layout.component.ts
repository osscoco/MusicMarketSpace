import { AfterViewInit, Component, OnInit } from '@angular/core';
import { NavigationEnd, Router, RouterOutlet } from '@angular/router';
import { NavbarAnonymousComponent } from '../../shared-components/navbar-anonymous/navbar-anonymous.component';
import { NavbarAuthenticatedComponent } from '../../shared-components/navbar-authenticated/navbar-authenticated.component';
import { AuthService } from '../../services/auth.service';
import { ToastComponent } from '../../shared-components/toast/toast.component';

@Component({
  selector: 'app-layout',
  standalone: true,
  imports: [RouterOutlet, NavbarAnonymousComponent, NavbarAuthenticatedComponent, ToastComponent],
  templateUrl: './layout.component.html',
  styleUrl: './layout.component.scss'
})
export class LayoutComponent implements OnInit, AfterViewInit {

  isAuthenticated: boolean = false;

  // Constructeur
  constructor(protected authService: AuthService, private router: Router) {}

  async ngOnInit() {
    this.isAuthenticated = await this.authService.checkAuthentication();
  }

  ngAfterViewInit() {
    this.router.events.subscribe((event) => {
      if (event instanceof NavigationEnd) {
        this.ngOnInit(); // Force le re-rendering du composant
      }
    });
  }
}