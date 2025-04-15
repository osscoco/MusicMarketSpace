import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-home',
  standalone: true,
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {
  
  isAuthenticated: boolean = false;

  // Constructeur
  constructor(protected authService: AuthService) {}

  async ngOnInit() {
    this.isAuthenticated = await this.authService.checkAuthentication();
  }
}