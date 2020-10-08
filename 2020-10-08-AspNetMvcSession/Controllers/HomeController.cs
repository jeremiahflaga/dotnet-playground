using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AspNetMvcSession.Models;

namespace AspNetMvcSession.Controllers
{
	public class HomeController : Controller
	{
		private const string STEP_1_SESSION_KEY = "Step1ViewModelSessionData";
		private const string STEP_2_SESSION_KEY = "Step2ViewModelSessionData";

		private readonly ILogger<HomeController> _logger;

		public HomeController(ILogger<HomeController> logger)
		{
			_logger = logger;
		}

		public IActionResult Index()
		{
			return View();
		}

		public IActionResult Step1()
		{
			var data = HttpContext.Session.Get<Step1ViewModel>(STEP_1_SESSION_KEY);
			return View(data);
		}

		[HttpPost]
		[ActionName("Step1")]
		public IActionResult Step1Post(Step1ViewModel vm)
		{
			HttpContext.Session.Set<Step1ViewModel>(STEP_1_SESSION_KEY, vm);
			return RedirectToAction("Step2");
		}

		public IActionResult Step2()
		{
			var data = HttpContext.Session.Get<Step2ViewModel>(STEP_2_SESSION_KEY);
			return View(data);
		}

		[HttpPost]
		[ActionName("Step2")]
		public IActionResult Step2Post(Step2ViewModel vm)
		{
			HttpContext.Session.Set<Step2ViewModel>(STEP_2_SESSION_KEY, vm);
			return RedirectToAction("LastStep");
		}

		public IActionResult LastStep()
		{
			var step1VM = HttpContext.Session.Get<Step1ViewModel>(STEP_1_SESSION_KEY);
			var step2VM = HttpContext.Session.Get<Step2ViewModel>(STEP_2_SESSION_KEY);
			var lastStepVM = new LastStepViewModel { Step1ViewModel = step1VM, Step2ViewModel = step2VM };
			return View(lastStepVM);
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
