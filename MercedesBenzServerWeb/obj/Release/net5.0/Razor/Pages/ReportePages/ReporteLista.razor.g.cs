#pragma checksum "D:\Proyectos\Net\MercedesBenzWeb\MercedesBenzServerWeb\Pages\ReportePages\ReporteLista.razor" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "a9e35ea7452679321a10c9c6501f7042e0e8dcac"
// <auto-generated/>
#pragma warning disable 1591
namespace MercedesBenzServerWeb.Pages.ReportePages
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components;
#nullable restore
#line 1 "D:\Proyectos\Net\MercedesBenzWeb\MercedesBenzServerWeb\_Imports.razor"
using System.Net.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\Proyectos\Net\MercedesBenzWeb\MercedesBenzServerWeb\_Imports.razor"
using Microsoft.AspNetCore.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "D:\Proyectos\Net\MercedesBenzWeb\MercedesBenzServerWeb\_Imports.razor"
using Microsoft.AspNetCore.Components.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "D:\Proyectos\Net\MercedesBenzWeb\MercedesBenzServerWeb\_Imports.razor"
using Microsoft.AspNetCore.Components.Forms;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "D:\Proyectos\Net\MercedesBenzWeb\MercedesBenzServerWeb\_Imports.razor"
using Microsoft.AspNetCore.Components.Routing;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "D:\Proyectos\Net\MercedesBenzWeb\MercedesBenzServerWeb\_Imports.razor"
using Microsoft.AspNetCore.Components.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "D:\Proyectos\Net\MercedesBenzWeb\MercedesBenzServerWeb\_Imports.razor"
using Microsoft.AspNetCore.Components.Web.Virtualization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 8 "D:\Proyectos\Net\MercedesBenzWeb\MercedesBenzServerWeb\_Imports.razor"
using Microsoft.JSInterop;

#line default
#line hidden
#nullable disable
#nullable restore
#line 9 "D:\Proyectos\Net\MercedesBenzWeb\MercedesBenzServerWeb\_Imports.razor"
using MercedesBenzServerWeb;

#line default
#line hidden
#nullable disable
#nullable restore
#line 10 "D:\Proyectos\Net\MercedesBenzWeb\MercedesBenzServerWeb\_Imports.razor"
using MercedesBenzServerWeb.Shared;

#line default
#line hidden
#nullable disable
#nullable restore
#line 11 "D:\Proyectos\Net\MercedesBenzWeb\MercedesBenzServerWeb\_Imports.razor"
using MercedesBenzModel;

#line default
#line hidden
#nullable disable
#nullable restore
#line 12 "D:\Proyectos\Net\MercedesBenzWeb\MercedesBenzServerWeb\_Imports.razor"
using Helpers;

#line default
#line hidden
#nullable disable
#nullable restore
#line 13 "D:\Proyectos\Net\MercedesBenzWeb\MercedesBenzServerWeb\_Imports.razor"
using MercedesBenzServerWeb.Authentication;

#line default
#line hidden
#nullable disable
#nullable restore
#line 14 "D:\Proyectos\Net\MercedesBenzWeb\MercedesBenzServerWeb\_Imports.razor"
using Newtonsoft.Json;

#line default
#line hidden
#nullable disable
#nullable restore
#line 15 "D:\Proyectos\Net\MercedesBenzWeb\MercedesBenzServerWeb\_Imports.razor"
using BlazorPro.Spinkit;

#line default
#line hidden
#nullable disable
    [Microsoft.AspNetCore.Components.RouteAttribute("/reporteLista")]
    public partial class ReporteLista : Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
            __builder.AddMarkupContent(0, "<div class=\"mbz-row-center\"><div class=\"mbz-card-titulo-1 \"><span class=\"mbz-font-ATitleCondOffice-40 mbz-text-secondary\">Reportes</span></div></div>\r\n\r\n");
            __builder.AddMarkupContent(1, @"<div class=""mbz-row-center""><div class=""mbz-card-center""><a href=""reporteSemaforos""><div class=""mbz-card-reporte-opcion mbz-text-dark""><div class=""p-2""></div>
                <div><img src=""../image/reportes/Semaforos.png""></div>
                <div class=""p-2""><span>Semáforos</span></div></div></a></div>
    <div class=""mbz-card-center""><a href=""reporteEnvioDetalles""><div class=""mbz-card-reporte-opcion mbz-text-dark""><div class=""p-2""></div>
                <div><img src=""../image/reportes/Envios.png""></div>
                <div class=""p-2""><span>Detalle de envío</span></div></div></a></div>
    <div class=""mbz-card-center""><a href=""reporteSemaforoFacialDetalles""><div class=""mbz-card-reporte-opcion mbz-text-dark""><div class=""p-2""></div>
                <div><img src=""../image/reportes/ComparativoFacial.png""></div>
                <div class=""p-2""><span>Comparativo facial</span></div></div></a></div></div>

");
            __builder.AddMarkupContent(2, @"<div class=""mbz-row-center""><div class=""mbz-card-center""><a href=""reporteBitacoras""><div class=""mbz-card-reporte-opcion mbz-text-dark""><div class=""p-2""></div>
                <div><img src=""../image/reportes/Bitacora.png""></div>
                <div class=""p-2""><span>Bitácora</span></div></div></a></div>
    <div class=""mbz-card-center""><a href=""reporteListasNegras""><div class=""mbz-card-reporte-opcion mbz-text-dark""><div class=""p-2""></div>
                <div><img src=""../image/reportes/ListaNegra.png""></div>
                <div class=""p-2""><span>Lista Negra</span></div></div></a></div>
    <div class=""mbz-card-center""><div class=""mbz-card-reporte-opcion border-0""></div></div></div>");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private BusquedaRequest Busqueda { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private UserCredential Credential { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private AppState AppState { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private IJSRuntime JS { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private NavigationManager NavigationManager { get; set; }
    }
}
#pragma warning restore 1591
