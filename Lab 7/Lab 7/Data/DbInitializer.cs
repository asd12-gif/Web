using Lab_7.Models;
using System;
using System.Linq;

namespace Lab_7.Data
{
    public static class DbInitializer
    {
        public static void Initialize(SchoolContext context)
        {
            // 0. Khởi tạo Database
            context.Database.EnsureCreated();

            // Kiểm tra xem đã có dữ liệu chưa (tránh nạp chồng)
            if (context.Students.Any())
            {
                return; // Đã có dữ liệu, không cần chạy tiếp
            }

            // 1. NẠP SINH VIÊN (STUDENTS)
            var students = new Student[]
            {
                new Student{FirstMidName="Carson",LastName="Alexander",EnrollmentDate=DateTime.Parse("2022-09-01")},
                new Student{FirstMidName="Meredith",LastName="Alonso",EnrollmentDate=DateTime.Parse("2022-09-01")},
                new Student{FirstMidName="Arturo",LastName="Anand",EnrollmentDate=DateTime.Parse("2023-09-01")},
                new Student{FirstMidName="Gytis",LastName="Barzdukas",EnrollmentDate=DateTime.Parse("2022-09-01")},
                new Student{FirstMidName="Yan",LastName="Li",EnrollmentDate=DateTime.Parse("2022-09-01")},
                new Student{FirstMidName="Peggy",LastName="Justice",EnrollmentDate=DateTime.Parse("2021-09-01")},
                new Student{FirstMidName="Laura",LastName="Norman",EnrollmentDate=DateTime.Parse("2023-09-01")},
                new Student{FirstMidName="Nino",LastName="Olivetto",EnrollmentDate=DateTime.Parse("2022-09-01")}
            };
            context.Students.AddRange(students);
            context.SaveChanges();

            // 2. NẠP GIẢNG VIÊN (INSTRUCTORS)
            var instructors = new Instructor[]
            {
                new Instructor { FirstMidName = "Kim",     LastName = "Abercrombie", HireDate = DateTime.Parse("1995-03-11") },
                new Instructor { FirstMidName = "Fadi",    LastName = "Fakhouri",    HireDate = DateTime.Parse("2002-07-06") },
                new Instructor { FirstMidName = "Roger",   LastName = "Zheng",       HireDate = DateTime.Parse("2004-02-12") },
                new Instructor { FirstMidName = "Candace", LastName = "Kapoor",      HireDate = DateTime.Parse("2001-01-15") },
                new Instructor { FirstMidName = "Roger",   LastName = "Harui",       HireDate = DateTime.Parse("1998-07-01") }
            };
            context.Instructors.AddRange(instructors);
            context.SaveChanges();

            // 3. NẠP KHOA (DEPARTMENTS)
            var departments = new Department[]
            {
                new Department { Name = "English",     Budget = 350000, StartDate = DateTime.Parse("2007-09-01"), InstructorID = instructors.Single(i => i.LastName == "Abercrombie").ID },
                new Department { Name = "Mathematics", Budget = 100000, StartDate = DateTime.Parse("2007-09-01"), InstructorID = instructors.Single(i => i.LastName == "Fakhouri").ID },
                new Department { Name = "Engineering", Budget = 350000, StartDate = DateTime.Parse("2007-09-01"), InstructorID = instructors.Single(i => i.LastName == "Harui").ID },
                new Department { Name = "Economics",   Budget = 100000, StartDate = DateTime.Parse("2007-09-01"), InstructorID = instructors.Single(i => i.LastName == "Zheng").ID }
            };
            context.Departments.AddRange(departments);
            context.SaveChanges();

            // 4. NẠP KHÓA HỌC (COURSES)
            var courses = new Course[]
            {
                new Course { CourseID = 1050, Title = "Chemistry",      Credits = 3, DepartmentID = departments.Single(s => s.Name == "Engineering").DepartmentID },
                new Course { CourseID = 4022, Title = "Microeconomics", Credits = 3, DepartmentID = departments.Single(s => s.Name == "Economics").DepartmentID },
                new Course { CourseID = 4041, Title = "Macroeconomics", Credits = 3, DepartmentID = departments.Single(s => s.Name == "Economics").DepartmentID },
                new Course { CourseID = 1045, Title = "Calculus",       Credits = 4, DepartmentID = departments.Single(s => s.Name == "Mathematics").DepartmentID },
                new Course { CourseID = 3141, Title = "Trigonometry",   Credits = 4, DepartmentID = departments.Single(s => s.Name == "Mathematics").DepartmentID },
                new Course { CourseID = 2021, Title = "Composition",    Credits = 3, DepartmentID = departments.Single(s => s.Name == "English").DepartmentID },
                new Course { CourseID = 2042, Title = "Literature",     Credits = 4, DepartmentID = departments.Single(s => s.Name == "English").DepartmentID }
            };
            context.Courses.AddRange(courses);
            context.SaveChanges();

            // 5. NẠP VĂN PHÒNG (OFFICE ASSIGNMENTS)
            var officeAssignments = new OfficeAssignment[]
            {
                new OfficeAssignment { InstructorID = instructors.Single(i => i.LastName == "Fakhouri").ID, Location = "Smith 17" },
                new OfficeAssignment { InstructorID = instructors.Single(i => i.LastName == "Harui").ID,    Location = "Gowan 27" },
                new OfficeAssignment { InstructorID = instructors.Single(i => i.LastName == "Zheng").ID,    Location = "Thompson 304" }
            };
            context.OfficeAssignments.AddRange(officeAssignments);
            context.SaveChanges();

            // 6. PHÂN CÔNG GIẢNG DẠY (COURSE ASSIGNMENTS)
            var courseInstructors = new CourseAssignment[]
            {
                new CourseAssignment { CourseID = 1050, InstructorID = instructors.Single(i => i.LastName == "Kapoor").ID },
                new CourseAssignment { CourseID = 1050, InstructorID = instructors.Single(i => i.LastName == "Harui").ID },
                new CourseAssignment { CourseID = 4022, InstructorID = instructors.Single(i => i.LastName == "Zheng").ID },
                new CourseAssignment { CourseID = 4041, InstructorID = instructors.Single(i => i.LastName == "Zheng").ID },
                new CourseAssignment { CourseID = 1045, InstructorID = instructors.Single(i => i.LastName == "Fakhouri").ID },
            };
            context.CourseAssignments.AddRange(courseInstructors);
            context.SaveChanges();

            // 7. ĐĂNG KÝ HỌC (ENROLLMENTS - Kết nối Student với Course)
            var enrollments = new Enrollment[]
            {
                new Enrollment { StudentID = students.Single(s => s.LastName == "Alexander").ID, CourseID = 1050, Grade = Grade.A },
                new Enrollment { StudentID = students.Single(s => s.LastName == "Alexander").ID, CourseID = 4022, Grade = Grade.C },
                new Enrollment { StudentID = students.Single(s => s.LastName == "Alonso").ID,    CourseID = 1045, Grade = Grade.B },
                new Enrollment { StudentID = students.Single(s => s.LastName == "Anand").ID,     CourseID = 1050, Grade = Grade.B },
                new Enrollment { StudentID = students.Single(s => s.LastName == "Barzdukas").ID, CourseID = 4022, Grade = Grade.F }
            };
            context.Enrollments.AddRange(enrollments);
            context.SaveChanges();
        }
    }
}