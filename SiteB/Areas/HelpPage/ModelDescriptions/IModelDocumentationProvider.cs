using System;
using System.Reflection;

namespace IdentityServerAzureSpike.SiteB.Areas.HelpPage.ModelDescriptions
{
    public interface IModelDocumentationProvider
    {
        string GetDocumentation(MemberInfo member);

        string GetDocumentation(Type type);
    }
}