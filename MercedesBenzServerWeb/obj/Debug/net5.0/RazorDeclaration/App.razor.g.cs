// <auto-generated/>
#pragma warning disable 1591
#pragma warning disable 0414
#pragma warning disable 0649
#pragma warning disable 0169

namespace MercedesBenzServerWeb
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components;
#nullable restore
#line 1 "E:\MERCEDES\MercedesBenzWeb\MercedesBenzServerWeb\_Imports.razor"
using System.Net.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "E:\MERCEDES\MercedesBenzWeb\MercedesBenzServerWeb\_Imports.razor"
using Microsoft.AspNetCore.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "E:\MERCEDES\MercedesBenzWeb\MercedesBenzServerWeb\_Imports.razor"
using Microsoft.AspNetCore.Components.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "E:\MERCEDES\MercedesBenzWeb\MercedesBenzServerWeb\_Imports.razor"
using Microsoft.AspNetCore.Components.Forms;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "E:\MERCEDES\MercedesBenzWeb\MercedesBenzServerWeb\_Imports.razor"
using Microsoft.AspNetCore.Components.Routing;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "E:\MERCEDES\MercedesBenzWeb\MercedesBenzServerWeb\_Imports.razor"
using Microsoft.AspNetCore.Components.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "E:\MERCEDES\MercedesBenzWeb\MercedesBenzServerWeb\_Imports.razor"
using Microsoft.AspNetCore.Components.Web.Virtualization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 8 "E:\MERCEDES\MercedesBenzWeb\MercedesBenzServerWeb\_Imports.razor"
using Microsoft.JSInterop;

#line default
#line hidden
#nullable disable
#nullable restore
#line 9 "E:\MERCEDES\MercedesBenzWeb\MercedesBenzServerWeb\_Imports.razor"
using MercedesBenzServerWeb;

#line default
#line hidden
#nullable disable
#nullable restore
#line 10 "E:\MERCEDES\MercedesBenzWeb\MercedesBenzServerWeb\_Imports.razor"
using MercedesBenzServerWeb.Shared;

#line default
#line hidden
#nullable disable
#nullable restore
#line 11 "E:\MERCEDES\MercedesBenzWeb\MercedesBenzServerWeb\_Imports.razor"
using MercedesBenzModel;

#line default
#line hidden
#nullable disable
#nullable restore
#line 12 "E:\MERCEDES\MercedesBenzWeb\MercedesBenzServerWeb\_Imports.razor"
using Helpers;

#line default
#line hidden
#nullable disable
#nullable restore
#line 13 "E:\MERCEDES\MercedesBenzWeb\MercedesBenzServerWeb\_Imports.razor"
using MercedesBenzServerWeb.Authentication;

#line default
#line hidden
#nullable disable
#nullable restore
#line 14 "E:\MERCEDES\MercedesBenzWeb\MercedesBenzServerWeb\_Imports.razor"
using Newtonsoft.Json;

#line default
#line hidden
#nullable disable
#nullable restore
#line 15 "E:\MERCEDES\MercedesBenzWeb\MercedesBenzServerWeb\_Imports.razor"
using BlazorPro.Spinkit;

#line default
#line hidden
#nullable disable
    public partial class App : Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
        }
        #pragma warning restore 1998
#nullable restore
#line 14 "E:\MERCEDES\MercedesBenzWeb\MercedesBenzServerWeb\App.razor"
      
    [Parameter] public UserIpAddress userIpAddress { get; set; }

    protected override Task OnInitializedAsync()
    {
        Credential.UserAgent = userIpAddress.UserAgent;
        Credential.RemoteIpAddress = userIpAddress.RemoteIpAddress;
        Credential.ForwardedIpAddress = userIpAddress.ForwardedIpAddress;

        return base.OnInitializedAsync();
    }

#line default
#line hidden
#nullable disable
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private BusquedaRequest Busqueda { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private UserCredential Credential { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private AppState AppState { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private IJSRuntime JS { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private NavigationManager NavigationManager { get; set; }
    }
}
#pragma warning restore 1591
