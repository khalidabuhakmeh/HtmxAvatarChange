using System.Diagnostics.CodeAnalysis;
using Htmx;
using HtmxAvatarChange.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HtmxAvatarChange.Pages;

public class IndexModel(UserService userService, ILogger<IndexModel> logger) : PageModel
{
    [BindProperty] public string? Name { get; set; }

    [BindProperty] public string? AvatarUrl { get; set; }

    [TempData] public string? Message { get; set; }
    [TempData] public string? MessageCssClass { get; set; }

    [MemberNotNullWhen(true, nameof(Message))]
    public bool HasMessage => Message != null;

    public List<SelectListItem> Avatars => UserService
        .AvatarUrls
        .Select((x, i) => new SelectListItem($"avatar-{i:00}", x))
        .ToList();

    public void OnGet()
    {
        Name = userService.Name;
        AvatarUrl = userService.AvatarUrl;
    }

    public IActionResult OnPost()
    {
        if (ModelState.IsValid)
        {
            Message = "Successfully saved account settings";
            MessageCssClass = "alert-success";

            // change values
            userService.Name = Name!;
            userService.AvatarUrl = AvatarUrl!;
            
            Response.Htmx(h =>
                h.WithTrigger("avatar"));
        }
        else
        {
            Message = "Failed to save account settings";
            MessageCssClass = "alert-danger";
        }

        if (Request.IsHtmx())
        {
            return Partial("_Form", this);
        }

        return RedirectToPage("Index");
    }

    public IActionResult OnGetAvatar()
    {
        return Partial("_Avatar", userService);
    }

    public string? IsCurrentAvatar(string avatarValue)
    {
        return avatarValue == AvatarUrl ? "checked" : null;
    }
}