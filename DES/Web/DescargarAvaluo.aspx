<%@ Page Language="C#" MasterPageFile="~/MasterPageFE/MasterDetalleFE.master" AutoEventWireup="true"
    CodeFile="DescargarAvaluo.aspx.cs" Inherits="DescargarAvaluo" Title="Descargar avalúo" %>

<%@ Register Src="UserControls/MenuLocal.ascx" TagName="MenuLocal" TagPrefix="uc1" %>
<%@ Register Src="UserControls/Navegacion.ascx" TagName="Navegacion" TagPrefix="uc2" %>
<%@ Register Src="UserControlsCommon/Progreso.ascx" TagName="Progreso" TagPrefix="uc3" %>
<%@ Register Src="UserControlsCommon/ModalInfo.ascx" TagName="ModalInfo" TagPrefix="uc6" %>
<%@ Register Src="UserControlsCommon/ModalInfoExcepcion.ascx" TagName="ModalInfoExcepcion"
    TagPrefix="uc11" %>
<%@ Register Src="UserControlsCommon/ModalInfo.ascx" TagName="ModalInfo" TagPrefix="uc12" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="UserControlsCommon/ModalConfirmarcion.ascx" TagName="ModalConfirmar"
    TagPrefix="uc7" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderDImagen" runat="Server">
    <asp:Image ID="Image5" runat="server" ImageUrl="~/Images/caracter_detalle.jpg" />

    <script type="text/javascript">
        function ocultarModalConfirmar() {
            $find("ctl00_ctl00_ctl00_ContentPlaceHolderContentBase_ContentPlaceHolderDContenido_ContentPlaceHolderDContenido_confirmar_ModalPopupExtender").hide();
        }

        function ocultarPanelError(boton, panel) {
            if (panel.style.display == "block") {
                panel.style.display = "none";
                boton.value = "Mostrar Detalles";
            }
            else {
                panel.style.display = "block";
                boton.value = "Ocultar Detalles";
            }
        }
        function validaAlfanumerico(e) {
            var Code = e.keyCode;
            //0-9 y .
            if ((Code >= 48 && Code <= 57) || (Code >= 65 && Code <= 90) || (Code >= 97 && Code <= 122)) {
                return true;
            }
            else {
                return false;
            }
        }

 

    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderDRuta" runat="Server">
    <uc2:Navegacion ID="Navegacion1" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderDContenido" runat="Server">
    <fieldset class="formulario">
        <legend class="formulario">Información</legend>
        <br />
        <asp:Label ID="Label2" runat="server" Text="Desde aquí podrá descargar una versión inicial del archivo XML del avalúo con la información registrada en catastral para la cuenta indicada.  El formato de la cuenta será de 11 dígitos más el dígito verificador, en total 12 dígitos sin espacios." />
        <br />
        <br />
    </fieldset>
    <br />
    <fieldset class="formulario">
        <legend class="formulario">Tipo de Avalúo</legend>
        <asp:UpdatePanel ID="uppErrorTareas" runat="server" UpdateMode="Conditional" RenderMode="Inline">
            <ContentTemplate>
                <cc1:ModalPopupExtender ID="mpeErrorTareas" runat="server" Enabled="True" TargetControlID="hlnErrorTareas"
                    PopupControlID="panErrorTareas" DropShadow="false" BackgroundCssClass="PanelModalBackground" />
                <asp:HyperLink ID="hlnErrorTareas" runat="server" Style="display: none" />
                <asp:Panel ID="panErrorTareas" runat="server" Style="display: none" SkinID="Modal">
                    <uc11:ModalInfoExcepcion ID="errorTareas" runat="server" />
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:Panel ID="PanelBusqueda" runat="server">
            <table class="TextLeftMiddle">
                <tr>
                    <td style="width: 100%">
                        <asp:RadioButton ID="RBAvaluoComercial" runat="server" Text="Avalúo Comercial" GroupName="rbTipoAvaluoGroup"
                            Checked="True" />
                    </td>
                </tr>
                <tr>
                    <td style="width: 100%">
                        <asp:RadioButton ID="RBAvaluoCatastral" runat="server" Text="Avalúo Catastral" GroupName="rbTipoAvaluoGroup" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </fieldset>
    <table style="width: 100%">
        <tr>
            <td>
                <br />
                <asp:Label ID="lblCuentaCatastral" SkinID="Titulo2" runat="server" Text="Cuenta catastral"></asp:Label>
                &nbsp;
                <asp:TextBox ID="txtRegion" runat="server" MaxLength="3" Width="32px" SkinID="TextBoxObligatorio"
                    onkeypress="return validaAlfanumerico(event)" onblur="javascript:if(this.value !=''){rellenar(this,this.value,3);}"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvRegion" runat="server" ControlToValidate="txtRegion"
                    SetFocusOnError="True" ErrorMessage="Requerida una región" ValidationGroup="ValidarCuenta"
                    Display="Dynamic">*</asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="rfvRegionExp" runat="server" ControlToValidate="txtRegion"
                    Display="None" SetFocusOnError="False" ValidationExpression="[0|1|2|3|4|5|6|7|8|9|A|B|C|D|E|F|H|J|K|M|N|P|Q|R|T|U|V|W|X|Y]{3}"
                    ErrorMessage="Formato incorrecto de región.(Caracteres admitidos: 0,1,2,3,4,5,6,7,8,9,A,B,C,D,E,F,H,J,K,M,N,P,Q,R,T,U,V,W,X,Y)"
                    ValidationGroup="ValidarCuenta">!</asp:RegularExpressionValidator>
                <asp:TextBox ID="txtManzana" Enabled="true" runat="server" MaxLength="3" Width="30px"
                    SkinID="TextBoxObligatorio" onblur="javascript:if(this.value !=''){rellenar(this,this.value,3);}"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvManzana" runat="server" ControlToValidate="txtManzana"
                    SetFocusOnError="True" ErrorMessage="Requerida una manzana" ValidationGroup="ValidarCuenta"
                    Display="Dynamic">*</asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="rfvManzanaExp" runat="server" ControlToValidate="txtManzana"
                    Display="None" SetFocusOnError="False" ValidationExpression="[0|1|2|3|4|5|6|7|8|9|A|B|C|D|E|F|H|J|K|M|N|P|Q|R|T|U|V|W|X|Y]{3}"
                    ErrorMessage="Formato incorrecto de manzana.(Caracteres admitidos: 0,1,2,3,4,5,6,7,8,9,A,B,C,D,E,F,H,J,K,M,N,P,Q,R,T,U,V,W,X,Y)"
                    ValidationGroup="ValidarCuenta">!</asp:RegularExpressionValidator>
                <asp:TextBox ID="txtLote" runat="server" MaxLength="2" Width="20px" Enabled="true"
                    SkinID="TextBoxObligatorio" onblur="javascript:if(this.value!=''){rellenar(this,this.value,2);}"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvLote" runat="server" ControlToValidate="txtLote"
                    SetFocusOnError="True" ErrorMessage="Requerido un lote" ValidationGroup="ValidarCuenta"
                    Display="Dynamic">*</asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="rfvLoteExp" runat="server" ControlToValidate="txtLote"
                    Display="None" SetFocusOnError="False" ValidationExpression="[0|1|2|3|4|5|6|7|8|9|A|B|C|D|E|F|H|J|K|M|N|P|Q|R|T|U|V|W|X|Y]{2}"
                    ErrorMessage="Formato incorrecto de lote.(Caracteres admitidos: 0,1,2,3,4,5,6,7,8,9,A,B,C,D,E,F,H,J,K,M,N,P,Q,R,T,U,V,W,X,Y)"
                    ValidationGroup="ValidarCuenta">!</asp:RegularExpressionValidator>
                <asp:TextBox ID="txtUnidadPrivativa" Enabled="true" runat="server" MaxLength="3"
                    Width="30px" SkinID="TextBoxObligatorio" onblur="javascript:if(this.value !=''){rellenar(this,this.value,3);}"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvUnidadPrivativa" runat="server" ControlToValidate="txtUnidadPrivativa"
                    SetFocusOnError="True" ErrorMessage="Requerido un condominio" ValidationGroup="ValidarCuenta"
                    Display="Dynamic">*</asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="rfvUnidadPrivativaExp" runat="server" ControlToValidate="txtUnidadPrivativa"
                    SetFocusOnError="False" Display="None" ValidationExpression="[0|1|2|3|4|5|6|7|8|9|A|B|C|D|E|F|H|J|K|M|N|P|Q|R|T|U|V|W|X|Y]{3}"
                    ErrorMessage="Formato incorrecto de unidad privativa.(Caracteres admitidos: 0,1,2,3,4,5,6,7,8,9,A,B,C,D,E,F,H,J,K,M,N,P,Q,R,T,U,V,W,X,Y)"
                    ValidationGroup="ValidarCuenta">!</asp:RegularExpressionValidator>
                <asp:TextBox ID="txtDigito" Enabled="true" runat="server" MaxLength="1" Width="14px"
                    SkinID="TextBoxObligatorio"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvDigito" runat="server" ControlToValidate="txtDigito"
                    SetFocusOnError="True" ErrorMessage="Requerido el dígito verificador" ValidationGroup="ValidarCuenta"
                    Display="Dynamic">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="TextRigthMiddle">
                <asp:UpdatePanel ID="UpdatePanelBotonDescargar" runat="server" RenderMode="Inline"
                    UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:Button ID="btnDescargaAvaluo" runat="server" OnClick="btnDescargaAvaluo_Click"
                            ValidationGroup="ValidarCuenta" Text="Descargar" />
                        <asp:HiddenField runat="server" ID="HiddenTokenModal" />
                        <asp:HiddenField runat="server" ID="HiddenInfoModal" />
                        <cc1:ModalPopupExtender ID="extenderPnlInfoTokenModal" runat="server" Enabled="True"
                            TargetControlID="HiddenTokenModal" PopupControlID="pnlTokenModal" BackgroundCssClass="PanelModalBackground"
                            DropShadow="True" />
                        <cc1:ModalPopupExtender ID="extenderPnlInfoModal" runat="server" Enabled="True" TargetControlID="HiddenInfoModal"
                            PopupControlID="pnlInfoModal" BackgroundCssClass="PanelModalBackground" DropShadow="True" />
                        <asp:Panel ID="pnlTokenModal" SkinID="Modal" Style="display: none; width: 280px;"
                            runat="server">
                            <uc6:ModalInfo ID="ModalInfoToken" runat="server" OnConfirmClick="ModalInfoToken_Ok_Click" />
                        </asp:Panel>
                        <asp:Panel ID="pnlInfoModal" SkinID="Modal" Style="display: none; width: 280px;"
                            runat="server">
                            <uc6:ModalInfo ID="ModalInfo" runat="server" OnConfirmClick="ModalInfo_Ok_Click" />
                        </asp:Panel>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td>
                <asp:ValidationSummary ID="vsFiltroAvaluos" runat="server" ValidationGroup="ValidarCuenta" />
            </td>
        </tr>
    </table>
    <br />
    <asp:UpdatePanel ID="UpdatePnlModalConfirmar" runat="server" UpdateMode="Conditional"
        RenderMode="Inline">
        <ContentTemplate>
            <asp:HiddenField runat="server" ID="HiddenConfirmarModal" Visible="true" />
            <cc1:ModalPopupExtender ID="confirmar_ModalPopupExtender" runat="server" Enabled="True"
                TargetControlID="HiddenConfirmarModal" PopupControlID="pnlModalConfirmar" BackgroundCssClass="PanelModalBackground"
                DropShadow="false" />
            <asp:Panel ID="pnlModalConfirmar" SkinID="Modal" Style="display: none;" runat="server">
                <uc7:ModalConfirmar ID="ModalConfirmacion" runat="server" OnConfirmClick="confirmar_ConfirmClick" />
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolderDMenuLocal" runat="Server">
    <uc1:MenuLocal ID="MenuLocal1" runat="server" />
</asp:Content>
