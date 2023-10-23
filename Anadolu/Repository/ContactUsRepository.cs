using Anadolu.Models;
using Anadolu.Repository.Base;

namespace Anadolu.Repository
{
    public class ContactUsRepository : Repository<ContactUs>,IContactUsRepository
    {
        private readonly Context Context;
        public ContactUsRepository(Context _Context) : base(_Context)
        {
            Context = _Context;
        }
    }
   
}
