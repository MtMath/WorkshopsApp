using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace Workshops.Application.Utils;

/// <summary>
/// 
/// </summary>
public class SlugHelper
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="title"></param>
    /// <returns></returns>
    public static string GenerateSlug(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
            return string.Empty;

        var normalizedString = title.Normalize(NormalizationForm.FormD);
        var stringBuilder = new StringBuilder();

        foreach (var c in normalizedString)
        {
            if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                stringBuilder.Append(c);
        }

        var slug = stringBuilder.ToString().Normalize(NormalizationForm.FormC).ToLower();
    
        slug = Regex.Replace(slug, @"\s+", "-");
        slug = Regex.Replace(slug, @"[^a-z0-9\-]", "");
        slug = Regex.Replace(slug, @"-+", "-");
    
        slug = slug.Trim('-');
    
        return slug;
    }
}