using Application.DTOs;
using Application.Interfaces.Services;
using Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace Web.Pages.Client
{
    public class SeatSelectionModel : PageModel
    {
        private readonly ISchedulesService _scheduleService;
        private readonly ISeatService _seatService;
        private readonly IPriceService _priceService;
        private readonly IBookingService _bookingService;

        public SeatSelectionModel(
            ISchedulesService scheduleService,
            ISeatService seatService,
            IPriceService priceService,
            IBookingService bookingService)
        {
            _scheduleService = scheduleService;
            _seatService = seatService;
            _priceService = priceService;
            _bookingService = bookingService;
        }

        public Schedule Schedule { get; set; }
        public Movie Movie { get; set; }
        public Room Room { get; set; }
        public List<SeatDto> Seats { get; set; } = new List<SeatDto>();
        public List<PriceDto> SeatPrices { get; set; } = new List<PriceDto>();
        public List<ScheduleSeatTypePriceDto> SeatPriceMultipliers { get; set; } = new List<ScheduleSeatTypePriceDto>();
        public MovieBasePriceDto MovieBasePrice { get; set; } = new MovieBasePriceDto();
        public List<SeatTypeDefaultPriceDto> DefaultMultipliers { get; set; } = new List<SeatTypeDefaultPriceDto>();
        public int MaxRow { get; set; }
        public int MaxCol { get; set; }
        public string SeatsJson { get; set; }
        public string PricesJson { get; set; }
        public string PriceMultipliersJson { get; set; }
        public string MovieBasePriceJson { get; set; }
        public string DefaultMultipliersJson { get; set; }
        public string ErrorMessage { get; set; }
        public bool HasError { get; set; }
        public bool IsAuthenticated { get; set; }

        [BindProperty]
        public Bill Bill { get; set; } = new Bill();

        [BindProperty]
        public string SelectedSeatsJson { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            // Check if user is authenticated
            IsAuthenticated = User.Identity.IsAuthenticated;

            if (id == null)
            {
                HasError = true;
                ErrorMessage = "Missing schedule ID";
                return Page();
            }

            try
            {
                Schedule = await _scheduleService.GetScheduleById(id.Value);

                if (Schedule == null)
                {
                    HasError = true;
                    ErrorMessage = $"Schedule with ID {id.Value} not found";
                    return Page();
                }

                Movie = Schedule.Movie ?? new Movie();
                Room = Schedule.Room ?? new Room();

                // Lấy thông tin ghế
                var seatsResult = await _seatService.GetSeatsWithAvailabilityAsDto(Room.RoomId, Schedule.ScheduleId);
                Seats = seatsResult?.ToList() ?? new List<SeatDto>();

                // Lấy giá vé cho các loại ghế
                var pricesResult = await _priceService.GetPricesForScheduleAsDto(Schedule.ScheduleId, Movie.MovieId);
                SeatPrices = pricesResult?.ToList() ?? new List<PriceDto>();

                // Lấy hệ số giá cho các loại ghế (DTO)
                var multipliersResult = await _priceService.GetPriceMultipliersForScheduleAsDto(Schedule.ScheduleId);
                SeatPriceMultipliers = multipliersResult?.ToList() ?? new List<ScheduleSeatTypePriceDto>();

                // Lấy giá cơ bản của phim (DTO)
                MovieBasePrice = await _priceService.GetBasePriceForMovieAsDto(Movie.MovieId) ?? new MovieBasePriceDto();

                // Lấy hệ số mặc định cho các loại ghế (DTO) - Giá trị mặc định từ code
                var defaultMultipliersResult = await _priceService.GetDefaultMultipliersAsDto();
                DefaultMultipliers = defaultMultipliersResult?.ToList() ?? new List<SeatTypeDefaultPriceDto>();

                // Tìm số hàng và cột tối đa
                if (Seats.Any())
                {
                    MaxRow = await _seatService.GetMaxRowForRoom(Room.RoomId);
                    MaxCol = await _seatService.GetMaxColForRoom(Room.RoomId);
                }
                else
                {
                    // If no seats found, create sample data for testing
                    CreateSampleData();
                }

                // Chuyển đổi dữ liệu sang JSON cho JavaScript
                SeatsJson = JsonConvert.SerializeObject(Seats);
                PricesJson = JsonConvert.SerializeObject(SeatPrices);
                PriceMultipliersJson = JsonConvert.SerializeObject(SeatPriceMultipliers);
                MovieBasePriceJson = JsonConvert.SerializeObject(MovieBasePrice);
                DefaultMultipliersJson = JsonConvert.SerializeObject(DefaultMultipliers);

                return Page();
            }
            catch (Exception ex)
            {
                HasError = true;
                ErrorMessage = $"Error loading seat data: {ex.Message}";

                // Create sample data for testing
                CreateSampleData();

                // Chuyển đổi dữ liệu sang JSON cho JavaScript
                SeatsJson = JsonConvert.SerializeObject(Seats);
                PricesJson = JsonConvert.SerializeObject(SeatPrices);
                PriceMultipliersJson = JsonConvert.SerializeObject(SeatPriceMultipliers);
                MovieBasePriceJson = JsonConvert.SerializeObject(MovieBasePrice);
                DefaultMultipliersJson = JsonConvert.SerializeObject(DefaultMultipliers);

                return Page();
            }
        }

        private void CreateSampleData()
        {
            // Create sample data for testing
            MaxRow = 9;  // A-I
            MaxCol = 18; // 1-18

            Seats = new List<SeatDto>();

            // Generate seats for each row and column
            for (int row = 1; row <= MaxRow; row++)
            {
                for (int col = 1; col <= MaxCol; col++)
                {
                    // Skip some positions to create aisles
                    if ((col == 4 || col == 15) && row > 1 && row < MaxRow)
                        continue;

                    string seatType = "Standard";

                    // Make middle rows VIP
                    if (row >= 3 && row <= 6 && col >= 5 && col <= 14)
                    {
                        seatType = "VIP";
                    }

                    // Make last row for couples
                    if (row == MaxRow)
                    {
                        seatType = "Couple";
                    }

                    // Randomly mark some seats as unavailable (about 10%)
                    string seatStatus = new Random().Next(10) < 1 ? "Booked" : "Available";

                    Seats.Add(new SeatDto
                    {
                        SeatId = row * 100 + col,
                        RoomId = Room?.RoomId ?? 1,
                        RowNo = row,
                        ColNo = col,
                        SeatType = seatType,
                        SeatStatus = seatStatus
                    });
                }
            }

            // Create sample price data
            if (SeatPrices == null || !SeatPrices.Any())
            {
                SeatPrices = new List<PriceDto>
                {
                    new PriceDto { SeatType = "Standard", PriceMultiplier = 1.0m },
                    new PriceDto { SeatType = "VIP", PriceMultiplier = 2.0m },
                    new PriceDto { SeatType = "Couple", PriceMultiplier = 3.0m }
                };
            }

            // Create sample base price
            if (MovieBasePrice == null || MovieBasePrice.BasePrice <= 0)
            {
                MovieBasePrice = new MovieBasePriceDto
                {
                    MovieId = Movie?.MovieId ?? 1,
                    BasePrice = 80000
                };
            }
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            // Check if user is authenticated
            if (!User.Identity.IsAuthenticated)
            {
                // Redirect to login page with return URL
                return RedirectToPage("/Auth/Login", new { returnUrl = $"/Client/SeatSelection?id={id}" });
            }

            if (!ModelState.IsValid)
            {
                return await OnGetAsync(id);
            }

            try
            {
                // Parse selected seats
                var selectedSeatIds = JsonConvert.DeserializeObject<List<int>>(SelectedSeatsJson);

                // Create booking
                var bill = await _bookingService.CreateBooking(id, selectedSeatIds, Bill.TotalCost);

                return RedirectToPage("./BookingConfirmation", new { id = bill.BillId });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Error creating booking: {ex.Message}");
                return await OnGetAsync(id);
            }
        }
    }
}
