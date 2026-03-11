using Karim.Customer.HrApplication.Application.Abstraction.ServicesContract.Employee;
using Karim.Customer.HrApplication.Application.Abstraction.ServicesContract.Identity;
using Karim.Customer.HrApplication.Domain.Entities.Employee;
using employee = Karim.Customer.HrApplication.Domain.Entities.Employee.Employee;
using Karim.Customer.HrApplication.Domain.Entities.Identity;
using Karim.Customer.HrApplication.Domain.UnitOfWork;
using Karim.Customer.HrApplication.Shared.DTOs.Auth;
using Karim.Customer.HrApplication.Shared.DTOs.CommonDTOs;
using Karim.Customer.HrApplication.Shared.Exceptions;
using MapsterMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text.RegularExpressions;
using Karim.Customer.HrApplication.Application.Specifications.Employee;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace Karim.Customer.HrApplication.Application.Services.Identity
{
    internal class AuthServices(
        UserManager<AppUser> _userManager,
        IConfiguration _configs,
        RoleManager<AppPrivilages> _roleManager,
        IMapper _mapper,
        IUnitOfWork _unitOfWork) : IAuthServices
    {
        private const string passwordPattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^a-zA-Z0-9]).{6,}$";
        public async Task<ActionStatusDto> EmployeeSignUp(SignUpDto? user)
        {
            //Check On User
            if (user is null) throw new BadRequestException("Provided Data is Invalid!");
            //Check On All Data
            _ = user switch
            {
                { Email: null or ""} => throw new BadRequestException("Must Provied an Email"),
                { UserName: null or ""} => throw new BadRequestException("Must Provied an UserName"),
                { Password: null or ""} => throw new BadRequestException("Must Provied an Password"),
                { PhoneNumber: null or ""} => throw new BadRequestException("Must Provied an PhoneNumber"),
                { EmpId: null or ""} => throw new BadRequestException("Must Provied an Existance Employee"),
                _ => user
            };
            //Check Password
            if (!Regex.IsMatch(user.Password, passwordPattern)) throw new BadRequestException("Provided Password is Weak! It Should be at Least 8 Characters, Contain UpperCase, LowerCase, Number and Special Character.");
            //Check If Email Already Exists
            var CheckUser = await _userManager.FindByEmailAsync(user.Email);
            if (CheckUser is not null) throw new ConflictException("Provided Email is Already Used!");
            //Check If UserName Already Exists
            var CheckUserName = await _userManager.FindByNameAsync(user.UserName);
            if (CheckUserName is not null) throw new ConflictException("Provided UserName is Already Used!");
            //Create Specification
            var specification = new EmployeeByIdSepecification(user.EmpId);
            //Get Employee With Provided EmpId
            var EmployeeExist = await _unitOfWork.GenerateRepository<employee, string>().GetByIdAsync(specification);
            //Check If There Is Employee Exist
            if (EmployeeExist is null) throw new NotFoundException("Employee You Try To Add Account For Him Is Not Exist!");
            //Check IF Employee Already Have An Account
            if (EmployeeExist.Account is not null) throw new ConflictException("This Employee Already Have An Account");
            //Update The Work Email
            EmployeeExist.WorkEmail = user.Email;
            //Create User
            var newUser = new AppUser()
            {
                DisplayName = user.DisplayName,
                UserName = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                EmpId = user.EmpId,
            };
            var result = await _userManager.CreateAsync(newUser, user.Password);
            //Check Result
            if (!result.Succeeded) throw new BadRequestException($"Failed to create user: {result.Errors.First().Description}");
            //Add Roles
            if (user.AsssignedPrivilages is not null && user.AsssignedPrivilages.Count > 0)
            {
                //Get Matched Privilages
                var MatchedPrivilages = await _roleManager.Roles.Where(R => user.AsssignedPrivilages.Contains(R.PrivNumber)).Select(R => R.NormalizedName).ToListAsync();
                if (MatchedPrivilages.Any())
                {
                    var addRolesResult = await _userManager.AddToRolesAsync(newUser, MatchedPrivilages!);
                    if (!addRolesResult.Succeeded) throw new BadRequestException($"Failed To Assign Privilages!: {addRolesResult.Errors.Select(e => e.Description.FirstOrDefault())}");
                }
            }
            //Update Employee
            _unitOfWork.GenerateRepository<employee, string>().Update(EmployeeExist);
            var empResult = await _unitOfWork.CompleteAsync();
            //Check On Emp Update
            if (empResult == 0) throw new Exception("Couldn't Update Employee Work Email");
            //Forming Object
            var Obj = new ActionStatusDto()
            {
                Status = true,
                Message = "Employee Account Created Successfully"
            };
            //returning Object
            return Obj;
        }
        public async Task<SignInResultDto> SignIn(SignInDto? user)
        {
            //Check On Data
            if (user is null) throw new BadRequestException("Provided Data Is Invalid");
            //Check On Internal Data
            _ = user switch
            {
                { UserNameOrEmail: null or "" } => throw new BadRequestException("User Name Or Password Must Be Provided!"),
                { Password: null or "" } => throw new BadRequestException("Password Must Be Provided!"),
                _ => user
            };
            AppUser userExist;
            //Check User Data Kind and get the user
            if (user.UserNameOrEmail.ToLower().Contains("@") || user.UserNameOrEmail.ToLower().Contains(".com")) userExist = await _userManager.FindByEmailAsync(user.UserNameOrEmail);
            else userExist = await _userManager.FindByNameAsync(user.UserNameOrEmail);
            //Check If User Exist
            if (userExist is null) throw new NotFoundException("User Isn't Exist!");
            //Check On Password
            bool passwordCorrect = await _userManager.CheckPasswordAsync(userExist, user.Password);
            //Check on Password
            if (!passwordCorrect) throw new BadRequestException("The Password You Have Entered is Wrong!");
            //Start Forming Object
            var Obj = new SignInResultDto()
            {
                Status = new ActionStatusDto()
                {
                    Status = true,
                    Message = "Logged In Successfuly"
                },
                Token = await tokenGenerator(userExist)
            };
            return Obj;
        }
        public async Task<ICollection<PrivilagesToReturnDto>> GetAllPrivilages()
        {
            //Get All Privs
            var privilagesList = await _roleManager.Roles.ToListAsync();
            //ConvertThem Into Dto
            var mappedPrivs = _mapper.Map<ICollection<PrivilagesToReturnDto>>(privilagesList);
            //Return List
            return mappedPrivs;
        }
        public async Task<ICollection<PrivilagesToReturnDto>> GetAllUserPrivilages(string? userNameOrEmail)
        {
            //Check On Data
            if(string.IsNullOrWhiteSpace(userNameOrEmail) || string.IsNullOrEmpty(userNameOrEmail)) throw new BadRequestException("User Name Or Email is invalid");
            //Check On User
            AppUser? user = null;
            //Check if Email
            if(userNameOrEmail.Contains("@")) user = await _userManager.FindByEmailAsync(userNameOrEmail);
            //Check if user name
            if(!userNameOrEmail.Contains("@")) user = await _userManager.FindByNameAsync(userNameOrEmail);
            //Check on User
            if (user is null) throw new NotFoundException("User Not Exist !");
            //Get his privilages
            var UserPrivilages = await _userManager.GetRolesAsync(user);
            //Get All Privilages
            var AllPrivs = await GetAllPrivilages();
            //Compare it with all
            var convertedUserPrivis = AllPrivs.Where(P => UserPrivilages.Contains(P.Name)).ToList();
            return convertedUserPrivis;
        }

        //Token Generator
        private async Task<string> tokenGenerator(AppUser? user)
        {
            //Check On User
            if (user is null) throw new BadRequestException("Entered User Data Not Valid!");
            //Forming Claims
            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Email, user.Email!),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName!),
                new Claim(JwtRegisteredClaimNames.Name, user.DisplayName!),
                new Claim(JwtRegisteredClaimNames.PhoneNumber, user.PhoneNumber!),
                new Claim("AccountId", user.Id!),
                new Claim("EmployeeId", user.EmpId!)
            };
            //Get Roles
            var roles = await _userManager.GetRolesAsync(user);
            //Adding Roles To Claims
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            //Get SecritKey
            var secritKey = _configs.GetSection("JwtConfigs")["SecretKey"];
            //Forming Key
            var Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secritKey!));
            //Forming Credensial
            var credinsial = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configs.GetSection("JwtConfigs")["Issure"],
                audience: _configs.GetSection("JwtConfigs")["ExpiringTime"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(int.Parse(_configs.GetSection("JwtConfigs")["ExpiringTime"]!)),
                signingCredentials: credinsial
                );

            //return token
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
