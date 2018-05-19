using DynamiConfig.Business.Services.Interfaces;
using DynamiConfig.Web.Business;
using DynamiConfig.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;

namespace DynamiConfig.Web.Controllers
{
    public class HomeController : Controller
    {
        public IConfigService ConfigService { get; set; }
        public IConfigurationReader ConfigReader { get; }

        public HomeController(IConfigService configService, IConfigurationReader configReader)
        {
            ConfigService = configService;
            ConfigReader = configReader;
        }

        [HttpGet]
        public IActionResult Index()
        {
            ConfigurationViewModel model = new ConfigurationViewModel();
            model.ConfigurationList = ConfigService.GetActiveConfigList();
            return View(model);
        }

        [HttpPost]
        public IActionResult Index(ConfigurationViewModel model, string submit)
        {
            switch (submit)
            {
                case "Delete":
                    model.DeleteResult = ConfigService.DeleteConfigEntity(model.DeletedObjectId);
                    break;
                case "Create":
                    model.CreateResult = ConfigService.CreateConfigEntity(model.NewConfigModel);
                    break;
                case "Update":
                    model.UpdateResult = ConfigService.UpdateConfigEntity(model.UpdateConfigModel);
                    break;
                default:
                    break;
            }
            model.ConfigurationList = ConfigService.GetActiveConfigList();
            
            return PartialView("IndexPartial", model);
        }
    }
}
