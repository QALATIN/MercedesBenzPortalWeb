#pragma checksum "D:\Proyectos\Net\MercedesBenzWeb\MercedesBenzServerWeb\Shared\NavMenu.razor" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "b208334f6864b942e2e07a4528b104081788def3"
// <auto-generated/>
#pragma warning disable 1591
namespace MercedesBenzServerWeb.Shared
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
    public partial class NavMenu : Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
            __builder.AddMarkupContent(0, "<div class=\"mbz-ctr-header mbz-flx-ah-center\" b-ub4z3nxei1><img src=\"../image/logo-estrella.png\" b-ub4z3nxei1></div>\r\n\r\n");
            __builder.AddMarkupContent(1, "<div class b-ub4z3nxei1><ul class=\"nav flex-column\" b-ub4z3nxei1><li class=\"mbz-nav-menu\" b-ub4z3nxei1><a class=\"mbz-font-STextOffice-B-14 mbz-text-primary\" href=\"index\" b-ub4z3nxei1>Menú</a></li></ul></div>\r\n\r\n");
            __builder.OpenElement(2, "div");
            __builder.AddAttribute(3, "class");
            __builder.AddAttribute(4, "b-ub4z3nxei1");
            __builder.OpenElement(5, "ul");
            __builder.AddAttribute(6, "class", "nav flex-column");
            __builder.AddAttribute(7, "b-ub4z3nxei1");
            __builder.OpenComponent<Microsoft.AspNetCore.Components.Authorization.AuthorizeView>(8);
            __builder.AddAttribute(9, "Roles", "Administrador, Análista");
            __builder.AddAttribute(10, "ChildContent", (Microsoft.AspNetCore.Components.RenderFragment<Microsoft.AspNetCore.Components.Authorization.AuthenticationState>)((context) => (__builder2) => {
                __builder2.OpenElement(11, "li");
                __builder2.AddAttribute(12, "class", "mbz-nav");
                __builder2.AddAttribute(13, "b-ub4z3nxei1");
                __builder2.OpenElement(14, "div");
                __builder2.AddAttribute(15, "class", "d-flex");
                __builder2.AddAttribute(16, "b-ub4z3nxei1");
                __builder2.AddMarkupContent(17, "<a class=\"mbz-nav-item mbz-font-STextOffice-B-14 mbz-text-secondary\" href=\"solicitudAdmin\" b-ub4z3nxei1><div class=\"mbz-opc-w-90p\" b-ub4z3nxei1><span class b-ub4z3nxei1>Solicitudes</span></div></a>\r\n                    ");
                __builder2.OpenElement(18, "a");
                __builder2.AddAttribute(19, "class", "mbz-font-STextOffice-B-14 mbz-text-secondary");
                __builder2.AddAttribute(20, "href", "javascript: void(0);");
                __builder2.AddAttribute(21, "onclick", Microsoft.AspNetCore.Components.EventCallback.Factory.Create<Microsoft.AspNetCore.Components.Web.MouseEventArgs>(this, 
#nullable restore
#line 25 "D:\Proyectos\Net\MercedesBenzWeb\MercedesBenzServerWeb\Shared\NavMenu.razor"
                                                                                                                  e => collapSolicitudes = !collapSolicitudes

#line default
#line hidden
#nullable disable
                ));
                __builder2.AddAttribute(22, "b-ub4z3nxei1");
                __builder2.OpenElement(23, "div");
                __builder2.AddAttribute(24, "class", "mbz-opc-w-10p");
                __builder2.AddAttribute(25, "b-ub4z3nxei1");
#nullable restore
#line 27 "D:\Proyectos\Net\MercedesBenzWeb\MercedesBenzServerWeb\Shared\NavMenu.razor"
                             if (collapSolicitudes)
                            {

#line default
#line hidden
#nullable disable
                __builder2.AddMarkupContent(26, "<img src=\"../image/chevron-forward-outline.svg\" style=\"width:20px;height:18px;\" b-ub4z3nxei1>");
#nullable restore
#line 30 "D:\Proyectos\Net\MercedesBenzWeb\MercedesBenzServerWeb\Shared\NavMenu.razor"
                            }
                            else
                            {

#line default
#line hidden
#nullable disable
                __builder2.AddMarkupContent(27, "<img src=\"../image/chevron-down-outline.svg\" style=\"width:20px;height:18px;\" b-ub4z3nxei1>");
#nullable restore
#line 34 "D:\Proyectos\Net\MercedesBenzWeb\MercedesBenzServerWeb\Shared\NavMenu.razor"
                            }

#line default
#line hidden
#nullable disable
                __builder2.CloseElement();
                __builder2.CloseElement();
                __builder2.CloseElement();
                __builder2.AddMarkupContent(28, "\r\n                ");
                __builder2.OpenElement(29, "ul");
                __builder2.AddAttribute(30, "class", "nav flex-column");
                __builder2.AddAttribute(31, "style", "margin-left:" + " 30px;" + " " + (
#nullable restore
#line 38 "D:\Proyectos\Net\MercedesBenzWeb\MercedesBenzServerWeb\Shared\NavMenu.razor"
                                                                        !collapSolicitudes ? string.Empty: "display: none"

#line default
#line hidden
#nullable disable
                ));
                __builder2.AddAttribute(32, "b-ub4z3nxei1");
                __builder2.AddMarkupContent(33, "<li class=\"mbz-nav\" b-ub4z3nxei1><a class=\"mbz-nav-item mbz-font-STextOffice-14 mbz-text-secondary\" href=\"solicitudBuscar\" b-ub4z3nxei1><span class b-ub4z3nxei1>Buscar</span></a></li>\r\n                    ");
                __builder2.OpenElement(34, "li");
                __builder2.AddAttribute(35, "class", "mbz-nav");
                __builder2.AddAttribute(36, "b-ub4z3nxei1");
                __builder2.OpenElement(37, "a");
                __builder2.AddAttribute(38, "class", "mbz-nav-item mbz-font-STextOffice-14 mbz-text-secondary");
                __builder2.AddAttribute(39, "href", 
#nullable restore
#line 45 "D:\Proyectos\Net\MercedesBenzWeb\MercedesBenzServerWeb\Shared\NavMenu.razor"
                                                                                                   $"solicitudesNuevas/{(int)@SolicitudEstatus.Nuevas}"

#line default
#line hidden
#nullable disable
                );
                __builder2.AddAttribute(40, "b-ub4z3nxei1");
                __builder2.AddMarkupContent(41, "<span class b-ub4z3nxei1>Nuevas</span>");
                __builder2.CloseElement();
                __builder2.CloseElement();
                __builder2.AddMarkupContent(42, "\r\n                    ");
                __builder2.OpenElement(43, "li");
                __builder2.AddAttribute(44, "class", "mbz-nav");
                __builder2.AddAttribute(45, "b-ub4z3nxei1");
                __builder2.OpenElement(46, "a");
                __builder2.AddAttribute(47, "class", "mbz-nav-item mbz-font-STextOffice-14 mbz-text-secondary");
                __builder2.AddAttribute(48, "href", 
#nullable restore
#line 50 "D:\Proyectos\Net\MercedesBenzWeb\MercedesBenzServerWeb\Shared\NavMenu.razor"
                                                                                                   $"solicitudesProceso/{(int)@SolicitudEstatus.Proceso}"

#line default
#line hidden
#nullable disable
                );
                __builder2.AddAttribute(49, "b-ub4z3nxei1");
                __builder2.AddMarkupContent(50, "<span class b-ub4z3nxei1>En proceso</span>");
                __builder2.CloseElement();
                __builder2.CloseElement();
                __builder2.AddMarkupContent(51, "\r\n                    ");
                __builder2.OpenElement(52, "li");
                __builder2.AddAttribute(53, "class", "mbz-nav");
                __builder2.AddAttribute(54, "b-ub4z3nxei1");
                __builder2.OpenElement(55, "a");
                __builder2.AddAttribute(56, "class", "mbz-nav-item mbz-font-STextOffice-14 mbz-text-secondary");
                __builder2.AddAttribute(57, "href", 
#nullable restore
#line 55 "D:\Proyectos\Net\MercedesBenzWeb\MercedesBenzServerWeb\Shared\NavMenu.razor"
                                                                                                   $"solicitudesFinalizadas/{(int)@SolicitudEstatus.Finalizadas}"

#line default
#line hidden
#nullable disable
                );
                __builder2.AddAttribute(58, "b-ub4z3nxei1");
                __builder2.AddMarkupContent(59, "<span class b-ub4z3nxei1>Finalizadas</span>");
                __builder2.CloseElement();
                __builder2.CloseElement();
                __builder2.CloseElement();
                __builder2.CloseElement();
                __builder2.AddMarkupContent(60, "\r\n            ");
                __builder2.OpenElement(61, "li");
                __builder2.AddAttribute(62, "class", "mbz-nav");
                __builder2.AddAttribute(63, "b-ub4z3nxei1");
                __builder2.OpenElement(64, "div");
                __builder2.AddAttribute(65, "class", "d-flex");
                __builder2.AddAttribute(66, "b-ub4z3nxei1");
                __builder2.AddMarkupContent(67, "<a class=\"mbz-nav-item mbz-font-STextOffice-B-14 mbz-text-secondary\" href=\"reporteLista\" b-ub4z3nxei1><div class=\"mbz-opc-w-90p\" b-ub4z3nxei1><span class b-ub4z3nxei1>Reportes</span></div></a>\r\n                    ");
                __builder2.OpenElement(68, "a");
                __builder2.AddAttribute(69, "class", "mbz-font-STextOffice-B-14 mbz-text-secondary");
                __builder2.AddAttribute(70, "href", "javascript: void(0);");
                __builder2.AddAttribute(71, "onclick", Microsoft.AspNetCore.Components.EventCallback.Factory.Create<Microsoft.AspNetCore.Components.Web.MouseEventArgs>(this, 
#nullable restore
#line 68 "D:\Proyectos\Net\MercedesBenzWeb\MercedesBenzServerWeb\Shared\NavMenu.razor"
                                                                                                                  e => collapReportes = !collapReportes

#line default
#line hidden
#nullable disable
                ));
                __builder2.AddAttribute(72, "b-ub4z3nxei1");
                __builder2.OpenElement(73, "div");
                __builder2.AddAttribute(74, "class", "mbz-opc-w-10p");
                __builder2.AddAttribute(75, "b-ub4z3nxei1");
#nullable restore
#line 70 "D:\Proyectos\Net\MercedesBenzWeb\MercedesBenzServerWeb\Shared\NavMenu.razor"
                             if (collapReportes)
                            {

#line default
#line hidden
#nullable disable
                __builder2.AddMarkupContent(76, "<img src=\"../image/chevron-forward-outline.svg\" style=\"width:20px;height:18px;\" b-ub4z3nxei1>");
#nullable restore
#line 73 "D:\Proyectos\Net\MercedesBenzWeb\MercedesBenzServerWeb\Shared\NavMenu.razor"
                            }
                            else
                            {

#line default
#line hidden
#nullable disable
                __builder2.AddMarkupContent(77, "<img src=\"../image/chevron-down-outline.svg\" style=\"width:20px;height:18px;\" b-ub4z3nxei1>");
#nullable restore
#line 77 "D:\Proyectos\Net\MercedesBenzWeb\MercedesBenzServerWeb\Shared\NavMenu.razor"
                            }

#line default
#line hidden
#nullable disable
                __builder2.CloseElement();
                __builder2.CloseElement();
                __builder2.CloseElement();
                __builder2.AddMarkupContent(78, "\r\n                ");
                __builder2.OpenElement(79, "ul");
                __builder2.AddAttribute(80, "class", "nav flex-column");
                __builder2.AddAttribute(81, "style", "margin-left:" + " 30px;" + " " + (
#nullable restore
#line 81 "D:\Proyectos\Net\MercedesBenzWeb\MercedesBenzServerWeb\Shared\NavMenu.razor"
                                                                        !collapReportes ? string.Empty: "display: none"

#line default
#line hidden
#nullable disable
                ));
                __builder2.AddAttribute(82, "b-ub4z3nxei1");
                __builder2.AddMarkupContent(83, "<li class=\"mbz-nav\" b-ub4z3nxei1><a class=\"mbz-nav-item mbz-font-STextOffice-14 mbz-text-secondary\" href=\"reporteSemaforos\" b-ub4z3nxei1><span class b-ub4z3nxei1>Semáforos</span></a></li>\r\n                    ");
                __builder2.AddMarkupContent(84, "<li class=\"mbz-nav\" b-ub4z3nxei1><a class=\"mbz-nav-item mbz-font-STextOffice-14 mbz-text-secondary\" href=\"reporteEnvioDetalles\" b-ub4z3nxei1><span class b-ub4z3nxei1>Detalle de envío</span></a></li>\r\n                    ");
                __builder2.AddMarkupContent(85, "<li class=\"mbz-nav\" b-ub4z3nxei1><a class=\"mbz-nav-item mbz-font-STextOffice-14 mbz-text-secondary\" href=\"reporteSemaforoFacialDetalles\" b-ub4z3nxei1><span class b-ub4z3nxei1>Comparativo facial</span></a></li>\r\n                    ");
                __builder2.AddMarkupContent(86, "<li class=\"mbz-nav\" b-ub4z3nxei1><a class=\"mbz-nav-item mbz-font-STextOffice-14 mbz-text-secondary\" href=\"reporteBitacoras\" b-ub4z3nxei1><span class b-ub4z3nxei1>Bitácora</span></a></li>\r\n                    ");
                __builder2.AddMarkupContent(87, "<li class=\"mbz-nav\" b-ub4z3nxei1><a class=\"mbz-nav-item mbz-font-STextOffice-14 mbz-text-secondary\" href=\"reporteListasNegras\" b-ub4z3nxei1><span class b-ub4z3nxei1>Lista Negra</span></a></li>");
                __builder2.CloseElement();
                __builder2.CloseElement();
            }
            ));
            __builder.CloseComponent();
            __builder.AddMarkupContent(88, "\r\n\r\n        ");
            __builder.OpenComponent<Microsoft.AspNetCore.Components.Authorization.AuthorizeView>(89);
            __builder.AddAttribute(90, "Roles", "Administrador");
            __builder.AddAttribute(91, "ChildContent", (Microsoft.AspNetCore.Components.RenderFragment<Microsoft.AspNetCore.Components.Authorization.AuthenticationState>)((context) => (__builder2) => {
                __builder2.OpenElement(92, "li");
                __builder2.AddAttribute(93, "class", "mbz-nav");
                __builder2.AddAttribute(94, "b-ub4z3nxei1");
                __builder2.OpenElement(95, "div");
                __builder2.AddAttribute(96, "class", "d-flex");
                __builder2.AddAttribute(97, "b-ub4z3nxei1");
                __builder2.AddMarkupContent(98, "<a class=\"mbz-nav-item mbz-font-STextOffice-B-14 mbz-text-secondary\" href=\"usuarioAdmin\" b-ub4z3nxei1><div class=\"mbz-opc-w-90p\" b-ub4z3nxei1><span class b-ub4z3nxei1>Administración de usuarios</span></div></a>\r\n                    ");
                __builder2.OpenElement(99, "a");
                __builder2.AddAttribute(100, "class", "mbz-font-STextOffice-B-14 mbz-text-secondary");
                __builder2.AddAttribute(101, "href", "javascript: void(0);");
                __builder2.AddAttribute(102, "onclick", Microsoft.AspNetCore.Components.EventCallback.Factory.Create<Microsoft.AspNetCore.Components.Web.MouseEventArgs>(this, 
#nullable restore
#line 119 "D:\Proyectos\Net\MercedesBenzWeb\MercedesBenzServerWeb\Shared\NavMenu.razor"
                                                                                                                  e => collapUsuarios  = !collapUsuarios 

#line default
#line hidden
#nullable disable
                ));
                __builder2.AddAttribute(103, "b-ub4z3nxei1");
                __builder2.OpenElement(104, "div");
                __builder2.AddAttribute(105, "class", "mbz-opc-w-10p");
                __builder2.AddAttribute(106, "b-ub4z3nxei1");
#nullable restore
#line 121 "D:\Proyectos\Net\MercedesBenzWeb\MercedesBenzServerWeb\Shared\NavMenu.razor"
                             if (collapUsuarios)
                            {

#line default
#line hidden
#nullable disable
                __builder2.AddMarkupContent(107, "<img src=\"../image/chevron-forward-outline.svg\" style=\"width:20px;height:18px;\" b-ub4z3nxei1>");
#nullable restore
#line 124 "D:\Proyectos\Net\MercedesBenzWeb\MercedesBenzServerWeb\Shared\NavMenu.razor"
                            }
                            else
                            {

#line default
#line hidden
#nullable disable
                __builder2.AddMarkupContent(108, "<img src=\"../image/chevron-down-outline.svg\" style=\"width:20px;height:18px;\" b-ub4z3nxei1>");
#nullable restore
#line 128 "D:\Proyectos\Net\MercedesBenzWeb\MercedesBenzServerWeb\Shared\NavMenu.razor"
                            }

#line default
#line hidden
#nullable disable
                __builder2.CloseElement();
                __builder2.CloseElement();
                __builder2.CloseElement();
                __builder2.AddMarkupContent(109, "\r\n                ");
                __builder2.OpenElement(110, "ul");
                __builder2.AddAttribute(111, "class", "nav flex-column");
                __builder2.AddAttribute(112, "style", "margin-left:" + " 30px;" + " " + (
#nullable restore
#line 132 "D:\Proyectos\Net\MercedesBenzWeb\MercedesBenzServerWeb\Shared\NavMenu.razor"
                                                                        !collapUsuarios  ? string.Empty: "display: none"

#line default
#line hidden
#nullable disable
                ));
                __builder2.AddAttribute(113, "b-ub4z3nxei1");
                __builder2.AddMarkupContent(114, "<li class=\"mbz-nav\" b-ub4z3nxei1><a class=\"mbz-nav-item mbz-font-STextOffice-14 mbz-text-secondary\" href=\"usuarioNuevo\" b-ub4z3nxei1><span class b-ub4z3nxei1>Agregar usuarios</span></a></li>\r\n                    ");
                __builder2.AddMarkupContent(115, "<li class=\"mbz-nav\" b-ub4z3nxei1><a class=\"mbz-nav-item mbz-font-STextOffice-14 mbz-text-secondary\" href=\"usuarioLista\" b-ub4z3nxei1><span class b-ub4z3nxei1>Lista de usuarios</span></a></li>");
                __builder2.CloseElement();
                __builder2.CloseElement();
                __builder2.AddMarkupContent(116, "\r\n            ");
                __builder2.OpenElement(117, "li");
                __builder2.AddAttribute(118, "class", "mbz-nav");
                __builder2.AddAttribute(119, "b-ub4z3nxei1");
                __builder2.OpenElement(120, "div");
                __builder2.AddAttribute(121, "class", "d-flex");
                __builder2.AddAttribute(122, "b-ub4z3nxei1");
                __builder2.AddMarkupContent(123, "<a class=\"mbz-nav-item mbz-font-STextOffice-B-14 mbz-text-secondary\" href=\"agenciaAdmin\" b-ub4z3nxei1><div class=\"mbz-opc-w-90p\" b-ub4z3nxei1><span class b-ub4z3nxei1>Administración de agencias</span></div></a>\r\n                    ");
                __builder2.OpenElement(124, "a");
                __builder2.AddAttribute(125, "class", "mbz-font-STextOffice-B-14 mbz-text-secondary");
                __builder2.AddAttribute(126, "href", "javascript: void(0);");
                __builder2.AddAttribute(127, "onclick", Microsoft.AspNetCore.Components.EventCallback.Factory.Create<Microsoft.AspNetCore.Components.Web.MouseEventArgs>(this, 
#nullable restore
#line 152 "D:\Proyectos\Net\MercedesBenzWeb\MercedesBenzServerWeb\Shared\NavMenu.razor"
                                                                                                                  e => collapAgencias  = !collapAgencias 

#line default
#line hidden
#nullable disable
                ));
                __builder2.AddAttribute(128, "b-ub4z3nxei1");
                __builder2.OpenElement(129, "div");
                __builder2.AddAttribute(130, "class", "mbz-opc-w-10p");
                __builder2.AddAttribute(131, "b-ub4z3nxei1");
#nullable restore
#line 154 "D:\Proyectos\Net\MercedesBenzWeb\MercedesBenzServerWeb\Shared\NavMenu.razor"
                             if (collapAgencias)
                            {

#line default
#line hidden
#nullable disable
                __builder2.AddMarkupContent(132, "<img src=\"../image/chevron-forward-outline.svg\" style=\"width:20px;height:18px;\" b-ub4z3nxei1>");
#nullable restore
#line 157 "D:\Proyectos\Net\MercedesBenzWeb\MercedesBenzServerWeb\Shared\NavMenu.razor"
                            }
                            else
                            {

#line default
#line hidden
#nullable disable
                __builder2.AddMarkupContent(133, "<img src=\"../image/chevron-down-outline.svg\" style=\"width:20px;height:18px;\" b-ub4z3nxei1>");
#nullable restore
#line 161 "D:\Proyectos\Net\MercedesBenzWeb\MercedesBenzServerWeb\Shared\NavMenu.razor"
                            }

#line default
#line hidden
#nullable disable
                __builder2.CloseElement();
                __builder2.CloseElement();
                __builder2.CloseElement();
                __builder2.AddMarkupContent(134, "\r\n                ");
                __builder2.OpenElement(135, "ul");
                __builder2.AddAttribute(136, "class", "nav flex-column");
                __builder2.AddAttribute(137, "style", "margin-left:" + " 30px;" + " " + (
#nullable restore
#line 165 "D:\Proyectos\Net\MercedesBenzWeb\MercedesBenzServerWeb\Shared\NavMenu.razor"
                                                                        !collapAgencias  ? string.Empty: "display: none"

#line default
#line hidden
#nullable disable
                ));
                __builder2.AddAttribute(138, "b-ub4z3nxei1");
                __builder2.AddMarkupContent(139, "<li class=\"mbz-nav\" b-ub4z3nxei1><a class=\"mbz-nav-item mbz-font-STextOffice-14 mbz-text-secondary\" href=\"agenciaNueva\" b-ub4z3nxei1><span class b-ub4z3nxei1>Agregar agencia</span></a></li>\r\n                    ");
                __builder2.AddMarkupContent(140, "<li class=\"mbz-nav\" b-ub4z3nxei1><a class=\"mbz-nav-item mbz-font-STextOffice-14 mbz-text-secondary\" href=\"agenciaLista\" b-ub4z3nxei1><span class b-ub4z3nxei1>Lista de agencias</span></a></li>");
                __builder2.CloseElement();
                __builder2.CloseElement();
            }
            ));
            __builder.CloseComponent();
            __builder.AddMarkupContent(141, "\r\n        ");
            __builder.OpenElement(142, "li");
            __builder.AddAttribute(143, "class", "mbz-nav");
            __builder.AddAttribute(144, "b-ub4z3nxei1");
            __builder.OpenElement(145, "a");
            __builder.AddAttribute(146, "class", "mbz-font-STextOffice-B-14 mbz-text-secondary");
            __builder.AddAttribute(147, "href", "javascript: void(0);");
            __builder.AddAttribute(148, "onclick", Microsoft.AspNetCore.Components.EventCallback.Factory.Create<Microsoft.AspNetCore.Components.Web.MouseEventArgs>(this, 
#nullable restore
#line 180 "D:\Proyectos\Net\MercedesBenzWeb\MercedesBenzServerWeb\Shared\NavMenu.razor"
                                                                                                          Logout

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(149, "b-ub4z3nxei1");
            __builder.AddMarkupContent(150, "<div class=\"mbz-opc-w-90p\" b-ub4z3nxei1><span class b-ub4z3nxei1>Cerrar sesión</span></div>\r\n                <div class=\"mbz-opc-w-10p\" b-ub4z3nxei1></div>");
            __builder.CloseElement();
            __builder.CloseElement();
            __builder.CloseElement();
            __builder.CloseElement();
        }
        #pragma warning restore 1998
#nullable restore
#line 191 "D:\Proyectos\Net\MercedesBenzWeb\MercedesBenzServerWeb\Shared\NavMenu.razor"
       
    private bool collapSolicitudes = true;
    private bool collapReportes = true;
    private bool collapUsuarios = true;
    private bool collapAgencias = true;

    private void Logout()
    {
        AppState.ShowPopup(true);
    }


#line default
#line hidden
#nullable disable
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private Services.IUsuarioService ServiceUsuario { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private BusquedaRequest Busqueda { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private UserCredential Credential { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private AppState AppState { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private IJSRuntime JS { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private NavigationManager NavigationManager { get; set; }
    }
}
#pragma warning restore 1591
