using Domain.Common;

namespace Domain.Clinics;

public sealed class Clinic : AuditableEntity
{
    public string Name { get; private set; } = string.Empty;

    public string Code { get; private set; } = string.Empty;

    public string? PhoneNumber { get; private set; }

    public string? Email { get; private set; }

    public string? Address { get; private set; }

    public bool IsActive { get; private set; } = true;
    private Clinic()
    {
    }

    public Clinic(string name, string code)
    {
        SetName(name);
        SetCode(code);
    }

    public void UpdateDetails(
        string name,
        string code,
        string? phoneNumber,
        string? email,
        string? address)
    {
        SetName(name);
        SetCode(code);

        PhoneNumber = NormalizeOptionalValue(phoneNumber);
        Email = NormalizeOptionalValue(email);
        Address = NormalizeOptionalValue(address);
    }

    public void Activate()
    {
        IsActive = true;
    }

    public void Deactivate()
    {
        IsActive = false;
    }

    private void SetName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException(
                "Clinic name is required.",
                nameof(name));
        }

        Name = name.Trim();
    }

    private void SetCode(string code)
    {
        if (string.IsNullOrWhiteSpace(code))
        {
            throw new ArgumentException(
                "Clinic code is required.",
                nameof(code));
        }

        Code = code.Trim().ToUpperInvariant();
    }

    private static string? NormalizeOptionalValue(string? value)
    {
        return string.IsNullOrWhiteSpace(value)
            ? null
            : value.Trim();
    }
}
