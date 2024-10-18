namespace HtmxAvatarChange.Models;

public class UserService
{
    public static readonly string[] AvatarUrls =
    [
        "~/img/avatar_one.png",
        "~/img/avatar_two.png",
        "~/img/avatar_three.png",
    ];
    
    public string Name { get; set; } = "Khalid Abuhakmeh";
    public string AvatarUrl { get; set; } = AvatarUrls[0];
}