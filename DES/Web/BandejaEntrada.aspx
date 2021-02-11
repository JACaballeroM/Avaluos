<%@ Page Language="C#" MasterPageFile="~/MasterPageFE/MasterDetalleFE.master" AutoEventWireup="true" CodeFile="BandejaEntrada.aspx.cs"  EnableEventValidation="true"  Inherits="BandejaEntrada" Title="Bandeja de entrada" %>
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
    <asp:UpdatePanel ID="UpdatePanelFiltro" runat="server" RenderMode="Inline" UpdateMode="Conditional">
        <ContentTemplate>
        <fieldset class="formulario">
        <legend class="formulario">Filtro de avalúos</legend>
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
                            <td style="width: 160px">
                                <asp:RadioButton ID="rbFechas" runat="server" GroupName="rbBusquedaGroup" Text="Rango de fechas"
                                    AutoPostBack="True" OnCheckedChanged="rbBusquedaGroup_CheckedChanged" Checked="True" />
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
                                <asp:RadioButton ID="rbNumeroAvaluo" runat="server" GroupName="rbBusquedaGroup" Text="Nº avalúo"
                                     AutoPostBack="True" OnCheckedChanged="rbBusquedaGroup_CheckedChanged" />
                            </td>
                            <td style="width: 336px" class="style5">
                                <asp:TextBox ID="textNumeroAvaluo" runat="server" Enabled="False" 
                                      SkinID="TextBoxObligatorio" Width="125px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvNumeroAvaluo" runat="server" ErrorMessage="Requiere un número de avalúo"
                                      ControlToValidate="textNumeroAvaluo" SetFocusOnError="True" ValidationGroup="FiltroAvaluos"
                                      Enabled="False" Display="Dynamic">*</asp:RequiredFieldValidator>
                                <asp:Label ID="lblNumPerito" SkinID="Titulo2" class="TextLeftTop" runat="server"
                                    Text="Perito"></asp:Label>
                                <asp:Label ID="lblNumPerSoci" SkinID="Titulo2" class="TextLeftTop" runat="server"
                                    Text="Per. o Soc." Visible="false" ></asp:Label>
                                <asp:TextBox ID="textNumeroPerito" runat="server" Enabled="False" Width="89px"></asp:TextBox>
                                <asp:ImageButton ID="btnPeritos" runat="server" CausesValidation="False" ImageUrl="~/Images/user_p.gif"
                                      OnClick="btnPeritos_Click" ToolTip="Buscar Peritos" Visible="False" 
                                      Enabled="False" />
                                <cc1:ModalPopupExtender ID="btnPeritos_ModalPopupExtender" runat="server" DynamicServicePath=""
                                     Enabled="True" TargetControlID="btnPeritoHidden" PopupControlID="PnlModalBuscarPerito"
                                     BackgroundCssClass="PanelModalBackground" DropShadow="true">
                                </cc1:ModalPopupExtender>
                                <asp:HiddenField runat="server" ID="btnPeritoHidden" />
                                <asp:Panel runat="server" ID="PnlModalBuscarPerito"  Style="width: 700px; display: none" SkinID="Modal">
                                <uc7:ModalBuscarPeritos ID="ModalBuscarPeritos1" runat="server" OnConfirmClick="buscarPerito_ConfirmClick" />
                            </asp:Panel>
                                <asp:RangeValidator ID="revNumeroPerito" runat="server" ErrorMessage="Rango del número de perito erróneo"
                                ValidationGroup="FiltroAvaluos" ControlToValidate="textNumeroPerito" ForeColor="Blue"
                                MaximumValue="99999999999999999999" MinimumValue="0" SetFocusOnError="True" Enabled="False"
                                Display="Dynamic">!</asp:RangeValidator>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 160px; height: 24px;">
                                <asp:RadioButton ID="rbIdAvaluo" runat="server" GroupName="rbBusquedaGroup" Text="Nº único avalúo"
                                    AutoPostBack="True" OnCheckedChanged="rbBusquedaGroup_CheckedChanged"  />
                            </td>
                            <td style="height: 24px; width: 336px;">
                                <asp:TextBox ID="txtIdAvaluo" runat="server" SkinID="TextBoxObligatorio" Enabled="False"
                                    MaxLength="20" Width="125px" />
                                <asp:RequiredFieldValidator ID="rfvIdAvaluo" runat="server" 
                                     ControlToValidate="txtIdAvaluo" Display="Dynamic" Enabled="False" 
                                     ErrorMessage="Requiere un identificador de avalúo" SetFocusOnError="True" 
                                     ValidationGroup="FiltroAvaluos">*</asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="revIdAvaluo" runat="server" 
                                    ErrorMessage = "Formato nº único erroneo. El nº único debe tener el formato A-xxx-aaaa-zzzzzz <BR> (Ej: A-COM-2010-1154)"
                                    ControlToValidate="txtIdAvaluo" Display="Dynamic" 
                                    SetFocusOnError="True" 
                                    ValidationExpression="^(\s)*A-(CAT|COM)-[0-9]{4}-[0-9]{1,9}(\s)*$"  
                                    ValidationGroup="FiltroAvaluos" ForeColor="Blue" >!</asp:RegularExpressionValidator>
                           </td>
                         </tr>
                        <tr>
                            <td style="width: 160px">
                                <asp:RadioButton ID="rbCuenta" runat="server" CssClass="Titulo2" GroupName="rbBusquedaGroup"
                                    Text="Cuenta catastral" AutoPostBack="True" OnCheckedChanged="rbBusquedaGroup_CheckedChanged" />
                            </td>
                            <td style="width: 336px" >

                                <asp:TextBox ID="txtRegion" runat="server" Enabled="false" MaxLength="3" Width="30px"
                                    SkinID="TextBoxObligatorio" onblur="javascript:if(this.value !=''){rellenar(this,this.value,3);}"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvRegion" runat="server" ControlToValidate="txtRegion" SetFocusOnError="True"
                                    ErrorMessage="Requerida una región" ValidationGroup="FiltroAvaluos" Enabled="false" Display="Dynamic" >*</asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="rfvRegionExp" runat="server" 
                                    ErrorMessage="Formato incorrecto de región.(Caracteres admitidos: <BR> 0,1,2,3,4,5,6,7,8,9,A,B,C,D,E,F,H,J,K,M,N,P,Q,R,T,U,V,W,X,Y)" ForeColor="Blue"
                                    SetFocusOnError="False" Display="None" ControlToValidate="txtRegion" 
                                    ValidationGroup="FiltroAvaluos" Enabled="false" Visible="false" 
                                    ValidationExpression="[0|1|2|3|4|5|6|7|8|9|A|B|C|D|E|F|H|J|K|M|N|P|Q|R|T|U|V|W|X|Y]{3}">!</asp:RegularExpressionValidator>

                                <asp:TextBox ID="txtManzana" runat="server" Enabled="false" MaxLength="3" Width="30px"
                                    SkinID="TextBoxObligatorio" onblur="if(this.value!=''){rellenar(this,this.value,3);}"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvManzana" runat="server" ControlToValidate="txtManzana" SetFocusOnError="True" Enabled="false"
                                     ErrorMessage="Requerida una manzana" ValidationGroup="FiltroAvaluos" Display="Dynamic" >*</asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="rfvManzanaExp" runat="server" 
                                    ErrorMessage="Formato incorrecto de manzana.(Caracteres admitidos: <BR> 0,1,2,3,4,5,6,7,8,9,A,B,C,D,E,F,H,J,K,M,N,P,Q,R,T,U,V,W,X,Y)"
                                    ForeColor="Blue" ControlToValidate="txtManzana" ValidationGroup="FiltroAvaluos" 
                                    Enabled="false" Visible="false"  Display="None" SetFocusOnError="False"
                                    ValidationExpression="[0|1|2|3|4|5|6|7|8|9|A|B|C|D|E|F|H|J|K|M|N|P|Q|R|T|U|V|W|X|Y]{3}">!</asp:RegularExpressionValidator>
                               
                                <asp:TextBox ID="txtLote" runat="server" Enabled="false" MaxLength="2" 
                                    Width="20px" 
                                    onblur="javascript:if(this.value!=''){rellenar(this,this.value,2);}"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvLote" runat="server" ControlToValidate="txtLote" SetFocusOnError="True" Enabled="false"
                                     ErrorMessage="Requerido un lote" ValidationGroup="FiltroAvaluos" Display="Dynamic">*</asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="rfvLoteExp" runat="server" 
                                    ErrorMessage="Formato incorrecto de lote.(Caracteres admitidos: <BR> 0,1,2,3,4,5,6,7,8,9,A,B,C,D,E,F,H,J,K,M,N,P,Q,R,T,U,V,W,X,Y)" 
                                    ForeColor="Blue" ControlToValidate="txtLote" ValidationGroup="FiltroAvaluos"
                                    Enabled="false" Visible="false"  Display="None" SetFocusOnError="False"
                                    ValidationExpression="[0|1|2|3|4|5|6|7|8|9|A|B|C|D|E|F|H|J|K|M|N|P|Q|R|T|U|V|W|X|Y]{2}">!</asp:RegularExpressionValidator>
                              
                                <asp:TextBox ID="txtUnidadPrivativa" runat="server" Enabled="false" MaxLength="3" Width="30px"  onblur="javascript:if(this.value !=''){rellenar(this,this.value,3);}"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvUnidadPrivativa" runat="server" ControlToValidate="txtUnidadPrivativa"
                                     SetFocusOnError="True" Enabled="false" ErrorMessage="Requerido un condominio"
                                     ValidationGroup="FiltroAvaluos" Display="Dynamic">*</asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="rfvUnidadPrivativaExp" runat="server" 
                                    ErrorMessage="Formato incorrecto de unidad privativa.(Caracteres admitidos: <BR> 0,1,2,3,4,5,6,7,8,9,A,B,C,D,E,F,H,J,K,M,N,P,Q,R,T,U,V,W,X,Y)" 
                                    ControlToValidate="txtUnidadPrivativa" ForeColor="Blue" ValidationGroup="FiltroAvaluos" Display="None" SetFocusOnError="False"
                                    Enabled="false"  ValidationExpression="[0|1|2|3|4|5|6|7|8|9|A|B|C|D|E|F|H|J|K|M|N|P|Q|R|T|U|V|W|X|Y]{3}">!</asp:RegularExpressionValidator>
                    
                        <asp:TextBox ID="txtCuenta" runat="server" Enabled="False" Visible="false" ></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 160px">
                                <asp:Label ID="lblEstado" runat="server" Text="Estado" class="TextLeftTop" SkinID="Titulo2" ></asp:Label>
                                <asp:CheckBox ID="chkEstado" runat="server" Text="Estado" AutoPostBack="true" 
                                     Checked="True" Visible="False" />
                                <asp:RadioButton ID="rbEstado" runat="server" GroupName="rbBusquedaGroup" Text="Estado"
                                     AutoPostBack="True" OnCheckedChanged="rbBusquedaGroup_CheckedChanged" Visible="False" />
                            </td>
                            <td style="width: 336px" class="style5">
                                <asp:DropDownList ID="ddlEstado" runat="server" Width="100%"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>  
                            <td style="width: 160px">
                              <asp:Label ID="lblVigente" runat="server" SkinID="Titulo2" Text="Vigencia" Visible="true"></asp:Label>
                              <asp:CheckBox ID="checkVigente" runat="server" Checked="True" Text="Vigente" Visible="False" />
                            </td>
                            <td style="width: 336px" class="style5">
                               <div>
                                 <asp:RadioButton ID="RadioButtonTodos" runat="server" Text="Todos" GroupName="vigencia" oncheckedchanged="RadioButtonTodos_CheckedChanged"/>
                                 <asp:RadioButton ID="RadioButtonVigente" runat="server" Text="Vigentes" GroupName="vigencia" Checked="True" />
                                 <asp:RadioButton ID="RadioButtonNoVigente" runat="server" Text="No vigentes" GroupName="vigencia"/>
                               </div>
                            </td>  
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
                <table style="width: 100%;">
                    <tr>  
                        <td align="left">
                            <asp:UpdatePanel ID="UpdatePanelBotonesGridView" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                                <ContentTemplate>
                                   <div>
                                    <asp:Label ID="lblTituloListado" SkinID="Titulo2" class="TextLeftTop" runat="server"
                                         Text="Listado de avalúos"></asp:Label><br/>
                                  </div>
                               
                                   <div class="botonera">
                                    <asp:ImageButton ID="btnCambiarEstado" runat="server" ImageUrl="~/Images/back-forth_p.gif"
                                         AlternateText="Cambiar estado del avalúo" Enabled="false" CausesValidation="False"  />
                                    <cc1:ModalPopupExtender ID="btnCambiarEstado_ModalPopupExtender" runat="server" DynamicServicePath=""
                                         Enabled="True" TargetControlID="btnCambiarEstado" PopupControlID="PnlModalCambiarEstado"
                                         BackgroundCssClass="PanelModalBackground">
                                    </cc1:ModalPopupExtender>
                                    <asp:ImageButton ID="btnVerAvaluosProximos" runat="server" enabled="false" CausesValidation="False"
                                         ImageUrl="~/Images/camera_p.gif" ToolTip="Avalúos Próximos" AlternateText="Avalúos Próximos" OnClick="btnAvaluosProximos_Click"></asp:ImageButton>
                                    <asp:ImageButton ID="btnNotario" runat="server" AlternateText="Asignar notario"
                                         ImageUrl="~/Images/user_p.gif" Enabled="false" OnClick="btnNotario_Click" 
                                         CausesValidation="False" Height="16px" />
                                    <asp:HyperLink ID="HlinkGenerarAcuse" runat="server" Target="_blank"
                                         ImageUrl="~/Images/two-docs_p.gif" ToolTip="Acuse de avalúo"></asp:HyperLink>
                                    <asp:HyperLink ID="HlinkInforme" runat="server" Target="_blank"
                                         ImageUrl="~/Images/zoom-in_p.gif" ToolTip="Visualizar Avalúo"></asp:HyperLink> 
                                    <cc1:ModalPopupExtender ID="btnNotario_ModalPopupExtender" runat="server" DynamicServicePath=""
                                         Enabled="True" TargetControlID="btnNotarioHidden" PopupControlID="PnlModalBuscarNotario"
                                         BackgroundCssClass="PanelModalBackground" DropShadow="true">
                                    </cc1:ModalPopupExtender>
                                    <asp:HiddenField runat="server" ID="btnNotarioHidden" 
                                         onvaluechanged="btnNotarioHidden_ValueChanged" /> 
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
                <asp:HiddenField ID="HiddenNumUnicoAv" runat="server"/>
                <asp:HiddenField ID="HiddenTokenModal" runat="server" />
                <asp:HiddenField ID="HiddenFechaFin"   runat="server" />
                <asp:HiddenField ID="HiddenFechaIni"   runat="server" />
                <asp:HiddenField ID="HiddenNumAvaluo"  runat="server" />
                <asp:HiddenField ID="HiddenEstado"     runat="server" />
                <asp:HiddenField ID="HiddenIdAvaluo"   runat="server" />
                <asp:HiddenField ID="HiddenSortExp"    runat="server" />
                <asp:HiddenField ID="HiddenReg_PerSoci" runat="server" />
                <asp:HiddenField ID="HiddenCuentaCatastral" runat="server" />
                <asp:HiddenField ID="HiddenVigente" runat="server" Value="T" />
                <asp:HiddenField ID="HiddenNumUnico" runat="server" />
                <!--!-->
                <asp:HiddenField ID="HiddenTipoRegistro" runat="server" />
                <cc1:ModalPopupExtender ID="extenderPnlInfoTokenModal" runat="server" Enabled="True"
                     TargetControlID="HiddenTokenModal" PopupControlID="pnlTokenModal" BackgroundCssClass="PanelModalBackground"
                     DropShadow="True" />
                <asp:Panel ID="pnlTokenModal" SkinID="Modal" Style="display: none; width:280px;" runat="server">
                    <uc6:ModalInfo ID="ModalInfoToken" runat="server" OnConfirmClick="ModalInfoToken_Ok_Click" />
                </asp:Panel>
                <!--DataSources del gridView !-->
                    <!--Usuarios perfil PERITO!--> 
                    <asp:ObjectDataSource ID="odsPorIdAvaluo" runat="server" 
                        SelectMethod="ObtenerAvaluoPorIdAvaluoEstadoVigencia" 
                        TypeName="DseAvaluosConsultaPager" SortParameterName="SortExpression" 
                        EnablePaging="True" MaximumRowsParameterName="pageSize" 
                        SelectCountMethod="NumTotalObtenerAvaluoPorIdAvaluoEstadoVigencia2" 
                        StartRowIndexParameterName="indice">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="HiddenNumUnicoAv" Name="idValuo" 
                                PropertyName="Value" Type="String" />
                            <asp:ControlParameter ControlID="HiddenEstado" Name="estado" 
                                PropertyName="Value" Type="Int32" />
                            <asp:ControlParameter ControlID="HiddenVigente" Name="vigencia" 
                                PropertyName="Value" Type="String" />
                            <asp:ControlParameter ControlID="HiddenIdPersonaToken" Name="idpersonaperito" 
                                PropertyName="Value" Type="Int32" />
                            <asp:Parameter Name="pageSize" Type="Int32" />
                            <asp:Parameter Name="indice" Type="Int32" />
                            <asp:Parameter Name="SortExpression" Type="String" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                    <asp:ObjectDataSource ID="odsPorFecha" runat="server" EnablePaging="True" MaximumRowsParameterName="pageSize"
                        SelectCountMethod="NumTotalFilasFechaPeritoVigenciaEstado" SelectMethod="ObtenerAvaluosPorFechaPeritoVigenciaEstado"
                        StartRowIndexParameterName="indice" TypeName="DseAvaluosConsultaPager" 
                        SortParameterName="SortExpression"  >
                        <SelectParameters>
                            <asp:ControlParameter ControlID="HiddenFechaIni" DefaultValue="" Name="fechaInicio"
                                PropertyName="Value" Type="DateTime" />
                            <asp:ControlParameter ControlID="HiddenFechaFin" Name="fechaFin" PropertyName="Value"
                                Type="DateTime" />
                            <asp:ControlParameter ControlID="HiddenIdPersonaToken" Name="idPerito" PropertyName="Value"
                                Type="Int32" />
                            <asp:ControlParameter ControlID="HiddenVigente" Name="vigente" 
                                PropertyName="Value" Type="String" />
                            <asp:ControlParameter ControlID="HiddenEstado" Name="codestado" 
                                PropertyName="Value" Type="Int32" />
                            <asp:Parameter Name="pageSize" Type="Int32" />
                            <asp:Parameter Name="indice" Type="Int32" />
                            <asp:Parameter DefaultValue="" Name="SortExpression" Type="String" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                    <asp:ObjectDataSource ID="odsPorNumAvaluo" runat="server" EnablePaging="True" MaximumRowsParameterName="pageSize"
                        SelectCountMethod="NumTotalFilasNumValuoPeritoEstadoVig" SelectMethod="ObtenerAvaluosPorNumValuoPerito_EstadoVig"
                        StartRowIndexParameterName="indice" TypeName="DseAvaluosConsultaPager"   SortParameterName="SortExpression" >
                        <SelectParameters>
                            <asp:ControlParameter ControlID="HiddenNumAvaluo" Name="numValuo" PropertyName="Value"
                                Type="String" />
                            <asp:ControlParameter ControlID="HiddenIdPersonaToken" Name="idPersona" PropertyName="Value"
                                Type="Decimal" />
                            <asp:ControlParameter ControlID="HiddenVigente" Name="vigente" PropertyName="Value"
                                Type="String" />
                            <asp:ControlParameter ControlID="HiddenEstado" Name="codestado" 
                                PropertyName="Value" Type="Int32" />
                            <asp:Parameter Name="pageSize" Type="Int32" />
                            <asp:Parameter Name="indice" Type="Int32" />
                            <asp:Parameter Name="SortExpression" Type="String" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                    <asp:ObjectDataSource ID="odsCuentaCatastral" runat="server" EnablePaging="True"
                        MaximumRowsParameterName="pageSize" SelectCountMethod="NumTotalFilasCuentaCatastralPeritoVigEstado"
                        SelectMethod="ObtenerAvaluosPorCuentaCatastralPeritoVigenciaEstado" StartRowIndexParameterName="indice"
                        TypeName="DseAvaluosConsultaPager" 
                         SortParameterName="SortExpression"  >
                        <SelectParameters>
                            <asp:ControlParameter ControlID="HiddenCuentaCatastral" Name="cuentaCatastral" PropertyName="Value"
                                Type="String" />
                            <asp:ControlParameter ControlID="HiddenIdPersonaToken" Name="idPerito" PropertyName="Value"
                                Type="String" />
                            <asp:ControlParameter ControlID="HiddenVigente" Name="vigente" PropertyName="Value"
                                Type="String" />
                            <asp:ControlParameter ControlID="HiddenEstado" Name="codestado" 
                                PropertyName="Value" Type="Int32" />
                            <asp:Parameter Name="pageSize" Type="Int32" />
                            <asp:Parameter Name="indice" Type="Int32" />
                            <asp:Parameter Name="SortExpression" Type="String" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                
                    <!--Usuarios perfil SOCIEDAD!--> 
                    <asp:ObjectDataSource ID="odsPorIdAvaluoSoci" runat="server" 
                      EnablePaging="True" MaximumRowsParameterName="pageSize" 
                      SelectCountMethod="NumTotalObtenerAvaluoPorIdAvaluoSociEstadoVigencia"
                      SelectMethod="ObtenerAvaluoPorIdAvaluoSociEstadoVigencia" 
                      StartRowIndexParameterName="indice" 
                      TypeName="DseAvaluosConsultaPager" SortParameterName="SortExpression" >
                    <SelectParameters>
                        <asp:ControlParameter ControlID="HiddenNumUnicoAv" Name="idValuo" 
                            PropertyName="Value" Type="String" />
                        <asp:ControlParameter ControlID="HiddenEstado" Name="estado" 
                            PropertyName="Value" Type="Int32" />
                        <asp:ControlParameter ControlID="HiddenVigente" Name="vigencia" 
                            PropertyName="Value" Type="String" />
                        <asp:ControlParameter ControlID="HiddenIdPersonaToken" Name="idpersonasoci" 
                            PropertyName="Value" Type="Int32" />
                        <asp:Parameter Name="pageSize" Type="Int32" />
                        <asp:Parameter Name="indice" Type="Int32" />
                        <asp:Parameter Name="SortExpression" Type="String" />
                    </SelectParameters>
                </asp:ObjectDataSource>
                    <asp:ObjectDataSource ID="odsPorFechaSoci" runat="server" EnablePaging="True" 
                    MaximumRowsParameterName="pageSize" 
                    SelectCountMethod="NumTotalFilasFechaSociedad_EstadoVig" 
                    SelectMethod="ObtenerAvaluosPorFechaSociedad_EstadoVig" 
                    StartRowIndexParameterName="indice" 
                    TypeName="DseAvaluosConsultaPager" SortParameterName="SortExpression">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="HiddenFechaIni" Name="fechaInicio" 
                            PropertyName="Value" Type="DateTime" />
                        <asp:ControlParameter ControlID="HiddenFechaFin" Name="fechaFin" 
                            PropertyName="Value" Type="DateTime" />
                        <asp:ControlParameter ControlID="HiddenIdPersonaToken" Name="idSociedad" 
                            PropertyName="Value" Type="Int32" />
                        <asp:ControlParameter ControlID="HiddenVigente" Name="vigente" 
                            PropertyName="Value" Type="String" />
                        <asp:ControlParameter ControlID="HiddenEstado" Name="codestado" 
                            PropertyName="Value" Type="Int32" />
                        <asp:Parameter Name="pageSize" Type="Int32" />
                        <asp:Parameter Name="indice" Type="Int32" />
                        <asp:Parameter Name="SortExpression" Type="String" />
                    </SelectParameters>
                </asp:ObjectDataSource>
                    <asp:ObjectDataSource ID="odsPorNumAvaluoSoci" runat="server" 
                    EnablePaging="True" MaximumRowsParameterName="pageSize" 
                    SelectCountMethod="NumTotalFilasNumValuoSociedadEstadoVig" 
                    SelectMethod="ObtenerAvaluosPorNumValuoSociedad_EstadoVig" 
                    StartRowIndexParameterName="indice" 
                    TypeName="DseAvaluosConsultaPager" SortParameterName="SortExpression">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="HiddenNumAvaluo" Name="numValuo" 
                            PropertyName="Value" Type="String" />
                        <asp:ControlParameter ControlID="HiddenReg_PerSoci" Name="registroPerito" 
                            PropertyName="Value" Type="String" />
                        <asp:ControlParameter ControlID="HiddenIdPersonaToken" Name="idSociedad" 
                            PropertyName="Value" Type="Decimal" />
                        <asp:ControlParameter ControlID="HiddenVigente" Name="vigente" 
                            PropertyName="Value" Type="String" />
                        <asp:ControlParameter ControlID="HiddenEstado" Name="codestado" 
                            PropertyName="Value" Type="Int32" />
                        <asp:Parameter Name="pageSize" Type="Int32" />
                        <asp:Parameter Name="indice" Type="Int32" />
                        <asp:Parameter Name="SortExpression" Type="String" />
                    </SelectParameters>
                </asp:ObjectDataSource>
                    <asp:ObjectDataSource ID="odsPorCuentaSoci" runat="server" 
                    EnablePaging="True" MaximumRowsParameterName="pageSize" 
                    SelectCountMethod="NumTotalFilasCuentaCatastralVigEstado" 
                    SelectMethod="ObtenerAvaluosPorCuentaCatastralSociedadEstadoVigencia" 
                    StartRowIndexParameterName="indice" 
                    TypeName="DseAvaluosConsultaPager" SortParameterName="SortExpression">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="HiddenCuentaCatastral" Name="cuentaCatastral" 
                            PropertyName="Value" Type="String" />
                        <asp:ControlParameter ControlID="HiddenIdPersonaToken" Name="idSociedad" PropertyName="Value" Type="Decimal" />
                        <asp:ControlParameter ControlID="HiddenVigente" Name="vigente" 
                            PropertyName="Value" Type="String" />
                        <asp:ControlParameter ControlID="HiddenEstado" Name="codestado" 
                            PropertyName="Value" Type="Int32" />
                        <asp:Parameter Name="pageSize" Type="Int32" />
                        <asp:Parameter Name="indice" Type="Int32" />
                        <asp:Parameter Name="SortExpression" Type="String" />
                    </SelectParameters>
                </asp:ObjectDataSource>
                
                    <!--Usuarios perfil Dictamenes!--> 
                    <asp:ObjectDataSource ID="odsPorIdAvaluoDictamenes" runat="server" 
                    SelectMethod="ObtenerAvaluoPorIdAvaluoEstadoVigenciaTodosPeritosSoci" 
                    TypeName="DseAvaluosConsultaPager" SortParameterName="SortExpression" 
                    MaximumRowsParameterName="pageSize" 
                    SelectCountMethod="NumTotalObtenerAvaluoPorIdAvaluoEstadoVigencia" 
                    StartRowIndexParameterName="indice" EnablePaging="True">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="HiddenNumUnicoAv" Name="idValuo" 
                            PropertyName="Value" Type="String" />
                        <asp:ControlParameter ControlID="HiddenEstado" Name="estado" 
                            PropertyName="Value" Type="Int32" />
                        <asp:ControlParameter ControlID="HiddenVigente" Name="vigencia" 
                            PropertyName="Value" Type="String" />
                        <asp:Parameter Name="pageSize" Type="Int32" />
                        <asp:Parameter Name="indice" Type="Int32" />
                        <asp:Parameter Name="SortExpression" Type="String" />
                    </SelectParameters>
                 </asp:ObjectDataSource>
                    <asp:ObjectDataSource ID="odsPorFechaDictamenes" runat="server" EnablePaging="True" 
                    MaximumRowsParameterName="pageSize" 
                    SelectCountMethod="NumTotalFilasFechaEstado" 
                    SelectMethod="ObtenerAvaluosPorFechaEstado" 
                    StartRowIndexParameterName="indice" TypeName="DseAvaluosConsultaPager" 
                    SortParameterName="SortExpression">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="HiddenFechaIni" DefaultValue="" 
                            Name="fechaInicio" PropertyName="Value" Type="DateTime" />
                        <asp:ControlParameter ControlID="HiddenFechaFin" Name="fechaFin" 
                            PropertyName="Value" Type="DateTime" />
                        <asp:ControlParameter ControlID="HiddenVigente" Name="vigente" 
                            PropertyName="Value" Type="String" />
                        <asp:ControlParameter ControlID="HiddenEstado" Name="codestado" 
                            PropertyName="Value" Type="Int32" />
                        <asp:Parameter Name="pageSize" Type="Int32" />
                        <asp:Parameter Name="indice" Type="Int32" />
                        <asp:Parameter DefaultValue="" Name="SortExpression" Type="String" />
                    </SelectParameters>
                </asp:ObjectDataSource>
                    <asp:ObjectDataSource ID="odsPorNumAvaluoDictamenes" runat="server" EnablePaging="True" 
                    MaximumRowsParameterName="pageSize" 
                    SelectCountMethod="NumTotalFilasNumValuoEstadoVig" 
                    SelectMethod="ObtenerAvaluosPorNumValuoVigEstado" 
                    StartRowIndexParameterName="indice" 
                    TypeName="DseAvaluosConsultaPager" SortParameterName="SortExpression">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="HiddenNumAvaluo" Name="numValuo" 
                            PropertyName="Value" Type="String" />
                        <asp:ControlParameter ControlID="HiddenReg_PerSoci" Name="registroPerito" 
                            PropertyName="Value" Type="String" />
                        <asp:ControlParameter ControlID="HiddenVigente" Name="vigente" PropertyName="Value"
                            Type="String" />
                        <asp:ControlParameter ControlID="HiddenEstado" Name="codestado" 
                            PropertyName="Value" Type="Int32" />
                        <asp:Parameter Name="pageSize" Type="Int32" />
                        <asp:Parameter Name="indice" Type="Int32" />
                        <asp:Parameter Name="SortExpression" Type="String" />
                    </SelectParameters>
                </asp:ObjectDataSource>
                    <asp:ObjectDataSource ID="odsCuentaDictamenes" runat="server" 
                    EnablePaging="True" MaximumRowsParameterName="pageSize" 
                    SelectCountMethod="NumTotalFilasCuentaVigenciaEstado" 
                    SelectMethod="ObtenerAvaluosPorCuentaVigenciaEstado" 
                    StartRowIndexParameterName="indice" 
                    TypeName="DseAvaluosConsultaPager" SortParameterName="SortExpression">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="HiddenCuentaCatastral" Name="cuentaCatastral" 
                            PropertyName="Value" Type="String" />
                        <asp:ControlParameter ControlID="HiddenVigente" Name="vigente" 
                            PropertyName="Value" Type="String" />
                        <asp:ControlParameter ControlID="HiddenEstado" Name="codestado" 
                            PropertyName="Value" Type="Int32" />
                        <asp:Parameter Name="pageSize" Type="Int32" />
                        <asp:Parameter Name="indice" Type="Int32" />
                        <asp:Parameter Name="SortExpression" Type="String" />
                    </SelectParameters>
                </asp:ObjectDataSource>

                <!--!-->
                <!-- GridView !-->
                <asp:GridView ID="gridViewAvaluos" runat="server" AllowPaging="True" AllowSorting="True"
                    AutoGenerateColumns="False" GridLines="Horizontal" EmptyDataText="No hay avalúos para el filtro seleccionado"
                    DataSourceID="odsPorFecha" OnRowDataBound="gridViewAvaluos_RowDataBound" DataKeyNames="IDAVALUO,CUENTACATASTRAL,NUMERO_NOTARIO,CODESTADOAVALUO,NUMEROUNICO,CODTIPOTRAMITE"
                    OnSelectedIndexChanged="gridViewAvaluos_SelectedIndexChanged" OnPageIndexChanging="gridViewAvaluos_PageIndexChanging"
                    PageSize="8" OnSorting="gridViewAvaluos_Sorting" HeaderStyle-CssClass="GridHeader" Width="100%" >
                    <Columns >
                        <asp:BoundField HeaderText="Nº único" DataField="NUMEROUNICO"  
                            SortExpression="NUMEROUNICO" HeaderStyle-HorizontalAlign="Center">
                              <HeaderStyle HorizontalAlign="Center"   Width="105px"/>
                              <ItemStyle Width="105px" HorizontalAlign="left" CssClass = "GridCell"/>
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Nº avalúo" DataField="NUMEROAVALUO" 
                            SortExpression="NUMEROAVALUO" HeaderStyle-HorizontalAlign="Center" >
                            <HeaderStyle HorizontalAlign="Center" Width="85px"/>
                            <ItemStyle  HorizontalAlign="left" CssClass = "GridCell" Width="85px"/>
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Cuenta cat." DataField="CUENTACATASTRAL" SortExpression="CUENTACATASTRAL" HeaderStyle-HorizontalAlign="center">
                            <HeaderStyle HorizontalAlign="Center" Width="110px"  />
                           <ItemStyle Width="110px" HorizontalAlign="center" CssClass = "GridCell"/>
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Fecha pres." DataField="FECHA_PRESENTACION" SortExpression="FECHA_PRESENTACION"
                            DataFormatString="{0:dd-MM-yy}" HeaderStyle-HorizontalAlign="center">
                            <HeaderStyle HorizontalAlign="Center" Width="60px"/>
                        <ItemStyle  HorizontalAlign="center" CssClass = "GridCell"  Width="60px"/>    
                        </asp:BoundField>
                        <asp:BoundField DataField="FECHAVALORREFERIDO" HeaderText="FECHAVALORREFERIDO" Visible="False" />
                        <asp:BoundField DataField="CODESTADOAVALUO" HeaderText="CODESTADOAVALUO" 
                            Visible="False" HeaderStyle-HorizontalAlign="center">
                            <HeaderStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="ESTADO" HeaderText="Estado" SortExpression="ESTADO" HeaderStyle-HorizontalAlign="center">
                            <HeaderStyle HorizontalAlign="Center" Width="60px"/>
                            <ItemStyle Width="60px" HorizontalAlign="center"  CssClass = "GridCell"/>
                        </asp:BoundField>
                        <asp:BoundField DataField="REGISTRO_PERITO" HeaderText="Perito" SortExpression="REGISTRO_PERITO"
                            HeaderStyle-HorizontalAlign="center">
                        
                            <HeaderStyle HorizontalAlign="Center" Width="6em" />
                        
                            <ItemStyle Width="7em" HorizontalAlign="Center"  CssClass = "GridCell" />
                        </asp:BoundField>
                          <asp:BoundField DataField="REGISTRO_SOCIEDAD" HeaderText="Sociedad" SortExpression="REGISTRO_SOCIEDAD"
                            ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center"  CssClass = "GridCell" />
                        </asp:BoundField>
                        <asp:BoundField DataField="NUMERO_NOTARIO" HeaderText="Notario" SortExpression="NUMERO_NOTARIO"
                            HeaderStyle-HorizontalAlign="center">
                     
                            <HeaderStyle HorizontalAlign="Center"  />
                     
                            <ItemStyle  HorizontalAlign="center"  CssClass = "GridCell"/>
                        </asp:BoundField>
                        <asp:BoundField DataField="TIPO" HeaderText="Tipo" SortExpression="TIPO" 
                            Visible="True" HeaderStyle-HorizontalAlign="center">
                            <HeaderStyle HorizontalAlign="Center" Width="5px"/>
                            <ItemStyle HorizontalAlign="Center" Width="5px"  />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Vig" Visible="false"  >
                            <ItemTemplate >
                                <asp:CheckBox runat="server" ID="checkboxVIG" HorizontalAlign="center"/></ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="CODTIPOTRAMITE" HeaderText="CODTIPOTRAMITE" Visible="False" />
                        <asp:BoundField DataField="REGION" HeaderText="REGION" Visible="False" />
                        <asp:BoundField DataField="MANZANA" HeaderText="MANZANA" Visible="False" />
                        <asp:BoundField DataField="LOTE" HeaderText="LOTE" Visible="False" />
                        <asp:BoundField DataField="UNIDADPRIVATIVA" HeaderText="UNIDADPRIVATIVA" Visible="False" />
                        <asp:BoundField DataField="REGISTRO_PERITO" HeaderText="REGISTRO_PERITO" Visible="False" />
                        <asp:BoundField DataField="VALORCOMERCIAL" HeaderText="VALORCOMERCIAL" Visible="False" />
                        <asp:BoundField DataField="VALORCATASTRAL" HeaderText="VALORCATASTRAL" Visible="False" />
                        <asp:BoundField DataField="VALORREFERIDO" HeaderText="VALORREFERIDO" Visible="False" />
                        <asp:BoundField DataField="OBJETO" HeaderText="OBJETO" Visible="False" />
                        <asp:BoundField DataField="PROPOSITO" HeaderText="PROPOSITO" Visible="False" />
                        <asp:BoundField DataField="IDPERSONAPERITO" HeaderText="IDPERSONAPERITO" Visible="False" />
                        <asp:BoundField DataField="IDPERSONANOTARIO" HeaderText="IDPERSONANOTARIO" Visible="False" />
                       <%-- #50--%>
                       <%-- <asp:CheckBoxField ID=""  HeaderText="Dict" ReadOnly="True" />--%>
                        <asp:TemplateField HeaderText="Dict">
                            <ItemTemplate>
                                <asp:CheckBox ID="RowCheckBox" runat="server" Checked="false" Enabled="false"/>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Right" Width="2.5px" />
                            <ItemStyle HorizontalAlign="Right" Width="2.5px" />
                        </asp:TemplateField>
                        <asp:TemplateField ShowHeader="False">
                            <ItemTemplate>
                                <asp:Button ID="btnSelect" runat="server" CausesValidation="False" CommandName="Select"
                                    Text="Sel" />
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Right" Width="2px" />
                            <ItemStyle HorizontalAlign="Right" Width="2px" />
                        </asp:TemplateField>
                        <%--<asp:CommandField SelectText="Sel" ShowSelectButton="True"  />--%>
                        
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
