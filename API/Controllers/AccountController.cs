using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using API.Dtos;
using API.Errors;
//using API.Errors;
using API.Extensions;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
   
    public class AccountController : BaseApiController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;
        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenService tokenService)
        {
            _tokenService = tokenService;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var user = await _userManager.FindByEmailFromClaimsPrinciple(HttpContext.User);

            return new UserDto
            {
                Email = user.Email,
                Token = await _tokenService.CreateTokenAsync(user),
                DisplayName = user.NickName,
                Avatar = user.Avatar,
                Occupation = user.Occupation
            };
        }
        [HttpGet("emailexists")]
        public async Task<ActionResult<bool>> CheckEmailExistsAsync([FromQuery] string email)
        {
            return await _userManager.FindByEmailAsync(email) != null;
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);

            if (user == null) return Unauthorized(new ApiResponse(401));

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if (!result.Succeeded) return Unauthorized(new ApiResponse(401));

            return new UserDto
            {
                Email = user.Email,
                Token = await _tokenService.CreateTokenAsync(user),
                DisplayName = user.NickName,
                Avatar = user.Avatar,
                Occupation = user.Occupation
            };
        }

        [HttpPost("registerclient")]
        public async Task<ActionResult<UserDto>> RegisterClient(RegisterDto registerDto)
        {
            var user = new AppUser
            {
                NickName = registerDto.DisplayName,
                Email = registerDto.Email,
                UserName = registerDto.Email,
                Occupation = "Client",
                Avatar = "https://res.cloudinary.com/dbalg7dya/image/upload/v1593803393/PlaceOrder_fkjr9a.png"

            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (!result.Succeeded) return BadRequest(new ApiResponse(400));

            return new UserDto
            {
                DisplayName = user.NickName,
                Token = await _tokenService.CreateTokenAsync(user),
                Email = user.Email,
                Avatar = user.Avatar,
                Occupation = user.Occupation,
            };
        }
        [HttpPost("registerCandidate")]
        public async Task<ActionResult<UserDto>> RegisterCandidate(RegisterDto registerDto)
        {
            var user = new AppUser
            {
                NickName = registerDto.DisplayName,
                Email = registerDto.Email,
                UserName = registerDto.Email,
                Occupation = "Candidate",
                Avatar = "https://res.cloudinary.com/dbalg7dya/image/upload/v1593803393/PlaceOrder_fkjr9a.png"
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (!result.Succeeded) return BadRequest(new ApiResponse(400));

            return new UserDto
            {
                DisplayName = user.NickName,
                Token = await _tokenService.CreateTokenAsync(user),
                Email = user.Email,
                Avatar = user.Avatar,
                Occupation = user.Occupation
            };
        }
        [HttpPost("registerHR")]
        public async Task<ActionResult<UserDto>> RegisterHR(RegisterDto registerDto)
        {
            var user = new AppUser
            {
                NickName = registerDto.DisplayName,
                Email = registerDto.Email,
                UserName = registerDto.Email,
                Occupation = "HR",
                Avatar = "https://res.cloudinary.com/dbalg7dya/image/upload/v1593803393/PlaceOrder_fkjr9a.png"
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (!result.Succeeded) return BadRequest(new ApiResponse(400));

            return new UserDto
            {
                DisplayName = user.NickName,
                Token = await _tokenService.CreateTokenAsync(user),
                Email = user.Email,
                Avatar = user.Avatar,
                Occupation = user.Occupation
            };
        }

    }
}