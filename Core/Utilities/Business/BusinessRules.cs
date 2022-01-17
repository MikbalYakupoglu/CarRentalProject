using Core.Results;

namespace Core.Utilities.Business;

public static class BusinessRules
{
    public static IResult Run(params IResult [] rules)
    {
        foreach (var rule in rules)
        {
            if (!rule.Success)
            {
                return rule;
            }
        }
        return null;
    }
}