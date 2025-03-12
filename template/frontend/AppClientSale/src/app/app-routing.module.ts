import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DashboardComponent } from './pages/dashboard/dashboard.component';
import { ListSaleComponent } from './pages/dashboard/sale/list-sale/list-sale.component';
import { ModifySaleComponent } from './pages/dashboard/sale/modify-sale/modify-sale.component';
import { CreateSaleComponent } from './pages/dashboard/sale/create-sale/create-sale.component';

const routes: Routes =
  [
    {
      path: '',
      //canActivate: [AuthService],
      component: DashboardComponent,      
      children: [
        { path: 'sale', component: ListSaleComponent },
        { path: 'sale/modify/:id', component: ModifySaleComponent },
        { path: 'sale/create', component: CreateSaleComponent },     
        { path: '', redirectTo: 'dashboard', pathMatch: 'full' } // Rota padrão
      ]
    }
  ];

@NgModule({
  imports: [RouterModule.forRoot(routes, { useHash: false })],
  exports: [RouterModule]
})
export class AppRoutingModule { }
