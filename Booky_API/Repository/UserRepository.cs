using Booky_API.Data;
using Booky_API.Models;
using Booky_API.Models.Dto;
using Booky_API.Repository.IRepository;

namespace Booky_API.Repository
{
	public class UserRepository : IUserRepository
	{
		private readonly ApplicationDBContext _db;

		public UserRepository(ApplicationDBContext db)
		{
			_db = db;
		}

		public bool IsUniqueUser(string username)
		{
			throw new NotImplementedException();
		}

		public Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO)
		{
			throw new NotImplementedException();
		}

		public Task<ApplicationUser> Register(RegisterationRequestDTO registerationRequestDTO)
		{
			throw new NotImplementedException();
		}
	}
}
