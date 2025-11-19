import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { IdentityService } from '../identity-service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-login-component',
  standalone: false,
  templateUrl: './login-component.html',
  styleUrl: './login-component.scss',
})
export class LoginComponent implements OnInit {
  formGroup: FormGroup;
  constructor(private _fb: FormBuilder, private _identityService: IdentityService,private _toaster: ToastrService) { }
  ngOnInit(): void {
    this.formValidation();
  }
  formValidation() {
    this.formGroup = this._fb.group({
      email: ['', [Validators.required,Validators.email]],
      password: ['', [Validators.required,Validators.minLength(6)]]
    })
  }

  get _email() {
    return this.formGroup.get('email');
  }
  get _password() {
    return this.formGroup.get('password');
  }

  Submit() {
    if (this.formGroup.valid) {
      this._identityService.login(this.formGroup.value).subscribe({
        next: (res) => {
          console.log(res);
          this._toaster.success("Login Successful","Success")
        },
        error: (err) => {
          console.log(err);
          this._toaster.error(err.error.message,"Error")
        }
      })
    }
  }



}
