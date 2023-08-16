using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using School.Data.Entities.Identity;
using School.Data.Helpers;
using School.Infrastructure.Abstracties;
using School.Service.Abstracts;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace School.Service.Implementation
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly JwtSettings _jwtSettings;

        private readonly IRefreshTokenRepository _refreshTokenRepository;

        public AuthenticationService(JwtSettings jwtSettings, IRefreshTokenRepository refreshTokenRepository)
        {
            _jwtSettings = jwtSettings;
            _refreshTokenRepository = refreshTokenRepository;

        }
        public async Task<JwtAuthResult> GetJWTToken(User user)
        {

            var (jwtToken, accessToken) = GenerateJWTToken(user);
            var refreshToken = GetRefreshToken(user.UserName);
            var userRefreshToken = new UserRefreshToken
            {
                AddedTime = DateTime.Now,
                ExpiryDate = DateTime.Now.AddDays(_jwtSettings.RefreshTokenExpireDate),
                IsUsed = false,
                IsRevoked = false,
                JwtId = jwtToken.Id,
                RefreshToken = refreshToken.TokenString,
                Token = accessToken,
                UserId = user.Id,




            };
            await _refreshTokenRepository.AddAsync(userRefreshToken);
            var response = new JwtAuthResult();
            response.refreshToken = refreshToken;
            response.AccessToken = accessToken;
            return response;
        }

        private RefreshToken GetRefreshToken(string userName)
        {

            //Refresh Token
            var refrshToken = new RefreshToken
            {
                ExpireAt = DateTime.Now.AddDays(_jwtSettings.RefreshTokenExpireDate),
                UserName = userName,
                TokenString = GenerateRefreshToken()
            };

            return refrshToken;

        }
        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            var randomNumberGenerate = RandomNumberGenerator.Create();
            randomNumberGenerate.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);

        }
        private List<Claim> GetClaims(User user)
        {
            var claims = new List<Claim>()
            {
                new Claim(nameof(UserClaimModel.UserName),user.UserName),
                new Claim(nameof(UserClaimModel.Email),user.Email),
                new Claim(nameof(UserClaimModel.PhoneNumber),user.PhoneNumber),
                new Claim(nameof(UserClaimModel.Id),user.Id.ToString()),
            };
            return claims;
        }

        private (JwtSecurityToken, string) GenerateJWTToken(User user)
        {
            var claims = GetClaims(user);
            var jwtToken = new JwtSecurityToken(
                _jwtSettings.Issure,
                _jwtSettings.Audience,
                claims,
                expires: DateTime.Now.AddDays(_jwtSettings.AccessTokenExpireDate),

               signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtSettings.Secret)), SecurityAlgorithms.HmacSha256Signature));
            var accessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            return (jwtToken, accessToken);
        }
        public async Task<JwtAuthResult> GetRefreshToken(User user, JwtSecurityToken jwtToken, DateTime? ExpiryDate, string refreshToken)
        {

            //Generate Refresh Token

            var (jwtSecurityToken, newToken) = GenerateJWTToken(user);

            var response = new JwtAuthResult();
            response.AccessToken = newToken;
            var refreshTokenResult = new RefreshToken();
            refreshTokenResult.UserName = jwtToken.Claims.FirstOrDefault(x => x.Type == nameof(UserClaimModel.UserName)).Value;
            refreshTokenResult.TokenString = refreshToken;
            refreshTokenResult.ExpireAt = (DateTime)ExpiryDate;
            response.refreshToken = refreshTokenResult;
            return response;

        }

        public JwtSecurityToken ReadJwtToken(string accessToken)
        {
            if (string.IsNullOrEmpty(accessToken))
            {
                throw new ArgumentNullException(nameof(accessToken));
            }
            var handler = new JwtSecurityTokenHandler();
            var response = handler.ReadJwtToken(accessToken);
            return response;
        }

        public async Task<string> validateToken(string accessToken)
        {
            var handler = new JwtSecurityTokenHandler();
            var parameters = new TokenValidationParameters
            {
                ValidateIssuer = _jwtSettings.ValidateIssuer,
                ValidIssuers = new[] { _jwtSettings.Issure },
                ValidateIssuerSigningKey = _jwtSettings.ValidateIssuerSignigKey,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtSettings.Secret)),
                ValidAudience = _jwtSettings.Audience,
                ValidateAudience = _jwtSettings.ValidateAudience,
                ValidateLifetime = _jwtSettings.ValidateLifetime,
            };
            var validator = handler.ValidateToken(accessToken, parameters, out SecurityToken validatedToken);
            try
            {
                if (validator == null)
                {
                    return ("Invalid Token");
                }
                return "NotExpired";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }



        public async Task<(string, DateTime?)> validateDetails(JwtSecurityToken jwtToken, string accessToken, string refreshToken)
        {
            //read Token To Get Cliams

            if (jwtToken == null || !jwtToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256Signature))
            {
                return ("Algorithm Is Wrong", null);
            }
            if (jwtToken.ValidTo > DateTime.UtcNow)
            {
                return ("Token Is not Expired", null);
            }

            //Get User
            var userId = jwtToken.Claims.FirstOrDefault(x => x.Type == nameof(UserClaimModel.Id)).Value;

            var userRefreshToken = await _refreshTokenRepository.GetTableNoTracking()
            .FirstOrDefaultAsync(x => x.Token == accessToken &&
                                 x.RefreshToken == refreshToken &&
                                 x.UserId == int.Parse(userId));


            if (userRefreshToken == null)
            {
                return ("Refresh Token Is Not Found", null);
            }
            //Validations Token Refresh Token
            if (userRefreshToken.ExpiryDate < DateTime.UtcNow)
            {
                userRefreshToken.IsRevoked = true;
                userRefreshToken.IsUsed = false;
                await _refreshTokenRepository.UpdateAsync(userRefreshToken);
                return ("Refresh Token Is Expired", null);
            }
            //var user = await _userManager.FindByIdAsync(userId);
            //if (user == null)
            //{
            //    throw new SecurityTokenException("User Not Found");
            //}

            var expirydate = userRefreshToken.ExpiryDate;
            return (userId, expirydate);
        }
    }

}
