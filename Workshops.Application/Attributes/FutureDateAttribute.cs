using System.ComponentModel.DataAnnotations;

namespace Workshops.Application.Attributes;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
public class FutureDateAttribute() : ValidationAttribute("The date must be in the future.")
{
    public override bool IsValid(object? value)
    {
        if (value is DateTime date) return date > DateTime.Now;
        return false;
    }
}