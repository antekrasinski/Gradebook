using Gradebook.Model;
using Gradebook.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Gradebook.Controllers
{
    [ApiController]
    [Route("grades")]
    public class GradesController : ControllerBase
    {
        private readonly GradesRepository _gradesRepository;

        public GradesController()
        {
            _gradesRepository = new GradesRepository();
        }

        [HttpGet]
        public IEnumerable<GradeRecord> GetGradeRecords()
        {
            var gradeRecords = _gradesRepository.GetItems();
            return gradeRecords;    
        }

        [HttpGet("{id}")]
        public ActionResult<GradeRecord> GetGradeRecord(Guid id)
        {
            var gradeRecord = _gradesRepository.GetGradeRecord(id);
            if (gradeRecord == null)
            {
                return NotFound();
            }
            return Ok(gradeRecord);
        }
/*
        // GET: GradesController
        public ActionResult Index()
        {
            return View();
        }

        // GET: GradesController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: GradesController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: GradesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: GradesController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: GradesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: GradesController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: GradesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }*/
    }
}
