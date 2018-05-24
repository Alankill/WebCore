using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.DTO.Tasks;
using Application.IAppService;
using Core.Logger;
using log4net;
using Microsoft.AspNetCore.Mvc;
using WebSite.ViewModel.Tasks;

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


        public async Task<IActionResult> Index(TaskListInput input)
        {
            var tasklist =await taskService.GetAll(input);
            return View(new TaskIndexVM() {
                Tasks=tasklist
            });
        }
    }
}
