import { Routes } from '@angular/router';

export const URLSLUG_ROUTES: Routes = [
  {
    path: '',
    loadComponent: () =>
      import('./components/slug-list/slug-list.components').then(m => m.SlugsMappingListComponent)
  },
  {
    path: 'create',
    loadComponent: () =>
      import('./components/slug-form/slug-form.components').then(m => m.SlugsMappingsFormComponent)
  },
  {
    path: 'edit/:id',
    loadComponent: () =>
      import('./components/slug-form/slug-form.components').then(m => m.SlugsMappingsFormComponent)
  }
];
