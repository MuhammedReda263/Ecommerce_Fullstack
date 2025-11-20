import { Component, OnInit } from '@angular/core';
import { ResetPassword } from '../../shared/Models/ResetPassword';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { IdentityService } from '../identity-service';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-reset-password-component',
  standalone: false,
  templateUrl: './reset-password-component.html',
  styleUrl: './reset-password-component.scss',
})
export class ResetPasswordComponent implements OnInit {
  _resetPassword: ResetPassword = new ResetPassword();
  _formGroup: FormGroup
  _loading: boolean = false;
  constructor(
    private _fb: FormBuilder,
    private _identityService: IdentityService,
    private _router: Router,
    private _activeRoute: ActivatedRoute,
    private _toaster: ToastrService
  ) { }
  ngOnInit(): void {
    this._activeRoute.queryParams.subscribe(params => {
      this._resetPassword.email = params['email'];
      this._resetPassword.token = params['code'];
    });
    this.formValidation();
  }

  formValidation() {
    this._formGroup = this._fb.group({
      newPassword: ['', [Validators.required, Validators.minLength(6)]],
      confirmedPassword: ['', [Validators.required, Validators.minLength(6)]]
    },
      {
        validators: this.passwordMatchValidator.bind(this)
      }
    );
  }
  passwordMatchValidator(form: FormGroup) {
    const newPass = form.get('newPassword')?.value;
    const confirmPass = form.get('confirmedPassword')?.value;

    if (newPass !== confirmPass) {
      form.get('confirmedPassword')?.setErrors({ notMatch: true });
    } else {
      form.get('confirmedPassword')?.setErrors(null);
    }

    return null;
  }

  get _newPassword() {
    return this._formGroup.get('newPassword');
  }
  get _confirmedPassword() {
    return this._formGroup.get('confirmedPassword');
  }

  Submit() {
    {
      if (this._formGroup.valid) {
        this._loading = true;
        this._resetPassword.newPassword = this._newPassword?.value;
        this._identityService.resetPassword(this._resetPassword).subscribe({
          next: (res) => {
            console.log(res);
            this._loading = false;
            this._toaster.success("Password has been reset successfully", "Success");
            this._router.navigate(['/account/login']);
          },

          error: (err) => {
            this._loading = false;
            console.log(err);
            this._toaster.error(err.error.message, "Error");
          }
        });
      }
    }
  }
}
