using FilmaiDB.Models;
using FilmaiDB.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmaiDB.Controllers
{
    public class ZanraiController : Controller
    {
        private readonly AppDbContext _ffilmaiDB;
        private readonly IZanrai _zanrai;

        public ZanraiController(AppDbContext ffilmaiDB, IZanrai zanrai)
        {
            _ffilmaiDB = ffilmaiDB;
            _zanrai = zanrai;
        }

        public ViewResult ZanruIndex()
        {
            var model = _zanrai.GetAllZanrai();
            return View(model);
        }

        [HttpGet]
        public ViewResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Zanrai zanrai)
        {
            if (ModelState.IsValid)
            {
                Zanrai naujasZanrai = _zanrai.Add(zanrai);
                return RedirectToAction("create_details", new { id = naujasZanrai.Id });
            }

            return View();
        }

        public ViewResult Create_Details(int? id)
        {
            Zanrai zanrai = _zanrai.GetZanrai(id.Value);

            if (zanrai == null)
            {
                Response.StatusCode = 404;
                return View("ZanraiNerastas", id.Value);
            }

            ZanruSukurimasDetailsViewModel zanruSukurimasDetailsViewModel = new ZanruSukurimasDetailsViewModel()
            {
                Zanrai = zanrai,
                PageTitle = "Sukurto žanro aparašas"
            };

            return View(zanruSukurimasDetailsViewModel);
        }

        [HttpGet]
        public ViewResult Create_BeZanrai()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create_BeZanrai(Zanrai zanrai)
        {
            if (ModelState.IsValid)
            {
                Zanrai naujasZanrai = _zanrai.Add(zanrai);
                return RedirectToAction("create_details_bezanrai", new { id = naujasZanrai.Id });
            }

            return View();
        }

        public ViewResult Create_Details_BeZanrai(int? id)
        {
            Zanrai zanrai = _zanrai.GetZanrai(id.Value);

            if (zanrai == null)
            {
                Response.StatusCode = 404;
                return View("ZanraiNerastas", id.Value);
            }

            ZanruSukurimasDetailsViewModel zanruSukurimasDetailsViewModel = new ZanruSukurimasDetailsViewModel()
            {
                Zanrai = zanrai,
                PageTitle = "Sukurto žanro aparašas"
            };

            return View(zanruSukurimasDetailsViewModel);
        }

        [HttpGet]
        public ViewResult Edit(int id)
        {
            Zanrai zanrai = _zanrai.GetZanrai(id);

            ZanraiRedagavimasViewModel zanraiRedagavimasViewModel = new ZanraiRedagavimasViewModel()
            {
                Id = zanrai.Id,
                Pavadinimas = zanrai.Pavadinimas
            };

            return View(zanraiRedagavimasViewModel);
        }

        [HttpPost]
        public IActionResult Edit(ZanraiRedagavimasViewModel model)
        {
            if (ModelState.IsValid)
            {
                Zanrai zanrai = _zanrai.GetZanrai(model.Id);
                zanrai.Pavadinimas = model.Pavadinimas;
                Zanrai atnaujintasZanrai = _zanrai.Update(zanrai);
                return RedirectToAction("zanruindex");
            }

            return View();
        }

        [HttpGet]
        public ViewResult Edit_BeZanrai(int id)
        {
            Zanrai zanrai = _zanrai.GetZanrai(id);

            ZanraiRedagavimasViewModel zanraiRedagavimasViewModel = new ZanraiRedagavimasViewModel()
            {
                Id = zanrai.Id,
                Pavadinimas = zanrai.Pavadinimas
            };

            return View(zanraiRedagavimasViewModel);
        }

        [HttpPost]
        public IActionResult Edit_BeZanrai(ZanraiRedagavimasViewModel model)
        {
            if (ModelState.IsValid)
            {
                Zanrai zanrai = _zanrai.GetZanrai(model.Id);
                zanrai.Pavadinimas = model.Pavadinimas;
                Zanrai atnaujintasZanrai = _zanrai.Update(zanrai);
                return RedirectToAction("create", "home");
            }

            return View();
        }

        [HttpGet]
        public ViewResult Delete(int? id)
        {
            Zanrai zanrai = _zanrai.GetZanrai(id.Value);

            IQueryable<Filmas> filmas = from m in _ffilmaiDB.Filmai
                                        select m;

            filmas = filmas.Where(s => s.ZanraiId == id);

            if (zanrai == null)
            {
                Response.StatusCode = 404;
                return View("ZanraiNerastas", id.Value);
            }

            ZanraiIstrynimasViewModel zanraiIstrynimasViewModel = new ZanraiIstrynimasViewModel()
            {
                Zanrai = zanrai,
                Filmai = filmas.ToList(),
                PageTitle = "Žanro ištrynimas"
            };

            ZanraiIstrynimasBeFilmuListViewModel zanraiIstrynimasBeFilmuListViewModel = new ZanraiIstrynimasBeFilmuListViewModel()
            {
                Zanrai = zanrai,
                PageTitle = "Žanro ištrynimas"
            };

            if (zanraiIstrynimasViewModel.Filmai.Count != 0)
            {
                return View(zanraiIstrynimasViewModel);
            }
            else
            {
                return View("ZanraiPaieskaBeRezultatu", zanraiIstrynimasBeFilmuListViewModel);
            }
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            Zanrai zanrai = _zanrai.GetZanrai(id);
            _zanrai.Delete(zanrai.Id);
            return RedirectToAction("zanruindex");
        }

        [HttpGet]
        public ViewResult Delete_BeZanrai(int? id)
        {
            Zanrai zanrai = _zanrai.GetZanrai(id.Value);

            IQueryable<Filmas> filmas = from m in _ffilmaiDB.Filmai
                                        select m;

            filmas = filmas.Where(s => s.ZanraiId == id);

            if (zanrai == null)
            {
                Response.StatusCode = 404;
                return View("ZanraiNerastas", id.Value);
            }

            ZanraiIstrynimasViewModel zanraiIstrynimasViewModel = new ZanraiIstrynimasViewModel()
            {
                Zanrai = zanrai,
                Filmai = filmas.ToList(),
                PageTitle = "Žanro ištrynimas"
            };

            ZanraiIstrynimasBeFilmuListViewModel zanraiIstrynimasBeFilmuListViewModel = new ZanraiIstrynimasBeFilmuListViewModel()
            {
                Zanrai = zanrai,
                PageTitle = "Žanro ištrynimas"
            };

            if (zanraiIstrynimasViewModel.Filmai.Count != 0)
            {
                return View(zanraiIstrynimasViewModel);
            }
            else
            {
                return View("ZanraiPaieskaBeRezultatu_BeZanrai", zanraiIstrynimasBeFilmuListViewModel);
            }
        }

        [HttpPost]
        public IActionResult Delete_BeZanrai(int id)
        {
            Zanrai zanrai = _zanrai.GetZanrai(id);
            _zanrai.Delete(zanrai.Id);
            return RedirectToAction("create", "home");
        }

    }
}
