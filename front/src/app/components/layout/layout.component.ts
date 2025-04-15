import { AfterViewInit, Component, OnInit } from '@angular/core';
import { NavigationCancel, NavigationEnd, NavigationError, NavigationStart, Router, RouterOutlet } from '@angular/router';
import { NavbarAnonymousComponent } from '../../shared-components/navbar-anonymous/navbar-anonymous.component';
import { NavbarAuthenticatedComponent } from '../../shared-components/navbar-authenticated/navbar-authenticated.component';
import { AuthService } from '../../services/auth.service';
import { ToastComponent } from '../../shared-components/toast/toast.component';
import { SpinnerComponent } from '../../shared-components/spinner/spinner.component';
import { SpinnerService } from '../../services/spinner.service';

@Component({
  selector: 'app-layout',
  standalone: true,
  imports: [RouterOutlet, NavbarAnonymousComponent, NavbarAuthenticatedComponent, ToastComponent, SpinnerComponent],
  templateUrl: './layout.component.html',
  styleUrl: './layout.component.scss'
})
export class LayoutComponent implements OnInit, AfterViewInit {

  isAuthenticated: boolean = false;
  isLoading = false;

  // Constructeur
  constructor(protected authService: AuthService, private router: Router, private spinnerService: SpinnerService) {}

  async ngOnInit() {

    // Spinner visible pendant 300–500ms pour un "rechargement F5"
    this.spinnerService.showFor(500);

    this.isAuthenticated = await this.authService.checkAuthentication();

    this.spinnerService.loading$.subscribe((loading) => {
      this.isLoading = loading;
    });
  }

  ngAfterViewInit() {
    this.router.events.subscribe((event) => {
      if (event instanceof NavigationStart) {
        this.spinnerService.show();
      }

      if (
        event instanceof NavigationEnd ||
        event instanceof NavigationCancel ||
        event instanceof NavigationError
      ) {
        this.spinnerService.hide();
      }
    });
  }
}