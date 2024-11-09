﻿namespace Wojcik.Shared.DTOs;

public class UserDTO
{
	public Guid Id { get; set; } = Guid.Empty;
	public string UserName { get; set; } = string.Empty;
	public string Email { get; set; } = string.Empty;
	public string Role { get; set; } = string.Empty;
	public bool Enabled { get; set; } = false;
}