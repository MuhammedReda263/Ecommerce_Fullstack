import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { IdentityService } from '../identity-service';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-forget-password-component',
  standalone: false,
  templateUrl: './forget-password-component.html',
  styleUrl: './forget-password-component.scss',
})
export class ForgetPasswordComponent {
  formGroup!: FormGroup;
  isLoading: boolean = false;

  constructor(
    private fb: FormBuilder,
    private identityService: IdentityService,
    private router: Router,
    private _toaster : ToastrService
  ) { }

  ngOnInit(): void {
    this.formValidation();
  }

  formValidation() {
    this.formGroup = this.fb.group({
      email: ['', [Validators.required, Validators.email]]
    });
  }

  get _email() {
    return this.formGroup.get('email');
  }

  submit() {
    if (this.formGroup.valid) {
    this.isLoading = true;

    this.identityService.forgetPassword(this.formGroup.value).subscribe({
      next: (res) => {
        this.isLoading = false;
        this._toaster.success("Verification code sent to your email","Success");
      },
      error: (err) => {
        this.isLoading = false;
        this._toaster.error(err.error.message,"Error");
        console.error(err);
      }
    });
  }
   }

}
