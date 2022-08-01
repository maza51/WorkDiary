using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WorkDiary.Entities.Diary;
using WorkDiary.Services.Diary;

namespace WorkDiary.Controllers.Api
{
    [Route("api/diary/days")]
    [Authorize]
    public class DayController : Controller
    {
        private IDayService _dayService;

        public DayController(IDayService dayService)
        {
            _dayService = dayService;
        }

        [HttpPut]
        public async Task CreateOrUpdate([FromBody] Day day)
        {
            if (day == null)
            {
                return;
            }

            await _dayService.CreateOrUpdateAsync(day, User.Identity.Name);
        }
    }
}

