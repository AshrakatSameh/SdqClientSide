import { animate, style, transition, trigger } from '@angular/animations';
import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Store } from '@ngrx/store';
import { TranslateService } from '@ngx-translate/core';
import { AppService } from 'src/app/service/app.service';
import { AuthService } from 'src/app/service/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
  animations: [
    trigger('toggleAnimation', [
        transition(':enter', [style({ opacity: 0, transform: 'scale(0.95)' }), animate('100ms ease-out', style({ opacity: 1, transform: 'scale(1)' }))]),
        transition(':leave', [animate('75ms', style({ opacity: 0, transform: 'scale(0.95)' }))]),
    ]),
],
})
export class LoginComponent {

  store: any;
  loginForm: FormGroup;
    constructor( private appSetting: AppService,
       public storeData: Store<any>,
       public translate: TranslateService,
      private auth: AuthService,
      private router: Router,
    private fb: FormBuilder) {
      this.initStore();

      this.loginForm = this.fb.group({
        email: ['', [Validators.required, Validators.email]],
        password: ['', Validators.required],
      });
    }
   
    async initStore() {
      this.storeData
          .select((d) => d.index)
          .subscribe((d) => {
              this.store = d;
          });
  }
  changeLanguage(item: any) {
    this.translate.use(item.code);
    this.appSetting.toggleLanguage(item);
    if (this.store.locale?.toLowerCase() === 'ae') {
        this.storeData.dispatch({ type: 'toggleRTL', payload: 'rtl' });
    } else {
        this.storeData.dispatch({ type: 'toggleRTL', payload: 'ltr' });
    }
    window.location.reload();
}

login() {
  this.auth.login(this.loginForm.value).subscribe({
    next: (response) => {
      if (response.isSuccess) {
        this.router.navigate(['/table']);
      }
     
    },
    error: (error) => {
      console.log(error);
      
    }
  });
}
}
