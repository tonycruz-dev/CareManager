import { Directive, Input, ViewContainerRef, TemplateRef, OnInit } from '@angular/core';
import { AccountService } from 'src/app/account/account.service';

@Directive({
  selector: '[appHasRole]'
})
export class HasRoleDirective implements OnInit{
  @Input() appHasRole: string[];
  isVisible = false;

  constructor(
    private viewContainerRef: ViewContainerRef,
    private templateRef: TemplateRef<any>,
    private accountService: AccountService) { }

    ngOnInit() {
      // tslint:disable-next-line:no-debugger
      // debugger;
      const userRoles = this.accountService.decodedToken.role as Array<string>;
      // if no roles clear the view container ref
      if (!userRoles) {
        this.viewContainerRef.clear();
      }

      // if user has role needed then render the element
      if (this.accountService.roleMatch(this.appHasRole)) {
        if (!this.isVisible) {
          this.isVisible = true;
          this.viewContainerRef.createEmbeddedView(this.templateRef);
        } else {
          this.isVisible = false;
          this.viewContainerRef.clear();
        }
      }
    }
}
