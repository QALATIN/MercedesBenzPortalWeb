#pragma checksum "D:\Proyectos\Net\MercedesBenzWeb\MercedesBenzServerWeb\Pages\LoginPages\Login.razor" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "7d25feb88588fa81ede7151a7d87d852b70945e6"
// <auto-generated/>
#pragma warning disable 1591
namespace MercedesBenzServerWeb.Pages.LoginPages
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
    [Microsoft.AspNetCore.Components.LayoutAttribute(typeof(LoginLayout))]
    [Microsoft.AspNetCore.Components.RouteAttribute("/login")]
    public partial class Login : Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
            __builder.OpenElement(0, "div");
            __builder.AddAttribute(1, "class", "mbz-ctr mbz-bg-login");
            __builder.AddMarkupContent(2, "<div class=\"mbz-ctr-login-head\"></div>\r\n\r\n    ");
            __builder.OpenElement(3, "div");
            __builder.AddAttribute(4, "class", "mbz-ctr-login");
            __builder.AddMarkupContent(5, "<div class=\"mbz-ctr-login-title\"><span>Iniciar sesión</span></div>\r\n\r\n        ");
            __builder.OpenElement(6, "div");
            __builder.AddAttribute(7, "class", "mbz-ctr-login-body");
            __builder.OpenComponent<Microsoft.AspNetCore.Components.Forms.EditForm>(8);
            __builder.AddAttribute(9, "Model", global::Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.Object>(
#nullable restore
#line 19 "D:\Proyectos\Net\MercedesBenzWeb\MercedesBenzServerWeb\Pages\LoginPages\Login.razor"
                              usuario

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(10, "OnValidSubmit", global::Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<Microsoft.AspNetCore.Components.EventCallback<Microsoft.AspNetCore.Components.Forms.EditContext>>(Microsoft.AspNetCore.Components.EventCallback.Factory.Create<Microsoft.AspNetCore.Components.Forms.EditContext>(this, 
#nullable restore
#line 19 "D:\Proyectos\Net\MercedesBenzWeb\MercedesBenzServerWeb\Pages\LoginPages\Login.razor"
                                                       OnValidSubmit

#line default
#line hidden
#nullable disable
            )));
            __builder.AddAttribute(11, "ChildContent", (Microsoft.AspNetCore.Components.RenderFragment<Microsoft.AspNetCore.Components.Forms.EditContext>)((context) => (__builder2) => {
                __builder2.OpenComponent<Microsoft.AspNetCore.Components.Forms.DataAnnotationsValidator>(12);
                __builder2.CloseComponent();
                __builder2.AddMarkupContent(13, "\r\n                ");
                __builder2.OpenElement(14, "div");
                __builder2.AddAttribute(15, "class", "form-group pb-3");
                __builder2.OpenComponent<Microsoft.AspNetCore.Components.Forms.InputText>(16);
                __builder2.AddAttribute(17, "id", "LoginInputUsuario");
                __builder2.AddAttribute(18, "class", "form-control mbz-font-STextOffice-16 mbz-form-control");
                __builder2.AddAttribute(19, "style", "height: 56px; box-shadow: none; -webkit-box-shadow: none; -moz-box-shadow: none; ");
                __builder2.AddAttribute(20, "autocomplete", "username");
                __builder2.AddAttribute(21, "placeholder", "Usuario");
                __builder2.AddAttribute(22, "Value", global::Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.String>(
#nullable restore
#line 22 "D:\Proyectos\Net\MercedesBenzWeb\MercedesBenzServerWeb\Pages\LoginPages\Login.razor"
                                                                                                                                                                                                                                                                          usuario.NombreUsuario

#line default
#line hidden
#nullable disable
                ));
                __builder2.AddAttribute(23, "ValueChanged", global::Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<Microsoft.AspNetCore.Components.EventCallback<System.String>>(Microsoft.AspNetCore.Components.EventCallback.Factory.Create<System.String>(this, global::Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.CreateInferredEventCallback(this, __value => usuario.NombreUsuario = __value, usuario.NombreUsuario))));
                __builder2.AddAttribute(24, "ValueExpression", global::Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.Linq.Expressions.Expression<System.Func<System.String>>>(() => usuario.NombreUsuario));
                __builder2.CloseComponent();
                __builder2.AddMarkupContent(25, "\r\n                    ");
                __Blazor.MercedesBenzServerWeb.Pages.LoginPages.Login.TypeInference.CreateValidationMessage_0(__builder2, 26, 27, 
#nullable restore
#line 23 "D:\Proyectos\Net\MercedesBenzWeb\MercedesBenzServerWeb\Pages\LoginPages\Login.razor"
                                              () => usuario.NombreUsuario

#line default
#line hidden
#nullable disable
                , 28, "mbz-font-STextOffice-16");
                __builder2.CloseElement();
                __builder2.AddMarkupContent(29, "\r\n                ");
                __builder2.OpenElement(30, "div");
                __builder2.AddAttribute(31, "class", "form-group");
                __builder2.OpenComponent<Microsoft.AspNetCore.Components.Forms.InputText>(32);
                __builder2.AddAttribute(33, "id", "LoginInputPassword");
                __builder2.AddAttribute(34, "class", "form-control mbz-font-STextOffice-16 mbz-form-control");
                __builder2.AddAttribute(35, "style", "height: 56px; box-shadow: none; -webkit-box-shadow: none; -moz-box-shadow: none; ");
                __builder2.AddAttribute(36, "autocomplete", "current-password");
                __builder2.AddAttribute(37, "type", "password");
                __builder2.AddAttribute(38, "placeholder", "Contraseña");
                __builder2.AddAttribute(39, "maxlength", "15");
                __builder2.AddAttribute(40, "Value", global::Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.String>(
#nullable restore
#line 26 "D:\Proyectos\Net\MercedesBenzWeb\MercedesBenzServerWeb\Pages\LoginPages\Login.razor"
                                                                                                                                                                                                                                                                                                                     usuario.Password

#line default
#line hidden
#nullable disable
                ));
                __builder2.AddAttribute(41, "ValueChanged", global::Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<Microsoft.AspNetCore.Components.EventCallback<System.String>>(Microsoft.AspNetCore.Components.EventCallback.Factory.Create<System.String>(this, global::Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.CreateInferredEventCallback(this, __value => usuario.Password = __value, usuario.Password))));
                __builder2.AddAttribute(42, "ValueExpression", global::Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.Linq.Expressions.Expression<System.Func<System.String>>>(() => usuario.Password));
                __builder2.CloseComponent();
                __builder2.AddMarkupContent(43, "\r\n                    ");
                __Blazor.MercedesBenzServerWeb.Pages.LoginPages.Login.TypeInference.CreateValidationMessage_1(__builder2, 44, 45, 
#nullable restore
#line 27 "D:\Proyectos\Net\MercedesBenzWeb\MercedesBenzServerWeb\Pages\LoginPages\Login.razor"
                                              () => usuario.Password

#line default
#line hidden
#nullable disable
                );
                __builder2.CloseElement();
#nullable restore
#line 30 "D:\Proyectos\Net\MercedesBenzWeb\MercedesBenzServerWeb\Pages\LoginPages\Login.razor"
                 if (!string.IsNullOrEmpty(mensaje))
                {

#line default
#line hidden
#nullable disable
                __builder2.OpenElement(46, "div");
                __builder2.AddAttribute(47, "class", "mbz-font-STextOffice-B-14 mbz-text-red pt-2");
                __builder2.AddContent(48, 
#nullable restore
#line 32 "D:\Proyectos\Net\MercedesBenzWeb\MercedesBenzServerWeb\Pages\LoginPages\Login.razor"
                                                                              mensaje

#line default
#line hidden
#nullable disable
                );
                __builder2.CloseElement();
#nullable restore
#line 33 "D:\Proyectos\Net\MercedesBenzWeb\MercedesBenzServerWeb\Pages\LoginPages\Login.razor"
                }

#line default
#line hidden
#nullable disable
                __builder2.OpenComponent<BlazorPro.Spinkit.SpinLoader>(49);
                __builder2.AddAttribute(50, "IsLoading", global::Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.Boolean>(
#nullable restore
#line 35 "D:\Proyectos\Net\MercedesBenzWeb\MercedesBenzServerWeb\Pages\LoginPages\Login.razor"
                                       isLoadingData

#line default
#line hidden
#nullable disable
                ));
                __builder2.AddAttribute(51, "LoadingTemplate", (Microsoft.AspNetCore.Components.RenderFragment)((__builder3) => {
                    __builder3.OpenElement(52, "div");
                    __builder3.AddAttribute(53, "class");
                    __builder3.AddMarkupContent(54, "<br>\r\n                            ");
                    __builder3.OpenComponent<BlazorPro.Spinkit.CircleFade>(55);
                    __builder3.AddAttribute(56, "Center", global::Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.Boolean>(
#nullable restore
#line 39 "D:\Proyectos\Net\MercedesBenzWeb\MercedesBenzServerWeb\Pages\LoginPages\Login.razor"
                                                true

#line default
#line hidden
#nullable disable
                    ));
                    __builder3.AddAttribute(57, "Size", "20px");
                    __builder3.AddAttribute(58, "Color", "#232F3B");
                    __builder3.CloseComponent();
                    __builder3.AddMarkupContent(59, "\r\n                            <br>");
                    __builder3.CloseElement();
                }
                ));
                __builder2.AddAttribute(60, "ContentTemplate", (Microsoft.AspNetCore.Components.RenderFragment)((__builder3) => {
                    __builder3.OpenElement(61, "div");
                    __builder3.AddAttribute(62, "class", "form-group pt-4");
                    __builder3.OpenElement(63, "button");
                    __builder3.AddAttribute(64, "type", "submit");
                    __builder3.AddAttribute(65, "class", "btn mbz-btn-primary mbz-font-STextOffice-18");
                    __builder3.AddAttribute(66, "style", "height: 56px;");
                    __builder3.AddAttribute(67, "disabled", 
#nullable restore
#line 45 "D:\Proyectos\Net\MercedesBenzWeb\MercedesBenzServerWeb\Pages\LoginPages\Login.razor"
                                                                                                                                      IsSubmit

#line default
#line hidden
#nullable disable
                    );
                    __builder3.AddContent(68, "Entrar");
                    __builder3.CloseElement();
                    __builder3.CloseElement();
                }
                ));
                __builder2.CloseComponent();
                __builder2.AddMarkupContent(69, "\r\n\r\n                ");
                __builder2.OpenElement(70, "div");
                __builder2.AddAttribute(71, "class", " text-center p-2");
                __builder2.OpenElement(72, "div");
                __builder2.AddAttribute(73, "class", "checkbox mbz-font-STextOffice-B-14");
                __builder2.OpenElement(74, "label");
                __builder2.AddAttribute(75, "for", "Recordarme");
                __builder2.OpenElement(76, "input");
                __builder2.AddAttribute(77, "type", "checkbox");
                __builder2.AddAttribute(78, "checked", 
#nullable restore
#line 53 "D:\Proyectos\Net\MercedesBenzWeb\MercedesBenzServerWeb\Pages\LoginPages\Login.razor"
                                                             recordarme

#line default
#line hidden
#nullable disable
                );
                __builder2.AddAttribute(79, "onclick", Microsoft.AspNetCore.Components.EventCallback.Factory.Create<Microsoft.AspNetCore.Components.Web.MouseEventArgs>(this, 
#nullable restore
#line 53 "D:\Proyectos\Net\MercedesBenzWeb\MercedesBenzServerWeb\Pages\LoginPages\Login.razor"
                                                                                   e => recordarme = !recordarme 

#line default
#line hidden
#nullable disable
                ));
                __builder2.CloseElement();
                __builder2.AddMarkupContent(80, "\r\n                            Recordar contraseña\r\n                        ");
                __builder2.CloseElement();
                __builder2.CloseElement();
                __builder2.CloseElement();
                __builder2.AddMarkupContent(81, "\r\n                ");
                __builder2.OpenElement(82, "div");
                __builder2.AddAttribute(83, "class", "text-center");
                __builder2.AddAttribute(84, "style", "margin-bottom: 10px;");
                __builder2.OpenElement(85, "a");
                __builder2.AddAttribute(86, "class", "mbz-font-STextOffice-B-14 mbz-text-primary");
                __builder2.AddAttribute(87, "id", "password");
                __builder2.AddAttribute(88, "href", "javascript: void(0);");
                __builder2.AddAttribute(89, "onclick", Microsoft.AspNetCore.Components.EventCallback.Factory.Create<Microsoft.AspNetCore.Components.Web.MouseEventArgs>(this, 
#nullable restore
#line 59 "D:\Proyectos\Net\MercedesBenzWeb\MercedesBenzServerWeb\Pages\LoginPages\Login.razor"
                                                                                                                              SendMail

#line default
#line hidden
#nullable disable
                ));
                __builder2.AddMarkupContent(90, "¿Olvidaste la contraseña?");
                __builder2.CloseElement();
                __builder2.CloseElement();
            }
            ));
            __builder.CloseComponent();
            __builder.CloseElement();
            __builder.CloseElement();
            __builder.AddMarkupContent(91, "\r\n\r\n    ");
            __builder.OpenElement(92, "div");
            __builder.AddAttribute(93, "class", "mbz-ctr-login-footer");
            __builder.OpenElement(94, "span");
            __builder.AddAttribute(95, "class", "mbz-text-dark");
            __builder.AddAttribute(96, "mbz-text-dark");
            __builder.AddContent(97, 
#nullable restore
#line 67 "D:\Proyectos\Net\MercedesBenzWeb\MercedesBenzServerWeb\Pages\LoginPages\Login.razor"
                                                   version

#line default
#line hidden
#nullable disable
            );
            __builder.CloseElement();
            __builder.CloseElement();
            __builder.CloseElement();
#nullable restore
#line 71 "D:\Proyectos\Net\MercedesBenzWeb\MercedesBenzServerWeb\Pages\LoginPages\Login.razor"
 if (popupOpen)
{

#line default
#line hidden
#nullable disable
            __builder.OpenComponent<MercedesBenzServerWeb.Pages.Popup.PopupCorreoEnviar>(98);
            __builder.AddAttribute(99, "Texto", global::Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.String>(
#nullable restore
#line 73 "D:\Proyectos\Net\MercedesBenzWeb\MercedesBenzServerWeb\Pages\LoginPages\Login.razor"
                                                                mensajePopup

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(100, "OnClose", global::Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<Microsoft.AspNetCore.Components.EventCallback<System.Boolean>>(Microsoft.AspNetCore.Components.EventCallback.Factory.Create<System.Boolean>(this, 
#nullable restore
#line 73 "D:\Proyectos\Net\MercedesBenzWeb\MercedesBenzServerWeb\Pages\LoginPages\Login.razor"
                                                                                       OnPopupClose

#line default
#line hidden
#nullable disable
            )));
            __builder.CloseComponent();
#nullable restore
#line 74 "D:\Proyectos\Net\MercedesBenzWeb\MercedesBenzServerWeb\Pages\LoginPages\Login.razor"
}

#line default
#line hidden
#nullable disable
#nullable restore
#line 76 "D:\Proyectos\Net\MercedesBenzWeb\MercedesBenzServerWeb\Pages\LoginPages\Login.razor"
 if (popupOpenNotify)
{

#line default
#line hidden
#nullable disable
            __builder.OpenComponent<MercedesBenzServerWeb.Pages.Popup.PopupCorreoEnviado>(101);
            __builder.AddAttribute(102, "Texto", "");
            __builder.AddAttribute(103, "OnClose", global::Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<Microsoft.AspNetCore.Components.EventCallback<System.Boolean>>(Microsoft.AspNetCore.Components.EventCallback.Factory.Create<System.Boolean>(this, 
#nullable restore
#line 78 "D:\Proyectos\Net\MercedesBenzWeb\MercedesBenzServerWeb\Pages\LoginPages\Login.razor"
                                                                             OnPopupNotifyClose

#line default
#line hidden
#nullable disable
            )));
            __builder.CloseComponent();
#nullable restore
#line 79 "D:\Proyectos\Net\MercedesBenzWeb\MercedesBenzServerWeb\Pages\LoginPages\Login.razor"
}

#line default
#line hidden
#nullable disable
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private LocalStorage LocalStorage { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private AuthenticationStateProvider AuthenticationStateProvider { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private Services.IUsuarioService ServiceUsuario { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private Services.IAutenticacionService ServiceAutenticacion { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private BusquedaRequest Busqueda { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private UserCredential Credential { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private AppState AppState { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private IJSRuntime JS { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private NavigationManager NavigationManager { get; set; }
    }
}
namespace __Blazor.MercedesBenzServerWeb.Pages.LoginPages.Login
{
    #line hidden
    internal static class TypeInference
    {
        public static void CreateValidationMessage_0<TValue>(global::Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder, int seq, int __seq0, global::System.Linq.Expressions.Expression<global::System.Func<TValue>> __arg0, int __seq1, System.Object __arg1)
        {
        __builder.OpenComponent<global::Microsoft.AspNetCore.Components.Forms.ValidationMessage<TValue>>(seq);
        __builder.AddAttribute(__seq0, "For", __arg0);
        __builder.AddAttribute(__seq1, "class", __arg1);
        __builder.CloseComponent();
        }
        public static void CreateValidationMessage_1<TValue>(global::Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder, int seq, int __seq0, global::System.Linq.Expressions.Expression<global::System.Func<TValue>> __arg0)
        {
        __builder.OpenComponent<global::Microsoft.AspNetCore.Components.Forms.ValidationMessage<TValue>>(seq);
        __builder.AddAttribute(__seq0, "For", __arg0);
        __builder.CloseComponent();
        }
    }
}
#pragma warning restore 1591
