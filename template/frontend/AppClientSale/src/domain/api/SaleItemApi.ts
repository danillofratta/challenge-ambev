import { Injectable } from '@angular/core';
import { API } from './API';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { environment } from '../../environments/environment.development';
import { ApiResponseDto } from '../dto/apibase/ApiResponseDto';
import { CancelSaleItemResponseDto } from '../dto/salitem/cancel/CancelSaleResponseDto';


@Injectable({
    providedIn: 'root'
})
export class SaleItemApi extends API {
  
  constructor(
    protected override http: HttpClient,    
    protected override router: Router
  ) {
    super(http, router);

    this._baseurlcommand = environment.ApiUrlSaleCommand;
    this._baseurlquery = environment.ApiUrlSaleQuery;

    this._endpointcommand = "api/v1/SalesItemCommand/";
  }
  
  async Cancel(record: CancelSaleItemResponseDto) {
    return this._http.put<ApiResponseDto<CancelSaleItemResponseDto>>(`${this._baseurlcommand + this._endpointcommand + 'cancel'}`, record);
  }

}
