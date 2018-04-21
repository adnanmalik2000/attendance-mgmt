<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="CompBranches.aspx.cs" Inherits="CompBranches" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <script type="text/javascript">
       function showEditModel(id,name,lati,longi,address,distance,chkdistance, uploadimg){

           $("[id*=txtComBraID]").text(id);
           $("[id*=txtComBraID]").val(id);

           $("[id*=txtBranchName]").text(name);
           $("[id*=txtBranchName]").val(name);

           $("[id*=txtlatitude]").text(lati);
           $("[id*=txtlatitude]").val(lati);

           $("[id*=txtlongitude]").text(longi);
           $("[id*=txtlongitude]").val(longi);

           $("[id*=txtaddress]").text(address);
           $("[id*=txtaddress]").val(address);

           $("[id*=txtdistance]").text(distance);
           $("[id*=txtdistance]").val(distance);

           
           if (chkdistance == 'True' || chkdistance == 'true')
           document.getElementById("<%= chkDistance.ClientID %>").checked = true;
           else
               document.getElementById("<%= chkDistance.ClientID %>").checked = false;

           
           if (uploadimg == 'True' || uploadimg == 'true')
               document.getElementById("<%= chkUploadimg.ClientID %>").checked = true;
           else
               document.getElementById("<%= chkUploadimg.ClientID %>").checked = false;

           var btn = $('[id$=btnUpdate]');
           var btnsave = $('[id$=btnSave]');

           btnsave.css("display", "none");
           btn.css("display", "inline-block");
       }

       function showAddModel() {


           $("[id*=txtBranchName]").text('');
           $("[id*=txtBranchName]").val('');

           $("[id*=txtlatitude]").text('');
           $("[id*=txtlatitude]").val('');

           $("[id*=txtlongitude]").text('');
           $("[id*=txtlongitude]").val('');

           $("[id*=txtaddress]").text('');
           $("[id*=txtaddress]").val('');

           $("[id*=txtdistance]").text('');
           $("[id*=txtdistance]").val('');

           document.getElementById("<%= chkDistance.ClientID %>").checked = false;
           document.getElementById("<%= chkUploadimg.ClientID %>").checked = false;

           var btn = $('[id$=btnUpdate]');
           var btnsave = $('[id$=btnSave]');

           btn.css("display", "none");
           btnsave.css("display", "inline-block");
       }

    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

      <div id="SuccessMsg" class="alert alert-success" runat="server" >
	<button type="button" class="close" data-dismiss="alert">
		<i class="ace-icon fa fa-times"></i>
	</button>

	<strong>
		<i class="ace-icon fa fa-check"></i>
		Record!
	</strong>

	saved successfully.
	<br />
</div>

    <div id="ExistMsg" class="alert alert-danger" runat="server" >
	<button type="button" class="close" data-dismiss="alert">
		<i class="ace-icon fa fa-times"></i>
	</button>

	<strong>
		<i class="ace-icon fa fa-check"></i>
		Company!
	</strong>

	already exists.
	<br />
