using FilmaiDB.Models;
using FilmaiDB.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmaiDB.Controllers
{
    public class AktoriaiController : Controller
    {
        private readonly AppDbContext _ffilmaiDB;
        private readonly IAktoriai _aktoriai;

        public AktoriaiController(AppDbContext ffilmaiDB, IAktoriai aktoriai)
        {
            _ffilmaiDB = ffilmaiDB;
            _aktoriai = aktoriai;
        }

        public ViewResult AktoriuIndex()
        {
            var model = _aktoriai.GetAllAktoriai();
            return View(model);
        }

        public ViewResult Details(int? id)
        {
            Aktorius aktorius = _aktoriai.GetAktorius(id.Value);

            var filmas = from m in _ffilmaiDB.Filmai
                         select m;

            var aktoriaiFilmasId = from m in _ffilmaiDB.AktoriaiFilmai
                                   select m.AktoriusId;

            var aktoriaiFilmas = from m in _ffilmaiDB.AktoriaiFilmai
                                 select m;

            foreach (var m in aktoriaiFilmasId)
            {
                if (m == id.Value)
                {
                    //aktoriaiFilmas = aktoriaiFilmas.Where(x => x.FilmasId == m);
                    filmas = aktoriaiFilmas.Where(x => x.AktoriusId == m).Select(z => z.Filmas);
                    aktorius.Filmai = filmas.ToList();
                }
            }

            //filmas.AktoriaiFilmai = aktoriaiFilmas.Include(p => p.Aktorius).ToList();

            AktoriusDetailsViewModel aktoriusDetailsViewModel = new AktoriusDetailsViewModel()
            {
                Aktorius = aktorius,
                //DB_Aktoriai = string.Join(", ", filmas.AktoriaiFilmai.Select(x => x.Aktorius.VardasPavarde)),
                DB_Filmai = string.Join(", ", aktorius.Filmai.Select(x => x.Pavadinimas)),
                FilmuSkaicius = aktorius.Filmai.Count(),
                PageTitle = "Pasirinkto aktoriaus aparašas"
            };

            return View(aktoriusDetailsViewModel);
        }

        [HttpGet]
        public ViewResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Aktorius aktorius)
        {
            if (ModelState.IsValid)
            {
                Aktorius naujasAktorius = _aktoriai.Add(aktorius);
                return RedirectToAction("create_details", new { id = naujasAktorius.Id });
            }
                        
            return View();
        }

        public ViewResult Create_Details(int? id)
        {
            Aktorius aktorius = _aktoriai.GetAktorius(id.Value);

            if (aktorius == null)
            {
                Response.StatusCode = 404;
                return View("AktoriusNerastas", id.Value);
            }

            AktoriusSukurimasDetailsViewModel aktoriusSukurimasDetailsViewModel = new AktoriusSukurimasDetailsViewModel()
            {
                Aktorius = aktorius,
                PageTitle = "Sukurto aktoriaus aparašas"
            };

            return View(aktoriusSukurimasDetailsViewModel);
        }

        [HttpGet]
        public ViewResult Create_BeAktoriai()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create_BeAktoriai(Aktorius aktorius)
        {
            if (ModelState.IsValid)
            {
                Aktorius naujasAktorius = _aktoriai.Add(aktorius);
                return RedirectToAction("create_details_beaktoriai", new { id = naujasAktorius.Id });
            }

            return View();
        }

        public ViewResult Create_Details_BeAktoriai(int? id)
        {
            Aktorius aktorius = _aktoriai.GetAktorius(id.Value);

            if (aktorius == null)
            {
                Response.StatusCode = 404;
                return View("AktoriusNerastas", id.Value);
            }

            AktoriusSukurimasDetailsViewModel aktoriusSukurimasDetailsViewModel = new AktoriusSukurimasDetailsViewModel()
            {
                Aktorius = aktorius,
                PageTitle = "Sukurto aktoriaus aparašas"
            };

            return View(aktoriusSukurimasDetailsViewModel);
        }

        [HttpGet]
        public ViewResult Edit(int id)
        {
            Aktorius aktorius = _aktoriai.GetAktorius(id);
            AktoriusRedagavimasViewModel aktoriusRedagavimasViewModel = new AktoriusRedagavimasViewModel
            {
                Id = aktorius.Id,
                VardasPavarde = aktorius.VardasPavarde
            };

            return View(aktoriusRedagavimasViewModel);
        }

        [HttpPost]
        public IActionResult Edit(AktoriusRedagavimasViewModel model)
        {
            if (ModelState.IsValid)
            {
                Aktorius aktorius = _aktoriai.GetAktorius(model.Id);
                aktorius.VardasPavarde = model.VardasPavarde;
                Aktorius atnaujintasAktorius = _aktoriai.Update(aktorius);
                return RedirectToAction("aktoriuindex");
            }

            return View();
        }

        [HttpGet]
        public ViewResult Edit_BeAktoriai(int id)
        {
            Aktorius aktorius = _aktoriai.GetAktorius(id);
            AktoriusRedagavimasViewModel aktoriusRedagavimasViewModel = new AktoriusRedagavimasViewModel
            {
                Id = aktorius.Id,
                VardasPavarde = aktorius.VardasPavarde
            };

            return View(aktoriusRedagavimasViewModel);
        }

        [HttpPost]
        public IActionResult Edit_BeAktoriai(AktoriusRedagavimasViewModel model)
        {
            if (ModelState.IsValid)
            {
                Aktorius aktorius = _aktoriai.GetAktorius(model.Id);
                aktorius.VardasPavarde = model.VardasPavarde;
                Aktorius atnaujintasAktorius = _aktoriai.Update(aktorius);
                return RedirectToAction("create", "home");
            }

            return View();
        }

        [HttpGet]
        public ViewResult Delete(int? id)
        {
            Aktorius aktorius = _aktoriai.GetAktorius(id.Value);

            //IQueryable<Filmas> filmas = from m in _ffilmaiDB.Filmai
            //                            select m;
            //filmas = filmas.Where(s => s.ZanraiId == id);

            //if (aktorius == null)
            //{
            //    Response.StatusCode = 404;
            //    return View("AktoriusNerastas", id.Value);
            //}

            AktoriusIstrynimasViewModel aktoriusIstrynimasViewModel = new AktoriusIstrynimasViewModel()
            //{
            //    Aktorius = aktorius,
            //    Filmai = filmas.ToList(),
            //    PageTitle = "Aktoriaus ištrynimas"
            //};

            //AktoriusIstrynimasBeFilmuListViewModel aktoriusIstrynimasBeFilmuListViewModel = new AktoriusIstrynimasBeFilmuListViewModel()
            {
                Aktorius = aktorius,
                PageTitle = "Aktoriaus ištrynimas"
            };

            //if (aktoriusIstrynimasViewModel.Filmai.Count != 0)
            //{
                return View(aktoriusIstrynimasViewModel);
            //}
            //else
            //{
            //    return View("AktoriusPaieskaBeRezultatu", aktoriusIstrynimasBeFilmuListViewModel);
            //}
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            Aktorius aktorius = _aktoriai.GetAktorius(id);
            _aktoriai.Delete(aktorius.Id);
            return RedirectToAction("aktoriuindex");
        }

        [HttpGet]
        public ViewResult Delete_BeAktoriai(int? id)
        {
            Aktorius aktorius = _aktoriai.GetAktorius(id.Value);

            //IQueryable<Filmas> filmas = from m in _ffilmaiDB.Filmai
            //                            select m;
            //filmas = filmas.Where(s => s.ZanraiId == id);

            //if (aktorius == null)
            //{
            //    Response.StatusCode = 404;
            //    return View("AktoriusNerastas", id.Value);
            //}

            AktoriusIstrynimasViewModel aktoriusIstrynimasViewModel = new AktoriusIstrynimasViewModel()
            //{
            //    Aktorius = aktorius,
            //    Filmai = filmas.ToList(),
            //    PageTitle = "Aktoriaus ištrynimas"
            //};

            //AktoriusIstrynimasBeFilmuListViewModel aktoriusIstrynimasBeFilmuListViewModel = new AktoriusIstrynimasBeFilmuListViewModel()
            {
                Aktorius = aktorius,
                PageTitle = "Aktoriaus ištrynimas"
            };

            //if (aktoriusIstrynimasViewModel.Filmai.Count != 0)
            //{
            return View(aktoriusIstrynimasViewModel);
            //}
            //else
            //{
            //    return View("AktoriusPaieskaBeRezultatu", aktoriusIstrynimasBeFilmuListViewModel);
            //}
        }

        [HttpPost]
        public IActionResult Delete_BeAktoriai(int id)
        {
            Aktorius aktorius = _aktoriai.GetAktorius(id);
            _aktoriai.Delete(aktorius.Id);
            return RedirectToAction("create", "home");
        }

    }
}
