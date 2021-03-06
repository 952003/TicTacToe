using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicTacToe.Interfaces;
using TicTacToe.Models;

namespace TicTacToe.Services.CRUD
{
    public class SessionTagCrudService : ISessionTagCrudService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IGameCrudService gamesCrudService;

        public SessionTagCrudService(IUnitOfWork unitOfWork, IGameCrudService gamesCrudService)
        {
            this.unitOfWork = unitOfWork;
            this.gamesCrudService = gamesCrudService;
        }

         public async Task CreateAsync(int gameId, int tagId)
        {
            var sessionData = (await gamesCrudService.GetGameAsync(gameId)).Data;
            if(sessionData != null && await unitOfWork.DbContext.Tags.AnyAsync(t => t.Id == tagId))
            {
                var entity = new SessionTag
                {
                    Session = sessionData,
                    TagId = tagId
                };
                unitOfWork.DbContext.SessionTags.Add(entity);
                await unitOfWork.DbContext.SaveChangesAsync();
            }
        }

        public async Task<IQueryable<SessionTag>> GetAll()
        {
            return unitOfWork.DbContext.SessionTags.AsNoTracking();
        }

        public async Task<IEnumerable<SessionData>> GetSessionByTagAsync(IEnumerable<int> tagIds)
        {
            var sessionTags = unitOfWork.DbContext.SessionTags.AsNoTracking();
            var session = unitOfWork.DbContext.SessionTags.Select(s => s.Session);
            foreach(var id in tagIds)
            {
                var s = sessionTags.Where(st => st.TagId == id).Select(s => s.Session);
                session = session.Intersect(s);
            }
            return await session.Distinct().ToListAsync();
        }

        public async Task<IEnumerable<Tag>> GetTagsBySessionAsync(int sessionId)
        {
            var sessionData = await unitOfWork.DbContext.Sessions.FindAsync(sessionId);
            if (sessionData == null)
                return new List<Tag>();
            var tagsId = unitOfWork.DbContext.SessionTags.Where(s => s.SessionId == sessionId).Select(st => st.TagId);
            var tags = unitOfWork.DbContext.Tags.Where(t => tagsId.Contains(t.Id));
            return tags.AsNoTracking();
        }

    }
}
