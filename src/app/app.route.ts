import { Routes } from '@angular/router';

// dashboard
import { IndexComponent } from './index';
import { AppLayout } from './layouts/app-layout';
import { AuthLayout } from './layouts/auth-layout';
import { LoginComponent } from './components/login/login.component';
import { TableComponent } from './components/table/table.component';
import { registerLocaleData } from '@angular/common';
import { RegisterComponent } from './components/register/register.component';
import { AuthGuard } from './guards/auth.guard';
import { tick } from '@angular/core/testing';
import { TicketComponent } from './components/ticket/ticket.component';

export const routes: Routes = [
    {
        path: '',
        component: AppLayout,
        children: [
            // dashboard
            { path: '', component: IndexComponent, title: 'Sales Admin | VRISTO - Multipurpose Tailwind Dashboard Template' },

            {path:'table', component: TableComponent},
            {path:'ticket/:id', component: TicketComponent},
        ], canActivate:[AuthGuard], 
    },

    {
        path: '',
        component: AuthLayout,
        children: [
        ],
    },
    {path:'login', component: LoginComponent},
    {path:'register', component: RegisterComponent},
];
