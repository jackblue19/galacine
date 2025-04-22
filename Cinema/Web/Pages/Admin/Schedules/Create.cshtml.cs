using Data.Entities;
using Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;

namespace Web.Pages.Admin.Schedules
{
    public class CreateModel : PageModel
    {
        private readonly ISchedulesService _schedulesService; 
        // Thêm IGenericRepository cho Movie và Room
        private readonly IGenericRepository<Movie> _movieRepo;
        private readonly IGenericRepository<Room> _roomRepo;
        public CreateModel(ISchedulesService schedulesService, IGenericRepository<Movie> movieRepo,
    IGenericRepository<Room> roomRepo)
        {
            _schedulesService = schedulesService;
            _movieRepo = movieRepo;
            _roomRepo = roomRepo;
        }

        [BindProperty]
        public Schedule Schedule { get; set; }
        public List<Movie> Movies { get; set; } = new();
        public List<Room> Rooms { get; set; } = new();
        public async Task<IActionResult> OnGetAsync()
        {
            Movies = (await _movieRepo.GetAllAsync()).ToList();
            Rooms = (await _roomRepo.GetAllAsync()).ToList();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            Movies = (await _movieRepo.GetAllAsync()).ToList();
            Rooms = (await _roomRepo.GetAllAsync()).ToList();


            // Kiểm tra thời gian kết thúc phải lớn hơn thời gian bắt đầu
            if (Schedule.EndDatetime <= Schedule.StartDatetime)
            {
                ModelState.AddModelError("Schedule.EndDatetime", "Thời gian kết thúc phải lớn hơn thời gian bắt đầu.");
            }

            // Kiểm tra lịch chiếu phải kéo dài ít nhất 1 tiếng
            if ((Schedule.EndDatetime - Schedule.StartDatetime).TotalMinutes < 60)
            {
                ModelState.AddModelError("Schedule.EndDatetime", "Lịch chiếu phải kéo dài ít nhất 1 tiếng.");
            }

            // Kiểm tra overlap
            if (await _schedulesService.HasOverlapAsync(Schedule))
            {
                ModelState.AddModelError("", "Lịch chiếu bị trùng với một lịch chiếu khác có cùng phim và phòng trong khoảng thời gian này.");
            }
            //if (!ModelState.IsValid)
            //{
            //    return Page();
            //}
            await _schedulesService.AddAsync(Schedule);
            return RedirectToPage("./Index");
        }
    }
}
