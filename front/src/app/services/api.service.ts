import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ApiService {

  // Url de l'Api Backend (dotnet)
  private apiUrl = environment.apiUrl + '/api/';

  // Constructeur
  constructor(private http: HttpClient) { }

  // Se connecter : Appel à l'Api Backend (dotnet)
  login(postData: any): Observable<any> {
    return this.http.post<any>(this.apiUrl + 'Auth/login', postData);
  }

  // Se créer un compte : Appel à l'Api Backend (dotnet)
  signup(postData: any): Observable<any> {
    return this.http.post<any>(this.apiUrl + 'User', postData);
  }

  // Se déconnecter : Appel à l'Api Backend (dotnet)
  logout(): Observable<any> {
    return this.http.get<any>(this.apiUrl + 'Auth/logout');
  }
}