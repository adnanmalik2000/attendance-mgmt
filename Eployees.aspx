<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" EnableEventValidation="false" CodeFile="Eployees.aspx.cs" Inherits="Eployees" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript">
        function GetBranches(selectvalue) {
            var selectedValue = selectvalue.value;
            $.ajax({
                type: "POST",
                url: "Eployees.aspx/GetBranchesddl",
                data: '{companyID: ' + JSON.stringify(selectedValue) + '}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (r) {
                    var ddlCustomers = $("[id*=ddlBranchAdd]");
                    ddlCustomers.empty().append('<option selected="selected" value="---Select---">---Select---</option>');
                    $.each(r.d, function () {
                        ddlCustomers.append($(" <option></option>").val(this['Value']).html(this['Text']));
                    });
                }
            });
        }

        function GetDept(selectvalue) {
            var selectedValue = selectvalue.value;
            $.ajax({
                type: "POST",
                url: "Eployees.aspx/GetDeptddl",
                data: '{BranchID: ' + JSON.stringify(selectedValue) + '}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (r) {
                    var ddlCustomers = $("[id*=ddlDeptAdd]");
                    ddlCustomers.empty().append('<option selected="selected" value="---Select---">---Select---</option>');
                    $.each(r.d, function () {
                        ddlCustomers.append($(" <option></option>").val(this['Value']).html(this['Text']));
                    });
                }
            });
        }

        function getvalue(svalue) {
            var selectedValue = svalue.value;

            var myHidden = document.getElementById('<%= hdDeptID.ClientID %>');
            myHidden.value = selectedValue;
        }

        function showEditModel(id,fname,lname,uname,pass,status,lati,longi,address,distance,chkDist,screenshot, image) {

            $("[id*=txtEmpID]").text(id);
            $("[id*=txtEmpID]").val(id);

            $("[id*=txtupFname]").text(fname);
            $("[id*=txtupFname]").val(fname);

            $("[id*=txtupLname]").text(lname);
            $("[id*=txtupLname]").val(lname);

            $("[id*=txtupUname]").text(uname);
            $("[id*=txtupUname]").val(uname);

            $("[id*=txtupPass]").text(pass);
            $("[id*=txtupPass]").val(pass);

            $("[id*=txtupLati]").text(lati);
            $("[id*=txtupLati]").val(lati);

            $("[id*=txtupLong]").text(longi);
            $("[id*=txtupLong]").val(longi);

            $("[id*=txtupAddress]").text(address);
            $("[id*=txtupAddress]").val(address);
           
            $("[id*=txtupDistance]").text(distance);
            $("[id*=txtupDistance]").val(distance);

            image = image.replace(/~/g, "");
            $("[id$='Image1']").attr("src", image);

            if (chkDist == 'True' || chkDist == 'true')
                document.getElementById("<%= chkDistance.ClientID %>").checked = true;
             else
                document.getElementById("<%= chkDistance.ClientID %>").checked = false;

            if (status == 'True' || status == 'true')
                document.getElementById("<%= chkStatus.ClientID %>").checked = true;
             else
                document.getElementById("<%= chkStatus.ClientID %>").checked = false; 

            if (screenshot == 'True' || screenshot == 'true')
                document.getElementById("<%= chkScreenshots.ClientID %>").checked = true;
            else
                document.getElementById("<%= chkScreenshots.ClientID %>").checked = false; 
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

     <div class="card bg-white">

        <div class="card-header">

           <h5> Employees</h5>

      </div>
     <div class="card-block">
            <div class="row m-a-0">
              <div class="col-lg-12">

                  <asp:HiddenField runat="server" ID="hdDeptID" />

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
                    <asp:Label ID="Label6"  CssClass="control-label" runat="server" Text="Company"></asp:Label>
            </div>
            <div class="col-sm-5">
                 <asp:DropDownList ID="ddlcompany" OnSelectedIndexChanged="ddlcompany_SelectedIndexChanged" 
                     AutoPostBack="true" CssClass="chosen" runat="server"></asp:DropDownList>
                
                 </div>
            </div>
               
                  <div class="form-group">

                       <div class="col-sm-2">
                    <asp:Label ID="Label7"  CssClass="control-label" runat="server" Text="Branch"></asp:Label>
            </div>
            <div class="col-sm-5">
                 <asp:DropDownList ID="ddlBranch" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged" AutoPostBack="true"
                      CssClass="chosen" runat="server"></asp:DropDownList>
                
                 </div>
            </div>
                     
                  <div class="form-group">

                       <div class="col-sm-2">
                    <asp:Label ID="Label1"  CssClass="control-label" runat="server" Text="Department"></asp:Label>
            </div>
            <div class="col-sm-5">
                 <asp:DropDownList ID="ddlDept" OnSelectedIndexChanged="ddlDept_SelectedIndexChanged" AutoPostBack="true" CssClass="chosen" runat="server"></asp:DropDownList>
                
                 </div>
            </div>

                             <div id="Div1" class="modal fade bs-example-modal-lg-add" tabindex="-1" role="dialog" aria-hidden="true">
                                    <div class="modal-dialog modal-lg">
                                        <div class="modal-content">

                                            <div class="modal-header">
                                                <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">×</span>
                                                </button>
                                                <h4 class="modal-title" id="H1">Employee</h4>
                                            </div>
                                            <div class="modal-body" style="min-height: 100px">
                                             <div style="display:none;">
                                          <asp:TextBox runat="server"  CssClass="input-small"  ID="txtDeptID" /> 
                                          </div>
                                     <div class="control-group " style="padding-left: 20px;">

                                           <div class="form-group">

                       <div class="col-sm-2">
                    <asp:Label ID="Label16"  CssClass="control-label" runat="server" Text="Company"></asp:Label>
            </div>
            <div class="col-sm-5">
                 <asp:DropDownList ID="ddlCompAdd"  style="min-width:100%" onchange="GetBranches(this)"  CssClass="form-control" runat="server"></asp:DropDownList>
                
                 </div>
                            <div class="col-sm-2">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="ddlCompAdd" ForeColor="Red"
                                     ValidationGroup="Save" runat="server" Display="Dynamic" InitialValue="---Select---"
                               ErrorMessage="RequiredFieldValidator">*</asp:RequiredFieldValidator>
                           </div>
            </div>
               
                  <div class="form-group">

                       <div class="col-sm-2">
                    <asp:Label ID="Label17"  CssClass="control-label" runat="server" Text="Branch"></asp:Label>
            </div>
            <div class="col-sm-5">
                 <asp:DropDownList ID="ddlBranchAdd" style="min-width:100%"  onchange="GetDept(this);" CssClass="form-control" runat="server"></asp:DropDownList>
                
                 </div>

                      <div class="col-sm-2">
                          <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="ddlBranchAdd" ForeColor="Red"
                               ValidationGroup="Save" runat="server" Display="Dynamic" InitialValue="---Select---"
                               ErrorMessage="RequiredFieldValidator">*</asp:RequiredFieldValidator>
                           </div>

            </div>
                     
                  <div class="form-group">

                       <div class="col-sm-2">
                    <asp:Label ID="Label18"  CssClass="control-label" runat="server" Text="Department"></asp:Label>
            </div>
            <div class="col-sm-5">
                 <asp:DropDownList ID="ddlDeptAdd" style="min-width:100%" onchange="getvalue(this);"  CssClass="form-control" runat="server"></asp:DropDownList>
                
                 </div>

                      <div class="col-sm-2">
                          <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ForeColor="Red" ControlToValidate="ddlDeptAdd"
                               ValidationGroup="Save" runat="server" Display="Dynamic" InitialValue="---Select---"
                               ErrorMessage="RequiredFieldValidator">*</asp:RequiredFieldValidator>
                           </div>
            </div>


                   <div class="form-group">

                       <div class="col-sm-2">
                    <asp:Label ID="Label2" runat="server" Text="First name"></asp:Label>
                           </div>

                        <div class="col-sm-5">
                 <asp:TextBox ID="txtFName" CssClass="form-control" runat="server"></asp:TextBox>
               
                        </div>   
                       <div class="col-sm-2">
                           <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="txtFName" ForeColor="Red" ValidationGroup="Save" 
                               runat="server" Display="Dynamic"
                               ErrorMessage="RequiredFieldValidator">*</asp:RequiredFieldValidator>
                           </div>
                       </div>

                  
                   <div class="form-group">

                       <div class="col-sm-2">
                    <asp:Label ID="Label3" runat="server" Text="Last name"></asp:Label>
                           </div>

                        <div class="col-sm-5">
                 <asp:TextBox ID="txtLName" CssClass="form-control" runat="server"></asp:TextBox>
               
                        </div>   

                       <div class="col-sm-2">

                           </div>

                       </div>

                  
                   <div class="form-group">

                       <div class="col-sm-2">
                    <asp:Label ID="Label4" runat="server" Text="Username"></asp:Label>
                           </div>

                        <div class="col-sm-5">
                 <asp:TextBox ID="txtUserName" CssClass="form-control" runat="server"></asp:TextBox>
               
                        </div>  
                       
                       <div class="col-sm-2">
                           <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="txtUserName" ForeColor="Red" ValidationGroup="Save" runat="server" Display="Dynamic"
                               ErrorMessage="RequiredFieldValidator">*</asp:RequiredFieldValidator>
                           </div> 
                       </div>

                  
                   <div class="form-group">

                       <div class="col-sm-2">
                    <asp:Label ID="Label5" runat="server" Text="Password"></asp:Label>
                           </div>

                        <div class="col-sm-5">
                 <asp:TextBox ID="txtPassword" CssClass="form-control" runat="server"></asp:TextBox>
               
                        </div> 
                       
                       <div class="col-sm-2">
                           <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ControlToValidate="txtPassword" ForeColor="Red" ValidationGroup="Save" runat="server" Display="Dynamic"
                               ErrorMessage="RequiredFieldValidator">*</asp:RequiredFieldValidator>
                           </div>  
                       </div>

                                          <div class="form-group">

                       <div class="col-sm-2">
    <asp:Label ID="Label22" runat="server" Text="Profile image"></asp:Label>
                           </div>

                        <div class="col-sm-5">
                            <asp:FileUpload ID="FileUploadInsert" runat="server" />
                 </div>
   
                       </div>

                            </div></div>
                

                                            <div class="modal-footer">
                                                <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                                              <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary" style="min-width:95px" ValidationGroup="Save" OnClick="btnSave_Click" Text="Save" />
                                                  </div>

                            </div></div></div>

                           <div class="table-responsive">

    <asp:GridView ID="grdEmployee" runat="server" CssClass="table table-bordered table-striped m-b-0" AutoGenerateColumns="false">

        <Columns>
              <asp:BoundField DataField="EmpID" HeaderText="EmpID"  Visible="false" />
          <asp:BoundField DataField="FirstName" HeaderText="F Name"  Visible="true" />
            <asp:BoundField DataField="LastName" HeaderText="L Name"  Visible="true" />
            <asp:BoundField DataField="userName" HeaderText="User name"  Visible="true" />
              <asp:BoundField DataField="Status" HeaderText="Status"  Visible="false" />
            <asp:BoundField DataField="ChkDistance" HeaderText="Chk Distance"  Visible="false" />
   
               <asp:TemplateField HeaderText="Status">
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%# Convert.ToBoolean(Eval("Status")) == true ? "Active" : "Inactive" %>'></asp:Label>
                                 </ItemTemplate>
                            </asp:TemplateField>

             <asp:TemplateField HeaderText="Chk Distance">
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%# Convert.ToBoolean(Eval("ChkDistance")) == true ? "Yes" : "No" %>'></asp:Label>
                                 </ItemTemplate>
                            </asp:TemplateField>

             <asp:TemplateField ShowHeader="true" HeaderStyle-Width="20px">
                      <ItemTemplate>
                       <button  class="btn btn-xs btn-success" data-toggle="modal" data-target=".bs-example-modal-lg-edit"  type="button"
                             onclick="showEditModel('<%#Eval("EmpID")%>' , '<%#Eval("FirstName")%>', '<%#Eval("LastName")%>','<%#Eval("userName")%>','<%#Eval("Password")%>'
                           ,'<%#Eval("Status")%>','<%#Eval("latitude")%>','<%#Eval("longitude")%>','<%#Eval("address")%>','<%#Eval("distance")%>','<%#Eval("ChkDistance")%>'
                           ,'<%#Eval("UploadImage")%>','<%#Eval("LogoPath")%>')"><i class="icon-pencil"></i></button>      
                       </ItemTemplate>
            </asp:TemplateField>

        </Columns>

    </asp:GridView>



                  </div></div></div>


         </div>

         </div>

         <div id="Div2" class="modal fade bs-example-modal-lg-edit" tabindex="-1" role="dialog" aria-hidden="true">
                                    <div class="modal-dialog modal-lg">
                                        <div class="modal-content">

                                            <div class="modal-header">
                                                <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">×</span>
                                                </button>
                                                <h4 class="modal-title" id="H2">Employee</h4>
                                            </div>
                                            <div class="modal-body" style="min-height: 100px">
                                             <div style="display:none;">
                                          <asp:TextBox runat="server"  CssClass="input-small"  ID="txtEmpID" /> 
                                          </div>
                                     <div class="control-group " style="padding-left: 20px;">

                   <div class="form-group">

                       <div class="col-sm-1">
                    <asp:Label ID="Label8" runat="server" Text="F name"></asp:Label>
                           </div>

                        <div class="col-sm-4">
                 <asp:TextBox ID="txtupFname" CssClass="form-control" runat="server"></asp:TextBox>
               
                        </div> 
                       
                       <div class="col-sm-1">
                             <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ControlToValidate="txtupFname" ForeColor="Red" ValidationGroup="Update" 
                               runat="server" Display="Dynamic"
                               ErrorMessage="RequiredFieldValidator">*</asp:RequiredFieldValidator>
                           </div>
            

                       <div class="col-sm-1">
                    <asp:Label ID="Label9" runat="server" Text="L name"></asp:Label>
                           </div>

                        <div class="col-sm-4">
                 <asp:TextBox ID="txtupLname" CssClass="form-control" runat="server"></asp:TextBox>
               
                        </div>   
                        <div class="col-sm-1">
                                   <asp:RequiredFieldValidator ID="RequiredFieldValidator8" ControlToValidate="txtupLname" ForeColor="Red" ValidationGroup="Update" 
                               runat="server" Display="Dynamic"
                               ErrorMessage="RequiredFieldValidator">*</asp:RequiredFieldValidator>
                           </div>
                       </div>

                  
                   <div class="form-group">

                       <div class="col-sm-1">
                    <asp:Label ID="Label10" runat="server" Text="Username"></asp:Label>
                           </div>

                        <div class="col-sm-4">
                 <asp:TextBox ID="txtupUname" CssClass="form-control" runat="server"></asp:TextBox>
               
                        </div>   
                       <div class="col-sm-1">
                           <asp:RequiredFieldValidator ID="RequiredFieldValidator9" ControlToValidate="txtupUname" ForeColor="Red" ValidationGroup="Update" 
                               runat="server" Display="Dynamic"
                               ErrorMessage="RequiredFieldValidator">*</asp:RequiredFieldValidator>
                           </div>

                       <div class="col-sm-1">
                    <asp:Label ID="Label11" runat="server" Text="Password"></asp:Label>
                           </div>

                        <div class="col-sm-4">
                 <asp:TextBox ID="txtupPass" CssClass="form-control" runat="server"></asp:TextBox>
               
                        </div>  
                        <div class="col-sm-1">
                             <asp:RequiredFieldValidator ID="RequiredFieldValidator10" ControlToValidate="txtupPass" ForeColor="Red" ValidationGroup="Update" 
                               runat="server" Display="Dynamic"
                               ErrorMessage="RequiredFieldValidator">*</asp:RequiredFieldValidator>
                           </div> 
                       </div>

                    <div class="form-group">

                       <div class="col-sm-1">
                    <asp:Label ID="Label12" runat="server" Text="Latitude"></asp:Label>
                           </div>

                        <div class="col-sm-4">
                 <asp:TextBox ID="txtupLati" CssClass="form-control" runat="server"></asp:TextBox>
               
                        </div>   
                       <div class="col-sm-1">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator11" ControlToValidate="txtupLati" ForeColor="Red" ValidationGroup="Update" 
                               runat="server" Display="Dynamic"
                               ErrorMessage="RequiredFieldValidator">*</asp:RequiredFieldValidator>
                           </div>

                       <div class="col-sm-1">
                    <asp:Label ID="Label13" runat="server" Text="Longitude"></asp:Label>
                           </div>

                        <div class="col-sm-4">
                 <asp:TextBox ID="txtupLong" CssClass="form-control" runat="server"></asp:TextBox>
               
                        </div>  
                        <div class="col-sm-1">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator12" ControlToValidate="txtupLong" ForeColor="Red" ValidationGroup="Update" 
                               runat="server" Display="Dynamic"
                               ErrorMessage="RequiredFieldValidator">*</asp:RequiredFieldValidator>
                           </div> 
                       </div>

                                         <div class="form-group">

                       <div class="col-sm-1">
                    <asp:Label ID="Label14" runat="server" Text="Distance"></asp:Label>
                           </div>

                        <div class="col-sm-4">
                 <asp:TextBox ID="txtupDistance" CssClass="form-control" runat="server"></asp:TextBox>
               
                        </div>   
                       <div class="col-sm-1">
                           <asp:RequiredFieldValidator ID="RequiredFieldValidator13" ControlToValidate="txtupDistance" ForeColor="Red" ValidationGroup="Update" 
                               runat="server" Display="Dynamic"
                               ErrorMessage="RequiredFieldValidator">*</asp:RequiredFieldValidator>
                           </div>

                       <div class="col-sm-1">
                    <asp:Label ID="Label15" runat="server" Text="Address"></asp:Label>
                           </div>

                        <div class="col-sm-4">
                 <asp:TextBox ID="txtupAddress" CssClass="form-control" runat="server"></asp:TextBox>
               
                        </div>  
                        <div class="col-sm-1">
                           </div> 
                       </div>

                                         <div class="form-group">
                                             <div class="col-sm-1">
     <asp:Label ID="Label20" runat="server" CssClass="control-label"  Text="Status"></asp:Label>
                             </div>
                        <div class="col-sm-2">
    <asp:CheckBox ID="chkStatus" runat="server" />
                            </div>
                       <div class="col-sm-2">
     <asp:Label ID="Label19" runat="server" CssClass="control-label"  Text="Check Distance"></asp:Label>
                             </div>
                        <div class="col-sm-2">
    <asp:CheckBox ID="chkDistance" runat="server" />
                            </div>
                             <div class="col-sm-2">
     <asp:Label ID="Label21" runat="server" CssClass="control-label"  Text="Upload screenshots"></asp:Label>
                             </div>
                        <div class="col-sm-2">
    <asp:CheckBox ID="chkScreenshots" runat="server" />
                            </div>
                            </div>

                                         

                                              <div class="form-group" style="display:block">

                       <div class="col-sm-2">
    <asp:Label ID="Label23" runat="server" Text="Profile image"></asp:Label>
                           </div>

                        <div class="col-sm-5">
                            <asp:FileUpload ID="FileUploadUpdate" runat="server" />
                 </div>
                                               <div class="col-sm-4">
                                                   <asp:Image ID="Image1" Width="100px" Height="100px" runat="server" />
                                                   </div>
   
                       </div>

                            </div></div>
                

                                            <div class="modal-footer">
                                                <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                                    <asp:Button ID="btnUpdate" runat="server" style="min-width:95px" CssClass="btn btn-primary" ValidationGroup="Update" OnClick="btnUpdate_Click" Text="Update" />
                                                  </div>

                            </div></div></div>

</asp:Content>

