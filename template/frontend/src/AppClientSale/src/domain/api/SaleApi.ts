import { Injectable } from '@angular/core';
import { API } from './API';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { environment } from '../../environments/environment.development';
import { CreateSaleRequestDto } from '../dto/sale/create/CreateSaleRequestDto';
import { ModifySaleResponseDto } from '../dto/sale/Modify/ModifySaleResponseDto';
import { GetSaleItemResponseDto, GetSaleResponseDto } from '../dto/sale/get/GetSaleResponseDto';
import { ApiResponsePaginatedDto } from '../dto/apibase/ApiResponseListPaginationDto';
import { ApiResponseDto } from '../dto/apibase/ApiResponseDto';
import { CancelSaleResponseDto } from '../dto/sale/cancel/CancelSaleResponseDto';
import { Observable } from 'rxjs';
import { DeleteSaleResponseDto } from '../dto/sale/delete/DeleteSaleResponseDto';


@Injectable({
    providedIn: 'root'
})
export class SaleApi extends API {
  
  constructor(
    protected override http: HttpClient,    
    protected override router: Router
  ) {
    super(http, router);

    this._baseurlcommand = environment.ApiUrlSaleCommand;
    this._baseurlquery = environment.ApiUrlSaleQuery;

    this._endpointcommand = "api/v1/SalesCommand/";
    this._endpointquery = "api/v1/SalesQuery/";
  }
  
  async GetListAll(pageNumber: number, pageSize: number): Promise<Observable<ApiResponsePaginatedDto<GetSaleResponseDto>>> {
    let filter = "paginated?pageNumber=" + pageNumber + "&pageSize=" + pageSize;
    return this._http.get<ApiResponsePaginatedDto<GetSaleResponseDto>>(`${this._baseurlquery + this._endpointquery + filter}`);    
  }

  async GetById(id: string) {
    return this._http.get<ApiResponseDto<GetSaleResponseDto>>(`${this._baseurlquery + this._endpointquery + id}`);
  }

  async GetItensOfSale(id: string) {
    return this._http.get<ApiResponseDto<GetSaleItemResponseDto[]>>(
      `${this._baseurlquery}${this._endpointquery}getitensofsale/${id}`
    );
  }

  async Create(record: CreateSaleRequestDto) {    
    return this._http.post<ApiResponseDto<CreateSaleRequestDto>>(`${this._baseurlcommand + this._endpointcommand}`, record);
  }

  async Update(record: ModifySaleResponseDto) {
    return this._http.put<ApiResponseDto<ModifySaleResponseDto>>(`${this._baseurlcommand + this._endpointcommand}`, record);
  }

  async Cancel(record: CancelSaleResponseDto) {
    return this._http.put<ApiResponseDto<CancelSaleResponseDto>>(`${this._baseurlcommand + this._endpointcommand + 'cancel'}`, record);
  }

  async Delete(record: DeleteSaleResponseDto)  {
    return this._http.delete<ApiResponseDto<DeleteSaleResponseDto>>(
      `${this._baseurlcommand}${this._endpointcommand}`,
      { body: record }
    );
  }

}
