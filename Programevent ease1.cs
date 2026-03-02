using System.ComponentModel.DataAnnotations;

namespace MyBlazorApp.AdvancedBlazorComponents;

public class SessionStore
{
    public RegistrationModel? CurrentUser { get; private set; }

    public bool HasUser => CurrentUser is not null;

    public void SetUser(RegistrationModel model)
    {
        CurrentUser = new RegistrationModel
        {
            FirstName = model.FirstName,
            LastName = model.LastName,
            Email = model.Email
        };
    }

    public void ClearUser()
    {
        CurrentUser = null;
    }
}

public class AttendanceTracker
{
    private readonly Dictionary<int, HashSet<string>> _attendance = new();

    public bool HasRegistered(int eventId, string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            return false;

        return _attendance.TryGetValue(eventId, out var attendees)
               && attendees.Contains(email.ToLowerInvariant());
    }

    public void Register(int eventId, string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            return;

        email = email.ToLowerInvariant();

        if (!_attendance.ContainsKey(eventId))
            _attendance[eventId] = new HashSet<string>();

        _attendance[eventId].Add(email);
    }
}

public class RegistrationModel
{
    [Required, MinLength(2), StringLength(50)]
    public string FirstName { get; set; } = string.Empty;

    [Required, MinLength(2), StringLength(50)]
    public string LastName { get; set; } = string.Empty;

    [Required, EmailAddress, StringLength(100)]
    public string Email { get; set; } = string.Empty;
}



