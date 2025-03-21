import { Routes } from '@angular/router';

export const routes: Routes = [
    // La route '' (racine) appel le composant 'Layout' via son module
    { path: '', loadChildren: () => import('../app/components/layout/layout.module').then(m => m.LayoutModule) },
    // La route contenant n'importe quelle url autre que '' (racine) redirige vers la route '' (racine)
    { path: '**', redirectTo: '' }
];
