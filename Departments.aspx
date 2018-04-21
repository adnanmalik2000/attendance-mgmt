<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master"  EnableEventValidation="false" AutoEventWireup="true" CodeFile="Departments.aspx.cs" Inherits="Departments" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <script type="text/javascript">

       

        function GetBranches(selectvalue) {
            var selectedValue = selectvalue.value;
            $.ajax({
                type: "POST",
                url: "Departments.aspx/GetBranchesddl",
                data: '{companyID: ' + JSON.stringify(selectedValue) + '}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (r) {
                    var ddlCustomers = $("[id*=ddlComBraAdd]");
                    ddlCustomers.empty().append('<option selected="selected" value="---Select---">---Select---</option>');
                    $.each(r.d, function () {
                        ddlCustomers.append($(" <option></option>").val(this['Value']).html(this['Text']));
                    });
                }
            });
        }

        function getvalue(svalue) {
            var selectedValue = svalue.value;

            var myHidden = document.getElementById('<%= hdbranchID.ClientID %>');
            myHidden.value = selectedValue;
        }

        function showEditModel(id, name) {
            $("[id*=txtDeptID]").text(id);
            $("[id*=txtDeptID]").val(id);

            $("[id*=txtDepartment]").text(name);
            $("[id*=txtDepartment]").val(name);

            var btn = $('[id$=btnUpdate]');
            var btnsave = $('[id$=btnSave]');

            btnsave.css("display", "none");
            btn.css("display", "inline-block");

            $('#divddlcomp').css('display', 'none');
            $('#divddlBranch').css('display', 'none');

        }

        function showAddModel() {

            $("[id*=txtDeptID]").text('');
            $("[id*=txtDeptID]").val('');

            $("[id*=txtDepartment]").text('');
            $("[id*=txtDepartment]").val('');

            var btn = $('[id$=btnUpdate]');
            var btnsave = $('[id$=btnSave]');

            btnsave.css("display", "inline-block");
            btn.css("display", "none");

            $('#divddlcomp').css('display', 'block');
            $('#divddlBranch').css('display', 'block');
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

           <h5> Departments</h5>

      </div>
     <div class="card-block">
            <div class="row m-a-0">
              <div class="col-lg-12">
                 
                  <asp:HiddenField runat="server" ID="hdbranchID" />
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
                    <asp:Label ID="Label3"  CssClass="control-label" runat="server" Text="Company"></asp:Label>
            </div>
            <div class="col-sm-5">
                 <asp:DropDownList ID="ddlcompany" OnSelectedIndexChanged="ddlcompany_SelectedIndexChanged" 
                     AutoPostBack="true" CssClass="chosen" runat="server"></asp:DropDownList>
                
                 </div>
            </div>
               
                  <div class="form-group">

                       <div class="col-sm-2">
                    <asp:Label ID="Label1"  CssClass="control-label" runat="server" Text="Branch"></asp:Label>
            </div>
            <div class="col-sm-5">
                 <asp:DropDownList ID="ddlBranch" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged" AutoPostBack="true"
                      CssClass="chosen" runat="server"></asp:DropDownList>
                
                 </div>
            </div>

                     <div id="Div1" class="modal fade bs-example-modal-lg-add" tabindex="-1" role="dialog" aria-hidden="true">
                                    <div class="modal-dialog modal-lg">
                                        <div class="modal-content">

                                            <div class="modal-header">
                                                <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">×</span>
                                                </button>
                                                <h4 class="modal-title" id="H1">Department</h4>
                                            </div>
                                            <div class="modal-body" style="min-height: 100px">
                                             <div style="display:none;">
                                          <asp:TextBox runat="server"  CssClass="input-small"  ID="txtDeptID" /> 
                                          </div>
                                     <div class="control-group " style="padding-left: 20px;">

                                          <div id="divddlcomp" class="form-group">

                       <div class="col-sm-2">
                    <asp:Label ID="Label4"  CssClass="control-label" runat="server" Text="Company"></asp:Label>
            </div>
            <div class="col-sm-5">
                 <asp:DropDownList ID="ddlCompAdd" style="width:100%" CssClass="form-control" onchange="GetBranches(this)" runat="server"></asp:DropDownList>
                
                 </div>

                                               <div class="col-sm-2">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="ddlCompAdd" ForeColor="Red"
                                     ValidationGroup="Save" runat="server" Display="Dynamic" InitialValue="---Select---"
                               ErrorMessage="RequiredFieldValidator">*</asp:RequiredFieldValidator>
                           </div>
            </div>
               
                  <div id="divddlBranch" class="form-group">

                       <div class="col-sm-2">
                    <asp:Label ID="Label5"  CssClass="control-label" runat="server" Text="Branch"></asp:Label>
            </div>
            <div class="col-sm-5">
                 <asp:DropDownList ID="ddlComBraAdd" style="width:100%" onchange="getvalue(this);" CssClass="form-control" runat="server">
                    
                 </asp:DropDownList>
                
                 </div>
                      <div class="col-sm-2">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="ddlComBraAdd" ForeColor="Red"
                                     ValidationGroup="Save" runat="server" Display="Dynamic" InitialValue="---Select---"
                               ErrorMessage="RequiredFieldValidator">*</asp:RequiredFieldValidator>
                           </div>
            </div>
                                    
                                          <div class="form-group">

                       <div class="col-sm-2">
    <asp:Label ID="Label2" runat="server" Text="Department"></asp:Label>
                           </div>

                        <div class="col-sm-5">
                 <asp:TextBox ID="txtDepartment" CssClass="form-control" runat="server"></asp:TextBox>
               
                 </div>
                                               <div class="col-sm-2">
                           <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="txtDepartment" ForeColor="Red" ValidationGroup="Save" 
                               runat="server" Display="Dynamic"
                               ErrorMessage="RequiredFieldValidator">*</asp:RequiredFieldValidator>
                           </div>
   
                       </div>

                                         </div></div>


                                              <div class="modal-footer">
                                                <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                                              <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary" ValidationGroup="Save"  style="min-width:95px" OnClick="btnSave_Click" Text="Save" />
                                                    <asp:Button ID="btnUpdate" runat="server" style="display:none;min-width:95px" CssClass="btn btn-primary" OnClick="btnUpdate_Click" Text="Update" />
                                                  </div>

                                            </div></div></div>


                   <div class="table-responsive">

    <asp:GridView ID="grdDept" runat="server" CssClass="table table-bordered table-striped m-b-0" AutoGenerateColumns="false">

        <Columns>
              <asp:BoundField DataField="DeptID" HeaderText="CompID"  Visible="false" />
          <asp:BoundField DataField="DeptName" HeaderText="Department"  Visible="true" />
   

             <asp:TemplateField ShowHeader="true" HeaderStyle-Width="20px">
                      <ItemTemplate>
                       <button  class="btn btn-xs btn-success" data-toggle="modal" data-target=".bs-example-modal-lg-add"  type="button"
                             onclick="showEditModel('<%#Eval("DeptID")%>' , '<%#Eval("DeptName")%>')"><i class="icon-pencil "></i></button>      
                       </ItemTemplate>
            </asp:TemplateField>

        </Columns>

    </asp:GridView>
                       </div>

          </div></div></div></div>

</asp:Content>

