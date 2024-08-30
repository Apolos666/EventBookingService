namespace BuildingBlocks.Pagination;

public record PaginatedRequest(int PageIndex = 0, int PageSize = 10);