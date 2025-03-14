export interface PaginatedListDto<T> {
  data: T[]; 
  currentPage: number;
  totalPages: number;
  totalCount: number;
}

export interface ApiResponsePaginatedDto<T> {
  success: boolean;
  message?: string; 
  data: PaginatedListDto<T>;
}
