using Greenhost.Areas.Manage.ViewModels.Member;
using Greenhost.DAL;
using Greenhost.Models;
using Greenhost.Utils.Extentions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace Greenhost.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class MemberController : Controller
    {
        AppDbContext _context;
        public MemberController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<Member> members = _context.members.ToList();
            return View(members);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Member member)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (!member.formFile.ContentType.Contains("image"))
            {
                ModelState.AddModelError("image", "duzgun sekil daxil edin");
            }
            if (member.formFile.Length > 2039445)
            {
                ModelState.AddModelError("file", "file 2mbdan boyuk ola bilmez");
            }


            string filename = member.formFile.FileName;
            string path = "C:\\Users\\I Novbe\\source\\repos\\Greenhost\\Greenhost\\wwwroot\\images\\";
            using (FileStream stream = new FileStream(path + filename, FileMode.Create))
            {
                member.formFile.CopyTo(stream);
            }
            member.ImgUrl = filename;
            await _context.AddAsync(member);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(int id)
        {
            var member = await _context.members.FirstOrDefaultAsync(x => x.Id == id);
            _context.members.Remove(member);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");

        }
        [HttpGet]
        public IActionResult Update(int id)
        {
            var member = _context.members.FirstOrDefault(x => x.Id == id);
            if (member == null) {
                return BadRequest();
            }
            MemberVm memberVm = new MemberVm()
            {
                Fullname = member.Fullname,
                Designation = member.Designation,
                ImgUrl = member.ImgUrl,
                formFile = member.formFile,

            };
            return View(memberVm);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int id,Member member)
        {
            if (id == null) { 

                return BadRequest();
            }

            var dbMember = await _context.members.FirstOrDefaultAsync(x => x.Id == id);
            if (dbMember == null) {
                return NotFound();
            }

            if (!ModelState.IsValid) {
                return View();
            }

            if (member.formFile != null) {
                if (!member.formFile.ContentType.Contains("image"))
                {
                    ModelState.AddModelError("File", "Invalid file type");
                    return View();
                }

                if(member.formFile.Length > 2039445)
                {

                }

                string filename = member.formFile.FileName;
                string path = "C:\\Users\\I Novbe\\source\\repos\\Greenhost\\Greenhost\\wwwroot\\images\\";
                using (FileStream stream = new FileStream(path + filename, FileMode.Create))
                {
                    member.formFile.CopyTo(stream);
                }

                member.ImgUrl = filename;


            }

            dbMember.Fullname = member.Fullname;
            dbMember.Designation = member.Designation;

            await _context.SaveChangesAsync();



            return RedirectToAction("Index");
        }
    }
}

