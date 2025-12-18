using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Lab_7.Data;
using Lab_7.Models;

namespace Lab_7.Controllers
{
    public class InstructorsController : Controller
    {
        private readonly SchoolContext _context;

        public InstructorsController(SchoolContext context)
        {
            _context = context;
        }

        // GET: Instructors
        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;

            // Reset trang về 1 nếu có từ khóa tìm kiếm mới
            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewData["CurrentFilter"] = searchString;

            var instructors = from i in _context.Instructors select i;

            // Lọc theo tên
            if (!String.IsNullOrEmpty(searchString))
            {
                instructors = instructors.Where(i => i.LastName.Contains(searchString) || i.FirstMidName.Contains(searchString));
            }

            // Sắp xếp
            switch (sortOrder)
            {
                case "name_desc": instructors = instructors.OrderByDescending(i => i.LastName); break;
                case "date_asc": instructors = instructors.OrderBy(i => i.HireDate); break;
                case "date_desc": instructors = instructors.OrderByDescending(i => i.HireDate); break;
                default: instructors = instructors.OrderBy(i => i.LastName); break;
            }

            int pageSize = 3;
            return View(await PaginatedList<Instructor>.CreateAsync(instructors.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        // GET: Instructors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var instructor = await _context.Instructors
                .Include(i => i.CourseAssignments)      
                    .ThenInclude(ca => ca.Course)         
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);

            if (instructor == null)
            {
                return NotFound();
            }

            return View(instructor);
        }

        // GET: Instructors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Instructors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,LastName,FirstMidName,HireDate")] Instructor instructor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(instructor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(instructor);
        }

        // GET: Instructors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var instructor = await _context.Instructors.FindAsync(id);
            if (instructor == null)
            {
                return NotFound();
            }
            return View(instructor);
        }

        // POST: Instructors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,LastName,FirstMidName,HireDate")] Instructor instructor)
        {
            if (id != instructor.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(instructor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InstructorExists(instructor.ID))
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
            return View(instructor);
        }

        // GET: Instructors/Delete/5
        public async Task<IActionResult> Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null) return NotFound();

            // Load Instructor kèm theo dữ liệu Trưởng khoa và Lịch dạy
            var instructor = await _context.Instructors
                .Include(i => i.CourseAssignments) // Lịch dạy
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);

            if (instructor == null) return NotFound();

            // Kiểm tra xem giảng viên có đang là Trưởng khoa (Administrator) của phòng ban nào không
            var isAdministrator = await _context.Departments
                .AnyAsync(d => d.InstructorID == id);

            bool hasAssignments = instructor.CourseAssignments.Any();

            // Thiết lập thông báo lỗi nếu vi phạm ràng buộc
            if (isAdministrator && hasAssignments)
            {
                ViewData["ErrorMessage"] = "Không thể xóa: Giảng viên này vừa là Trưởng khoa, vừa có lịch dạy.";
            }
            else if (isAdministrator)
            {
                ViewData["ErrorMessage"] = "Không thể xóa: Giảng viên này hiện đang là Trưởng khoa của một phòng ban.";
            }
            else if (hasAssignments)
            {
                ViewData["ErrorMessage"] = $"Không thể xóa: Giảng viên này hiện đang có {instructor.CourseAssignments.Count} lịch dạy.";
            }

            if (saveChangesError.GetValueOrDefault())
            {
                ViewData["ErrorMessage"] = "Xóa thất bại do lỗi hệ thống. Vui lòng thử lại.";
            }

            // Truyền trạng thái sang View để xử lý giao diện
            ViewBag.CanDelete = !isAdministrator && !hasAssignments;

            return View(instructor);
        }

        // POST: Instructors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var instructor = await _context.Instructors
                .Include(i => i.CourseAssignments)
                .FirstOrDefaultAsync(m => m.ID == id);

            var isAdministrator = await _context.Departments.AnyAsync(d => d.InstructorID == id);

            if (instructor != null && !isAdministrator && !instructor.CourseAssignments.Any())
            {
                try
                {
                    _context.Instructors.Remove(instructor);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException)
                {
                    return RedirectToAction(nameof(Delete), new { id = id, saveChangesError = true });
                }
            }

            // Nếu vi phạm, quay lại trang xóa kèm báo lỗi
            return RedirectToAction(nameof(Delete), new { id = id });
        }

        private bool InstructorExists(int id)
        {
            return _context.Instructors.Any(e => e.ID == id);
        }
    }
}
