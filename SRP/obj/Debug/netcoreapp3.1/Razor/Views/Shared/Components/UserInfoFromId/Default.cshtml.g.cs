#pragma checksum "C:\Users\Wojciech\source\repos\SRP\SRP\Views\Shared\Components\UserInfoFromId\Default.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "00aa387e19d92cf7150b4ab734bfe99a84a04932"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared_Components_UserInfoFromId_Default), @"mvc.1.0.view", @"/Views/Shared/Components/UserInfoFromId/Default.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "C:\Users\Wojciech\source\repos\SRP\SRP\Views\_ViewImports.cshtml"
using SRP;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\Wojciech\source\repos\SRP\SRP\Views\_ViewImports.cshtml"
using SRP.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"00aa387e19d92cf7150b4ab734bfe99a84a04932", @"/Views/Shared/Components/UserInfoFromId/Default.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"9a2e16ed0b994d829f8f8df2740519cf83e915cb", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Views_Shared_Components_UserInfoFromId_Default : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<SRP.Models.DTOs.SRPUserDTO>
    #nullable disable
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n<td>\r\n    ");
#nullable restore
#line 4 "C:\Users\Wojciech\source\repos\SRP\SRP\Views\Shared\Components\UserInfoFromId\Default.cshtml"
Write(Html.DisplayFor(x => x.FirstName));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n</td>\r\n<td>\r\n    ");
#nullable restore
#line 7 "C:\Users\Wojciech\source\repos\SRP\SRP\Views\Shared\Components\UserInfoFromId\Default.cshtml"
Write(Html.DisplayFor(x => x.LastName));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n</td>\r\n<td>\r\n    ");
#nullable restore
#line 10 "C:\Users\Wojciech\source\repos\SRP\SRP\Views\Shared\Components\UserInfoFromId\Default.cshtml"
Write(Html.DisplayFor(x => x.UserName));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n</td>\r\n");
        }
        #pragma warning restore 1998
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<SRP.Models.DTOs.SRPUserDTO> Html { get; private set; } = default!;
        #nullable disable
    }
}
#pragma warning restore 1591
