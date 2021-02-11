<%@ Page Language="C#" MasterPageFile="~/MasterPageFE/MasterDetalleFE.master" AutoEventWireup="true" CodeFile="CuentasDuplicadas.aspx.cs" EnableEventValidation="true" Inherits="CuentasDuplicadas" Title="Cuentas duplicadas" %>

<%@ Register Src="UserControls/Navegacion.ascx" TagName="Navegacion" TagPrefix="uc1" %>
<%@ Register Src="UserControls/MenuLocal.ascx" TagName="MenuLocal" TagPrefix="uc2" %>
<%@ Register Src="UserControls/ModalEstado.ascx" TagName="ModalEstado" TagPrefix="uc3" %>
<%@ Register Src="UserControls/ModalBuscarNotarios.ascx" TagName="ModalBuscarNotarios" TagPrefix="uc4" %>
<%@ Register Src="UserControlsCommon/Progreso.ascx" TagName="Progreso" TagPrefix="uc5" %>
<%@ Register Src="UserControlsCommon/ModalInfo.ascx" TagName="ModalInfo" TagPrefix="uc6" %>
<%@ Register Src="UserControls/ModalBuscarPeritos.ascx" TagName="ModalBuscarPeritos" TagPrefix="uc7" %>
<%@ Register Src="~/UserControlsCommon/ProgresoGridView.ascx" TagName="ProgresoGridView" TagPrefix="uc8" %>
<%@ Register Src="~/UserControlsCommon/ModalInfoExcepcion.ascx" TagName="ModalInfoExcepcion" TagPrefix="uc11" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="ContentImagen" ContentPlaceHolderID="ContentPlaceHolderDImagen" runat="Server">

    <script type="text/javascript">
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
    </script>
    <asp:Image ID="Image5" runat="server" ImageUrl="~/Images/caracter_detalle.jpg" />
</asp:Content>
<asp:Content ID="ContentRuta" ContentPlaceHolderID="ContentPlaceHolderDRuta" runat="Server">
    <uc1:Navegacion ID="navegacion" runat="server" />
