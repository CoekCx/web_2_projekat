export interface ListResponse<T> {
  currentPage: number;
  hasNextPage: number;
  hasPreviousPage: number;
  items: T[];
  pageSize: number;
  totalCount: number;
  totalPages: number;
}

export interface ListQueryParams {
  page: number;
  itemsPerPage: number;
}

export interface DealerQueryParams extends ListQueryParams {
  pending?: boolean;
}
