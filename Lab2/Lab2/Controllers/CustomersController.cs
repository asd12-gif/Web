using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Lab2.Models;
using Lab2.Data; // Đảm bảo có using này nếu DbContext của bạn nằm trong thư mục Data

namespace Lab2.Controllers
{
    public class CustomersController : Controller
    {
        private readonly MvcMovieContext _context;

        public CustomersController(MvcMovieContext context)
        {
            _context = context;
        }

        // GET: Customers
        public async Task<IActionResult> Index(string searchString)
        {
            // Bắt đầu truy vấn
            var customers = from c in _context.Customer
                            select c;

            // Xử lý tìm kiếm
            if (!string.IsNullOrEmpty(searchString))
            {
                ViewData["CurrentFilter"] = searchString;

                // Lọc Customer theo FullName HOẶC Email 
                customers = customers.Where(c =>
                    c.FullName.ToUpper().Contains(searchString.ToUpper()) ||
                    c.Email.ToUpper().Contains(searchString.ToUpper())
                );
            }

            // Hiển thị thông báo lỗi/thành công từ TempData (Nếu có)
            ViewBag.ErrorMessage = TempData["ErrorMessage"];
            ViewBag.SuccessMessage = TempData["SuccessMessage"];

            return View(await customers.ToListAsync());
        }

        // GET: Customers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customer
                .FirstOrDefaultAsync(m => m.Id == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // GET: Customers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FullName,Email,PhoneNumber")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(customer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }

        // GET: Customers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customer.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }

        // POST: Customers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FullName,Email,PhoneNumber")] Customer customer)
        {
            if (id != customer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(customer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerExists(customer.Id))
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
            return View(customer);
        }

        // GET: Customers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // THÊM INCLUDE: Tải Tickets liên quan để có thể kiểm tra trong View Delete (nếu cần)
            var customer = await _context.Customer
                .Include(c => c.Tickets)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            // TẢI CUSTOMER VÀ DANH SÁCH TICKETS LIÊN QUAN
            var customer = await _context.Customer
                .Include(c => c.Tickets)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (customer == null)
            {
                TempData["ErrorMessage"] = "Không tìm thấy khách hàng này.";
                return RedirectToAction(nameof(Index));
            }

            // RÀNG BUỘC LOGIC: KIỂM TRA NẾU CÓ BẤT KỲ TICKET NÀO LIÊN QUAN
            if (customer.Tickets != null && customer.Tickets.Any())
            {
                // Truyền thông báo lỗi về trang Index
                TempData["ErrorMessage"] = $"Không thể xóa Khách hàng '{customer.FullName}' vì vẫn còn {customer.Tickets.Count} Ticket liên quan. Vui lòng xóa các Ticket đó trước.";
                return RedirectToAction(nameof(Index));
            }

            // Nếu không có Ticket nào, tiến hành xóa
            _context.Customer.Remove(customer);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = $"Khách hàng '{customer.FullName}' đã được xóa thành công.";
            return RedirectToAction(nameof(Index));
        }

        private bool CustomerExists(int id)
        {
            return _context.Customer.Any(e => e.Id == id);
        }
    }
}