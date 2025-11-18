import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { validate } from 'uuid';
import { IdentityService } from '../identity-service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-register',
  standalone: false,
  templateUrl: './register.html',
  styleUrl: './register.scss',
})
export class RegisterComponent implements OnInit {
  fromgroup: FormGroup;
  constructor(private fb: FormBuilder, private _identityService: IdentityService, private _toster: ToastrService) { }
  ngOnInit(): void {
    this.formValidation();
  }

  formValidation() {
    this.fromgroup = this.fb.group({
      userName: ['', [Validators.required, Validators.minLength(6)]],
      email: ['', [Validators.required, Validators.email]],
      displayName: ['', [Validators.required]],
      password: ['', [Validators.required, Validators.minLength(6)]]
    });
  }

  get UserName() {
    return this.fromgroup.get('UserName');
  }

  get Email() {
    return this.fromgroup.get('Email');
  }
  get DisplayName() {
    return this.fromgroup.get('DisplayName');
  }

  get Password() {
    return this.fromgroup.get('Password');
  }

  Submit() {
    if (this.fromgroup.valid) {
      this._identityService.register(this.fromgroup.value).subscribe({
        next: (res) => {
          console.log(res);
          this._toster.success("Registeration Successfully, Please Check your Email to Activate your Account", "Success")
        },
        error: (err) => {
          this._toster.error(err.error.message, "error")
        }
      })
    }
  }
}
