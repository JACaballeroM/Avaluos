<%@ Page Language="C#" MasterPageFile="~/MasterPageFE/MasterDetalleFE.master" AutoEventWireup="true" CodeFile="InvMercado.aspx.cs"  EnableEventValidation="true"  Inherits="BandejaEntrada" Title="Investigación de Mercado" %>
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
        <cc1:modalpopupextender ID="mpeErrorTareas" runat="server"  Enabled="True" 
        TargetControlID="hlnErrorTareas" PopupControlID="panErrorTareas" 
        Dropshadow="false" BackgroundCssClass="PanelModalBackground" />
        <asp:HyperLink ID="hlnErrorTareas" runat="server" Style="Display:none" />
        <asp:Panel ID="panErrorTareas" runat="server" Style="Display:none" SkinID="Modal">
            <uc11:ModalInfoExcepcion ID="errorTareas" runat="server" /> 
        </asp:Panel>
    </ContentTemplate> 
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="UpdatePanelFiltro" runat="server" RenderMode="Inline" UpdateMode="Always">
        <ContentTemplate>
        <fieldset class="formulario">
        <legend class="formulario">Reporte de investigación de mercado.</legend>
            <div class="DivPaddingBotton TextLeftMiddle">
                <asp:Panel ID="PanelBusqueda" runat="server"  
                    ondatabinding="Page_Load" >
                   <div style="width: 100%">
                        <asp:ValidationSummary ID="vsFiltroAvaluos" runat="server" 
                            ValidationGroup="FiltroAvaluos" Width ="100%"/>
                   </div>
                   <div>
                    <table class="TextLeftMiddle">
                         <tr>
                              <td style="width: 80px">
                                 <asp:Label ID="Label1" runat="server" Text="Fecha inicial" class="TextLeftTop" SkinID="Titulo2" ></asp:Label>
                            </td>
                            <td style="width: 168px" class="style5">
                                <asp:TextBox ID="txtFechaIni" runat="server" SkinID="TextBoxObligatorio" 
                                    MaxLength="10" Width="115px"></asp:TextBox>
                                <cc1:CalendarExtender ID="txtFechaIni_CalendarExtender" runat="server" Enabled="True" 
                                    PopupButtonID="btnFechaIni" TargetControlID="txtFechaIni">
                                </cc1:CalendarExtender>
                                <asp:ImageButton runat="server" ID="btnFechaIni" ImageUrl="~/images/calendario.png" CausesValidation="false" Height="16" Width="16" AlternateText="Seleccione una Fecha" />
                                <asp:RequiredFieldValidator ID="rfvFechaInicial" runat="server" ErrorMessage="Requerida una fecha" ControlToValidate="txtFechaIni"
                                     SetFocusOnError="True" ValidationGroup="FiltroAvaluos" Display="Dynamic" Enabled="false">*</asp:RequiredFieldValidator>
                                <asp:CompareValidator ID="cvFechaInicial" runat="server" ErrorMessage="Fecha errónea" ValidationGroup="FiltroAvaluos"
                                     ControlToValidate="txtFechaIni" ForeColor="Blue" Operator="DataTypeCheck" SetFocusOnError="True"
                                     Type="Date" Display="Dynamic">!</asp:CompareValidator>
                                </td>
                              <td style="width: 80px">
                                 <asp:Label ID="Label2" runat="server" Text="Fecha Final" class="TextLeftTop" SkinID="Titulo2" ></asp:Label>
                            </td>
                             <td style="width: 168px" class="style5">
                                <asp:TextBox ID="txtFechaFin" runat="server" SkinID="TextBoxObligatorio" MaxLength="10" Width="115px" />
                               <cc1:CalendarExtender ID="txtFechaFin_CalendarExtender" runat="server" Enabled="True" TargetControlID="txtFechaFin" PopupButtonID="btnFechaFin"></cc1:CalendarExtender>
                                <asp:ImageButton runat="server" ID="btnFechaFin" ImageUrl="~/images/calendario.png"
                                    CausesValidation="false" Height="16" Width="16" AlternateText="Seleccione una Fecha" />
                                <asp:RequiredFieldValidator ID="rfvFechaFin" runat="server" ErrorMessage="Requerida una fecha" ValidationGroup="FiltroAvaluos"
                                        ControlToValidate="txtFechaFin" SetFocusOnError="True" Display="Dynamic" Enabled="false">*</asp:RequiredFieldValidator>
                                <asp:CompareValidator ID="cvFechaFin" runat="server" ErrorMessage="Fecha errónea" ValidationGroup="FiltroAvaluos"
                                     ControlToValidate="txtFechaFin" ForeColor="Blue" Operator="DataTypeCheck" SetFocusOnError="True"
                                     Type="Date" Display="Dynamic">!</asp:CompareValidator>
                                <asp:CompareValidator ID="cvRangofechas" runat="server" ErrorMessage="Rango entre fechas erróneo" ValidationGroup="FiltroAvaluos"
                                     ControlToCompare="txtFechaFin" ControlToValidate="txtFechaIni" ForeColor="Blue"
                                    Operator="LessThan" SetFocusOnError="True" Type="Date" Display="Dynamic">!</asp:CompareValidator>
                            </td>
                        </tr>    
                    </table>

                    <table class="TextLeftMiddle">
                        <tr>
                            <td style="width: 160px">
                                 <asp:Label ID="lblCuenta" runat="server" Text="Cuenta catastral" class="TextLeftTop" SkinID="Titulo2" ></asp:Label>
                            </td>
                            <td style="width: 336px" >

                                <asp:TextBox ID="txtRegion" runat="server" MaxLength="3" Width="30px"
                                    SkinID="TextBoxNormal" onblur="javascript:if(this.value !=''){rellenar(this,this.value,3);}"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvRegion" runat="server" ControlToValidate="txtRegion" SetFocusOnError="True"
                                    ErrorMessage="Requerida una región" ValidationGroup="FiltroAvaluos" Enabled="false" Display="Dynamic" >*</asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="rfvRegionExp" runat="server" 
                                    ErrorMessage="Formato incorrecto de región.(Caracteres admitidos: <BR> 0,1,2,3,4,5,6,7,8,9,A,B,C,D,E,F,H,J,K,M,N,P,Q,R,T,U,V,W,X,Y)" ForeColor="Blue"
                                    SetFocusOnError="False" Display="None" ControlToValidate="txtRegion" 
                                    ValidationGroup="FiltroAvaluos" Enabled="false" Visible="false" 
                                    ValidationExpression="[0|1|2|3|4|5|6|7|8|9|A|B|C|D|E|F|H|J|K|M|N|P|Q|R|T|U|V|W|X|Y]{3}">!</asp:RegularExpressionValidator>

                                <asp:TextBox ID="txtManzana" runat="server" MaxLength="3" Width="30px"
                                    SkinID="TextBoxNormal" onblur="if(this.value!=''){rellenar(this,this.value,3);}"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvManzana" runat="server" ControlToValidate="txtManzana" SetFocusOnError="True" Enabled="false"
                                     ErrorMessage="Requerida una manzana" ValidationGroup="FiltroAvaluos" Display="Dynamic" >*</asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="rfvManzanaExp" runat="server" 
                                    ErrorMessage="Formato incorrecto de manzana.(Caracteres admitidos: <BR> 0,1,2,3,4,5,6,7,8,9,A,B,C,D,E,F,H,J,K,M,N,P,Q,R,T,U,V,W,X,Y)"
                                    ForeColor="Blue" ControlToValidate="txtManzana" ValidationGroup="FiltroAvaluos" 
                                    Enabled="false" Visible="false"  Display="None" SetFocusOnError="False"
                                    ValidationExpression="[0|1|2|3|4|5|6|7|8|9|A|B|C|D|E|F|H|J|K|M|N|P|Q|R|T|U|V|W|X|Y]{3}">!</asp:RegularExpressionValidator>
                               
                        <asp:TextBox ID="txtCuenta" runat="server" Visible="false" ></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 160px">
                                <asp:Label ID="lblTipo" runat="server" Text="Tipo" class="TextLeftTop" SkinID="Titulo2" ></asp:Label>
                            </td>
                            <td style="width: 336px" class="style5">
                                <asp:DropDownList ID="ddlTipo" runat="server" Width="100%"></asp:DropDownList>
                            </td>
                        </tr>
                       <tr>
                            <td style="width: 160px">
                                <uc5:Progreso ID="Progreso1" runat="server" AssociatedUpdatePanelID="UpdatePanelFiltro" DisplayAfter="0" />
                                <asp:Label ID="lblDelegación" runat="server" Text="Delegación" class="TextLeftTop" SkinID="Titulo2" ></asp:Label>
                            </td>
                            <td style="width: 336px" class="style5">
                                <asp:DropDownList ID="ddlDelegacion" runat="server" Width="100%"  AutoPostBack="True"
                                    OnSelectedIndexChanged="ddlDelegacion_SelectedIndexChanged"></asp:DropDownList>
                            </td>
                        </tr>
                       <tr>
                            <td style="width: 160px">
                                <asp:Label ID="lblColonia" runat="server" Text="Colonia" class="TextLeftTop" SkinID="Titulo2" ></asp:Label>
                            </td>
                            <td style="width: 336px" class="style5">
                                <asp:DropDownList ID="ddlColonia" runat="server" Width="100%" Enabled="false"></asp:DropDownList>
                            </td>
                        </tr>
                         <tr>
                            <td style="width: 160px"></td>
                            <td class="TextRigthBottom" rowspan="4" align="right" style="width: 336px">
                                <asp:UpdatePanel ID="UpdatePanelBotonBuscar" runat="server" RenderMode="Inline" UpdateMode="Always">
                                   <ContentTemplate>
                                      <uc5:Progreso ID="ProgresoBuscar" runat="server" AssociatedUpdatePanelID="UpdatePanelBotonBuscar" DisplayAfter="0" />
                                       <asp:ImageButton ID="btnBuscar" runat="server" ImageUrl="~/Images/search.gif" 
                                            OnClick="btnBuscar_Click" Width="16px" ValidationGroup="FiltroAvaluos" />
                                       
                                      <asp:ImageButton ID="btnLimpiar" runat="server" ImageUrl="~/Images/x.gif" 
                                            OnClick="btnLimpiar_Click" Width="16px" />
                                 </ContentTemplate>
                                    <%--<Triggers>
                                        <asp:AsyncPostBackTrigger ControlID ="btnLimpiar" EventName ="click" />
                                    </Triggers>--%>
                                </asp:UpdatePanel>
                                
                            </td>
                        </tr> 
                    </table>
                    </div>
                </asp:Panel>
            </div>
        </fieldset>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnLimpiar" />
        </Triggers>
    </asp:UpdatePanel> 

    <asp:UpdatePanel ID="UpdatePanelGridBuscador" runat="server" RenderMode="Inline"
        UpdateMode="Conditional">
        <ContentTemplate>
            <uc5:Progreso ID="Progreso2" runat="server" AssociatedUpdatePanelID="UpdatePanelGridBuscador" DisplayAfter="0" />
            <div class="DivPaddingBotton TextLeftTop">
                <table style="width: 100%;">
                    <tr>  
                        <td align="left">
                            <asp:UpdatePanel ID="UpdatePanelBotonesGridView" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                                <ContentTemplate>
                                   <div>
                                    <asp:Label ID="lblTituloListado" SkinID="Titulo2" class="TextLeftTop" runat="server"
                                         Text="Reporte"></asp:Label><br/>
                                  </div>
                               
                                   <div class="botonera">
                                   <asp:ImageButton ID="btnWord" runat="server" ImageUrl="~/Images/two-docs.gif" 
                                            OnClick="btnWord_Click" Width="16px" Visible="false" ValidationGroup="FiltroAvaluos" />
                                  </div>                
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                        <td align="right"></td>
                    </tr>
                </table>
                <!-- Campos ocultos para guardar el criterio de busqueda de la última busqueda realizada 
                (Son los SelectParameters de los DataSources) !-->
                <asp:HiddenField ID="HiddenIdPersonaToken" runat="server"/>
                <asp:HiddenField ID="HiddenTokenModal" runat="server"/>
                <asp:HiddenField ID="HiddenSortExp"    runat="server" />
                <asp:HiddenField ID="HiddenCuentaCatastral" runat="server" />
                <!--!-->
                <cc1:ModalPopupExtender ID="extenderPnlInfoTokenModal" runat="server" Enabled="True"
                     TargetControlID="HiddenTokenModal" PopupControlID="pnlTokenModal" BackgroundCssClass="PanelModalBackground"
                     DropShadow="True" />
                <asp:Panel ID="pnlTokenModal" SkinID="Modal" Style="display: none; width:280px;" runat="server">
                    <uc6:ModalInfo ID="ModalInfoToken" runat="server" OnConfirmClick="ModalInfoToken_Ok_Click" />
                </asp:Panel>
                <!--DataSources del gridView !-->
                    <!--Usuarios perfil PERITO!--> 
                
                    <!--Usuarios perfil SOCIEDAD!--> 
                
                    <!--Usuarios perfil Dictamenes!--> 

                <!--!-->
                <!-- GridView !-->
                <asp:GridView ID="gridViewAvaluos" runat="server" AllowPaging="True" AllowSorting="false"
                    AutoGenerateColumns="False" SkinID="GridMercadoView" GridLines="Horizontal" EmptyDataText="No hay registros para el filtro seleccionado"
                    OnRowDataBound="gridViewAvaluos_RowDataBound"
                    OnPageIndexChanging="gridViewAvaluos_PageIndexChanging"
                    PageSize="10" OnSorting="gridViewAvaluos_Sorting" HeaderStyle-CssClass="GridHeader" Width="100%" >
                    <Columns >
                        <asp:BoundField HeaderText="Delegación" DataField="DELEGACION"  
                            SortExpression="DELEGACION" HeaderStyle-HorizontalAlign="Center">
                              <HeaderStyle HorizontalAlign="Center"   Width="105px"/>
                              <ItemStyle Width="105px" HorizontalAlign="left" CssClass = "GridCell"/>
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Colonia" DataField="COLONIA" 
                            SortExpression="COLONIA" HeaderStyle-HorizontalAlign="Center" >
                            <HeaderStyle HorizontalAlign="Center" Width="85px"/>
                            <ItemStyle  HorizontalAlign="left" CssClass = "GridCell" Width="85px"/>
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Región" DataField="REGION" SortExpression="REGION" HeaderStyle-HorizontalAlign="center">
                            <HeaderStyle HorizontalAlign="Center" Width="110px"  />
                           <ItemStyle Width="110px" HorizontalAlign="center" CssClass = "GridCell"/>
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Manzana" DataField="MANZANA" SortExpression="MANZANA" HeaderStyle-HorizontalAlign="center">
                            <HeaderStyle HorizontalAlign="Center" Width="110px"  />
                           <ItemStyle Width="110px" HorizontalAlign="center" CssClass = "GridCell"/>
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Ubicación" DataField="UBICACION" SortExpression="UBICACION" HeaderStyle-HorizontalAlign="center">
                            <HeaderStyle HorizontalAlign="Center" Width="110px"  />
                           <ItemStyle Width="110px" HorizontalAlign="center" CssClass = "GridCell"/>
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Descripción" DataField="DESCRIPCION" SortExpression="DESCRIPCION" HeaderStyle-HorizontalAlign="center">
                            <HeaderStyle HorizontalAlign="Center" Width="110px"  />
                           <ItemStyle Width="110px" HorizontalAlign="center" CssClass = "GridCell"/>
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Precios solicitado" DataField="PRECIOSOLICITADO" SortExpression="PRECIO" HeaderStyle-HorizontalAlign="center">
                            <HeaderStyle HorizontalAlign="Center" Width="110px"  />
                           <ItemStyle Width="110px" HorizontalAlign="center" CssClass = "GridCell"/>
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Superficie" DataField="SUPERFICIE" SortExpression="SUPERFICIE" HeaderStyle-HorizontalAlign="center">
                            <HeaderStyle HorizontalAlign="Center" Width="110px"  />
                           <ItemStyle Width="110px" HorizontalAlign="center" CssClass = "GridCell"/>
                        </asp:BoundField>
                        <asp:BoundField HeaderText="VU" DataField="VALORUNITARIO" SortExpression="VUNITARIO" HeaderStyle-HorizontalAlign="center">
                            <HeaderStyle HorizontalAlign="Center" Width="110px"  />
                           <ItemStyle Width="105px" HorizontalAlign="center" CssClass = "GridCell"/>
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Tipo" DataField="TIPO" SortExpression="TIPO" HeaderStyle-HorizontalAlign="center">
                            <HeaderStyle HorizontalAlign="Center" Width="110px"  />
                           <ItemStyle Width="110px" HorizontalAlign="center" CssClass = "GridCell"/>
                        </asp:BoundField>
                    </Columns>
                </asp:GridView>
                <!--!-->
                <asp:Panel ID="PnlModalCambiarEstado" runat="server" Style="width: 400px; display: none;" SkinID="Modal">
                    <uc3:ModalEstado ID="ModalEstado1" runat="server" OnConfirmClick="modalEstado_ConfirmClick" />
                </asp:Panel>
                <asp:Panel runat="server" ID="PnlModalBuscarNotario" Style="width: 700px; display: none"  SkinID="Modal">
                    <uc4:ModalBuscarNotarios ID="ModalBuscarNotarios1" runat="server"  OnConfirmClick="buscarNotario_ConfirmClick" />
                </asp:Panel>
                <cc1:AlwaysVisibleControlExtender ID="PnlModalBuscarNotarioAlwaysVisibleControlExtender"
                    runat="server" TargetControlID="PnlModalBuscarNotario" Enabled="false">
                </cc1:AlwaysVisibleControlExtender>   
            </div>
           <div style="text-align: right">
                <asp:Label ID="lblCount" runat="server"></asp:Label>
           </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnBuscar" />
        </Triggers>
    </asp:UpdatePanel>
       <input type="hidden" id="hidBusquedaActual" runat="server" />
</asp:Content>
<asp:Content ID="ContentMenu" ContentPlaceHolderID="ContentPlaceHolderDMenuLocal"  runat="Server">
    <uc2:MenuLocal ID="MenuLocal1" runat="server" />
</asp:Content>
