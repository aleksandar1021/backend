using Backend.DataAccess;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Backend.Models
{
    public class JwtServicecs
    {
        #region Data
        private readonly IConfiguration config;
        #endregion

        #region Properties
        public String SecretKey { get; set; }
        public int TokenDuration { get; set; }
        #endregion

        #region Constructor
        public JwtServicecs(IConfiguration _config)
        {
            config = _config;
            this.SecretKey = config.GetSection("jwtConfig").GetSection("Key").Value;
            this.TokenDuration = Int32.Parse(config.GetSection("jwtConfig").GetSection("Duration").Value);
        }
        #endregion

        #region Methods
        public string GenerateToken(User user) //dodati objekat
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.SecretKey));

            var signature = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var payloda = new[]
            {
                new Claim("id", user.Id.ToString()),
                new Claim("name", user.Name),
                new Claim("lastName", user.Lastname),
                new Claim("email", user.Email),
            };

            var jwtToken = new JwtSecurityToken(
                issuer: "localhost",
                audience: "localhost",
                claims: payloda,
                expires: DateTime.Now.AddMinutes(TokenDuration),
                signingCredentials: signature
                );

            var response = new JwtSecurityTokenHandler().WriteToken(jwtToken);

            return response;
        }
        #endregion
    }
}
