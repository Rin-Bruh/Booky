using Booky_API.Models;
using Booky_API.Models.Dto;

namespace Booky_API.Repository.IRepository
{
	public interface IUserRepository
	{
		bool IsUniqueUser(string username);
		Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO);
		Task<UserDTO> Register(RegisterationRequestDTO registerationRequestDTO);
	}
}
