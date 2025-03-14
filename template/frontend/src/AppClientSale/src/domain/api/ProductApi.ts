import { Injectable } from '@angular/core';
import { API } from './API';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { ProductDto } from '../dto/ProductDto';
import { environment } from '../../environments/environment.development';
import { Observable } from 'rxjs';


@Injectable({
    providedIn: 'root'
})
export class ProductApi extends API {
   
  constructor(
    protected override http: HttpClient,    
    protected override router: Router
  ) {
    super(http, router);

    this._baseurlcommand = environment.ApiUrlSaleCommand;

    if (!environment.production)
      this._endpointcommand = '/api/v1/product/';
    else
      this._endpointcommand = '/product';
  }
  
  async GetListAll(): Promise<Observable<ProductDto[]>> {
    return this._http.get<ProductDto[]>(`${this._baseurlquery + this._endpointquery}`);
  }

  async GetById(id: number): Promise<Observable<ProductDto[]>>{   
    return this._http.get<ProductDto[]>(`${this._baseurlquery + this._endpointquery + '/getbyid/' + id}`);
  }

  async GetByName(name: string): Promise<Observable<ProductDto[]>>{
    return this._http.get<ProductDto[]>(`${this._baseurlquery + this._endpointquery + '/getbyname/' + name}`);
  }

  async Save(dto: ProductDto): Promise<Observable<ProductDto>> {
    return this._http.post<ProductDto>(`${this._baseurlcommand + this._endpointquery}`, dto);
   }

  async Update(dto: ProductDto): Promise<Observable<ProductDto>> {
    return this._http.put<ProductDto>(`${this._baseurlcommand + this._endpointquery}`, dto);
    }

  async Delete(id: number): Promise<Observable<void>>{
    return this._http.delete<void>(`${this._baseurlcommand + this._endpointquery +'/'+ id}`);
  }

}
