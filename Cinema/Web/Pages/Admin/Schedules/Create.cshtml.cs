using Data.Entities;
using Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.Interfaces;

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
        [BindProperty]
        public string Is3DSelection { get; set; } // Thuộc tính trung gian cho dropdown Is3D

        [BindProperty]
        public string IsSubtitleSelection { get; set; } 
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
            Schedule.Is3D = Is3DSelection switch
            {
                "true" => true,
                "false" => false,
                _ => null // "" hoặc bất kỳ giá trị nào khác sẽ là null
            };

            Schedule.IsSubtitle = IsSubtitleSelection switch
            {
                "true" => true,
                "false" => false,
                _ => null
            };

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
            ModelState.Remove("Schedule.Movie");
            ModelState.Remove("Schedule.Room");
            if (!ModelState.IsValid)
            {
                foreach (var state in ModelState)
                {
                    foreach (var error in state.Value.Errors)
                    {
                        System.Diagnostics.Debug.WriteLine($"Key: {state.Key}, Error: {error.ErrorMessage}");
                    }
                }
                return Page();
            }
            await _schedulesService.AddAsync(Schedule);
            return RedirectToPage("./Index");
        }
    }
}
