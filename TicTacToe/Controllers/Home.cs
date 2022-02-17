using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TicTacToe.Interfaces;
using TicTacToe.Models;
using TicTacToe.ViewModels;

namespace TicTacToe.Controllers
{
    public class Home : Controller
    {
        private readonly IGameManager gameManagerService;
        private readonly ITagCrudService tagCrudService;
        private readonly IAppAuthenticationService authenticationService;
        private readonly ISessionTagCrudService sessionTagCrudService;
        private readonly IGameCrudService gamesCrudService;
        private readonly IMapper mapper;

        public Home(IGameManager gameManagerService, IAppAuthenticationService authenticationService, ITagCrudService tagCrudService, ISessionTagCrudService sessionTagCrudService,
            IGameCrudService gamesCrudService, IMapper mapper)
        {
            this.gameManagerService = gameManagerService;
            this.authenticationService = authenticationService;
            this.tagCrudService = tagCrudService;
            this.sessionTagCrudService = sessionTagCrudService;
            this.gamesCrudService = gamesCrudService;
            this.mapper = mapper;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> _GamesTable(string tagsJson)
        {
            List<SessionData> session;
            if (string.IsNullOrEmpty(tagsJson))
                session = (await gamesCrudService.GetAllGamesAsync()).Where(g => g.IsAlive()).Select(g => g.Data).Where(g => !g.Started).ToList();
            else
            {
                var inputTags = JsonConvert.DeserializeObject<Tag[]>(tagsJson).Select(t => t.Id);
                session = (await sessionTagCrudService.GetSessionByTagAsync(inputTags)).ToList();
            }
            var VMs = mapper.Map<IEnumerable<SessionVM>>(session);
            return PartialView(VMs);
        }

        [HttpGet]
        public async Task<IActionResult> GetTags()
        {
            var tags = await tagCrudService.GetAll();
            return Json(tags);
        }

        [Authorize]
        [HttpGet]
        public IActionResult Game(int id)
        {
            HttpContext.Session.SetInt32("gameId", id);
            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateGame(CreateGameVM createGameVM)
        {
            if (!ModelState.IsValid)
                return PartialView("_CreateGame", createGameVM);
            int id = await gameManagerService.OpenGameAsync(createGameVM, HttpContext.User.Identity.Name);
            if(id == -1)
            {
                ModelState.AddModelError("", "Name is already taken");
                return PartialView("_CreateGame", createGameVM);
            }
            return PartialView("_GameCreated", id);
        }

        [ResponseCache(Duration =0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });

        }
    }
}
