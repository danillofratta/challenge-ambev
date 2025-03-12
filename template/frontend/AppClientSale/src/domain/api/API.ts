import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Router } from '@angular/router';
import { environment } from '../../environments/environment.development';


@Injectable({
  providedIn: 'root'
})
export class API {

  protected _baseurlcommand = "";
  protected _baseurlquery = "";

  protected _endpointcommand = "";
  protected _endpointquery = "";

  protected _http: HttpClient;
  protected _router: Router

  constructor(
    protected http: HttpClient,
    protected router: Router
  ) {
    this._http = http;
    this._router = router; 
  }
}
