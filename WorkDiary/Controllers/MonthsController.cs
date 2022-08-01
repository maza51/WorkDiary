using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WorkDiary.Entities.Diary;
using WorkDiary.Services.Diary;

namespace WorkDiary.Controllers
{
    [Route("diary/months")]
    [Authorize]
    public class MonthsController : Controller
    {
        private IMonthService _monthService;

        public MonthsController(IMonthService monthService)
        {
            _monthService = monthService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                await _monthService.CreateCurrentMonthIfDoesNotExistAsync(User.Identity.Name);

                var months = await _monthService.GetAllAsync(User.Identity.Name);

                return View(months);
            }
            catch (Exception ex)
            {
                return NotFound(ex);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Month(int id)
        {
            try
            {
                var month = await _monthService.GetByIdAsync(id, User.Identity.Name);

                if (month != null)
                {
                    return View(month);
                }

                return NotFound("there is no such month in the database");
            }
            catch (Exception ex)
            {
                return NotFound(ex);
            }
        }
    }
}

