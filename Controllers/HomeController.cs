using FilmaiDB.Models;
using FilmaiDB.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmaiDB.Controllers
{
    public class HomeController : Controller
    {
        private readonly IFilmaiDB _filmaiDB;
        private readonly AppDbContext _ffilmaiDB;
        private readonly IZanrai _zanrai;

        public HomeController(IFilmaiDB filmaiDB, AppDbContext ffilmaiDB, IZanrai zanrai)
        {
            _filmaiDB = filmaiDB;
            _ffilmaiDB = ffilmaiDB;
            _zanrai = zanrai;
        }

        public async Task<IActionResult> Index(string searchBy, string search, string filmoZanras, int filmoMetai)
        {
            IQueryable<string> zanraiQuery = from m in _ffilmaiDB.Filmai
                                             select m.Zanrai.Pavadinimas;

            IQueryable<Filmas> filmas = from m in _ffilmaiDB.Filmai
                                        select m;

            IQueryable<int> filmasId = from m in _ffilmaiDB.Filmai
                                        select m.Id;

            IQueryable<int> metaiQuery = from m in _ffilmaiDB.Filmai
                                         select m.IsleidimoData.Value;

            //if (!string.IsNullOrEmpty(searchString))
            //{
            //    filmas = filmas.Where(s => s.Pavadinimas.Contains(searchString));
            //}

            if (!string.IsNullOrEmpty(filmoZanras))
            {
                filmas = filmas.Where(x => x.Zanrai.Pavadinimas == filmoZanras);
            }

            if (!filmoMetai.Equals(0))
            {
                filmas = filmas.Where(z => z.IsleidimoData == filmoMetai);
            }

            if (searchBy == "SearchString")
            {
                filmas = filmas.Where(s => s.Pavadinimas.Contains(search) || search == null);
            }

            if (searchBy == "FilmoZanras")
            {
                filmas = filmas.Where(x => x.Zanrai.Pavadinimas.Contains(search) || search == null);
            }

            if (searchBy == "FilmoMetai")
            {
                filmas = filmas.Where(z => z.IsleidimoData.ToString() == search || search == null);
            }

            var movieGenreVM = new FilmuPaieskaViewModel
            {
                Filmai = await filmas.Include(p => p.Zanrai).Include(r => r.AktoriaiFilmai).ToListAsync(),
                ZanraiList = new SelectList(await zanraiQuery.Distinct().ToListAsync()),
                MetuList = new SelectList(await metaiQuery.Distinct().ToListAsync())
            };

            if (movieGenreVM.Filmai.Count != 0)
            {
                return View(movieGenreVM);
            }
            else
            {
                if (movieGenreVM.Filmai.Count == 0 && movieGenreVM.ZanraiList.Count() != 0)
                {
                    return View("Index_PaieskaBeRezultatu");
                }
                else
                {
                    return View("Index_Tuscias");
                }
            }

            //var model = _filmaiDB.GetAllFilmas();
            //return View(model);
        }
        
        public ViewResult Details(int? id)
        {
            Filmas filmas = _filmaiDB.GetFilmas(id.Value);
            Zanrai zanrai = _zanrai.GetZanrai(filmas.ZanraiId);

            IQueryable<Aktorius> aktorius = from m in _ffilmaiDB.Aktoriai
                                            select m;

            var aktoriaiFilmasId = from m in _ffilmaiDB.AktoriaiFilmai
                                   select m.FilmasId;

            var aktoriaiFilmas = from m in _ffilmaiDB.AktoriaiFilmai
                                 select m;

            foreach (var m in aktoriaiFilmasId)
            {
                if (m == id.Value)
                {
                    //aktoriaiFilmas = aktoriaiFilmas.Where(x => x.FilmasId == m);
                    aktorius = aktoriaiFilmas.Where(x => x.FilmasId == m).Select(z => z.Aktorius);
                }
            }

            //filmas.AktoriaiFilmai = aktoriaiFilmas.Include(p => p.Aktorius).ToList();
            filmas.DB_Aktoriai = aktorius.ToList();

            if (filmas == null)
            {
                Response.StatusCode = 404;
                return View("FilmasNerastas", id.Value);
            }

            HomeDetailsViewModel homeDetailsViewModel = new HomeDetailsViewModel()
            {
                Filmas = filmas,
                //Zanrai = zanrai,
                //DB_Aktoriai = string.Join(", ", filmas.AktoriaiFilmai.Select(x => x.Aktorius.VardasPavarde)),
                DB_Aktoriai = string.Join(", ", filmas.DB_Aktoriai.Select(x => x.VardasPavarde)),
                PageTitle = "Pasirinkto filmo aparašas"
            };
            
            return View(homeDetailsViewModel);
        }

        [HttpGet]
        public ViewResult Create()
        {
            ViewBag.Zanrai = new SelectList(_ffilmaiDB.Zanrai, "Id", "Pavadinimas");
            ViewBag.AktoriaiList = new MultiSelectList(_ffilmaiDB.Aktoriai, "Id", "VardasPavarde");

            if (_ffilmaiDB.Zanrai.Count() == 0 && _ffilmaiDB.Aktoriai.Count() == 0)
            {
                return View("BeZanraiBeAktoriai");
            }

            if (_ffilmaiDB.Aktoriai.Count() == 0)
            {
                return View("BeAktoriai");
            }

            if (_ffilmaiDB.Zanrai.Count() == 0)
            {
                return View("BeZanrai");
            }

            return View();
        }

        [HttpPost]
        public IActionResult Create(FilmoSukurimasViewModel model)
        {
            ViewBag.Zanrai = new SelectList(_ffilmaiDB.Zanrai, "Id", "Pavadinimas");
            ViewBag.AktoriaiList = new MultiSelectList(_ffilmaiDB.Aktoriai, "Id", "VardasPavarde");

            if (ModelState.IsValid)
            {
                Filmas filmas = new Filmas()
                {
                    Id = model.Id,
                    Pavadinimas = model.Pavadinimas,
                    IsleidimoData = model.IsleidimoData,
                    ZanraiId = model.ZanraiId.Value,
                    Zanrai = model.Zanrai
                };

                Filmas naujasFilmas = _filmaiDB.Add(filmas);

                foreach (var item in model.SelectedVal)
                {
                    _ffilmaiDB.AktoriaiFilmai.Add(new AktoriaiFilmas()
                    {
                        FilmasId = naujasFilmas.Id,
                        Filmas = _filmaiDB.GetFilmas(naujasFilmas.Id),
                        AktoriusId = item,
                        Aktorius = _ffilmaiDB.Aktoriai.FirstOrDefault(s => s.Id == item)
                    });

                    _ffilmaiDB.SaveChanges();
                    //Aktorius aktorius = _ffilmaiDB.Aktoriai.Find(item);
                    //naujasFilmas.DB_Aktoriai.Add(aktorius);
                }
                
                return RedirectToAction("create_details", new { id = naujasFilmas.Id });
            }

            return View();
        }

        public ActionResult Create_Details(int? id)
        {
            Filmas filmas = _filmaiDB.GetFilmas(id.Value);
            Zanrai zanrai = _zanrai.GetZanrai(filmas.ZanraiId);

            IQueryable<Aktorius> aktorius = from m in _ffilmaiDB.Aktoriai
                                            select m;

            var aktoriaiFilmasId = from m in _ffilmaiDB.AktoriaiFilmai
                                 select m.FilmasId;

            var aktoriaiFilmas = from m in _ffilmaiDB.AktoriaiFilmai
                                 select m;

            foreach (var m in aktoriaiFilmasId)
            {
                if (m == id.Value)
                {
                    //aktoriaiFilmas = aktoriaiFilmas.Where(x => x.FilmasId == m);
                    aktorius = aktoriaiFilmas.Where(x => x.FilmasId == m).Select(z => z.Aktorius);
                }
            }

            //filmas.AktoriaiFilmai = aktoriaiFilmas.Include(p => p.Aktorius).ToList();
            filmas.DB_Aktoriai = aktorius.ToList();

            HomeDetailsViewModel homeDetailsViewModel = new HomeDetailsViewModel()
            {
                Filmas = filmas,
                //Zanrai = zanrai,
                //DB_Aktoriai = string.Join(", ", filmas.AktoriaiFilmai.Select(x => x.Aktorius.VardasPavarde)),
                DB_Aktoriai = string.Join(", ", filmas.DB_Aktoriai.Select(x => x.VardasPavarde)),
                PageTitle = "Sukurto filmo aparašas"
            };

            return View(homeDetailsViewModel);
        }

        [HttpGet]
        public ViewResult Edit(int id)
        {
            Filmas filmas = _filmaiDB.GetFilmas(id);
            ViewBag.Zanrai = new SelectList(_ffilmaiDB.Zanrai, "Id", "Pavadinimas");
            //ViewBag.AktoriaiList = new MultiSelectList(_ffilmaiDB.Aktoriai, "Id", "VardasPavarde");

            FilmoRedagavimasViewModel filmoRedagavimasViewModel = new FilmoRedagavimasViewModel
            {
                Id = filmas.Id,
                Pavadinimas = filmas.Pavadinimas,
                IsleidimoData = filmas.IsleidimoData.Value,
                //Zanras = filmas.Zanras,
                //Aktoriai = filmas.Aktoriai,
                ZanraiId = filmas.ZanraiId,
                Zanrai = filmas.Zanrai,
                SenasId = filmas.ZanraiId
            };

            IQueryable<Aktorius> aktorius = from m in _ffilmaiDB.Aktoriai
                                            select m;

            var aktoriaiFilmasId = from m in _ffilmaiDB.AktoriaiFilmai
                                   select m.FilmasId;

            var aktoriaiFilmas = from m in _ffilmaiDB.AktoriaiFilmai
                                 select m;

            foreach (var m in aktoriaiFilmasId)
            {
                if (m == id)
                {
                    //aktoriaiFilmas = aktoriaiFilmas.Where(x => x.FilmasId == m);
                    aktorius = aktoriaiFilmas.Where(x => x.FilmasId == m).Select(z => z.Aktorius);
                }
            }

            //filmas.AktoriaiFilmai = aktoriaiFilmas.Include(p => p.Aktorius).ToList();
            var filmas_DB_Aktoriai = aktorius.ToList();

            if (filmas_DB_Aktoriai != null)
            {
                int[] DB_AktoriaiIds = new int[filmas_DB_Aktoriai.Count()];
                int length = filmas_DB_Aktoriai.Count();

                for (int i = 0; i < length; i++)
                {
                    DB_AktoriaiIds[i] = filmas_DB_Aktoriai[i].Id;
                }

                //ViewBag.AktoriaiList = new MultiSelectList(_ffilmaiDB.Aktoriai, "Id", "VardasPavarde", DB_AktoriaiIds);
                MultiSelectList AktoriaiList = new MultiSelectList(_ffilmaiDB.Aktoriai.ToList(), "Id", "VardasPavarde", DB_AktoriaiIds);
                filmoRedagavimasViewModel.MultiSel_Aktoriai = AktoriaiList;
            }

            return View(filmoRedagavimasViewModel);
        }

        [HttpPost]
        public IActionResult Edit(FilmoRedagavimasViewModel model)
        {
            ViewBag.Zanrai = new SelectList(_ffilmaiDB.Zanrai, "Id", "Pavadinimas");

            if (ModelState.IsValid)
            {
                Filmas filmas = _filmaiDB.GetFilmas(model.Id);
                filmas.Pavadinimas = model.Pavadinimas;
                filmas.IsleidimoData = model.IsleidimoData;
                //filmas.Zanras = model.Zanras;
                //filmas.Aktoriai = model.Aktoriai;
                filmas.ZanraiId = model.ZanraiId.Value;
                filmas.Zanrai = model.Zanrai;
                                
                var aktoriaiFilmasId = from m in _ffilmaiDB.AktoriaiFilmai
                                       select m.FilmasId;

                var aktoriaiFilmas = from m in _ffilmaiDB.AktoriaiFilmai
                                     select m;

                foreach (var m in aktoriaiFilmasId)
                {
                    if (m == filmas.Id)
                    {
                        _ffilmaiDB.AktoriaiFilmai.Remove(aktoriaiFilmas.First(x => x.FilmasId == m));
                        _ffilmaiDB.SaveChanges();
                    }
                }
                                
                if (model.SelectedVal.Count() > 0)
                {
                    List<Aktorius> viewModelAktoriai = new List<Aktorius>();

                    foreach (var id in model.SelectedVal)
                    {
                        var aktoriai = _ffilmaiDB.Aktoriai.Find(id);

                        if (aktoriai != null)
                        {
                            filmas.DB_Aktoriai.Add(aktoriai);
                            viewModelAktoriai.Add(aktoriai);                            
                        }
                    }

                    var visiAktoriai = _ffilmaiDB.Aktoriai.ToList();
                    var aktoriaiPasalinti = visiAktoriai.Except(viewModelAktoriai);

                    foreach (var aktorius in aktoriaiPasalinti)
                    {
                        filmas.DB_Aktoriai.Remove(aktorius);                        
                    }
                }

                foreach (var item in model.SelectedVal)
                {
                    _ffilmaiDB.AktoriaiFilmai.Add(new AktoriaiFilmas()
                    {
                        FilmasId = filmas.Id,
                        Filmas = _filmaiDB.GetFilmas(filmas.Id),
                        AktoriusId = item,
                        Aktorius = _ffilmaiDB.Aktoriai.FirstOrDefault(s => s.Id == item)
                    });
                }

                Filmas atnaujintasFilmas = _filmaiDB.Update(filmas);
                return RedirectToAction("index");
                //return RedirectToAction("details", new { id = filmas.Id });
            }

            return View();
        }

        [HttpGet]
        public ViewResult Edit_ZanraiDelete(int id)
        {
            Filmas filmas = _filmaiDB.GetFilmas(id);
            ViewBag.Zanrai = new SelectList(_ffilmaiDB.Zanrai, "Id", "Pavadinimas");

            FilmoRedagavimasViewModel filmoRedagavimasViewModel = new FilmoRedagavimasViewModel
            {
                Id = filmas.Id,
                Pavadinimas = filmas.Pavadinimas,
                IsleidimoData = filmas.IsleidimoData.Value,
                //Zanras = filmas.Zanras,
                //Aktoriai = filmas.Aktoriai,
                ZanraiId = filmas.ZanraiId,
                Zanrai = filmas.Zanrai,
                SenasId = filmas.ZanraiId
            };

            IQueryable<Aktorius> aktorius = from m in _ffilmaiDB.Aktoriai
                                            select m;

            var aktoriaiFilmasId = from m in _ffilmaiDB.AktoriaiFilmai
                                   select m.FilmasId;

            var aktoriaiFilmas = from m in _ffilmaiDB.AktoriaiFilmai
                                 select m;

            foreach (var m in aktoriaiFilmasId)
            {
                if (m == id)
                {
                    //aktoriaiFilmas = aktoriaiFilmas.Where(x => x.FilmasId == m);
                    aktorius = aktoriaiFilmas.Where(x => x.FilmasId == m).Select(z => z.Aktorius);
                }
            }

            //filmas.AktoriaiFilmai = aktoriaiFilmas.Include(p => p.Aktorius).ToList();
            var filmas_DB_Aktoriai = aktorius.ToList();

            if (filmas_DB_Aktoriai != null)
            {
                int[] DB_AktoriaiIds = new int[filmas_DB_Aktoriai.Count()];
                int length = filmas_DB_Aktoriai.Count();

                for (int i = 0; i < length; i++)
                {
                    DB_AktoriaiIds[i] = filmas_DB_Aktoriai[i].Id;
                }

                //ViewBag.AktoriaiList = new MultiSelectList(_ffilmaiDB.Aktoriai, "Id", "VardasPavarde", DB_AktoriaiIds);
                MultiSelectList AktoriaiList = new MultiSelectList(_ffilmaiDB.Aktoriai.ToList(), "Id", "VardasPavarde", DB_AktoriaiIds);
                filmoRedagavimasViewModel.MultiSel_Aktoriai = AktoriaiList;
            }

            return View(filmoRedagavimasViewModel);
        }

        [HttpPost]
        public IActionResult Edit_ZanraiDelete(FilmoRedagavimasViewModel model)
        {
            if (ModelState.IsValid)
            {
                Filmas filmas = _filmaiDB.GetFilmas(model.Id);
                filmas.Pavadinimas = model.Pavadinimas;
                filmas.IsleidimoData = model.IsleidimoData;
                //filmas.Zanras = model.Zanras;
                //filmas.Aktoriai = model.Aktoriai;
                filmas.ZanraiId = model.ZanraiId.Value;
                filmas.Zanrai = model.Zanrai;

                var aktoriaiFilmasId = from m in _ffilmaiDB.AktoriaiFilmai
                                       select m.FilmasId;

                var aktoriaiFilmas = from m in _ffilmaiDB.AktoriaiFilmai
                                     select m;

                foreach (var m in aktoriaiFilmasId)
                {
                    if (m == filmas.Id)
                    {
                        _ffilmaiDB.AktoriaiFilmai.Remove(aktoriaiFilmas.First(x => x.FilmasId == m));
                        _ffilmaiDB.SaveChanges();
                    }
                }

                if (model.SelectedVal.Count() > 0)
                {
                    List<Aktorius> viewModelAktoriai = new List<Aktorius>();

                    foreach (var id in model.SelectedVal)
                    {
                        var aktoriai = _ffilmaiDB.Aktoriai.Find(id);

                        if (aktoriai != null)
                        {
                            filmas.DB_Aktoriai.Add(aktoriai);
                            viewModelAktoriai.Add(aktoriai);
                        }
                    }

                    var visiAktoriai = _ffilmaiDB.Aktoriai.ToList();
                    var aktoriaiPasalinti = visiAktoriai.Except(viewModelAktoriai);

                    foreach (var aktorius in aktoriaiPasalinti)
                    {
                        filmas.DB_Aktoriai.Remove(aktorius);
                    }
                }

                foreach (var item in model.SelectedVal)
                {
                    _ffilmaiDB.AktoriaiFilmai.Add(new AktoriaiFilmas()
                    {
                        FilmasId = filmas.Id,
                        Filmas = _filmaiDB.GetFilmas(filmas.Id),
                        AktoriusId = item,
                        Aktorius = _ffilmaiDB.Aktoriai.FirstOrDefault(s => s.Id == item)
                    });
                }

                Filmas atnaujintasFilmas = _filmaiDB.Update(filmas);
                return RedirectToAction("delete", "zanrai", new { id = model.SenasId });
            }

            return View();
        }

        [HttpGet]
        public ViewResult Delete(int? id)
        {
            Filmas filmas = _filmaiDB.GetFilmas(id.Value);
            string zanrai = _zanrai.GetZanrai(filmas.ZanraiId).Pavadinimas;

            IQueryable<Aktorius> aktorius = from m in _ffilmaiDB.Aktoriai
                                            select m;

            var aktoriaiFilmasId = from m in _ffilmaiDB.AktoriaiFilmai
                                   select m.FilmasId;

            var aktoriaiFilmas = from m in _ffilmaiDB.AktoriaiFilmai
                                 select m;

            foreach (var m in aktoriaiFilmasId)
            {
                if (m == id.Value)
                {
                    aktoriaiFilmas = aktoriaiFilmas.Where(x => x.FilmasId == m);
                    aktorius = aktoriaiFilmas.Where(x => x.FilmasId == m).Select(z => z.Aktorius);
                };
            };

            filmas.AktoriaiFilmai = aktoriaiFilmas.Include(p => p.Aktorius).ToList();
            filmas.DB_Aktoriai = aktorius.ToList();

            if (filmas == null)
            {
                Response.StatusCode = 404;
                return View("FilmasNerastas", id.Value);
            }

            FilmoIstrynimasViewModel filmoIstrynimasViewModel = new FilmoIstrynimasViewModel()
            {
                Filmas = filmas,
                ZanraiPavadinimas = zanrai,
                //DB_Aktoriai = string.Join(", ", filmas.AktoriaiFilmai.Select(x => x.Aktorius.VardasPavarde)),
                DB_Aktoriai = string.Join(", ", filmas.DB_Aktoriai.Select(x => x.VardasPavarde)),
                PageTitle = "Filmo ištrynimas"
            };

            return View(filmoIstrynimasViewModel);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            Filmas filmas = _filmaiDB.GetFilmas(id);
            _filmaiDB.Delete(filmas.Id);
            return RedirectToAction("index");        
        }

    }
}
