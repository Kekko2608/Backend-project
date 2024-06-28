using Microsoft.AspNetCore.Mvc;
using S2L5_Scarpe.Models;
using S2L5_Scarpe.Services;
using System.Diagnostics;



namespace S2L5_Scarpe.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductService _productService;
        private readonly IWebHostEnvironment _env;
        public HomeController(ILogger<HomeController> logger, IProductService productService, IWebHostEnvironment env)
        {
            _logger = logger;
            _productService = productService;
            _env = env;
        }

        public IActionResult Index()
        {
            var products = _productService.GetAllProducts();
            foreach (var product in products)
            {
                string coverImagePath = $"/images/{product.Id}_Cover.jpg";
                if (System.IO.File.Exists(_env.WebRootPath + coverImagePath))
                {
                    ViewBag.Cover = coverImagePath;
                }
                else
                {
                    ViewBag.Cover = null; // Nessuna immagine di copertina disponibile
                }
            }
            return View(products);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Details(int id)
        {
            var product = _productService.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }

            // Costruisci il percorso dell'immagine di copertina
            string coverImagePath = $"/images/{product.Id}_Cover.jpg";
            if (System.IO.File.Exists(_env.WebRootPath + coverImagePath))
            {
                ViewBag.Cover = coverImagePath;
            }
            else
            {
                ViewBag.Cover = null; // Nessuna immagine di copertina disponibile
            }

            // Costruisci il percorso della prima immagine aggiuntiva
            string addImage1Path = $"/images/{product.Id}_AddImage1.jpg";
            if (System.IO.File.Exists(_env.WebRootPath + addImage1Path))
            {
                ViewBag.AddImage1 = addImage1Path;
            }
            else
            {
                ViewBag.AddImage1 = null; // Nessuna immagine aggiuntiva 1 disponibile
            }

            // Costruisci il percorso della seconda immagine aggiuntiva
            string addImage2Path = $"/images/{product.Id}_AddImage2.jpg";
            if (System.IO.File.Exists(_env.WebRootPath + addImage2Path))
            {
                ViewBag.AddImage2 = addImage2Path;
            }
            else
            {
                ViewBag.AddImage2 = null; // Nessuna immagine aggiuntiva 2 disponibile
            }

            return View(product);
        }


        public IActionResult Create()
        {
            return View(new Product());
        }

        [HttpPost]
        public IActionResult Create(Product model)
        {
            if (ModelState.IsValid)
            {
                var product = new Product
                {
                    Name = model.Name,
                    Price = model.Price,
                    Description = model.Description,
                    CoverImage = model.CoverImage,
                    AddImage1 = model.AddImage1,
                    AddImage2 = model.AddImage2
                };

                _productService.CreateProduct(product);

                if (product.CoverImage != null)
                {
                    string coverFilePath = Path.Combine(_env.WebRootPath, "images", $"{product.Id}_Cover.jpg");
                    using (var fileStream = new FileStream(coverFilePath, FileMode.Create))

                        product.CoverImage.CopyTo(fileStream);
                }   
                
                if (product.AddImage1 != null)
                {
                    string addImage1FilePath = Path.Combine(_env.WebRootPath, "images", $"{product.Id}_AddImage1.jpg");
                    using (var fileStream = new FileStream(addImage1FilePath, FileMode.Create))
                    
                        product.AddImage1.CopyTo(fileStream);
                    
                }

                if (product.AddImage2 != null)
                {
                    string addImage2FilePath = Path.Combine(_env.WebRootPath, "images", $"{product.Id}_AddImage2.jpg");
                    using (var fileStream = new FileStream(addImage2FilePath, FileMode.Create))
                    
                        product.AddImage2.CopyTo(fileStream);
                    
                }

                return RedirectToAction(nameof(Details), new { id = product.Id });
            }

            return View(model);
        }

        public IActionResult Delete(int id)
            {
                var product = _productService.GetProductById(id);
                if (product == null)
                {
                    return NotFound();
                }
                return View(product);
            }

            [HttpPost, ActionName("Delete")]
            [ValidateAntiForgeryToken]
            public IActionResult DeleteConfirmed(int id)
            {
                _productService.DeleteProduct(id);
                return RedirectToAction(nameof(Index));
            }


            [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
            public IActionResult Error()
            {
                return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }
        }
    } 
