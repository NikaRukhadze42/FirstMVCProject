﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using ProductsManagementSystem.Interfaces;
using ProductsManagementSystem.Models.VM.Role;
using ProductsManagementSystem.Services;
using System.Threading.Tasks;

namespace ProductsManagementSystem.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RoleController : Controller
    {
        public readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.Roles = await _roleService.GetAllRoles();
            return View();
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(string roleName)
        {
            await _roleService.Add(roleName);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Remove(string Id)
        {
            await _roleService.Remove(Id);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> UpdateRole(string Id)
        {
            var role = await _roleService.GetRole(Id);
            ViewBag.Role = role;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateRole(UpdateRoleViewModel model)
        {
            await _roleService.UpdateRole(model);
            return RedirectToAction("Index");
        }
    }
}
