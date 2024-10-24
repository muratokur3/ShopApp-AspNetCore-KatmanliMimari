﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using shopapp.webui.Models;
using ShopApp.Business.Abstratc;
using ShopApp.Entity;
using ShopApp.WebUI.Extentions;
using ShopApp.WebUI.Identity;
using ShopApp.WebUI.Models;

namespace ShopApp.WebUI.Controllers
{
    public class AdminController : Controller
    {
        private IProductService _productService;
        private ICategoryService _categoryService;
        private RoleManager<IdentityRole> _roleManager;
        private UserManager<User> _userManager;
        public AdminController(
                                IProductService productService,
                                ICategoryService categoryService,
                                RoleManager<IdentityRole> roleManager,
                                UserManager<User> userManager
                                )
        {
            _productService = productService;
            _categoryService = categoryService;
            _roleManager = roleManager;
            _userManager = userManager;
        }


        public IActionResult UserList()
        {
            return View(_userManager.Users);
        }


        public async Task<IActionResult> UserEdit(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                var selectedRoles = await _userManager.GetRolesAsync(user);
                var roles = _roleManager.Roles.Select(i => i.Name);
                ViewBag.Roles = roles;

                return View(new UserDetailModel()
                {
                    UserId = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    UserName = user.UserName,
                    Email = user.Email,
                    EmailConfirmed = user.EmailConfirmed,
                    SelectedRoles = selectedRoles
                });
            }
            TempData.Put("message", new AlertMessage()
            {
                Title = "User not found",
                Message = "User not found",
                AlertType = "danger"
            });
            return Redirect("~/");
        }

