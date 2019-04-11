
using CHSR.Domain.Setup;
using CHSR.Repository;

namespace CHSR.DataCrudService
{
    public class InstituteDataCrudService : DataCrudService<Institute>
    {
        public InstituteDataCrudService(Repository<Institute> repository) : base(repository)
        {

        }
    }
}
