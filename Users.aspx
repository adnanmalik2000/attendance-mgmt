<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Users.aspx.cs" Inherits="Users" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <script type="text/javascript">
        function showEditModel(id,name,password,compID,status) {

            $("[id*=txtUserID]").text(id);
            $("[id*=txtUserID]").val(id);

            $("[id*=txtUsername]").text(name);
            $("[id*=txtUsername]").val(name);

            $("[id*=txtPassword]").text(password);
            $("[id*=txtPassword]").val(password);

            $("[id*=ddlcompany]").val(compID);

            $("[id*=txtUsername]").prop('disabled', true);

            if (status == 'True' || status == 'true')
                document.getElementById("<%= chkStatus.ClientID %>").checked = true;
             else
                 document.getElementById("<%= chkStatus.ClientID %>").checked = false;

            var btn = $('[id$=btnUpdate]');
            var btnsave = $('[id$=btnSave]');

            btnsave.css("display", "none");
            btn.css("display", "inline-block");
        }

        function showAddModel() {

            $("[id*=txtUserID]").text("");
            $("[id*=txtUserID]").val("");

            $("[id*=txtUsername]").text("");
            $("[id*=txtUsername]").val("");

            $("[id*=txtPassword]").text("");
            $("[id*=txtPassword]").val("");

            $("[id*=txtUsername]").prop('disabled', false);

            document.getElementById("<%= chkStatus.ClientID %>").checked = false;

            var btn = $('[id$=btnUpdate]');
            var btnsave = $('[id$=btnSave]');

            btnsave.css("display", "inline-block");
            btn.css("display", "none");
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
		Username!
	</strong>

	already exists.
	<br />
</div>

    
     <div class="card bg-white">

        <div class="card-header">

           <h5>Users</h5>

      </div>
     <div class="card-block">
            <div class="row m-a-0">
              <div class="col-lg-12">

                    <div class="form-group">

                       <div class="col-sm-2">
                    <button  class="btn btn-primary" style="min-width:95px" onclick="showAddModel();"  data-toggle="modal" data-target=".bs-example-modal-lg-add"  type="button">
                     Add New</button>      
                    </div>
                    <div class="col-sm-5">
                       
                         </div>
                    <div class="col-sm-2">
                          </div>
            </div>

                   <div class="form-group">

                       <div class="col-sm-2">
                    <asp:Label ID="Label8"  CssClass="control-label" runat="server" Text="View Users by Company"></asp:Label>
            </div>
            <div class="col-sm-5">
        <asp:DropDownList ID="ddlCompSearch" OnSelectedIndexChanged="ddlCompSearch_SelectedIndexChanged" AutoPostBack="true" CssClass="chosen" runat="server">

                 </asp:DropDownList>
                </div>
                       </div>
                 
                   <div id="Div1" class="modal fade bs-example-modal-lg-add" tabindex="-1" role="dialog" aria-hidden="true">
                                    <div class="modal-dialog modal-lg">
                                        <div class="modal-content">

                                            <div class="modal-header">
                                                <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">×</span>
                                                </button>
                                                <h4 class="modal-title" id="H1">User</h4>
                                            </div>
                                            <div class="modal-body" style="min-height: 100px">
                                             <div style="display:none;">
                                          <asp:TextBox runat="server"  CssClass="input-small"  ID="txtUserID" /> 
                                          </div>
                                     <div class="control-group " style="padding-left: 20px;">

                                         
                   <div class="form-group">

                       <div class="col-sm-2">
                    <asp:Label ID="Label3" runat="server" Text="Company"></asp:Label>
                           </div>

                        <div class="col-sm-6">
                <asp:DropDownList ID="ddlcompany" runat="server" style="width:100%" CssClass="form-control"></asp:DropDownList>
                             </div>
   
                       </div>

                                         <div class="form-group">

                       <div class="col-sm-2">
                    <asp:Label ID="Label4" runat="server" Text="Role"></asp:Label>
                           </div>

                        <div class="col-sm-6">
                <asp:DropDownList ID="ddlRole" runat="server" style="width:100%" CssClass="form-control">
                    <asp:ListItem>Admin</asp:ListItem>
                </asp:DropDownList>
                             </div>
   
                       </div>
               
               
                  <div class="form-group">

                       <div class="col-sm-2">
                    <asp:Label ID="Label1"  CssClass="control-label" runat="server" Text="Username"></asp:Label>
                    </div>
                    <div class="col-sm-6">
                        <span class="input m-b-md">
                         <asp:TextBox ID="txtUsername" CssClass="form-control" runat="server"></asp:TextBox>
                        <label class="input__label" for="input-1">
                        <span class="input__label-content"></span>
                      </label>
                      </span>
                         </div>
                    <div class="col-sm-4">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ForeColor="Red" ValidationGroup="Save" ControlToValidate="txtUsername"
                    runat="server" ErrorMessage="RequiredFieldValidator">*</asp:RequiredFieldValidator>

                  </div>
            </div>

                   <div class="form-group">

                       <div class="col-sm-2">
                    <asp:Label ID="Label2" runat="server" Text="Password"></asp:Label>
                           </div>

                        <div class="col-sm-6">
                <span class="input m-b-md">
                 <asp:TextBox ID="txtPassword" CssClass="form-control" runat="server"></asp:TextBox>
                <label class="input__label" for="input-1">
                <span class="input__label-content"></span>
              </label>
              </span>
                 </div>
   
                       </div>

                                            <div class="form-group">
                                             <div class="col-sm-2">
     <asp:Label ID="Label20" runat="server" CssClass="control-label"  Text="Status"></asp:Label>
                             </div>
                        <div class="col-sm-4">
    <asp:CheckBox ID="chkStatus" runat="server" />
                            </div>


                             </div></div></div>
                 
                                      

                            <div class="modal-footer">
                            <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                                <asp:Button ID="btnSave" runat="server" ValidationGroup="Save" CssClass="btn btn-primary" style="min-width:95px" OnClick="btnSave_Click" Text="Save" />
                               <asp:Button ID="btnUpdate" CssClass="btn btn-primary" style="display:none;min-width:95px" OnClick="btnUpdate_Click" runat="server" Text="Update" />
                                                 
                                   </div>

                                        </div></div></div>

                   <div class="table-responsive">

    <asp:GridView ID="grdUsers" runat="server" CssClass="table table-bordered table-striped m-b-0" AutoGenerateColumns="false">

        <Columns>
              <asp:BoundField DataField="CompanyID" HeaderText="CompID"  Visible="false" />
              <asp:BoundField DataField="CompanyName" HeaderText="Company Name"  Visible="true" />
              <asp:BoundField DataField="Username" HeaderText="Username"  Visible="true" />
              <asp:BoundField DataField="Password" HeaderText="Password"  Visible="true" />
             <asp:TemplateField HeaderText="Status">
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%# Convert.ToBoolean(Eval("Status")) == true ? "Active" : "Inactive" %>'></asp:Label>
                                 </ItemTemplate>
                            </asp:TemplateField>
             <asp:TemplateField ShowHeader="true" HeaderStyle-Width="20px">
                      <ItemTemplate>
                       <button  class="btn btn-xs btn-success" data-toggle="modal" data-target=".bs-example-modal-lg-add"  type="button"
                             onclick="showEditModel('<%#Eval("UserID")%>' , '<%#Eval("Username")%>', '<%#Eval("Password")%>', '<%#Eval("CompanyID")%>', '<%#Eval("Status")%>')"><i class="icon-trash "></i></button>      
                       </ItemTemplate>
            </asp:TemplateField>

        </Columns>

    </asp:GridView>
                       </div>

          </div></div></div></div>


</asp:Content>

