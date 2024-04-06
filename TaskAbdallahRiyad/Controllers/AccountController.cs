using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TaskAbdallahRiyad.Models.ViewModel;

namespace TaskAbdallahRiyad.Controllers
{
    public class AccountController : Controller
    {
        /*configuration*/
        #region configuration
        private UserManager<IdentityUser> userManager;
        private RoleManager<IdentityRole> roleManager;
        private SignInManager<IdentityUser> signInManager;
        public AccountController(UserManager<IdentityUser> _userManager,
           RoleManager<IdentityRole> _roleManager, SignInManager<IdentityUser> _signInManager)
        {
            userManager = _userManager;
            roleManager = _roleManager;
            signInManager = _signInManager;
        }
        #endregion

        /*Register&Login*/
        #region Register&Login


        /*Register HttpGet*/
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }


        /*Register HttpPost*/
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if (ModelState.IsValid)
            {
                IdentityUser user = new IdentityUser
                {
                    Email = registerViewModel.Email,
                    UserName = registerViewModel.Email,
                    PhoneNumber = registerViewModel.PhoneNumber,
                };
                var result = await userManager.CreateAsync(user, registerViewModel.Password!);
                if (result.Succeeded) { return RedirectToAction("Login"); }
                foreach (var Error in result.Errors)
                {
                    ModelState.AddModelError(Error.Code, Error.Description);
                }
                return View(registerViewModel);
            }
            return View(registerViewModel);
        }


        /*Login HttpGet*/
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        /*Login HttpPost*/
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                var myResult = await signInManager.PasswordSignInAsync(loginViewModel.Email!, loginViewModel.Password!, loginViewModel.RememberMe, false);
                if (myResult.Succeeded) { return RedirectToAction("Index","Home"); }
                ModelState.AddModelError("", "Invalid User or Password");
                return View(loginViewModel);
            }

            return View(loginViewModel);
        }


        /*Logout*/
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }
        #endregion

        /*permission*/
        #region permission
        //RolesList
        
        [HttpGet]
        public IActionResult RolesList()
        {
            return View(roleManager.Roles);
        }

        //CreateRole => HttpGet
        
        [HttpGet]
        public IActionResult CreateRole()
        {
            return View();
        }

        //CreateRole => HttpPost
        
        [HttpPost]
        public async Task<IActionResult> CreateRole(CreateRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityRole role = new IdentityRole
                {
                    Name = model.RoleName
                };
                var result = await roleManager.CreateAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction("RolesList");
                }
                foreach (var err in result.Errors)
                {
                    ModelState.AddModelError(err.Code, err.Description);
                }
                return View(model);
            }
            return View(model);
        }

        //EditRole => HttpGet
       
        [HttpGet]
        public async Task<IActionResult> EditRole(string Id)
        {
            if (Id == null) { return RedirectToAction("RolesList"); }
            var role = await roleManager.FindByIdAsync(Id);
            if (role == null) { return RedirectToAction("RolesList"); }
            EditRoleViewModel model = new EditRoleViewModel
            {
                RoleId = role!.Id,
                RoleName = role.Name!,
            };
            foreach (var user in userManager.Users)
            {
                if (await userManager.IsInRoleAsync(user, role.Name!))
                {
                    model.Users!.Add(user.UserName!);
                }
            }
            return View(model);
        }

        //EditRole => HttpPost
        
        [HttpPost]
        public async Task<IActionResult> EditRole(EditRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var role = await roleManager.FindByIdAsync(model.RoleId!);
                role!.Name = model.RoleName;
                var result = await roleManager.UpdateAsync(role);

                if (result.Succeeded)
                {
                    return RedirectToAction("RolesList");
                }
                foreach (var err in result.Errors)
                {
                    ModelState.AddModelError("", err.Description);
                }
                return View(model);

            }
            return View(model);
        }

        //EditUserInRole => HttpGet
        
        [HttpGet]
        public async Task<IActionResult> EditUserInRole(string id)
        {
            var role = await roleManager.FindByIdAsync(id);
            if (role == null) { return RedirectToAction("RolesList"); }
            var model = new List<UserRoleViewModel>();
            foreach (var user in userManager.Users)
            {
                var userRoleViewModel = new UserRoleViewModel
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                };
                if (await userManager.IsInRoleAsync(user, role.Name!))
                {
                    userRoleViewModel.IsSelected = true;
                }
                else
                {
                    userRoleViewModel.IsSelected = false;
                }
                model.Add(userRoleViewModel);
            }
            return View(model);
        }

        //EditUserInRole => HttpPost
        
        [HttpPost]
        public async Task<IActionResult> EditUserInRole(List<UserRoleViewModel> model, string id)
        {
            var r = await roleManager.FindByIdAsync(id);
            if (r == null) { return NotFound(); }
            for (int i = 0; i < model.Count; i++)
            {
                var user = await userManager.FindByIdAsync(model[i].UserId!);
                IdentityResult result = null!;
                if (model[i].IsSelected && !(await userManager.IsInRoleAsync(user!, r.Name!)))
                {
                    result = await userManager.AddToRoleAsync(user!, r.Name!);
                }
                else if (model[i].IsSelected && (await userManager.IsInRoleAsync(user!, r.Name!)))
                {
                    result = await userManager.RemoveFromRoleAsync(user!, r.Name!);
                }
            }
            return RedirectToAction("RolesList");
        }



        //DeleteRole => HttpGet
        
        [HttpGet]
        public async Task<IActionResult> DeleteRole(string id)
        {
            if (id == null) { return RedirectToAction("RolesList"); }
            var role = await roleManager.FindByIdAsync(id);
            if (role == null) { return RedirectToAction("RolesList"); }
            DeleteRoleViewModel model = new DeleteRoleViewModel
            {
                RoleId = role!.Id,
                RoleName = role.Name!,
            };
            return View(model);
        }

        //DeleteRole => HttpPost
        
        [HttpPost]
        public async Task<IActionResult> DeleteRole(DeleteRoleViewModel model)
        {
            if (ModelState.IsValid)
            {

                var role = await roleManager.FindByIdAsync(model.RoleId!);
                role!.Name = model.RoleName;
                var result = await roleManager.DeleteAsync(role);

                if (result.Succeeded)
                {
                    return RedirectToAction("RolesList");
                }
                foreach (var err in result.Errors)
                {
                    ModelState.AddModelError("", err.Description);
                }
                return View(model);
            }
            return View(model);
        }

        //DetailsRole => HttpGet
       
        [HttpGet]
        public async Task<IActionResult> DetailsRole(string id)
        {
            if (id == null) { return RedirectToAction("RolesList"); }
            var role = await roleManager.FindByIdAsync(id);
            if (role == null) { return RedirectToAction("RolesList"); }
            DetailsRoleViewModel model = new DetailsRoleViewModel
            {
                RoleId = role!.Id,
                RoleName = role.Name!,
            };
            return View(model);
        }
        #endregion

    }
}
