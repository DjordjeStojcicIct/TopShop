using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopShop.Domain.Entities;

namespace TopShop.DataAccess
{
    public static class InitialData
    {
        public static RoleUseCase[] getRoleUseCases()
        {
            return new RoleUseCase[]
            {
                new RoleUseCase{RoleId= 1,UseCaseId = 1, Id = 1},
                new RoleUseCase{RoleId= 1,UseCaseId = 2 , Id = 2},
                new RoleUseCase{RoleId= 1,UseCaseId = 3, Id = 3},
                new RoleUseCase{RoleId= 1,UseCaseId = 4, Id = 4},
                new RoleUseCase{RoleId= 1,UseCaseId = 5, Id = 5},
                new RoleUseCase{RoleId= 1,UseCaseId = 6, Id = 6},
                new RoleUseCase{RoleId= 1,UseCaseId = 7, Id = 7},
                new RoleUseCase{RoleId= 1,UseCaseId = 8, Id = 8},
                new RoleUseCase{RoleId= 1,UseCaseId = 9, Id = 9},
                new RoleUseCase{RoleId= 1,UseCaseId = 10, Id = 10},
                new RoleUseCase{RoleId= 1,UseCaseId = 11, Id = 11},
                new RoleUseCase{RoleId= 1,UseCaseId = 12, Id = 12},
                new RoleUseCase{RoleId= 1,UseCaseId = 13, Id = 13},
                new RoleUseCase{RoleId= 1,UseCaseId = 14, Id = 14},
                new RoleUseCase{RoleId= 1,UseCaseId = 15, Id = 15},
                new RoleUseCase{RoleId= 1,UseCaseId = 16, Id = 16},
                new RoleUseCase{RoleId= 1,UseCaseId = 17, Id = 17},
                new RoleUseCase{RoleId= 1,UseCaseId = 18, Id = 18},
                new RoleUseCase{RoleId= 1,UseCaseId = 19, Id = 19},
                new RoleUseCase{RoleId= 1,UseCaseId = 20, Id = 20},
                new RoleUseCase{RoleId= 1,UseCaseId = 21, Id = 21},
                new RoleUseCase{RoleId= 1,UseCaseId = 22, Id = 22},
                new RoleUseCase{RoleId= 1,UseCaseId = 23, Id = 23},
                new RoleUseCase{RoleId= 2,UseCaseId = 2, Id = 24},
                new RoleUseCase{RoleId= 2,UseCaseId = 4, Id = 25},
                new RoleUseCase{RoleId= 2,UseCaseId = 5, Id = 26},
                new RoleUseCase{RoleId= 2,UseCaseId = 8, Id = 27},
                new RoleUseCase{RoleId= 2,UseCaseId = 10, Id = 28},
                new RoleUseCase{RoleId= 2,UseCaseId = 13, Id = 29},
                new RoleUseCase{RoleId= 2,UseCaseId = 17, Id = 30},
                new RoleUseCase{RoleId= 2,UseCaseId = 18, Id = 31},
                new RoleUseCase{RoleId= 2,UseCaseId = 21, Id = 32},
                new RoleUseCase{RoleId= 2,UseCaseId = 23, Id = 33},
                new RoleUseCase{RoleId= 1,UseCaseId = 24, Id = 34},
                new RoleUseCase{RoleId= 1,UseCaseId = 25, Id = 35},
                new RoleUseCase{RoleId= 1,UseCaseId = 26, Id = 36},
                new RoleUseCase{RoleId= 1,UseCaseId = 27, Id = 37},
            };
        }

        public static Category[] getCategories()
        {
            return new Category[]
            {
                new Category { Id = 1, Name = "Electronics", Description = "Electronics encompass a wide range of products that are designed to make life easier, more efficient, and more entertaining." },
                new Category { Id = 2, Name = "Home & Kitchen", Description = "The Home & Kitchen category includes a variety of products that enhance the comfort, functionality, and aesthetics of living spaces." },
                new Category { Id = 3, Name = "Health & Personal Care", Description = "Health & Personal Care products are designed to support wellness, hygiene, and overall well-being." },
            };
        }
        public static Address[] GetAddresses()
        {
            return new Address[]
            {
                new Address
                {
                    Id = 1,
                    State = "",
                    City = "Belgrade",
                    Country = "Serbia",
                    Street = "bb",
                    PostalCode = "11213"
                },
                new Address
                {
                    Id = 2,
                    State = "",
                    City = "Belgrade",
                    Country = "Serbia",
                    Street = "bb",
                    PostalCode = "11213"
                }
            };
        }
        public static User[] getUsers()
        {
            return new User[]
            {
                new User
                {
                    Id = 1,
                    FirstName = "Djordje",
                    LastName = "Stojcic",
                    Email = "djordje.stojcic.173.16@ict.edu.rs",
                    Username = "Djole",
                    AddressId = 1,
                    RoleId = 1,
                    Password = "test"
                },
                new User
                {
                    Id = 2,
                    FirstName = "Aleksa",
                    LastName = "Stojcic",
                    Email = "aleksa.stojcic.173.16@ict.edu.rs",
                    Username = "Lale",
                    AddressId = 2,
                    RoleId = 2,
                    Password = "test"
                }
            };
        } 
    }
}
