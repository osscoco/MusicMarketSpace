import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
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
export class LayoutComponent {

  // Constructeur
  constructor(protected authService: AuthService) {}
}