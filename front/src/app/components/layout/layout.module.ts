import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LayoutComponent } from '../layout/layout.component';
import { HomeComponent } from '../../components/home/home.component';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { AuthInterceptor } from '../../interceptors/auth.interceptor';
import { AdminComponent } from '../admin/admin.component';
import { AdminGuard } from '../../guards/admin/admin.guard';

const routes: Routes = [
  // La route '' (racine) appel le composant 'Layout'
  {
    path: '',
    component: LayoutComponent,
    children: [
      // La route '' (racine) appel le sous composant 'Home'
      { path: '', component: HomeComponent },
      { path: 'admin', component: AdminComponent, canActivate: [AdminGuard] }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
  //Ajout de l'interceptor pour certaines requêtes nécessitants l'authorization bearer token
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor,
      multi: true
    }
  ],
})
export class LayoutModule {}