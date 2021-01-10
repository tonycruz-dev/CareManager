import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {ToastrModule} from 'ngx-toastr';
import { SharedModule } from '../shared/shared.module';
import { HasRoleDirective } from './directives/has-role.directive';
import { NavBarComponent } from './nav-bar/nav-bar.component';
import { SadeBarComponent } from './sade-bar/sade-bar.component';
import { RouterModule } from '@angular/router';
import { MenuSideComponent } from './menu-side/menu-side.component';



@NgModule({
  declarations: [HasRoleDirective, NavBarComponent, SadeBarComponent, MenuSideComponent],
  imports: [
    CommonModule,
    RouterModule,
    SharedModule,
    ToastrModule.forRoot({
      positionClass: 'toast-bottom-right',
      preventDuplicates: true
    })
  ],
  exports: [NavBarComponent, SadeBarComponent],
})
export class CoreModule { }
