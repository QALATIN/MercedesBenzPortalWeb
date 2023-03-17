// <auto-generated/>
#pragma warning disable 1591
#pragma warning disable 0414
#pragma warning disable 0649
#pragma warning disable 0169

namespace MercedesBenzServerWeb.Pages.SolicitudesPages
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
    public partial class SemaforoHuellas : Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
        }
        #pragma warning restore 1998
#nullable restore
#line 267 "D:\Proyectos\Net\MercedesBenzWeb\MercedesBenzServerWeb\Pages\SolicitudesPages\SemaforoHuellas.razor"
       
    [Parameter] public SolicitudComparacionHuellas Model { get; set; }
    [Parameter] public EventCallback<int> OnAccion { get; set; }

    private int manoIzquierdaPulgar = -1;
    private int manoIzquierdaIndice = -1;
    private int manoIzquierdaMedio = -1;
    private int manoIzquierdaAnular = -1;
    private int manoIzquierdaMenique = -1;
    private int manoDerechaPulgar = -1;
    private int manoDerechaIndice = -1;
    private int manoDerechaMedio = -1;
    private int manoDerechaAnular = -1;
    private int manoDerechaMenique = -1;
    private string manoIzquierdaPulgarEstatus = "Sin información";
    private string manoIzquierdaIndiceEstatus = "Sin información";
    private string manoIzquierdaMedioEstatus = "Sin información";
    private string manoIzquierdaAnularEstatus = "Sin información";
    private string manoIzquierdaMeniqueEstatus = "Sin información";
    private string manoDerechaPulgarEstatus = "Sin información";
    private string manoDerechaIndiceEstatus = "Sin información";
    private string manoDerechaMedioEstatus = "Sin información";
    private string manoDerechaAnularEstatus = "Sin información";
    private string manoDerechaMeniqueEstatus = "Sin información";
    private string colorClase = "mbz-text-gray";
    private string iconoMostrar = "../image/Exclamacion.png";

    protected override void OnParametersSet()
    {

        int indice = 0;
        foreach (var item in Model.Huellas)
        {

            switch (item.DedoIndiceId)
            {
                case 1:
                    if (item.Imagen != null)
                        manoDerechaPulgar = indice;
                    manoDerechaPulgarEstatus = item.DedoEstatusNombre;
                    break;
                case 2:
                    if (item.Imagen != null)
                        manoDerechaIndice = indice;
                    manoDerechaIndiceEstatus = item.DedoEstatusNombre;
                    break;
                case 3:
                    if (item.Imagen != null)
                        manoDerechaMedio = indice;
                    manoDerechaMedioEstatus = item.DedoEstatusNombre;
                    break;
                case 4:
                    if (item.Imagen != null)
                        manoDerechaAnular = indice;
                    manoDerechaAnularEstatus = item.DedoEstatusNombre;
                    break;
                case 5:
                    if (item.Imagen != null)
                        manoDerechaMenique = indice;
                    manoDerechaMeniqueEstatus = item.DedoEstatusNombre;
                    break;
                case 6:
                    if (item.Imagen != null)
                        manoIzquierdaPulgar = indice;
                    manoIzquierdaPulgarEstatus = item.DedoEstatusNombre;
                    break;
                case 7:
                    if (item.Imagen != null)
                        manoIzquierdaIndice = indice;
                    manoIzquierdaIndiceEstatus = item.DedoEstatusNombre;
                    break;
                case 8:
                    if (item.Imagen != null)
                        manoIzquierdaMedio = indice;
                    manoIzquierdaMedioEstatus = item.DedoEstatusNombre;
                    break;
                case 9:
                    if (item.Imagen != null)
                        manoIzquierdaAnular = indice;
                    manoIzquierdaAnularEstatus = item.DedoEstatusNombre;
                    break;
                case 10:
                    if (item.Imagen != null)
                        manoIzquierdaMenique = indice;
                    manoIzquierdaMeniqueEstatus = item.DedoEstatusNombre;
                    break;
            }
            indice += 1;
        }
        switch (Model.Resultado.Semaforo.ToUpper())
        {
            case "VERDE":
                colorClase = "mbz-text-green";
                iconoMostrar = "../image/Exito.png";
                break;
            case "AMARILLO":
                colorClase = "mbz-text-yellow";
                iconoMostrar = "../image/Exclamacion.png";
                break;
            case "ROJO":
                colorClase = "mbz-text-red";
                iconoMostrar = "../image/Exclamacion.png";
                break;
            default:
                colorClase = "mbz-text-gray";
                iconoMostrar = "";
                break;
        }

    }

    private Task OnAccionOk(int Id)
    {
        return OnAccion.InvokeAsync(Id);
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
