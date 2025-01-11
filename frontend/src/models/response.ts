export interface PageResultResponse<T> {
    items: T[];
    currentPage: number;
    pageSize: number;
    totalCount: number;
    totalPage: number;
    hasNextPage: boolean;
    hasPreviousPage: boolean;
  }
  
  export interface ApiResponse<T> {
    success: boolean;
    data: T | null;
    errorMessage?: string;
    errors?: string[];
  }