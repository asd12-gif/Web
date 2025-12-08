using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Lab2.Models;
using Lab2.Data; // Đảm bảo có using này để truy cập DbContext

namespace Lab2.Controllers
{
    public class TicketsController : Controller
    {
        // Lưu ý: Đảm bảo MvcMovieContext là tên chính xác của DbContext của bạn.
        private readonly MvcMovieContext _context;

        public TicketsController(MvcMovieContext context)
        {
            _context = context;
        }

        // GET: Tickets - Hiển thị danh sách, tìm kiếm và lọc
        public async Task<IActionResult> Index(string searchString, TicketStatus? ticketStatus)
        {
            // Bắt đầu truy vấn, Include Customer và Movie để hiển thị tên
            var tickets = _context.Ticket
                .Include(t => t.Customer)
                .Include(t => t.Movie)
                .AsQueryable();

            // 1. Lọc theo Trạng thái (Status)
            if (ticketStatus.HasValue)
            {
                tickets = tickets.Where(t => t.Status == ticketStatus.Value);
                ViewData["CurrentStatus"] = ticketStatus.Value;
            }

            // 2. Tìm kiếm theo Tiêu đề (Title)
            if (!string.IsNullOrEmpty(searchString))
            {
                ViewData["CurrentFilter"] = searchString;

                tickets = tickets.Where(t =>
                    t.Title.ToUpper().Contains(searchString.ToUpper())
                );
            }

            // Tạo SelectList cho Dropdown trạng thái
            ViewBag.StatusList = new SelectList(
                Enum.GetValues(typeof(TicketStatus)).Cast<TicketStatus>().ToList()
            );

            return View(await tickets.ToListAsync());
        }

        // GET: Tickets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = await _context.Ticket
                .Include(t => t.Customer)
                .Include(t => t.Movie)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (ticket == null)
            {
                return NotFound();
            }

            return View(ticket);
        }

        // GET: Tickets/Create
        public IActionResult Create()
        {
            // Truyền SelectList cho các Khóa ngoại (Movie và Customer)
            ViewData["CustomerId"] = new SelectList(_context.Customer, "Id", "FullName");
            ViewData["MovieId"] = new SelectList(_context.Movie, "Id", "Title");
            return View();
        }

        // POST: Tickets/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        // [Bind] đã được cập nhật để bao gồm MovieId
        public async Task<IActionResult> Create([Bind("Id,Title,Description,CreatedDate,Status,CustomerId,MovieId")] Ticket ticket)
        {
            // Thiết lập CreatedDate bằng DateTime.Now trước khi lưu
            ticket.CreatedDate = DateTime.Now;

            if (ModelState.IsValid)
            {
                _context.Add(ticket);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // Nếu ModelState không hợp lệ, tải lại SelectList
            ViewData["CustomerId"] = new SelectList(_context.Customer, "Id", "FullName", ticket.CustomerId);
            ViewData["MovieId"] = new SelectList(_context.Movie, "Id", "Title", ticket.MovieId);
            return View(ticket);
        }

        // GET: Tickets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = await _context.Ticket.FindAsync(id);
            if (ticket == null)
            {
                return NotFound();
            }
            // Truyền SelectList cho các Khóa ngoại (Movie và Customer)
            ViewData["CustomerId"] = new SelectList(_context.Customer, "Id", "FullName", ticket.CustomerId);
            ViewData["MovieId"] = new SelectList(_context.Movie, "Id", "Title", ticket.MovieId);
            return View(ticket);
        }

        // POST: Tickets/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        // [Bind] đã được cập nhật để bao gồm MovieId
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,CreatedDate,Status,CustomerId,MovieId")] Ticket ticket)
        {
            if (id != ticket.Id)
            {
                return NotFound();
            }

            // Gỡ bỏ CreatedDate khỏi ModelState để ngăn việc ghi đè từ Form (chỉ giữ lại giá trị cũ)
            ModelState.Remove("CreatedDate");

            if (ModelState.IsValid)
            {
                try
                {
                    // Lấy bản ghi cũ (chỉ để lấy CreatedDate)
                    var ticketToUpdate = await _context.Ticket.AsNoTracking().FirstOrDefaultAsync(t => t.Id == id);
                    if (ticketToUpdate == null) return NotFound();

                    // Gán CreatedDate cũ vào object mới trước khi Update
                    ticket.CreatedDate = ticketToUpdate.CreatedDate;

                    _context.Update(ticket);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TicketExists(ticket.Id)) // KHẮC PHỤC LỖI THIẾU HÀM TicketExists
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            // Nếu ModelState không hợp lệ, tải lại SelectList
            ViewData["CustomerId"] = new SelectList(_context.Customer, "Id", "FullName", ticket.CustomerId);
            ViewData["MovieId"] = new SelectList(_context.Movie, "Id", "Title", ticket.MovieId);
            return View(ticket);
        }

        // GET: Tickets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = await _context.Ticket
                .Include(t => t.Customer)
                .Include(t => t.Movie)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (ticket == null)
            {
                return NotFound();
            }

            return View(ticket);
        }

        // POST: Tickets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ticket = await _context.Ticket.FindAsync(id);

            if (ticket == null)
            {
                // Xử lý trường hợp không tìm thấy ticket (đã xóa bởi người khác)
                TempData["ErrorMessage"] = "Ticket này có vẻ đã được xóa trước đó.";
                return RedirectToAction(nameof(Index));
            }

            // RÀNG BUỘC XÓA: KHÔNG CHO PHÉP XÓA KHI ĐANG XỬ LÝ
            if (ticket.Status == TicketStatus.InProgress)
            {
                // Sử dụng TempData để gửi thông báo lỗi về trang Index
                TempData["ErrorMessage"] = "Không thể xóa Ticket khi đang ở trạng thái 'Đang xử lý'. Vui lòng chuyển trạng thái trước khi xóa.";
                return RedirectToAction(nameof(Index));
            }

            _context.Ticket.Remove(ticket);
            await _context.SaveChangesAsync();

            // Thông báo xóa thành công
            TempData["SuccessMessage"] = $"Ticket '{ticket.Title}' đã được xóa thành công.";

            return RedirectToAction(nameof(Index));
        }

        // KHẮC PHỤC LỖI: THÊM HÀM PRIVATE HELPER TicketExists
        private bool TicketExists(int id)
        {
            return _context.Ticket.Any(e => e.Id == id);
        }
    }
}