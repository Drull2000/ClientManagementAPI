using System.ComponentModel.DataAnnotations;

namespace ClientManagementAPI.DTOs;

public class ClientCreateDto
{
    [Required(ErrorMessage = "Поле FullName є обов'язковим")]
    public string FullName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Поле Email є обов'язковим")]
    [EmailAddress(ErrorMessage = "Некоректний формат Email")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Поле Phone є обов'язковим")]
    public string Phone { get; set; } = string.Empty;
}