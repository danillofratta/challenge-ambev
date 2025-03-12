import { Component, OnInit, ViewChild } from '@angular/core';
import { SaleApi } from '../../../../../domain/api/SaleApi';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ModifySaleResponseDto } from '../../../../../domain/dto/sale/Modify/ModifySaleResponseDto';
import { Observable, catchError, delay, finalize, lastValueFrom, map, of } from 'rxjs';
import { GetSaleItemResponseDto, GetSaleResponseDto } from '../../../../../domain/dto/sale/get/GetSaleResponseDto';
import { ApiResponseDto } from '../../../../../domain/dto/apibase/ApiResponseDto';
import { MatTableDataSource } from '@angular/material/table';
import { SaleItemApi } from '../../../../../domain/api/SaleItemApi';
import { CancelSaleItemResponseDto } from '../../../../../domain/dto/salitem/cancel/CancelSaleResponseDto';

@Component({
  selector: 'app-modify-sale',
  templateUrl: './modify-sale.component.html',
  styleUrl: './modify-sale.component.css'
})
export class ModifySaleComponent implements OnInit {

  id: any;
  public record: Observable<ModifySaleResponseDto> = new Observable<ModifySaleResponseDto>();
  public form: FormGroup;

  length = 0;
  pageSize = 10;
  pageIndex = 0;

  public list$: any[] = [];

  public busy = false;

  public _ListError: string[] = [];

  dataSource = new MatTableDataSource<GetSaleItemResponseDto>();

  displayedColumns = ['actions', 'id', 'productId', 'productName', 'quantity', 'unitPrice', 'discount', 'totalPrice', 'status'];
  constructor
    (
    private apiSale: SaleApi,
    private apiSaleItem: SaleItemApi,
      private fb: FormBuilder,
      private router: Router,
    private activatedRoute: ActivatedRoute)
  {
        
      this.form = this.fb.group({
      id: ['id', Validators.compose([
        Validators.required
      ])],
      customerId: ['customerId', Validators.compose([
        Validators.required
      ])],
      customerName: ['customerName', Validators.compose([
        Validators.required
      ])],
        status: ['status'],
        totalAmount: ['totalAmount'],
      branchId: ['branchId', Validators.compose([
              Validators.required
            ])],
      branchName: ['branchName', Validators.compose([
        Validators.required
      ])]
    });

  }


  ClearErrorList() {
    this._ListError = [];
  }

  async ngOnInit() {
    this.id = this.activatedRoute.snapshot.paramMap.get("id");
    this.busy = true;
    try {
      (await this.apiSale.GetById(this.id)).subscribe(
        (response: ApiResponseDto<GetSaleResponseDto>) => {
          if (response.success && response.data) {
            this.form.controls['id'].setValue(response.data.data.id);
            this.form.controls['customerId'].setValue(response.data.data.customerId);
            this.form.controls['customerName'].setValue(response.data.data.customerName);
            this.form.controls['branchId'].setValue(response.data.data.branchId);
            this.form.controls['branchName'].setValue(response.data.data.branchName);
            this.form.controls['totalAmount'].setValue(response.data.data.totalAmount);
            

            if (response.data.data.status == 1)
              this.form.controls['status'].setValue("Active");
            else
              this.form.controls['status'].setValue("Cancelled");

            //this.dataSource.data = response.data.saleItems || [];
            this.loadSaleItems(this.id!);

          } else {
            console.error("Erro ao buscar dados:", response.errors);
          }
        },
        (error) => {
          console.error("Erro na requisição:", error);
        }
      );
    }
    catch (error) {

    }
    this.busy = false;
  }

  async loadSaleItems(saleId: string) {
    this.busy = true;

    (await this.apiSale.GetItensOfSale(saleId)).pipe(
      delay(2000),
      catchError((error) => {
        this._ListError.push(error.message || 'Error');
        return of(null); // Retorna null em caso de erro
      }),
      finalize(() => (this.busy = false))
    )
      .subscribe((response) => {
        if (response && response.success) {
          this.dataSource.data = response.data.data;
        } else if (response && !response.success) {
          this._ListError.push(response.message || 'Error');
        }
      });

    this.busy = false;
  }

  async onCancel(id: string) {
    this.busy = true;
    this.ClearErrorList();
    const record: CancelSaleItemResponseDto = { Id: id }

    await (await this.apiSaleItem.Cancel(record)).subscribe({
      next: (response) => {
        if (response.success) {
          this.loadSaleItems(this.id);
          this.busy = false;
        } else {
          this._ListError.push(response.message);
        }
      },
      error: (error) => {
        console.error('Error occurred:', error);

        this._ListError.push(error.error.message);
        this.busy = false;
      },
      complete: () => {
        this.busy = false;
      }
    });
    
  }

  async modify() {

    if (this.form.valid) {
      this.busy = true;

      const record = this.form.value as ModifySaleResponseDto;

      try {


        await (await this.apiSale.Update(record)).subscribe({
          next: (response) => {
            if (response.success) {
              this.router.navigate(['/sale']);
            } else {
              this._ListError.push(response.message);
            }
          },
          error: (error) => {
            console.error('Error occurred:', error);

            this._ListError.push(error.error.message);
            this.busy = false;
          },
          complete: () => {
            this.busy = false;
          }
        });     

      } catch (error) {
        console.error('Erro ao salvar o stock:', error);
      } finally {
        this.busy = false;
      }        
    }
  }

}
