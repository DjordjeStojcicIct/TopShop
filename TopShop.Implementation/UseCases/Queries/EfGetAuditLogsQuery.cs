using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopShop.Application.UseCases.DTO.QueryDTO;
using TopShop.Application.UseCases.DTO.SearchDTO;
using TopShop.Application.UseCases.Queries;
using TopShop.DataAccess;
using TopShop.Domain.Entities;

namespace TopShop.Implementation.UseCases.Queries
{

    public class EfGetAuditLogsQuery : EfUseCase, IGetAuditLogsQuery
    {
        public EfGetAuditLogsQuery(TopShopContext context) : base(context)
        {
        }

        public int Id => 7;

        public string Name => "Get Logs";

        public PagedResponse<AuditLogDTO> Execute(AuditLogSearchDTO search)
        {
            IQueryable<AuditLog> query = Context.AuditLogs.AsQueryable();
            if (search.UserId != null) 
            {
                query = query.Where(x => x.UserId == search.UserId);
            }
            if (!string.IsNullOrEmpty(search.UseCaseName) || !string.IsNullOrWhiteSpace(search.UseCaseName))
            {
                query = query.Where(x => x.UseCaseName.ToLower().Contains(search.UseCaseName.ToLower()));
            }
            if (!string.IsNullOrEmpty(search.UseCaseData) || !string.IsNullOrWhiteSpace(search.UseCaseData))
            {
                query = query.Where(x => x.UseCaseData.ToLower().Contains(search.UseCaseData.ToLower()));
            }

            query = query.Where(x => x.IsActive);


            int skipCount = search.PerPage * (search.Page - 1);

            PagedResponse<AuditLogDTO> response = new PagedResponse<AuditLogDTO>
            {
                CurrentPage = search.Page,
                ItemsPerPage = search.PerPage,
                TotalCount = query.Count(),
                Items = query.Skip(skipCount).Take(search.PerPage)
                .Select(x => new AuditLogDTO
                {
                    Username = x.User.Username,
                    UseCaseName = x.UseCaseName,
                    UseCaseData = x.UseCaseData,
                    CreatedAt = x.CreatedAt,
                }).ToList(),
            };

            return response;
        }
    }
}
