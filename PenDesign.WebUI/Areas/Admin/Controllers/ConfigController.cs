using PenDesign.Core.Interface.Service.BasicServiceInterface;
using PenDesign.Core.Model;
using PenDesign.WebUI.Authencation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PenDesign.WebUI.Areas.Admin.Controllers
{
    [CustomAuthorize(Roles = "Admin")]
    public class ConfigsController : ApiController
    {
        private IConfigService _configService;

        public ConfigsController(IConfigService configService)
        {
            this._configService = configService;
        }

        // GET: api/Configs
        public IQueryable<Config> Get()
        {
            return _configService.GetAll();
        }


        // PUT: api/Configs/5
        public HttpResponseMessage Put(int id, Config config)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }

            try
            {
                if (config.LogoUrl != null && config.LogoUrl.ToString() != "")
                {
                    if (config.LogoUrl.ToString().Contains("/Content"))
                        config.LogoUrl = config.LogoUrl;
                    else
                        config.LogoUrl = "/Content/UploadFiles/images/images/" + config.LogoUrl;
                }
                else
                    config.LogoUrl = "/Content/images/No_image_available.png";

                _configService.Update(config);

                return Request.CreateResponse(HttpStatusCode.OK);

            }
            catch (Exception)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
                throw;
            }
        }




        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

    }

}
