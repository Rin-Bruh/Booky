using Booky_API.Data;
using Booky_API.Models;
using Booky_API.Models.Dto;
using Booky_API.Repository.IRepository;
using Microsoft.Extensions.Configuration;

namespace Booky_API.Repository
{
	public class UserRepository : IUserRepository
	{
		private readonly ApplicationDBContext _db;
		private string secretKey; 

		public UserRepository(ApplicationDBContext db, IConfiguration configuration)
		{
			_db = db;
			secretKey = configuration.GetValue<string>("ApiSettings:Secret");
		}

		public bool IsUniqueUser(string username)
		{
			var user = _db.ApplicationUsers.FirstOrDefault(x => x.UserName == username);
			if (user == null)
			{
				return true;
			}
			return false;
		}

		public async Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO)
		{
			var user = _db.ApplicationUsers
				.FirstOrDefault(u => u.UserName.ToLower() == loginRequestDTO.UserName.ToLower() && u.Password == loginRequestDTO.Password);
			
			if (user == null)
			{
				return null;
			}

			//if user was found generate JWT Token
		}

		public async Task<ApplicationUser> Register(RegisterationRequestDTO registerationRequestDTO)
		{
			ApplicationUser user = new()
			{
				UserName = registerationRequestDTO.UserName,
				Password = registerationRequestDTO.Password,
				StreetAddress = registerationRequestDTO.StreetAddress,
				City = registerationRequestDTO.City,
				State = registerationRequestDTO.State,
				PostalCode = registerationRequestDTO.PostalCode,
				Name = registerationRequestDTO.Name,
				Role = registerationRequestDTO.Role
			};
			_db.ApplicationUsers.Add(user);
			await _db.SaveChangesAsync();
			user.Password = "";
			return user;
		}
	}
}
