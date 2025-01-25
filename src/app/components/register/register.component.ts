import { animate, style, transition, trigger } from '@angular/animations';
import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Store } from '@ngrx/store';
import { TranslateService } from '@ngx-translate/core';
import { AppService } from 'src/app/service/app.service';
import { AuthService } from 'src/app/service/auth.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css'],
  animations: [
    trigger('toggleAnimation', [
      transition(':enter', [style({ opacity: 0, transform: 'scale(0.95)' }), animate('100ms ease-out', style({ opacity: 1, transform: 'scale(1)' }))]),
      transition(':leave', [animate('75ms', style({ opacity: 0, transform: 'scale(0.95)' }))]),
    ]),
  ],
})
export class RegisterComponent {

  store: any;
  registerForm: FormGroup;
  constructor(public translate: TranslateService,
    public storeData: Store<any>, public router: Router,
    private appSetting: AppService, private fb: FormBuilder, private auth: AuthService) {
    this.initStore();

    this.registerForm = this.fb.group({
      fullName: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      password: ['', Validators.required],
      roles: this.fb.array([]),
    });

    this.getRoles();
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

  signUp() {
    this.auth.register(this.registerForm.value).subscribe({
      next: (response) => {
        alert(response.message);
      },
      error: (error) => {
        console.log(error);
      }
    });
  }

  roles:any[] = [];
  getRoles() {
    this.auth.getAllRolses().subscribe({
      next: (response) => {
        this.roles = response;
        console.log(this.roles);
      },
      error: (error) => {
        console.log(error);
      }
    });
  }
}
