using System;
using System.Reflection;

namespace IdentityServerAzureSpike.SiteD.Areas.HelpPage.ModelDescriptions
{
    public interface IModelDocumentationProvider
    {
        string GetDocumentation(MemberInfo member);

        string GetDocumentation(Type type);
    }
}