using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicTacToe.Interfaces;
using TicTacToe.Models;
using TicTacToe.ViewModels;

namespace TicTacToe.Services.CRUD
{
    public class UserCrudService : IUserCrudService
    {
        private readonly IUnitOfWork unitOfWork;

        private readonly IMapper mapper;

        public UserCrudService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task CreateAsync(UserSignUpVM createUserVM)
        {
            var user = mapper.Map<User>(createUserVM);
            unitOfWork.DbContext.Users.Add(user);
            await unitOfWork.DbContext.SaveChangesAsync();
        }

        public async Task<User> GetByLoginAsync(string login)
        {
            var user = await unitOfWork.DbContext.Users.FirstOrDefaultAsync(u => u.Login == login);
            return user;
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await unitOfWork.DbContext.Users.ToListAsync();
        }
    }
}
