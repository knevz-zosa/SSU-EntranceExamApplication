using System.ComponentModel.DataAnnotations;
namespace Common.Requests;
public class CampusRequest
{
    [Required(ErrorMessage = "Name is required")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Address is required")]
    public string Address { get; set; }
    public bool HasDepartment { get; set; } = false;
    public string CreatedBy { get; set; }

}

public class CampusUpdate
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Name is required")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Address is required")]
    public string Address { get; set; }

    public bool HasDepartment { get; set; } = false;
    public string UpdatedBy { get; set; }
}
