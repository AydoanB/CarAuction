namespace CarAuction.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using CarAuction.Data;
    using CarAuction.Data.Common.Repositories;
    using CarAuction.Data.Models.AuctionModels;
    using CarAuction.Data.Models.CarModel;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;

    [Area("Administration")]
    public class CarsController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly IDeletableEntityRepository<Car> carsRepository;
        private readonly IDeletableEntityRepository<Auction> auctionsRepository;
        private readonly IDeletableEntityRepository<Model> modelsRepository;

        public CarsController(ApplicationDbContext context,
            IDeletableEntityRepository<Car> carsRepository,
            IDeletableEntityRepository<Auction> auctionsRepository,
            IDeletableEntityRepository<Model> modelsRepository)
        {
            this.context = context;
            this.carsRepository = carsRepository;
            this.auctionsRepository = auctionsRepository;
            this.modelsRepository = modelsRepository;
        }

        // GET: Administration/Cars
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = await this.carsRepository
                .AllWithDeleted()
                .Include(x => x.Model).ThenInclude(x => x.Manufacturer)
                .Include(x => x.Auction).ThenInclude(x => x.Location)
                .Include(x => x.User)
                .ToListAsync();

            return this.View(applicationDbContext);
        }

        // GET: Administration/Cars/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || this.carsRepository.All() == null)
            {
                return this.NotFound();
            }

            var car = await this.carsRepository
                .All()
                .FirstOrDefaultAsync(m => m.Id == id);

            if (car == null)
            {
                return this.NotFound();
            }

            return this.View(car);
        }

        // GET: Administration/Cars/Create
        public IActionResult Create()
        {
            this.ViewData["AuctionId"] = new SelectList(this.auctionsRepository.All(), "Id", "UserId");
            this.ViewData["ModelId"] = new SelectList(this.modelsRepository.All(), "Id", "Name");
            this.ViewData["UserId"] = new SelectList(this.context.Users, "Id", "Id");
            return this.View();
        }

        // POST: Administration/Cars/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ModelId,AuctionId,UserId,StartingPrice,BuyNowPrice,Color,Milleage,IsRunning,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] Car car)
        {
            if (this.ModelState.IsValid)
            {
               await this.carsRepository.AddAsync(car);
               await this.carsRepository.SaveChangesAsync();
               return this.RedirectToAction(nameof(this.Index));
            }

            this.ViewData["AuctionId"] = new SelectList(this.auctionsRepository.All(), "Id", "UserId", car.AuctionId);
            this.ViewData["ModelId"] = new SelectList(this.modelsRepository.All(), "Id", "Name", car.ModelId);
            this.ViewData["UserId"] = new SelectList(this.context.Users, "Id", "Id", car.UserId);
            return this.View(car);
        }

        // GET: Administration/Cars/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || this.carsRepository.All() == null)
            {
                return this.NotFound();
            }

            var car = await this.carsRepository.All()
                .FirstOrDefaultAsync(x => x.Id == id);
            if (car == null)
            {
                return this.NotFound();
            }

            this.ViewData["AuctionId"] = new SelectList(this.auctionsRepository.All(), "Id", "UserId", car.AuctionId);
            this.ViewData["ModelId"] = new SelectList(this.modelsRepository.All(), "Id", "Name", car.ModelId);
            this.ViewData["UserId"] = new SelectList(context.Users, "Id", "Id", car.UserId);
            return this.View(car);
        }

        // GET: Administration/Cars/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || this.carsRepository.All() == null)
            {
                return this.NotFound();
            }

            var car = await this.carsRepository
                .All()
                .FirstOrDefaultAsync(m => m.Id == id);

            if (car == null)
            {
                return this.NotFound();
            }

            return this.View(car);
        }

        // POST: Administration/Cars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (!this.carsRepository.AllAsNoTracking().Any())
            {
                return this.Problem("Entity set 'ApplicationDbContext.Cars'  is null.");
            }

            var car = this.carsRepository
                .All()
                .FirstOrDefault(x => x.Id == id);
            if (car != null)
            {
                this.carsRepository.Delete(car);
            }

            await this.carsRepository.SaveChangesAsync();
            return this.RedirectToAction(nameof(this.Index));
        }
    }
}
