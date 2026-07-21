import { Routes } from '@angular/router';

export const URLMAPPING_ROUTES: Routes = [
  {
    path: '',
    loadComponent: () =>
      import('./components/url-list/url-list.components').then(m => m.UrlMappingListComponent)
  },
  {
    path: 'create',
    loadComponent: () =>
      import('./components/url-form/url-form.components').then(m => m.UrlMappingsFormComponent)
  },
  {
    path: 'edit/:id',
    loadComponent: () =>
      import('./components/url-form/url-form.components').then(m => m.UrlMappingsFormComponent)
  }
];
