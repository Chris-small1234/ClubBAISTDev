#pragma checksum "C:\Users\chrissmall\source\repos\ClubBAISTDev\ClubBAISTDev\Pages\ViewMemberAccount.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "ab47715da3b39cf1d80c24a2de2616a315fb3866"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(ClubBAISTDev.Pages.Pages_ViewMemberAccount), @"mvc.1.0.razor-page", @"/Pages/ViewMemberAccount.cshtml")]
namespace ClubBAISTDev.Pages
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
#line 1 "C:\Users\chrissmall\source\repos\ClubBAISTDev\ClubBAISTDev\Pages\_ViewImports.cshtml"
using ClubBAISTDev;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"ab47715da3b39cf1d80c24a2de2616a315fb3866", @"/Pages/ViewMemberAccount.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"c5fda8f1a5073d9c850a11f540dc5251250d3a10", @"/Pages/_ViewImports.cshtml")]
    public class Pages_ViewMemberAccount : global::Microsoft.AspNetCore.Mvc.RazorPages.Page
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 3 "C:\Users\chrissmall\source\repos\ClubBAISTDev\ClubBAISTDev\Pages\ViewMemberAccount.cshtml"
  
    ViewData["Title"] = "Account Details";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<div>\r\n    <h1>Account Details</h1>\r\n    <table>\r\n        <tr>\r\n            <td>Member ID:</td>\r\n            <td>");
#nullable restore
#line 12 "C:\Users\chrissmall\source\repos\ClubBAISTDev\ClubBAISTDev\Pages\ViewMemberAccount.cshtml"
           Write(Model.LoggedInMember.MemberId);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n        </tr>\r\n        <tr>\r\n            <td>Membership Level:</td>\r\n            <td>");
#nullable restore
#line 16 "C:\Users\chrissmall\source\repos\ClubBAISTDev\ClubBAISTDev\Pages\ViewMemberAccount.cshtml"
           Write(Model.LoggedInMember.MembershipLevel);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n        </tr>\r\n");
#nullable restore
#line 18 "C:\Users\chrissmall\source\repos\ClubBAISTDev\ClubBAISTDev\Pages\ViewMemberAccount.cshtml"
         if (Model.LoggedInMember.MembershipType != null)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <tr>\r\n                <td>Membership Type:</td>\r\n                <td>");
#nullable restore
#line 22 "C:\Users\chrissmall\source\repos\ClubBAISTDev\ClubBAISTDev\Pages\ViewMemberAccount.cshtml"
               Write(Model.LoggedInMember.MembershipType);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n            </tr>\r\n");
#nullable restore
#line 24 "C:\Users\chrissmall\source\repos\ClubBAISTDev\ClubBAISTDev\Pages\ViewMemberAccount.cshtml"
        }

#line default
#line hidden
#nullable disable
            WriteLiteral("        <tr>\r\n            <td>Name:</td>\r\n            <td>");
#nullable restore
#line 27 "C:\Users\chrissmall\source\repos\ClubBAISTDev\ClubBAISTDev\Pages\ViewMemberAccount.cshtml"
           Write(Model.LoggedInMember.MemberName);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n        </tr>\r\n        <tr>\r\n            <td>Email:</td>\r\n            <td>");
#nullable restore
#line 31 "C:\Users\chrissmall\source\repos\ClubBAISTDev\ClubBAISTDev\Pages\ViewMemberAccount.cshtml"
           Write(Model.LoggedInMember.MemberEmail);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n        </tr>\r\n        <tr>\r\n            <td>Membership Standing:</td>\r\n            <td>");
#nullable restore
#line 35 "C:\Users\chrissmall\source\repos\ClubBAISTDev\ClubBAISTDev\Pages\ViewMemberAccount.cshtml"
           Write(Model.LoggedInMember.MemberStanding);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</td>
        </tr>
        <tr>
            <td>Charges:</td>
            <td>
                <table style=""border: 2px solid black"">
                    <tr>
                        <td style=""border: 1px solid black"">Amount</td>
                        <td style=""border: 1px solid black"">When Charged</td>
                    </tr>
");
#nullable restore
#line 45 "C:\Users\chrissmall\source\repos\ClubBAISTDev\ClubBAISTDev\Pages\ViewMemberAccount.cshtml"
                     foreach(var element in Model.AllCharges)
                    {

#line default
#line hidden
#nullable disable
            WriteLiteral("                        <tr>\r\n                            <td style=\"border: 1px solid black\">$");
#nullable restore
#line 48 "C:\Users\chrissmall\source\repos\ClubBAISTDev\ClubBAISTDev\Pages\ViewMemberAccount.cshtml"
                                                            Write(element.Amount);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                            <td style=\"border: 1px solid black\">");
#nullable restore
#line 49 "C:\Users\chrissmall\source\repos\ClubBAISTDev\ClubBAISTDev\Pages\ViewMemberAccount.cshtml"
                                                           Write(element.WhenCharged);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                        </tr>\r\n");
#nullable restore
#line 51 "C:\Users\chrissmall\source\repos\ClubBAISTDev\ClubBAISTDev\Pages\ViewMemberAccount.cshtml"
                    }

#line default
#line hidden
#nullable disable
            WriteLiteral("                </table>\r\n            </td>\r\n        </tr>\r\n        <tr>\r\n            <td>Total Balance:</td>\r\n            <td>$");
#nullable restore
#line 57 "C:\Users\chrissmall\source\repos\ClubBAISTDev\ClubBAISTDev\Pages\ViewMemberAccount.cshtml"
            Write(Model.LoggedInMember.Balance);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n        </tr>\r\n    </table>\r\n    ");
#nullable restore
#line 60 "C:\Users\chrissmall\source\repos\ClubBAISTDev\ClubBAISTDev\Pages\ViewMemberAccount.cshtml"
Write(Model.Message);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n</div>");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<ClubBAISTDev.Pages.ViewMemberAccountModel> Html { get; private set; }
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<ClubBAISTDev.Pages.ViewMemberAccountModel> ViewData => (global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<ClubBAISTDev.Pages.ViewMemberAccountModel>)PageContext?.ViewData;
        public ClubBAISTDev.Pages.ViewMemberAccountModel Model => ViewData.Model;
    }
}
#pragma warning restore 1591
