
import { Component, inject } from '@angular/core';
import { LabelComponent } from '../../form/label/label.component';
import { CheckboxComponent } from '../../form/input/checkbox.component';
import { ButtonComponent } from '../../ui/button/button.component';
import { InputFieldComponent } from '../../form/input/input-field.component';
import { Router, RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { UserLogin } from '../../../../models/auth';
import { AuthService } from '../../../services/auth.service';

@Component({
  selector: 'app-signin-form',
  imports: [
    LabelComponent,
    CheckboxComponent,
    ButtonComponent,
    InputFieldComponent,
    RouterModule,
    FormsModule
],
  templateUrl: './signin-form.component.html',
  styles: ``
})
export class SigninFormComponent {

  showPassword = false;
  isChecked = false;
   loginUser : UserLogin = {
    username: '',
    password: ''
  };

  togglePasswordVisibility() {
    this.showPassword = !this.showPassword;
  }

  authService= inject(AuthService);
  router = inject(Router);
  isLogin = true;
  
  LoginUser() {
    console.log('LoginUser called with:', this.loginUser);
    this.authService.Login(this.loginUser).subscribe(
      (response) => {
        this.authService.saveAuthData(response);     
        this.router.navigate(['']);
      },
      (error) => {
        alert(error.error.message);
      }
    );
  }

  logout(){
      this.authService.logout();
      this.router.navigateByUrl('/login');
  }
}
