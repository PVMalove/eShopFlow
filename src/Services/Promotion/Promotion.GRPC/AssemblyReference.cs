using System.Reflection;

namespace Promotion.GRPS;

public static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}