using HowToMockAsyncAction.Infrastructure;
using HowToMockAsyncAction.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HowToMockAsyncAction.Model
{
    public class EFPersonsRepository: IPersonRepository
    {
        private HowToMockAsyncActionContext _context;
        public EFPersonsRepository(HowToMockAsyncActionContext ctx)
        {
            this._context = ctx;
        }

        public IQueryable<Person> Persons =>_context.Person;

        public async Task SaveProduct(Person product)
        {
            if (product.Id == 0)
                _context.Person.Add(product);
            else
            {
                Person dbEntry = _context.Person
                    .FirstOrDefault(p => p.Id == product.Id);

                if (dbEntry != null)
                    dbEntry.Name = product.Name;
            }
           await _context.SaveChangesAsync();
        }
    }
}
