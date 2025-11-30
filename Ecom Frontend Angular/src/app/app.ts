import { HttpClient } from '@angular/common/http';
import { Component, OnInit, signal } from '@angular/core';
import { IProducts } from './shared/Models/Product';
import { IPagination } from './shared/Models/Pagination';
import { IdentityService } from './identity/identity-service';

@Component({
  selector: 'app-root',
  templateUrl: './app.html',
  standalone: false,
  styleUrl: './app.scss'
})
export class App implements OnInit {
constructor(private _identitySerivce : IdentityService){}

  ngOnInit(): void {
      this._identitySerivce.loadRole();
      this._identitySerivce.isAuth().subscribe({
        next:(value)=> {
          this._identitySerivce.authState.next(true);
        },
        error:(value)=>{
          this._identitySerivce.authState.next(false)
        }
      })

  }

}
