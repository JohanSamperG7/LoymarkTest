import { Routes } from '@angular/router';

export const routes: Routes = [
    {
        path: '',
        loadComponent: () => import('./shared/layout/layout.component'),
        children:[
            {
                path: 'usuarios',
                loadComponent: () => import('./business/user/user.component')
            },
            {
                path: 'actividades',
                loadComponent: () => import('./business/activity/activity.component')
            },
            {
                path: '',
                redirectTo: 'usuarios',
                pathMatch: 'full'
            }           
        ]
    }
];