</div>
    
     <div class="card bg-white">

        <div class="card-header">

           <h5> Company Branches</h5>

      </div>
     <div class="card-block">
            <div class="row m-a-0">
              <div class="col-lg-12">

                 
                    <div class="form-group">

                       <div class="col-sm-2">
                    <button  class="btn btn-primary" style="min-width:95px" onclick="showAddModel()" data-toggle="modal" data-target=".bs-example-modal-lg-add"  type="button">
                     Add New</button>      
                    </div>
                    <div class="col-sm-5">
                       
                         </div>
                    <div class="col-sm-2">
                          </div>
            </div>

                    <div class="form-group">

                       <div class="col-sm-2">
                    <asp:Label ID="Label8"  CssClass="control-label" runat="server" Text="View Branches by Company"></asp:Label>
            </div>
            <div class="col-sm-5">
        <asp:DropDownList ID="ddlcompany" OnSelectedIndexChanged="ddlcompany_SelectedIndexChanged" AutoPostBack="true" CssClass="chosen" runat="server">

                 </asp:DropDownList>
                </div>
                       </div>
                      

                     <div id="Div1" class="modal fade bs-example-modal-lg-add" tabindex="-1" role="dialog" aria-hidden="true">
                                    <div class="modal-dialog modal-lg">
                                        <div class="modal-content">

                                            <div class="modal-header">
                                                <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">×</span>
                                                </button>
                                                <h4 class="modal-title" id="H1">Add Company branch</h4>
                                            </div>
                                            <div class="modal-body" style="min-height: 100px">
                                             <div style="display:none;">
                                          <asp:TextBox runat="server"  CssClass="input-small"  ID="txtComBraID" /> 
                                          </div>
                                     <div class="control-group " style="padding-left: 20px;">

                  <div  class="form-group">

                       <div class="col-sm-2">
                    <asp:Label ID="Label1"  CssClass="control-label" runat="server" Text="Company"></asp:Label>
            </div>
            <div class="col-sm-5">
        <asp:DropDownList ID="ddlcompanyAdd" CssClass="chosen" runat="server">

                 </asp:DropDownList>
                </div>
                       </div>


                   <div class="form-group">

                       <div class="col-sm-2">
                    <asp:Label ID="Label7"  CssClass="control-label" runat="server" Text="Branch Name"></asp:Label>
            </div>
            <div class="col-sm-5">
    <asp:TextBox ID="txtBranchName" CssClass="form-control" runat="server"></asp:TextBox>
                </div>
                       <div class="col-sm-2">
                             <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ForeColor="Red" ValidationGroup="Save" 
                                 Display="Dynamic" ControlToValidate="txtBranchName"
                    runat="server" ErrorMessage="RequiredFieldValidator">*</asp:RequiredFieldValidator>
                       </div>
                       </div>

                  
                   <div class="form-group">

                       <div class="col-sm-2">
     <asp:Label ID="Label2" runat="server" CssClass="control-label"  Text="Latitude"></asp:Label>
                           </div>
                        <div class="col-sm-5">
    <asp:TextBox ID="txtlatitude" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                       <div class="col-sm-2">
                             <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ForeColor="Red" ValidationGroup="Save" 
                                 Display="Dynamic" ControlToValidate="txtlatitude"
                    runat="server" ErrorMessage="RequiredFieldValidator">*</asp:RequiredFieldValidator>
                       </div>
                   </div>

                        <div class="form-group">

                       <div class="col-sm-2">
     <asp:Label ID="Label3" runat="server" CssClass="control-label"  Text="Longitude"></asp:Label>
                             </div>
                        <div class="col-sm-5">
    <asp:TextBox ID="txtlongitude" CssClass="form-control" runat="server"></asp:TextBox>
                             </div>
                            <div class="col-sm-2">
                             <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ForeColor="Red" ValidationGroup="Save" 
                                 Display="Dynamic" ControlToValidate="txtlongitude"
                    runat="server" ErrorMessage="RequiredFieldValidator">*</asp:RequiredFieldValidator>
                       </div>
                        </div>

                        <div class="form-group">

                       <div class="col-sm-2">
     <asp:Label ID="Label4" runat="server" CssClass="control-label"  Text="Address"></asp:Label>
                             </div>
                        <div class="col-sm-5">
    <asp:TextBox ID="txtaddress" CssClass="form-control" runat="server"></asp:TextBox>
                             </div>
                            </div>

                        <div class="form-group">

                       <div class="col-sm-2">
     <asp:Label ID="Label5" runat="server" CssClass="control-label"  Text="Distance"></asp:Label>
                            </div>
                        <div class="col-sm-5">
    <asp:TextBox ID="txtdistance" CssClass="form-control" runat="server"></asp:TextBox>
                             </div>
                            <div class="col-sm-2">
                             <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ForeColor="Red" ValidationGroup="Save" 
                                 Display="Dynamic" ControlToValidate="txtdistance"
                    runat="server" ErrorMessage="RequiredFieldValidator">*</asp:RequiredFieldValidator>
                       </div>
                             </div>

                        <div class="form-group">

                       <div class="col-sm-2">
     <asp:Label ID="Label6" runat="server" CssClass="control-label"  Text="Check Distance"></asp:Label>
                             </div>
                        <div class="col-sm-5">
    <asp:CheckBox ID="chkDistance" runat="server" />
                            </div></div>
                                         <div class="form-group">

                       <div class="col-sm-2">
     <asp:Label ID="Label9" runat="server" CssClass="control-label"  Text="Upload Screenshots"></asp:Label>
                             </div>
                        <div class="col-sm-5">
    <asp:CheckBox ID="chkUploadimg" runat="server" />
                            </div></div>

                                         </div></div>

                   
                                             <div class="modal-footer">
                                                <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                                                   <asp:Button ID="btnSave" CssClass="btn btn-primary" style="min-width:95px" OnClick="btnSave_Click" runat="server" Text="Save" />
                                                 <asp:Button ID="btnUpdate" CssClass="btn btn-primary" style="display:none;min-width:95px" OnClick="btnUpdate_Click" runat="server" Text="Update" />
                                                 
                                                  </div>


                                        </div></div></div>

                  <div class="table-responsive">

    <asp:GridView ID="grdCompany" runat="server" CssClass="table table-bordered table-striped m-b-0" AutoGenerateColumns="false">

        <Columns>
              <asp:BoundField DataField="CompBranchID" HeaderText="CompID"  Visible="false" />
          <asp:BoundField DataField="BranchName" HeaderText="Branch Name"  Visible="true" />
     <asp:BoundField DataField="latitude" HeaderText="Latitude"  Visible="true" />
               <asp:BoundField DataField="longitude" HeaderText="Longitude"  Visible="true" />
               <asp:BoundField DataField="distance" HeaderText="Distance"  Visible="true" />

             <asp:TemplateField ShowHeader="true" HeaderStyle-Width="20px">
                      <ItemTemplate>
                       <button  class="btn btn-xs btn-success" data-toggle="modal" data-target=".bs-example-modal-lg-add"  type="button"
       onclick="showEditModel('<%#Eval("CompBranchID")%>' , '<%#Eval("BranchName")%>', '<%#Eval("latitude")%>', '<%#Eval("longitude")%>',
                            '<%#Eval("address")%>', '<%#Eval("distance")%>', '<%#Eval("ChkDistance")%>', '<%#Eval("UploadImage") %>')"><i class="icon-pencil "></i></button>      
                       </ItemTemplate>
            </asp:TemplateField>

        </Columns>

    </asp:GridView>
                       </div>

                  </div></div></div></div>
</asp:Content>

