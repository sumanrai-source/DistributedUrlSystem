import { Routes } from '@angular/router';

export const routes: Routes = [
   {
    path: '',
    loadComponent: () => import('./features/gate/gate.component').then(m => m.GateComponent)
  },
  {
    path: 'url-shortner',
    loadChildren: () =>
      import('./features/url-shortner/url.routes')
        .then(m => m.URLMAPPING_ROUTES)
  },
  {
    path: 'all-slugs',
    loadChildren: () =>
      import('./features/url-slugs/slug.routes')
        .then(m => m.URLSLUG_ROUTES)
  },
  {
    path: '**',
    redirectTo: 'url-mapping'
  }
];