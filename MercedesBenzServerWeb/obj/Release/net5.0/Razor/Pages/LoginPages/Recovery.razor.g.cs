#pragma checksum "D:\Proyectos\Net\MercedesBenzWeb\MercedesBenzServerWeb\Pages\LoginPages\Recovery.razor" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "3bb91a5036817a5e1988b8f062871ef748d46b30"
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
    [Microsoft.AspNetCore.Components.RouteAttribute("/login/recovery/{token}")]
    public partial class Recovery : Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
            __builder.OpenElement(0, "div");
            __builder.AddAttribute(1, "class", "mbz-ctr-full");
            __builder.OpenElement(2, "div");
            __builder.AddAttribute(3, "class", "mbz-ctr mbz-bg-login");
            __builder.AddMarkupContent(4, "<div class=\"mbz-ctr-login-head\"></div>\r\n\r\n        ");
            __builder.OpenElement(5, "div");
            __builder.AddAttribute(6, "class", "mbz-ctr-recovery");
            __builder.AddMarkupContent(7, "<div class=\"mbz-ctr-login-title\"><span>Recuperación de contraseña</span></div>");
#nullable restore
#line 16 "D:\Proyectos\Net\MercedesBenzWeb\MercedesBenzServerWeb\Pages\LoginPages\Recovery.razor"
             if (!passwordRestablecido)
            {
                

#line default
#line hidden
#nullable disable
#nullable restore
#line 18 "D:\Proyectos\Net\MercedesBenzWeb\MercedesBenzServerWeb\Pages\LoginPages\Recovery.razor"
                 if (model != null)
                {

#line default
#line hidden
#nullable disable
            __builder.OpenElement(8, "div");
            __builder.AddAttribute(9, "class", "mbz-ctr-login-body");
            __builder.OpenComponent<Microsoft.AspNetCore.Components.Forms.EditForm>(10);
            __builder.AddAttribute(11, "Model", global::Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.Object>(
#nullable restore
#line 21 "D:\Proyectos\Net\MercedesBenzWeb\MercedesBenzServerWeb\Pages\LoginPages\Recovery.razor"
                                          model

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(12, "OnValidSubmit", global::Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<Microsoft.AspNetCore.Components.EventCallback<Microsoft.AspNetCore.Components.Forms.EditContext>>(Microsoft.AspNetCore.Components.EventCallback.Factory.Create<Microsoft.AspNetCore.Components.Forms.EditContext>(this, 
#nullable restore
#line 21 "D:\Proyectos\Net\MercedesBenzWeb\MercedesBenzServerWeb\Pages\LoginPages\Recovery.razor"
                                                                 OnValidSubmit

#line default
#line hidden
#nullable disable
            )));
            __builder.AddAttribute(13, "ChildContent", (Microsoft.AspNetCore.Components.RenderFragment<Microsoft.AspNetCore.Components.Forms.EditContext>)((context) => (__builder2) => {
                __builder2.OpenComponent<Microsoft.AspNetCore.Components.Forms.DataAnnotationsValidator>(14);
                __builder2.CloseComponent();
                __builder2.AddMarkupContent(15, "\r\n                            ");
                __builder2.OpenElement(16, "div");
                __builder2.AddAttribute(17, "class", "form-group pb-3");
                __builder2.OpenComponent<Microsoft.AspNetCore.Components.Forms.InputText>(18);
                __builder2.AddAttribute(19, "class", "form-control mbz-font-STextOffice-16");
                __builder2.AddAttribute(20, "style", "height: 56px;");
                __builder2.AddAttribute(21, "autocomplete", "email");
                __builder2.AddAttribute(22, "placeholder", "Correo electrónico");
                __builder2.AddAttribute(23, "disabled", "true");
                __builder2.AddAttribute(24, "Value", global::Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.String>(
#nullable restore
#line 24 "D:\Proyectos\Net\MercedesBenzWeb\MercedesBenzServerWeb\Pages\LoginPages\Recovery.razor"
                                                                                                                                                                                  model.CorreoElectronico

#line default
#line hidden
#nullable disable
                ));
                __builder2.AddAttribute(25, "ValueChanged", global::Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<Microsoft.AspNetCore.Components.EventCallback<System.String>>(Microsoft.AspNetCore.Components.EventCallback.Factory.Create<System.String>(this, global::Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.CreateInferredEventCallback(this, __value => model.CorreoElectronico = __value, model.CorreoElectronico))));
                __builder2.AddAttribute(26, "ValueExpression", global::Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.Linq.Expressions.Expression<System.Func<System.String>>>(() => model.CorreoElectronico));
                __builder2.CloseComponent();
                __builder2.CloseElement();
                __builder2.AddMarkupContent(27, "\r\n                            ");
                __builder2.OpenElement(28, "div");
                __builder2.AddAttribute(29, "class", "form-group pb-3");
                __builder2.OpenComponent<Microsoft.AspNetCore.Components.Forms.InputText>(30);
                __builder2.AddAttribute(31, "class", "form-control mbz-font-STextOffice-16");
                __builder2.AddAttribute(32, "style", "height: 56px;");
                __builder2.AddAttribute(33, "autocomplete", "password");
                __builder2.AddAttribute(34, "type", "password");
                __builder2.AddAttribute(35, "placeholder", "Contraseña");
                __builder2.AddAttribute(36, "maxlength", "15");
                __builder2.AddAttribute(37, "Value", global::Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.String>(
#nullable restore
#line 27 "D:\Proyectos\Net\MercedesBenzWeb\MercedesBenzServerWeb\Pages\LoginPages\Recovery.razor"
                                                                                                                                                                                                            model.Password

#line default
#line hidden
#nullable disable
                ));
                __builder2.AddAttribute(38, "ValueChanged", global::Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<Microsoft.AspNetCore.Components.EventCallback<System.String>>(Microsoft.AspNetCore.Components.EventCallback.Factory.Create<System.String>(this, global::Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.CreateInferredEventCallback(this, __value => model.Password = __value, model.Password))));
                __builder2.AddAttribute(39, "ValueExpression", global::Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.Linq.Expressions.Expression<System.Func<System.String>>>(() => model.Password));
                __builder2.CloseComponent();
                __builder2.AddMarkupContent(40, "\r\n                                ");
                __Blazor.MercedesBenzServerWeb.Pages.LoginPages.Recovery.TypeInference.CreateValidationMessage_0(__builder2, 41, 42, 
#nullable restore
#line 28 "D:\Proyectos\Net\MercedesBenzWeb\MercedesBenzServerWeb\Pages\LoginPages\Recovery.razor"
                                                          () => model.Password

#line default
#line hidden
#nullable disable
                );
                __builder2.CloseElement();
                __builder2.AddMarkupContent(43, "\r\n                            ");
                __builder2.OpenElement(44, "div");
                __builder2.AddAttribute(45, "class", "form-group pb-3");
                __builder2.OpenComponent<Microsoft.AspNetCore.Components.Forms.InputText>(46);
                __builder2.AddAttribute(47, "class", "form-control mbz-font-STextOffice-16");
                __builder2.AddAttribute(48, "style", "height: 56px;");
                __builder2.AddAttribute(49, "autocomplete", "confirm-password");
                __builder2.AddAttribute(50, "type", "password");
                __builder2.AddAttribute(51, "placeholder", "Confirmar Contraseña");
                __builder2.AddAttribute(52, "maxlength", "15");
                __builder2.AddAttribute(53, "Value", global::Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.String>(
#nullable restore
#line 31 "D:\Proyectos\Net\MercedesBenzWeb\MercedesBenzServerWeb\Pages\LoginPages\Recovery.razor"
                                                                                                                                                                                                                              model.ConfirmarPassword

#line default
#line hidden
#nullable disable
                ));
                __builder2.AddAttribute(54, "ValueChanged", global::Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<Microsoft.AspNetCore.Components.EventCallback<System.String>>(Microsoft.AspNetCore.Components.EventCallback.Factory.Create<System.String>(this, global::Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.CreateInferredEventCallback(this, __value => model.ConfirmarPassword = __value, model.ConfirmarPassword))));
                __builder2.AddAttribute(55, "ValueExpression", global::Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.Linq.Expressions.Expression<System.Func<System.String>>>(() => model.ConfirmarPassword));
                __builder2.CloseComponent();
                __builder2.AddMarkupContent(56, "\r\n                                ");
                __Blazor.MercedesBenzServerWeb.Pages.LoginPages.Recovery.TypeInference.CreateValidationMessage_1(__builder2, 57, 58, 
#nullable restore
#line 32 "D:\Proyectos\Net\MercedesBenzWeb\MercedesBenzServerWeb\Pages\LoginPages\Recovery.razor"
                                                          () => model.ConfirmarPassword

#line default
#line hidden
#nullable disable
                );
                __builder2.CloseElement();
                __builder2.AddMarkupContent(59, "\r\n\r\n                            ");
                __builder2.OpenElement(60, "div");
                __builder2.AddAttribute(61, "class", "form-group");
                __builder2.OpenElement(62, "button");
                __builder2.AddAttribute(63, "type", "submit");
                __builder2.AddAttribute(64, "class", "btn mbz-btn-primary mbz-font-STextOffice-18");
                __builder2.AddAttribute(65, "style", "height: 56px;");
                __builder2.AddAttribute(66, "disabled", 
#nullable restore
#line 36 "D:\Proyectos\Net\MercedesBenzWeb\MercedesBenzServerWeb\Pages\LoginPages\Recovery.razor"
                                                                                                                                          IsSubmit

#line default
#line hidden
#nullable disable
                );
                __builder2.AddContent(67, "Guardar");
                __builder2.CloseElement();
                __builder2.CloseElement();
            }
            ));
            __builder.CloseComponent();
            __builder.CloseElement();
#nullable restore
#line 40 "D:\Proyectos\Net\MercedesBenzWeb\MercedesBenzServerWeb\Pages\LoginPages\Recovery.razor"
                     if (!string.IsNullOrEmpty(mensaje))
                    {

#line default
#line hidden
#nullable disable
            __builder.OpenElement(68, "div");
            __builder.AddAttribute(69, "class", "container alert-info mbz-font-STextOffice-16");
            __builder.AddContent(70, 
#nullable restore
#line 42 "D:\Proyectos\Net\MercedesBenzWeb\MercedesBenzServerWeb\Pages\LoginPages\Recovery.razor"
                                                                                   mensaje

#line default
#line hidden
#nullable disable
            );
            __builder.CloseElement();
#nullable restore
#line 43 "D:\Proyectos\Net\MercedesBenzWeb\MercedesBenzServerWeb\Pages\LoginPages\Recovery.razor"
                    }

#line default
#line hidden
#nullable disable
#nullable restore
#line 43 "D:\Proyectos\Net\MercedesBenzWeb\MercedesBenzServerWeb\Pages\LoginPages\Recovery.razor"
                     
                }
                else
                

#line default
#line hidden
#nullable disable
#nullable restore
#line 46 "D:\Proyectos\Net\MercedesBenzWeb\MercedesBenzServerWeb\Pages\LoginPages\Recovery.razor"
                 if (!string.IsNullOrEmpty(mensaje))
                {

#line default
#line hidden
#nullable disable
            __builder.OpenElement(71, "div");
            __builder.AddAttribute(72, "class", "mbz-ctr-recovery-mensaje");
            __builder.AddContent(73, 
#nullable restore
#line 49 "D:\Proyectos\Net\MercedesBenzWeb\MercedesBenzServerWeb\Pages\LoginPages\Recovery.razor"
                         mensaje

#line default
#line hidden
#nullable disable
            );
            __builder.CloseElement();
#nullable restore
#line 51 "D:\Proyectos\Net\MercedesBenzWeb\MercedesBenzServerWeb\Pages\LoginPages\Recovery.razor"
                }

#line default
#line hidden
#nullable disable
#nullable restore
#line 51 "D:\Proyectos\Net\MercedesBenzWeb\MercedesBenzServerWeb\Pages\LoginPages\Recovery.razor"
                 
            }
            else
            {

#line default
#line hidden
#nullable disable
            __builder.AddMarkupContent(74, "<div class=\"mbz-ctr-recovery-mensaje\">\r\n                    Se ha restablecido la contraseña\r\n                </div>\r\n                ");
            __builder.AddMarkupContent(75, "<div class=\"mbz-ctr-recovery-mensaje\"><a class=\"mbz-nav-item mbz-font-STextOffice-25 mbz-text-primary\" href=\"Index\" style=\"text-decoration: none;\"><span class=\"mbz-text-primary\">Ir a la página de inicio</span></a></div>");
#nullable restore
#line 63 "D:\Proyectos\Net\MercedesBenzWeb\MercedesBenzServerWeb\Pages\LoginPages\Recovery.razor"
            }

#line default
#line hidden
#nullable disable
            __builder.CloseElement();
            __builder.CloseElement();
            __builder.CloseElement();
        }
        #pragma warning restore 1998
#nullable restore
#line 69 "D:\Proyectos\Net\MercedesBenzWeb\MercedesBenzServerWeb\Pages\LoginPages\Recovery.razor"
       

    [Parameter] public string token { get; set; }

    ResetPassword model = null;

    private string mensaje = String.Empty;
    bool IsSubmit = false;
    bool passwordRestablecido = false;

    protected override async Task OnInitializedAsync()
    {
        if (string.IsNullOrEmpty(token))
            mensaje = "El Link es inválido";
        else
        {
            if (model == null)
            {
                (string mensajeResponse, ValidacionTokenRecuperacion recuperacion) = await Service.ValidarTokenRecuperacionAsync(new TokenRequest() { Token = token });
                if (!string.IsNullOrEmpty(mensajeResponse))
                    mensaje = mensajeResponse;
                else
                {
                    model = new() { CorreoElectronico = recuperacion.CorreoElectronico };
                }
            }
        }
    }

    public async Task OnValidSubmit()
    {
        mensaje = "Validando ingreso ...";
        IsSubmit = true;

        mensaje = "";
        (string mensajeResponse, bool resultado) = await Service.ResetPasswordAsync(model);
        if (resultado)
        {
            passwordRestablecido = true;
        }
        else
        {
            mensaje = mensajeResponse;
        }
        IsSubmit = false;
    }


#line default
#line hidden
#nullable disable
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private Services.IRecuperacionService Service { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private BusquedaRequest Busqueda { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private UserCredential Credential { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private AppState AppState { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private IJSRuntime JS { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private NavigationManager NavigationManager { get; set; }
    }
}
namespace __Blazor.MercedesBenzServerWeb.Pages.LoginPages.Recovery
{
    #line hidden
    internal static class TypeInference
    {
        public static void CreateValidationMessage_0<TValue>(global::Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder, int seq, int __seq0, global::System.Linq.Expressions.Expression<global::System.Func<TValue>> __arg0)
        {
        __builder.OpenComponent<global::Microsoft.AspNetCore.Components.Forms.ValidationMessage<TValue>>(seq);
        __builder.AddAttribute(__seq0, "For", __arg0);
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
