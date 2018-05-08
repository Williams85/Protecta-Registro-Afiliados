<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Reporte.aspx.vb" Inherits="Reporte2" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <script src="crystalreportviewers13/js/crviewer/crv.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true" 
            HasToggleGroupTreeButton="False" HasToggleParameterPanelButton="False" ToolPanelView="None" />
    </div>
    </form>
</body>
</html>
