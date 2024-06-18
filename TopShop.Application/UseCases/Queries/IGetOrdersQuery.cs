using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopShop.Application.UseCases.DTO.QueryDTO;
using TopShop.Application.UseCases.DTO.SearchDTO;

namespace TopShop.Application.UseCases.Queries
{
 
    public interface IGetOrdersQuery : IQuery<OrderSearchDTO, PagedResponse<OrderDTO>>
    {
    }

    public interface IFindOrderQuery : IQuery<int, OrderDTO> { }

}
