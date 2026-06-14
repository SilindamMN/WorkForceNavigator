using Microsoft.AspNetCore.Mvc;
using Viewing.Models;

namespace Viewing.Controllers.Dashboard
{
    public class JobTitlesController : BaseController
    {
        public JobTitlesController(IHttpClientFactory httpClientFactory)
            : base(httpClientFactory)
        {
        }

        // GET: JobTitlesController
        public async Task<IActionResult> JobTitles()
        {
            var jobTitles = await _httpClient.GetFromJsonAsync<List<JobTitle>>("api/JobTitle");
            return PartialView(jobTitles);
        }

        // GET: JobTitlesController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var jobTitle = await _httpClient.GetFromJsonAsync<JobTitle>($"api/jobtitles/{id}");
            if (jobTitle == null)
                return NotFound();

            return View(jobTitle);
        }

        // GET: JobTitlesController/Create
        public IActionResult Create()
        {
            return View();
        }

        //// POST: JobTitlesController/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create(JobTitle model)
        //{
        //    if (!ModelState.IsValid)
        //        return View(model);

        //    var response = await _httpClient.PostAsJsonAsync("api/jobtitles", model);

        //    if (response.IsSuccessStatusCode)
        //        return RedirectToAction(nameof(Index));

        //    ModelState.AddModelError("", "Unable to create job title.");
        //    return View(model);
        //}

        // GET: JobTitlesController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var jobTitle = await _httpClient.GetFromJsonAsync<JobTitle>($"api/jobtitles/{id}");
            if (jobTitle == null)
                return NotFound();

            return View(jobTitle);
        }

        // POST: JobTitlesController/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, JobTitle model)
        //{
        //    if (id != model.Id)
        //        return BadRequest();

        //    var response = await _httpClient.PutAsJsonAsync($"api/jobtitles/{id}", model);

        //    if (response.IsSuccessStatusCode)
        //        return RedirectToAction(nameof(Index));

        //    ModelState.AddModelError("", "Unable to update job title.");
        //    return View(model);
        //}

        // GET: JobTitlesController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var jobTitle = await _httpClient.GetFromJsonAsync<JobTitle>($"api/jobtitles/{id}");
            if (jobTitle == null)
                return NotFound();

            return View(jobTitle);
        }

        // POST: JobTitlesController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/jobtitles/{id}");

            if (response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            ModelState.AddModelError("", "Unable to delete job title.");
            return View();
        }
    }
}