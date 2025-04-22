using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Web.Pages.Admin.Schedules
{
    public class EditModel : PageModel
    {
        private readonly ISchedulesService _schedulesService;
        // Thêm IGenericRepository cho Movie và Room
        private readonly IGenericRepository<Movie> _movieRepo;
        private readonly IGenericRepository<Room> _roomRepo;
        public EditModel(ISchedulesService schedulesService, IGenericRepository<Movie> movieRepo,
    IGenericRepository<Room> roomRepo)
        {
            _schedulesService = schedulesService;
            _movieRepo = movieRepo;
            _roomRepo = roomRepo;
        }
        public List<Movie> Movies { get; set; } = new();
        public List<Room> Rooms { get; set; } = new();

       
        [BindProperty]
        public Schedule Schedule { get; set; }

        [BindProperty]
        public bool Is3DValue { get; set; } // Thuộc tính trung gian cho Is3D

        [BindProperty]
        public bool IsSubtitleValue { get; set; } // Thuộc tính trung gian cho IsSubtitle

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Schedule = await _schedulesService.GetSchedulesById(id);         
            Is3DValue = Schedule.Is3D ?? false;
            IsSubtitleValue = Schedule.IsSubtitle ?? false;
            Movies = (await _movieRepo.GetAllAsync()).ToList();
            Rooms = (await _roomRepo.GetAllAsync()).ToList();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var scheduleToUpdate = await _schedulesService.GetSchedulesById(Schedule.ScheduleId);

            if (scheduleToUpdate == null)
            {
                return NotFound();
            }

            // Cập nhật các trường
            scheduleToUpdate.StartDatetime = Schedule.StartDatetime;
            scheduleToUpdate.EndDatetime = Schedule.EndDatetime;
            scheduleToUpdate.Is3D = Is3DValue; // Sử dụng giá trị từ thuộc tính trung gian
            scheduleToUpdate.IsSubtitle = IsSubtitleValue; // Sử dụng giá trị từ thuộc tính trung gian
            scheduleToUpdate.MovieId = Schedule.MovieId;
            scheduleToUpdate.RoomId = Schedule.RoomId;

            try
            {
                await _schedulesService.UpdateAsync(scheduleToUpdate);
            }
            catch (Exception)
            {
                if (await _schedulesService.GetSchedulesById(Schedule.ScheduleId) == null)
                {
                    return NotFound();
                }
                throw;
            }

            return RedirectToPage("./Index");
        }
    }
}