        [HttpPost]
        public async Task<IActionResult> UserEdit(UserDetailModel model, string[] selectedRoles)
        {
            var user = await _userManager.FindByIdAsync(model.UserId);
            if (user != null)
            {
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.UserName = model.UserName;
                user.Email = model.Email;
                user.EmailConfirmed = model.EmailConfirmed;

                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    var userRoles = await _userManager.GetRolesAsync(user);
                    selectedRoles = selectedRoles ?? new string[] { };
                    result = await _userManager.AddToRolesAsync(user, selectedRoles.Except(userRoles).ToArray<string>());
                    if (!result.Succeeded)
                    {
                        ModelState.AddModelError("", "Selected roles cannot be added");
                        return View(model);
                    }
                    result = await _userManager.RemoveFromRolesAsync(user, userRoles.Except(selectedRoles).ToArray<string>());
                    if (!result.Succeeded)
                    {
                        ModelState.AddModelError("", "Selected roles cannot be removed");
                        return View(model);
                    }
                    TempData.Put("message", new AlertMessage()
                    {
                        Title = "User updated successfully",
                        Message = "User updated successfully",
                        AlertType = "success"
                    });
                    return RedirectToAction("UserList");
                }
            }
            return View(model);
        }





        public async Task<IActionResult> RoleEdit(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);

            var members = new List<User>();
            var nonmembers = new List<User>();
            var users = await _userManager.Users.ToListAsync();
            foreach (var user in users)
            {
                var list = await _userManager.IsInRoleAsync(user, role.Name) ? members : nonmembers;
                list.Add(user);
            }
            var model = new RoleDetails()
            {
                Role = role,
                Members = members,
                NonMembers = nonmembers
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> RoleEdit(RoleEditModel model)
        {
            //if (ModelState.IsValid)
            //{
            foreach (var userId in model.IdsToAdd ?? new string[] { })
            {
                var user = await _userManager.FindByIdAsync(userId);
                if (user != null)
                {
                    var result = await _userManager.AddToRoleAsync(user, model.RoleName);
                    if (!result.Succeeded)
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }
                    }
                }
            }

            foreach (var userId in model.IdsToDelete ?? new string[] { })
            {
                var user = await _userManager.FindByIdAsync(userId);
                if (user != null)
                {
                    var result = await _userManager.RemoveFromRoleAsync(user, model.RoleName);
                    if (!result.Succeeded)
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }
                    }
                }
                //}
            }
            return Redirect("/admin/role/" + model.RoleId);
        }

        public IActionResult RoleList()
        {
            return View(_roleManager.Roles);
        }
        public IActionResult RoleCreate()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> RoleCreate(RoleModel model)
        {
            if (ModelState.IsValid)
            {
                var resul = await _roleManager.CreateAsync(new IdentityRole(model.Name));
                if (resul.Succeeded)
                {
                    return RedirectToAction("RoleList");
                }
                else
                {
                    foreach (var item in resul.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }
            }
            return View(model);
        }

        public async Task<IActionResult> ProductList()
        {
            var products = await _productService.GetAll();
            return View(new ProductListViewModel()
            {
                Products = products
            });
        }

        public IActionResult ProductCreate()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ProductCreate(ProductModel model)
        {

            var entity = new Product()
            {
                Name = model.Name,
                Url = model.Url,
                Price = model.Price,
                Description = model.Description,
                ImageUrl = model.ImageUrl
            };
            if (_productService.Create(entity))
            {
                TempData.Put("message", new AlertMessage()
                {
                    Title = "was added successfully",
                    Message = $"{entity.Name} was added successfully",
                    AlertType = "success"
                });
                return RedirectToAction("productList");
            }

            TempData.Put("message", new AlertMessage()
            {
                Title = _productService.ErrorMessage,
                Message = _productService.ErrorMessage,
                AlertType = "danger"
            });
            return View(model);


        }

        public async Task<IActionResult> ProductEdit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entity = _productService.GetByIdWithCategories((int)id);

            if (entity == null)
            {
                return NotFound();
            }

            var model = new ProductModel()
            {
                ProductId = entity.ProductId,
                Name = entity.Name,
                Url = entity.Url,
                Price = entity.Price,
                ImageUrl = entity.ImageUrl,
                Description = entity.Description,
                IsApproved = entity.IsApproved,
                IsHome = entity.IsHome,
                SelectedCategories = entity.ProductCategories.Select(i => i.Category).ToList()
            };

            ViewBag.Categories =await _categoryService.GetAll();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ProductEdit(ProductModel model, int[] categoryIds, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                var entity =await _productService.GetById(model.ProductId);
                if (entity == null)
                {
                    return NotFound();
                }
                entity.Name = model.Name;
                entity.Price = model.Price;
                entity.Url = model.Url;
                entity.Description = model.Description;
                entity.IsApproved = model.IsApproved;
                entity.IsHome = model.IsHome;

                if (file != null)
                {

                    entity.ImageUrl = file.FileName;
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Img", file.FileName);

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                }


                if (_productService.Update(entity, categoryIds))
                {
                    TempData.Put("message", new AlertMessage()
                    {
                        Title = "updated successfully",
                        Message = $"{entity.Name} was updated successfully",
                        AlertType = "successful"
                    });
                    return RedirectToAction("ProductList");
                }
                TempData.Put("message", new AlertMessage()
                {
                    Title = _productService.ErrorMessage,
                    Message = _productService.ErrorMessage,
                    AlertType = "danger"
                });

            }

            ViewBag.Categories =await _categoryService.GetAll();
            return View(model);
        }

        public async Task<IActionResult> DeleteProduct(int productId)
        {
            var entity =await _productService.GetById(productId);
            if (entity != null)
            {
                _productService.Delete(entity);
            }
            var msg = new AlertMessage()
            {
                AlertType = "danger",
                Message = $"{entity.Name} was deleted successfully"
            };
            TempData["message"] = JsonConvert.SerializeObject(msg);
            return RedirectToAction("ProductList");
        }





        public async Task<IActionResult> CategoryList()
        {
            var categories = await _categoryService.GetAll();
            return View(new CategoryViewModel()
            {
                Categories = categories
            });
        }
        public IActionResult CategoryCreate()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CategoryCreate(CategoryModel model)
        {
            if (ModelState.IsValid)
            {
                var entity = new Category()
                {
                    Name = model.Name,
                    Url = model.Url
                };
                _categoryService.Create(entity);

                TempData.Put("message", new AlertMessage()
                {
                    Title = "was added successfully",
                    AlertType = "success",
                    Message = $"{entity.Name} was added successfully"
                });
                return RedirectToAction("CategoryList");
            }
            return View(model);
        }
        public IActionResult CategoryEdit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entity = _categoryService.GetByIdWithProducts((int)id);

            if (entity == null)
            {
                return NotFound();
            }

            var model = new CategoryModel()
            {
                CategoryId = entity.CategoryId,
                Name = entity.Name,
                Url = entity.Url,
                Products = entity.ProductCategories.Select(p => p.Product).ToList()
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CategoryEdit(CategoryModel model)
        {
            if (ModelState.IsValid)
            {
                var entity =await _categoryService.GetById(model.CategoryId);
                if (entity == null)
                {
                    return NotFound();
                }
                entity.Name = model.Name;
                entity.Url = model.Url;

                _categoryService.Update(entity);


                TempData.Put("message", new AlertMessage()
                {
                    Title = "was updated successfully",
                    Message = $"{entity.Name} isimli category güncellendi.",
                    AlertType = "success"
                });
                return RedirectToAction("CategoryList");
            }
            return View(model);
        }
        public async Task<IActionResult> DeleteCategory(int CategoryId)
        {
            var entity =await _categoryService.GetById(CategoryId);
            if (entity != null)
            {
                _categoryService.Delete(entity);
            }

            TempData.Put("message", new AlertMessage()
            {
                Title = "was deleted successfully",
                AlertType = "danger",
                Message = $"{entity.Name} was deleted successfully"
            });
            return RedirectToAction("CategoryList");
        }

        public IActionResult DeleteFromCategory(int productId, int categoryId)
        {
            _categoryService.DeleteFromCategory(productId, categoryId);
            return Redirect("/admin/categoryedit/" + categoryId);
        }

    }
}


