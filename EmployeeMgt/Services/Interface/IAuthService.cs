namespace EmployeeMgt.Services.Interface
{
    public class AuthResult
    {
        public bool Success { get; set; }          // true if login succeeded
        public string? Token { get; set; }         // JWT or session token
        public string? Message { get; set; }       // Optional message (error or success)
    }

    public interface IAuthService
    {
        public Task<AuthResult> AuthenticateAsync(string username, string password);
    }
    public class AuthService : IAuthService
    {
        //private readonly IUserRepository _userRepo;
        private readonly ITokenGenerator _tokenGenerator;

        public AuthService(ITokenGenerator tokenGenerator)
        {
           // _userRepo = userRepo;
            _tokenGenerator = tokenGenerator;
        }

        public async Task<AuthResult> AuthenticateAsync(string username, string password)
        {
           // var user = await _userRepo.GetUserByUsernameAsync(username);
            //if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
               // return new AuthResult { Success = false };

            var token = _tokenGenerator.GenerateToken(username,password);
            return new AuthResult { Success = true, Token = token };
        }
    }
}