</asp:Content>
<asp:Content ID="ContentContenido" ContentPlaceHolderID="ContentPlaceHolderDContenido" runat="Server">
    <asp:UpdatePanel ID="uppErrorTareas" runat="server" UpdateMode="Conditional" RenderMode="Inline">
        <ContentTemplate>
            <cc1:ModalPopupExtender ID="mpeErrorTareas" runat="server" Enabled="True"
                TargetControlID="hlnErrorTareas" PopupControlID="panErrorTareas"
                DropShadow="false" BackgroundCssClass="PanelModalBackground" />
            <asp:HyperLink ID="hlnErrorTareas" runat="server" Style="Display: none" />
            <asp:Panel ID="panErrorTareas" runat="server" Style="Display: none" SkinID="Modal">
                <uc11:ModalInfoExcepcion ID="errorTareas" runat="server" />
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="UpdatePanelFiltro" runat="server" RenderMode="Inline" UpdateMode="Conditional">
        <ContentTemplate>
            <fieldset class="formulario">
                <legend class="formulario">Filtro de avalúos</legend>
                <div class="DivPaddingBotton TextLeftMiddle">
                    <asp:Panel ID="PanelBusqueda" runat="server"
                        OnDataBinding="Page_Load">
                        <div style="width: 100%">
                             <asp:Label ID="lblTextoError" SkinID="TextError" class="TextLeftTop" runat="server"
                                            Text=""></asp:Label>
                            <asp:ValidationSummary ID="vsFiltroAvaluos" runat="server"
                                ValidationGroup="FiltroAvaluos" Width="100%" />
                        </div>
                        <div>
                            <table class="TextLeftMiddle">
                                <tr>
                                    <td style="width: 160px">
                                        <asp:Label ID="Label1" SkinID="Titulo2" class="TextLeftTop" runat="server"
                                            Text="Rango de fechas"></asp:Label>
                                    </td>
                                    <td style="width: 336px" class="style5">
                                        <asp:TextBox ID="txtFechaIni" runat="server" SkinID="TextBoxObligatorio"
                                            MaxLength="10" Width="125px"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtFechaIni_CalendarExtender" runat="server" Enabled="True"
                                            PopupButtonID="btnFechaIni" TargetControlID="txtFechaIni">
                                        </cc1:CalendarExtender>
                                        <asp:ImageButton runat="server" ID="btnFechaIni" ImageUrl="~/images/calendario.png" CausesValidation="false" Height="16" Width="16" AlternateText="Seleccione una Fecha" />
                                        <asp:RequiredFieldValidator ID="rfvFechaInicial" runat="server" ErrorMessage="Requerida una fecha" ControlToValidate="txtFechaIni"
                                            SetFocusOnError="True" ValidationGroup="FiltroAvaluos" Display="Dynamic">*</asp:RequiredFieldValidator>
                                        <asp:CompareValidator ID="cvFechaInicial" runat="server" ErrorMessage="Fecha errónea" ValidationGroup="FiltroAvaluos"
                                            ControlToValidate="txtFechaIni" ForeColor="Blue" Operator="DataTypeCheck" SetFocusOnError="True"
                                            Type="Date" Display="Dynamic">!</asp:CompareValidator>-
                                <asp:TextBox ID="txtFechaFin" runat="server" SkinID="TextBoxObligatorio" MaxLength="10" Width="125px" />
                                        <cc1:CalendarExtender ID="txtFechaFin_CalendarExtender" runat="server" Enabled="True" TargetControlID="txtFechaFin" PopupButtonID="btnFechaFin"></cc1:CalendarExtender>
                                        <asp:ImageButton runat="server" ID="btnFechaFin" ImageUrl="~/images/calendario.png"
                                            CausesValidation="false" Height="16" Width="16" AlternateText="Seleccione una Fecha" />
                                        <asp:RequiredFieldValidator ID="rfvFechaFin" runat="server" ErrorMessage="Requerida una fecha" ValidationGroup="FiltroAvaluos"
                                            ControlToValidate="txtFechaFin" SetFocusOnError="True" Display="Dynamic">*</asp:RequiredFieldValidator>
                                        <asp:CompareValidator ID="cvFechaFin" runat="server" ErrorMessage="Fecha errónea" ValidationGroup="FiltroAvaluos"
                                            ControlToValidate="txtFechaFin" ForeColor="Blue" Operator="DataTypeCheck" SetFocusOnError="True"
                                            Type="Date" Display="Dynamic">!</asp:CompareValidator>
                                        <asp:CompareValidator ID="cvRangofechas" runat="server" ErrorMessage="Rango entre fechas erróneo" ValidationGroup="FiltroAvaluos"
                                            ControlToCompare="txtFechaFin" ControlToValidate="txtFechaIni" ForeColor="Blue"
                                            Operator="LessThan" SetFocusOnError="True" Type="Date" Display="Dynamic">!</asp:CompareValidator>
                                    </td>
                                </tr>


                                <tr>
                                    <td style="width: 160px">
                                        <asp:Label ID="Label3" SkinID="Titulo2" class="TextLeftTop" runat="server"
                                            Text="Cuenta catastral"></asp:Label>
                                    </td>
                                    <td style="width: 336px">

                                        <asp:TextBox ID="txtRegion" runat="server" Enabled="true" MaxLength="3" Width="30px"
                                            onblur="javascript:if(this.value !=''){rellenar(this,this.value,3);}"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvRegion" runat="server" ControlToValidate="txtRegion" SetFocusOnError="True"
                                            ErrorMessage="Requerida una región" ValidationGroup="FiltroAvaluos" Enabled="false" Display="Dynamic">*</asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="rfvRegionExp" runat="server"
                                            ErrorMessage="Formato incorrecto de región.(Caracteres admitidos: <BR> 0,1,2,3,4,5,6,7,8,9,A,B,C,D,E,F,H,J,K,M,N,P,Q,R,T,U,V,W,X,Y)" ForeColor="Blue"
                                            SetFocusOnError="False" Display="None" ControlToValidate="txtRegion"
                                            ValidationGroup="FiltroAvaluos" Enabled="true" Visible="true"
                                            ValidationExpression="[0|1|2|3|4|5|6|7|8|9|A|B|C|D|E|F|H|J|K|M|N|P|Q|R|T|U|V|W|X|Y]{3}">!</asp:RegularExpressionValidator>

                                        <asp:TextBox ID="txtManzana" runat="server" Enabled="true" MaxLength="3" Width="30px"
                                            onblur="if(this.value!=''){rellenar(this,this.value,3);}"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvManzana" runat="server" ControlToValidate="txtManzana" SetFocusOnError="True" Enabled="false"
                                            ErrorMessage="Requerida una manzana" ValidationGroup="FiltroAvaluos" Display="Dynamic">*</asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="rfvManzanaExp" runat="server"
                                            ErrorMessage="Formato incorrecto de manzana.(Caracteres admitidos: <BR> 0,1,2,3,4,5,6,7,8,9,A,B,C,D,E,F,H,J,K,M,N,P,Q,R,T,U,V,W,X,Y)"
                                            ForeColor="Blue" ControlToValidate="txtManzana" ValidationGroup="FiltroAvaluos"
                                            Enabled="true" Visible="true" Display="None" SetFocusOnError="False"
                                            ValidationExpression="[0|1|2|3|4|5|6|7|8|9|A|B|C|D|E|F|H|J|K|M|N|P|Q|R|T|U|V|W|X|Y]{3}">!</asp:RegularExpressionValidator>

                                        <asp:TextBox ID="txtLote" runat="server" Enabled="true" MaxLength="2"
                                            Width="20px"
                                            onblur="javascript:if(this.value!=''){rellenar(this,this.value,2);}"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvLote" runat="server" ControlToValidate="txtLote" SetFocusOnError="True" Enabled="false"
                                            ErrorMessage="Requerido un lote" ValidationGroup="FiltroAvaluos" Display="Dynamic">*</asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="rfvLoteExp" runat="server"
                                            ErrorMessage="Formato incorrecto de lote.(Caracteres admitidos: <BR> 0,1,2,3,4,5,6,7,8,9,A,B,C,D,E,F,H,J,K,M,N,P,Q,R,T,U,V,W,X,Y)"
                                            ForeColor="Blue" ControlToValidate="txtLote" ValidationGroup="FiltroAvaluos"
                                            Enabled="true" Visible="true" Display="None" SetFocusOnError="False"
                                            ValidationExpression="[0|1|2|3|4|5|6|7|8|9|A|B|C|D|E|F|H|J|K|M|N|P|Q|R|T|U|V|W|X|Y]{2}">!</asp:RegularExpressionValidator>

                                        <asp:TextBox ID="txtUnidadPrivativa" runat="server" Enabled="true" MaxLength="3" Width="30px" onblur="javascript:if(this.value !=''){rellenar(this,this.value,3);}"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvUnidadPrivativa" runat="server" ControlToValidate="txtUnidadPrivativa"
                                            SetFocusOnError="True" Enabled="false" ErrorMessage="Requerido un condominio"
                                            ValidationGroup="FiltroAvaluos" Display="Dynamic">*</asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="rfvUnidadPrivativaExp" runat="server"
                                            ErrorMessage="Formato incorrecto de unidad privativa.(Caracteres admitidos: <BR> 0,1,2,3,4,5,6,7,8,9,A,B,C,D,E,F,H,J,K,M,N,P,Q,R,T,U,V,W,X,Y)"
                                            ControlToValidate="txtUnidadPrivativa" ForeColor="Blue" ValidationGroup="FiltroAvaluos" Display="None" SetFocusOnError="False"
                                            Enabled="true" ValidationExpression="[0|1|2|3|4|5|6|7|8|9|A|B|C|D|E|F|H|J|K|M|N|P|Q|R|T|U|V|W|X|Y]{3}">!</asp:RegularExpressionValidator>

                                        <asp:TextBox ID="txtCuenta" runat="server" Enabled="False" Visible="false"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 160px">
                                        <asp:Label ID="Label2" SkinID="Titulo2" class="TextLeftTop" runat="server"
                                            Text="Perito"></asp:Label>
                                    </td>
                                    <td style="width: 336px" class="style5">
                                        <asp:ImageButton ID="btnPeritos" runat="server" CausesValidation="False" ImageUrl="~/Images/user.gif"
                                            OnClick="btnPeritos_Click" ToolTip="Buscar Peritos" Visible="true"
                                            Enabled="true" />
                                        <br />
                                        <asp:Label ID="Label4" SkinID="Titulo2" class="TextLeftTop" runat="server"
                                            Text="Registro"></asp:Label>
                                        <br />
                                        <asp:TextBox ID="textNumeroPerito" runat="server" ReadOnly="True" Width="85px" BorderStyle="None" Text="No seleccionado"></asp:TextBox>
                                        <br />
                                        <asp:Label ID="Label5" SkinID="Titulo2" class="TextLeftTop" runat="server"
                                            Text="Nombre"></asp:Label>
                                        <asp:TextBox ID="textNombre" runat="server" ReadOnly="true" Width="100%" BorderStyle="None" Text="No seleccionado"></asp:TextBox>
                                        
                                        <cc1:ModalPopupExtender ID="btnPeritos_ModalPopupExtender" runat="server"
                                            Enabled="True" TargetControlID="btnPeritoHidden" PopupControlID="PnlModalBuscarPerito"
                                            BackgroundCssClass="PanelModalBackground" DropShadow="true">
                                        </cc1:ModalPopupExtender>
                                        <asp:HiddenField runat="server" ID="btnPeritoHidden" />

                                        <asp:Panel runat="server" ID="PnlModalBuscarPerito" Style="width: 700px; display: none" SkinID="Modal">
                                            <uc7:ModalBuscarPeritos ID="ModalBuscarPeritos1" runat="server" OnConfirmClick="buscarPerito_ConfirmClick" />
                                        </asp:Panel>
                                        <asp:RangeValidator ID="revNumeroPerito" runat="server" ErrorMessage="Rango del número de perito erróneo"
                                            ValidationGroup="FiltroAvaluos" ControlToValidate="textNumeroPerito" ForeColor="Blue"
                                            MaximumValue="99999999999999999999" MinimumValue="0" SetFocusOnError="True" Enabled="False"
                                            Display="Dynamic">!</asp:RangeValidator>
                                    </td>
                                </tr>

                                <tr>
                                    <td style="width: 160px"></td>
                                    <td class="TextRigthBottom" rowspan="4" align="right" style="width: 336px">
                                        <asp:UpdatePanel ID="UpdatePanelBotonBuscar" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <uc5:Progreso ID="ProgresoBuscar" runat="server" AssociatedUpdatePanelID="UpdatePanelBotonBuscar" DisplayAfter="0" />
                                                <asp:ImageButton ID="btnBuscar" runat="server" ImageUrl="~/Images/search.gif"
                                                    OnClick="btnBuscar_Click" Width="16px" ValidationGroup="FiltroAvaluos" />
                                                <asp:ImageButton ID="btnLimpiar" runat="server" ImageUrl="~/Images/x.gif"
                                                    OnClick="btnLimpiar_Click" Width="16px" />
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                                
                            </table>
                        </div>
                    </asp:Panel>
                </div>
            </fieldset>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="UpdatePanelGridBuscador" runat="server" RenderMode="Inline"
        UpdateMode="Conditional">
        <ContentTemplate>
            <div class="DivPaddingBotton TextLeftTop">
                
                <!-- Campos ocultos para guardar el criterio de busqueda de la última busqueda realizada 
                (Son los SelectParameters de los DataSources) !-->
                <asp:HiddenField ID="HiddenIdPersonaToken" runat="server" />
                <asp:HiddenField ID="HiddenTokenModal" runat="server" />
                <!--!-->
                <cc1:ModalPopupExtender ID="extenderPnlInfoTokenModal" runat="server" Enabled="True"
                    TargetControlID="HiddenTokenModal" PopupControlID="pnlTokenModal" BackgroundCssClass="PanelModalBackground"
                    DropShadow="True" />
                <asp:Panel ID="pnlTokenModal" SkinID="Modal" Style="display: none; width: 280px;" runat="server">
                    <uc6:ModalInfo ID="ModalInfoToken" runat="server" OnConfirmClick="ModalInfoToken_Ok_Click" />
                </asp:Panel>
                <!--DataSources del gridView !-->
                <!--Usuarios perfil PERITO!-->

                <!--Usuarios perfil SOCIEDAD!-->

                <!--Usuarios perfil Dictamenes!-->

                <!--!-->
                <!-- GridView !-->
                <asp:GridView ID="gridViewAvaluos" runat="server" AllowPaging="True" AllowSorting="False"
                    AutoGenerateColumns="False" GridLines="Horizontal" EmptyDataText="No hay avalúos para el filtro seleccionado"
                    DataSourceID="" OnRowDataBound="gridViewAvaluos_RowDataBound" DataKeyNames=""
                    OnSelectedIndexChanged="gridViewAvaluos_SelectedIndexChanged" OnPageIndexChanging="gridViewAvaluos_PageIndexChanging"
                    PageSize="10" OnSorting="gridViewAvaluos_Sorting" HeaderStyle-CssClass="GridHeader" Width="100%">
                    <Columns>
                        <asp:BoundField HeaderText="Nº único" DataField="NUMEROUNICO"
                            SortExpression="NUMEROUNICO" HeaderStyle-HorizontalAlign="Center">
                            <HeaderStyle HorizontalAlign="Center" Width="105px" />
                            <ItemStyle Width="105px" HorizontalAlign="left" CssClass="GridCell" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Cuenta cat." DataField="CUENTACATASTRAL" SortExpression="CUENTACATASTRAL" HeaderStyle-HorizontalAlign="center">
                            <HeaderStyle HorizontalAlign="Center" Width="110px" />
                            <ItemStyle Width="110px" HorizontalAlign="center" CssClass="GridCell" />
                        </asp:BoundField>
                        <asp:BoundField DataField="REGISTROPERITO" HeaderText="Perito" SortExpression="REGISTROPERITO"
                            HeaderStyle-HorizontalAlign="center">

                            <HeaderStyle HorizontalAlign="Center" Width="6em" />

                            <ItemStyle Width="7em" HorizontalAlign="Center" CssClass="GridCell" />
                        </asp:BoundField>
                        <asp:BoundField DataField="REGISTROSOCIEDAD" HeaderText="Sociedad" SortExpression="REGISTROSOCIEDAD"
                            ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" CssClass="GridCell" />
                        </asp:BoundField>
                    </Columns>
                </asp:GridView>
                <!--!-->
                <asp:Panel ID="PnlModalCambiarEstado" runat="server" Style="width: 400px; display: none;" SkinID="Modal">
                    <uc3:ModalEstado ID="ModalEstado1" runat="server" OnConfirmClick="modalEstado_ConfirmClick" />
                </asp:Panel>

            </div>
            <div style="text-align: right">
                <asp:Label ID="lblCount" runat="server"></asp:Label>
            </div>
             <div id="divDownload" runat="server" style="text-align: right" visible="false">
                                <asp:Label ID="lblDownload" runat="server" Text="Descargar como:" />
                                <asp:DropDownList ID="ddlDowanload" runat="server" Width="100" />
                                <asp:ImageButton ID="btnDownload" runat="server" ImageUrl="~/Images/paper-clip.gif" OnClick="btnDownload_Click" />
                            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnBuscar" />
        </Triggers>
    </asp:UpdatePanel>
    <input type="hidden" id="hidBusquedaActual" runat="server" />
</asp:Content>
<asp:Content ID="ContentMenu" ContentPlaceHolderID="ContentPlaceHolderDMenuLocal" runat="Server">
    <uc2:MenuLocal ID="MenuLocal1" runat="server" />
</asp:Content>
