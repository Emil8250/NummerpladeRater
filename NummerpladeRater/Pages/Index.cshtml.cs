using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NummerpladeRater.Data;
using NummerpladeRater.Models;

namespace NummerpladeRater.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly LicensePlateDbContext _licensePlateDbContext;

    public IndexModel(ILogger<IndexModel> logger, LicensePlateDbContext licensePlateDbContext)
    {
        _logger = logger;
        _licensePlateDbContext = licensePlateDbContext;
    }

    public void OnGet(string? plate)
    {
        if (plate == null) return;
        LicensePlate = _licensePlateDbContext.LicensePlate.FirstOrDefault(x => x.Plate == plate);
        Upvotes = _licensePlateDbContext.Upvotes.Count(x => x.PlateId == LicensePlate!.Id);
        Downvotes = _licensePlateDbContext.Downvotes.Count(x => x.PlateId == LicensePlate!.Id);

    }
    
    [BindProperty]
    public LicensePlate? LicensePlate { get; set; }
    public int Upvotes { get; set; }
    public int Downvotes { get; set; }
    
    public async Task<IActionResult> OnPostAsync(bool? upvote)
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        var licensePlates = _licensePlateDbContext.LicensePlate;
        var existingLicensePlate = licensePlates.FirstOrDefault(x => x.Plate == LicensePlate!.Plate);
        if (LicensePlate != null && existingLicensePlate == null)
        {
            _licensePlateDbContext.LicensePlate.Add(LicensePlate);
            await _licensePlateDbContext.SaveChangesAsync();
            existingLicensePlate = licensePlates.FirstOrDefault(x => x.Plate == LicensePlate!.Plate);
        }

        switch (upvote)
        {
            case null:
                return RedirectToPage("./Index", new { plate = existingLicensePlate!.Plate });
            case true:
                _licensePlateDbContext.Upvotes.Add(new Upvote { PlateId = existingLicensePlate!.Id });
                break;
            default:
                _licensePlateDbContext.Downvotes.Add(new Downvote { PlateId = existingLicensePlate!.Id });
                break;
        }

        await _licensePlateDbContext.SaveChangesAsync();
        return RedirectToPage("./Index", new { plate = existingLicensePlate.Plate });
    }
}