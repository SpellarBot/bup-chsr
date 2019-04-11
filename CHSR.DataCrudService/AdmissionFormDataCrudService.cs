using CHSR.Domain;
using CHSR.Repository;

namespace CHSR.DataCrudService
{
    public class AdmissionFormDataCrudService : DataCrudService<AdmissionApplication>
    {
        public AdmissionFormDataCrudService(Repository<AdmissionApplication> repository): base(repository)
        {
        }
    }
}
