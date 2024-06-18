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

    public class EfGetAddressesQuery : EfUseCase, IGetAddressesQuery
    {
        public EfGetAddressesQuery(TopShopContext context) : base(context)
        {
        }

        public int Id => 6;

        public string Name => "Get Addresses";

        public PagedResponse<AddressDTO> Execute(AddressSearchDTO search)
        {
            IQueryable<Address> query = Context.Addresses.AsQueryable();
            if (!string.IsNullOrEmpty(search.City) || !string.IsNullOrWhiteSpace(search.City))
            {
                query = query.Where(x => x.City.ToLower().Contains(search.City.ToLower()));
            }
            if (!string.IsNullOrEmpty(search.Street) || !string.IsNullOrWhiteSpace(search.Street))
            {
                query = query.Where(x => x.Street.ToLower().Contains(search.Street.ToLower()));
            }
            if (!string.IsNullOrEmpty(search.PostalCode) || !string.IsNullOrWhiteSpace(search.PostalCode))
            {
                query = query.Where(x => x.PostalCode.ToLower().Contains(search.PostalCode.ToLower()));
            }
            if (!string.IsNullOrEmpty(search.Country) || !string.IsNullOrWhiteSpace(search.Country))
            {
                query = query.Where(x => x.Country.ToLower().Contains(search.Country.ToLower()));
            }

            query = query.Where(x => x.IsActive);

            int skipCount = search.PerPage * (search.Page - 1);

            PagedResponse<AddressDTO> response = new PagedResponse<AddressDTO>
            {
                CurrentPage = search.Page,
                ItemsPerPage = search.PerPage,
                TotalCount = query.Count(),
                Items = query.Skip(skipCount).Take(search.PerPage)
                .Select(x => new AddressDTO
                {
                    State= x.State,
                    City= x.City,
                    PostalCode= x.PostalCode,
                    Country= x.Country,
                    Street= x.Street,
                }).ToList(),
            };

            return response;
        }
    }
}
