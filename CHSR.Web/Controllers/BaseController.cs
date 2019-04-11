
using CHSR.Data;
using CHSR.DataCrudService;
using CHSR.Domain;
using Microsoft.AspNetCore.Mvc;

namespace CHSR.Web.Controllers
{
    public class BaseController<TEntity> : Controller where TEntity : Entity
    {
        protected DataCrudService<TEntity> dataCrudService;
        protected ApplicationDbContext context;

        public BaseController(DataCrudService<TEntity> dataCrudService)
        {
            this.dataCrudService = dataCrudService;
        }
    }
}