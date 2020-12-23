namespace SightsView.Web.Areas.Administration.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;
    using SightsView.Data;
    using SightsView.Data.Common.Repositories;
    using SightsView.Data.Models;

    [Area("Administration")]
    public class TagsController : AdministrationController
    {
        private readonly IRepository<Tag> tagsRepository;

        public TagsController(IRepository<Tag> tagsRepository)
        {
            this.tagsRepository = tagsRepository;
        }

        // GET: Administration/Tags
        public async Task<IActionResult> Index()
        {
            return this.View(await this.tagsRepository.All().ToListAsync());
        }

        // GET: Administration/Tags/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var tag = await this.tagsRepository.All()
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tag == null)
            {
                return this.NotFound();
            }

            return this.View(tag);
        }

        // GET: Administration/Tags/Create
        public IActionResult Create()
        {
            return this.View();
        }

        // POST: Administration/Tags/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] Tag tag)
        {
            if (this.ModelState.IsValid)
            {
                await this.tagsRepository.AddAsync(tag);
                await this.tagsRepository.SaveChangesAsync();
                return this.RedirectToAction(nameof(this.Index));
            }
            return this.View(tag);
        }

        // GET: Administration/Tags/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var tag = await this.tagsRepository.All().FirstOrDefaultAsync(x => x.Id == id);
            if (tag == null)
            {
                return this.NotFound();
            }

            return this.View(tag);
        }

        // POST: Administration/Tags/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] Tag tag)
        {
            if (id != tag.Id)
            {
                return this.NotFound();
            }

            if (this.ModelState.IsValid)
            {
                try
                {
                    this.tagsRepository.Update(tag);
                    await this.tagsRepository.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!this.TagExists(tag.Id))
                    {
                        return this.NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return this.RedirectToAction(nameof(this.Index));
            }

            return this.View(tag);
        }

        // GET: Administration/Tags/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var tag = await this.tagsRepository.All()
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tag == null)
            {
                return this.NotFound();
            }

            return this.View(tag);
        }

        // POST: Administration/Tags/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tag = await this.tagsRepository.All().FirstOrDefaultAsync(x => x.Id == id);
            this.tagsRepository.Delete(tag);
            await this.tagsRepository.SaveChangesAsync();
            return this.RedirectToAction(nameof(this.Index));
        }

        private bool TagExists(int id)
        {
            return this.tagsRepository.All().Any(e => e.Id == id);
        }
    }
}
