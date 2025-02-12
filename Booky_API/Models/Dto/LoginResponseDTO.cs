namespace Booky_API.Models.Dto
{
	public class LoginResponseDTO
	{
		public ApplicationUser User { get; set; }
		public string Token { get; set; }
	}
}
