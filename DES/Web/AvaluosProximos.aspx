<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AvaluosProximos.aspx.cs" Inherits="AvaluosProximos" Title="Avalúos próximos" MasterPageFile="~/MasterPageFE/MasterDetalleFE.master" %>

<%@ Register Src="UserControls/Navegacion.ascx" TagName="Navegacion" TagPrefix="uc1" %>
<%@ Register Src="UserControls/MenuLocal.ascx" TagName="MenuLocal" TagPrefix="uc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="UserControlsCommon/ModalInfoExcepcion.ascx" TagName="ModalInfoExcepcion" TagPrefix="uc11" %>
<%@ Register Src="UserControlsCommon/ModalInfo.ascx" TagName="ModalInfo" TagPrefix="uc4" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolderDImagen">
    <asp:Image ID="Image5" runat="server" ImageUrl="~/Images/caracter_detalle.jpg" />
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
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="ContentPlaceHolderDRuta">
    <uc1:Navegacion ID="navegacion" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderDContenido" runat="Server">
    <fieldset class="formulario">
        <legend class="formulario">Avalúos próximos</legend>
        <asp:Panel ID="UpdatePanel1" runat="server">
                <asp:Label ID="lblTitulo" runat="server" SkinID="Titulo2" Text="Cuenta del avalúo: "></asp:Label>
                <asp:Label ID="lblCuenta" runat="server" Text=""></asp:Label>
        </asp:Panel>
    </fieldset>
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
    <asp:UpdatePanel ID="UpdatePanelGridBuscador" runat="server" RenderMode="Inline" UpdateMode="Conditional">
        <ContentTemplate>
    <br />
    <asp:Label runat="server" SkinID="Titulo2" ID="lblListado" Text="Listado de avalúos próximos"></asp:Label>
    <br />
    <asp:HiddenField ID="IdAvaluo" runat="server" />
    <asp:HiddenField ID="HiddenIdAvaluoProximo" runat="server" />
    <asp:HiddenField runat="server" ID="HiddenIdPersonaToken" />
    <asp:HiddenField ID="HiddenNumUnico" runat="server" />
    <asp:GridView ID="gridViewAvaluos" runat="server" AllowPaging="True" AutoGenerateColumns="False"
        GridLines="Horizontal" AllowSorting="True" EmptyDataText="No hay avalúos para el filtro seleccionado"
        DataKeyNames="IDAVALUO" DataSourceID="odsAvaluosProximos" OnSelectedIndexChanged="gridViewAvaluos_SelectedIndexChanged"

        OnRowDataBound="gridViewAvaluos_RowDataBound" OnSorting="gridViewAvaluos_Sorting">
          <Columns>
            <asp:BoundField HeaderText="Nº único" DataField="NUMEROUNICO" Visible="true" SortExpression="NUMEROUNICO" HeaderStyle-HorizontalAlign="Center"/>
            <asp:BoundField HeaderText="Nº avalúo" DataField="NUMEROAVALUO" SortExpression="NUMEROAVALUO">
            </asp:BoundField>
            <asp:BoundField HeaderText="Cuenta Cat" DataField="CUENTACATASTRAL" SortExpression="CUENTACATASTRAL" HeaderStyle-HorizontalAlign="Center">
            </asp:BoundField>
            <asp:BoundField HeaderText="Fecha Pres" DataField="FECHA_DOCDIGITAL" SortExpression="FECHA_DOCDIGITAL"
                DataFormatString="{0:dd-MM-yy}"   ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"></asp:BoundField>
            <asp:BoundField DataField="FECHAVALORREFERIDO" HeaderText="FECHAVALORREFERIDO" Visible="False" />
            <asp:BoundField DataField="CODESTADOAVALUO" HeaderText="CODESTADOAVALUO" Visible="False" />
            <asp:BoundField DataField="ESTADO" HeaderText="Estado" SortExpression="ESTADO" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center"/>
            <asp:BoundField DataField="REGISTRO_PERITO" HeaderText="Perito" SortExpression="REGISTRO_PERITO" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center">
            </asp:BoundField>
            <asp:BoundField DataField="REGISTRO_SOCIEDAD" HeaderText="Sociedad" SortExpression="REGISTRO_SOCIEDAD"
                ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
            </asp:BoundField>
            <asp:BoundField DataField="NUMERO_NOTARIO" HeaderText="Notario" SortExpression="NUMERO_NOTARIO">
            </asp:BoundField>
            <asp:TemplateField HeaderText="Vig" HeaderStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:CheckBox runat="server" ID="checkboxVIG" />
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:BoundField DataField="VIGENTE" HeaderText="Vigente" SortExpression="VIGENTE"
                Visible="false" />
            <asp:BoundField DataField="CODTIPOTRAMITE" HeaderText="CODTIPOTRAMITE" Visible="False" />
            <asp:BoundField DataField="TIPO" HeaderText="TIPO" Visible="False" />
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
            <asp:BoundField DataField="PAGES_TOTAL" HeaderText="PAGES_TOTAL" Visible="False" />
            <asp:TemplateField ShowHeader="False">
                <ItemTemplate>
                       <asp:HyperLink ID="LinkVerAv"  Enable="true"  Target="_blank" ImageUrl="~/Images/zoom-in.gif" 
                       Text="ver informe" CommandName="Select"  CommandArgument="<%# Container.DataItemIndex %>" 
                       runat="server"  Enabled="true"  ToolTip="Ver informe"  />
               </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <asp:ObjectDataSource ID="odsAvaluosProximos" runat="server" EnablePaging="True"
        MaximumRowsParameterName="pageSize" SelectCountMethod="NumTotalFilasProximidad"
        SelectMethod="ObtenerAvaluosPorProximidad" StartRowIndexParameterName="indice"
        TypeName="DseAvaluosConsultaPager" SortParameterName="SortExpression">
        <SelectParameters>
            <asp:QueryStringParameter Name="idAvaluo" QueryStringField="IdAvaluo" Type="Int32" />
            <asp:Parameter DefaultValue="0" Name="codestado" Type="Int32" />
            <asp:Parameter Name="pageSize" Type="Int32" />
            <asp:Parameter Name="indice" Type="Int32" />
            <asp:Parameter Name="SortExpression" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="odsAvaluosProximosFinanzas" runat="server" 
                EnablePaging="True" MaximumRowsParameterName="pageSize" 
                SelectCountMethod="NumTotalFilasProximidad" 
                SelectMethod="ObtenerAvaluosPorProximidad" SortParameterName="SortExpression" 
                StartRowIndexParameterName="indice" TypeName="DseAvaluosConsultaPager">
                <SelectParameters>
                    <asp:QueryStringParameter Name="idAvaluo" QueryStringField="IdAvaluo" Type="Int32" />
                    <asp:Parameter DefaultValue="6" Name="codestado" Type="Int32" />
                    <asp:Parameter Name="pageSize" Type="Int32" />
                    <asp:Parameter Name="indice" Type="Int32" />
                    <asp:Parameter Name="SortExpression" Type="String" />
                </SelectParameters>
            </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="odsAvaluosProximos_idPerito" runat="server" SelectMethod="ObtenerAvaluosPorProximidadPerito"
        TypeName="DseAvaluosConsultaPager" EnablePaging="True" MaximumRowsParameterName="pageSize"
        SelectCountMethod="NumTotalFilasProximidadPerito" StartRowIndexParameterName="indice"
        SortParameterName="SortExpression">
        <SelectParameters>
            <asp:ControlParameter ControlID="HiddenIdAvaluoProximo" Name="idAvaluo" PropertyName="Value" Type="Int32" />
            <asp:ControlParameter ControlID="HiddenIdPersonaToken" Name="idPerito" PropertyName="Value"  Type="Int32" />
            <asp:Parameter Name="pageSize" Type="Int32" />
            <asp:Parameter Name="indice" Type="Int32" />
            <asp:Parameter Name="SortExpression" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="odsAvaluosProximos_idSoci" runat="server" SelectMethod="ObtenerAvaluosPorProximidadSociedad"
        TypeName="DseAvaluosConsultaPager" EnablePaging="True" MaximumRowsParameterName="pageSize"
        SelectCountMethod="NumTotalFilasProximidadSociedad" StartRowIndexParameterName="indice"
        SortParameterName="SortExpression">
        <SelectParameters>
            <asp:QueryStringParameter Name="idAvaluo" QueryStringField="IdAvaluo" Type="Int32" />
            <asp:ControlParameter ControlID="HiddenIdPersonaToken" Name="idSociedad" PropertyName="Value" Type="Int32" />
            <asp:Parameter Name="pageSize" Type="Int32" />
            <asp:Parameter Name="indice" Type="Int32" />
            <asp:Parameter Name="SortExpression" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
    </ContentTemplate>
    </asp:UpdatePanel>
    <br/>
    <div style="float: right" dir="ltr">
        <asp:Button ID="btnVolver" Visible="true" runat="server" CausesValidation="False"
            OnClick="btnVolver_Click" Text="Volver a Bandeja de Entrada" OnClientClick="aspnetForm.target ='';" />
        <br/>
    </div>
     <asp:HiddenField ID="HiddenTokenModal" runat="server" />
    <cc1:ModalPopupExtender ID="extenderPnlInfoTokenModal" runat="server" Enabled="True"
        TargetControlID="HiddenTokenModal" PopupControlID="pnlTokenModal" 
        BackgroundCssClass="PanelModalBackground"  DropShadow="True" />
    <asp:Panel ID="pnlTokenModal" SkinID="Modal" Style="display: none; width:280px;" runat="server">
        <uc4:ModalInfo ID="ModalInfoToken" runat="server" OnConfirmClick="ModalInfoToken_Ok_Click" />
    </asp:Panel>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolderDMenuLocal" runat="Server">
    <uc2:MenuLocal ID="MenuLocal1" runat="server" />
</asp:Content>
