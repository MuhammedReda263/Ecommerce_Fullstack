import { Component, OnInit } from '@angular/core';
import { IdentityService } from '../identity-service';
import { ActivatedRoute, Router } from '@angular/router';
import { ActiveAccount } from '../../shared/Models/ActiveAccount';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-active-componemt',
  standalone: false,
  templateUrl: './active-componemt.html',
  styleUrl: './active-componemt.scss',
})
export class ActiveComponemt implements OnInit {
  constructor(private _identityService: IdentityService,
    private _activeRoute: ActivatedRoute,
    private _toaster: ToastrService,
    private router: Router
  ) { }
  activeAcount: ActiveAccount = new ActiveAccount();
  status: 'loading' | 'success' | 'failed' = 'loading';
  ngOnInit(): void {
    this._activeRoute.queryParams.subscribe(params => {
      this.activeAcount.Email = params['email'];
      this.activeAcount.Token = params['code'];
     if (!this.activeAcount.Email || !this.activeAcount.Token) return; // Stop execution on first empty emit

      // After getting the params from the URL, call the active method from the service -- Very Important
      this._identityService.active(this.activeAcount).subscribe({
        next: (res) => {
          console.log(res);
          this._toaster.success('Your account is active', 'SUCCESS');
          this.status = 'success';
          setTimeout(() => {
            this.router.navigate(['account/login']);
          }, 2000);

        },
        error: (err) => {
          console.log(err);
          this._toaster.error('Your account is not active, token is expired', 'ERROR');
          this.status = 'failed';

        }
      })
    }

    );
  }
}
