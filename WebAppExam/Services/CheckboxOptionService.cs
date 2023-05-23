using WebAppExam.Models;

namespace WebAppExam.Services;

public class CheckboxOptionService
{
    public readonly CategoryService _categoryService;
    public readonly RoleService _roleService;

    public CheckboxOptionService(CategoryService categoryService, RoleService roleService)
    {
        _categoryService = categoryService;
        _roleService = roleService;
    }

    public async Task<List<CheckboxOptionModel>> PopulateCategoryCheckBoxesAsync()
    {
        var checkboxes = new List<CheckboxOptionModel>();
        var categories = await _categoryService.GetCategoriesAsync();

        foreach (var category in categories)
        {
            var checkbox = new CheckboxOptionModel();

            if (category == categories.First()) { checkbox.IsChecked = true; }

            checkbox.Description = category.Name;
            checkbox.Value = category.Id.ToString();

            checkboxes.Add(checkbox);
        }

        return checkboxes;
    }

    public async Task<List<CheckboxOptionModel>> PopulateRoleCheckBoxesAsync()
    {
        var checkboxes = new List<CheckboxOptionModel>();
        var roles = await _roleService.GetRolesAsync();

        foreach (var role in roles)
        {
            var checkbox = new CheckboxOptionModel
            {
                Description = role.Name!,
                Value = role.Id
            };

            checkboxes.Add(checkbox);
        }

        return checkboxes;
    }
}
