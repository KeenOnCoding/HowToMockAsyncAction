using HowToMockAsyncAction.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HowToMockAsyncAction.Infrastructure
{
    public interface IPersonRepository
    {
        IQueryable<Person> Persons { get; }

        Task SaveProduct(Person product);
    }
}
