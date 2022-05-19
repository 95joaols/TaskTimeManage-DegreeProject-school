using System.ComponentModel.DataAnnotations;

namespace WebUI.Contracts.Authentications.Requests;

public record UserRequest(string Username, string Password);
