import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { NavbarAnonymousComponent } from '../../shared-components/navbar-anonymous/navbar-anonymous.component';
import { NavbarAuthenticatedComponent } from '../../shared-components/navbar-authenticated/navbar-authenticated.component';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-layout',
  standalone: true,
  imports: [RouterOutlet, NavbarAnonymousComponent, NavbarAuthenticatedComponent],
  templateUrl: './layout.component.html',
  styleUrl: './layout.component.scss'
})
export class LayoutComponent {

  // Constructeur
  constructor(protected authService: AuthService) {}
}