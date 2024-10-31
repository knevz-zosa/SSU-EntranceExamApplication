using Common.CustomClasses;
using System.ComponentModel.DataAnnotations;

namespace Common.Requests;
public class DepartmentRequest
{
    [Required(ErrorMessage = "Name is required")]
    public string Name { get; set; }

    [IdValidator(ErrorMessage = "Campus is required")]
    public int CampusId { get; set; }
    public string CreatedBy { get; set; }
}

public class DepartmentUpdate
{
    public int Id { get; set; }
    [Required(ErrorMessage = "Name is require.")]
    public string Name { get; set; }
    [IdValidator(ErrorMessage = "Campus is required")]
    public int CampusId { get; set; }
    public string UpdatedBy { get; set; }
}
