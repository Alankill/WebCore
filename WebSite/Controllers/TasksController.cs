using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.IAppService;
using Core.Logger;
using log4net;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebSite.Controllers
{
    public class TasksController : Controller
    {
        ILog logger;
        ITaskAppService taskService;
        public TasksController(ITaskAppService taskappservice)
        {
            logger = LoggerManager.GetLogger(this.GetType());
            taskService = taskappservice;
        }


        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Add([Bind("Account,Password,UserName,Phone")]Common.DTO.UserDTO["属性验证"] user)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        userService.Add(MapperManager.Mapper.Map<User>(user));//应在service层到repository层中转换 
        //        if (!userService.SaveAllChange())
        //        {
        //            ModelState.AddModelError("", userService.ErrorInfo.Message);
        //            logger.Error($"新增用户错误:{userService.ErrorInfo.Message}");
        //        }
        //        else
        //        {
        //            logger.Info($"新增用户:{user.Account}");
        //            return RedirectToAction("Index");
        //        }
        //    }
        //    user.Title = "新建";
        //    return View(user);
        //}
    }
}
