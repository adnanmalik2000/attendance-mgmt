<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Company.aspx.cs" Inherits="Company" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
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

           <h5> Company</h5>

      </div>
     <div class="card-block">
            <div class="row m-a-0">
              <div class="col-lg-12">

                    <div class="form-group">

                       <div class="col-sm-2">
                    <button  class="btn btn-primary" style="min-width:95px"  data-toggle="modal" data-target=".bs-example-modal-lg-add"  type="button">
                     Add New</button>      
                    </div>
                    <div class="col-sm-5">
                       
                         </div>
                    <div class="col-sm-2">
                          </div>
            </div>
                 
                   <div id="Div1" class="modal fade bs-example-modal-lg-add" tabindex="-1" role="dialog" aria-hidden="true">
                                    <div class="modal-dialog modal-lg">
                                        <div class="modal-content">

                                            <div class="modal-header">
                                                <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">×</span>
                                                </button>
                                                <h4 class="modal-title" id="H1">Add Company</h4>
                                            </div>
                                            <div class="modal-body" style="min-height: 100px">
                                             <div style="display:none;">
                                          <asp:TextBox runat="server"  CssClass="input-small"  ID="TextBox1" /> 
                                          </div>
                                     <div class="control-group " style="padding-left: 20px;">
               
                  <div class="form-group">

                       <div class="col-sm-2">
                    <asp:Label ID="Label1"  CssClass="control-label" runat="server" Text="Company Name"></asp:Label>
                    </div>
                    <div class="col-sm-5">
                        <span class="input m-b-md">
                         <asp:TextBox ID="txtCompany" CssClass="form-control" runat="server"></asp:TextBox>
                        <label class="input__label" for="input-1">
                        <span class="input__label-content"></span>
                      </label>
                      </span>
                         </div>
                    <div class="col-sm-2">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ForeColor="Red" ValidationGroup="Save" ControlToValidate="txtCompany"
                    runat="server" ErrorMessage="RequiredFieldValidator">*</asp:RequiredFieldValidator>

            </div>
            </div>

                   <div class="form-group">

                       <div class="col-sm-2">
    <asp:Label ID="Label2" runat="server" Text="Company Logo"></asp:Label>
                           </div>

                        <div class="col-sm-5">
                            <asp:FileUpload ID="FileUploadInsert" runat="server" />
                 </div>
   
                       </div>
                             </div></div>
                 
                                      

                                              <div class="modal-footer">
                                                <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                                                 <asp:Button ID="btnSave" runat="server" ValidationGroup="Save" CssClass="btn btn-primary" style="min-width:95px" OnClick="btnSave_Click" Text="Save" />
                                                 </div>

                                        </div></div></div>

                   <div class="table-responsive">

    <asp:GridView ID="grdCompany" runat="server" CssClass="table table-bordered table-striped m-b-0" AutoGenerateColumns="false">

        <Columns>
              <asp:BoundField DataField="CompanyID" HeaderText="CompID"  Visible="false" />
          <asp:BoundField DataField="CompanyName" HeaderText="Company Name"  Visible="true" />
     <asp:BoundField DataField="CompanyIDForApp" HeaderText="Company AppID"  Visible="true" />
               
             <asp:TemplateField ShowHeader="true" HeaderStyle-Width="20px">
                      <ItemTemplate>
                       <button  class="btn btn-xs btn-success" data-toggle="modal" data-target=".bs-example-modal-lg-edit"  type="button"
                             onclick="showEditModel('<%#Eval("CompanyID")%>' , '<%#Eval("CompanyName")%>', '<%#Eval("CompanyIDForApp")%>', '<%#Eval("LogoPath")%>')">
                           <i class="icon-pencil "></i></button>      
                       </ItemTemplate>
            </asp:TemplateField>

        </Columns>

    </asp:GridView>
                       </div>

          </div></div></div></div>


    
                   <div id="showDeleteEdit" class="modal fade bs-example-modal-lg-edit" tabindex="-1" role="dialog" aria-hidden="true">
                                    <div class="modal-dialog modal-lg">
                                        <div class="modal-content">

                                            <div class="modal-header">
                                                <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">×</span>
                                                </button>
                                                <h4 class="modal-title" id="myModalLabel2">Update Company</h4>
                                            </div>
                                            <div class="modal-body" style="min-height: 100px">
                                             <div style="display:none;">
                                          <asp:TextBox runat="server"  CssClass="input-small"  ID="txtComIDUpdate" /> 
                                          </div>
                                     <div class="control-group " style="padding-left: 20px;">
                                      <div class="form-group">
                                                        <div class="col-sm-3">
                                                            <asp:Label ID="Label9" CssClass="control-label" runat="server" Text="Company Name:"></asp:Label>
                                                        </div>
                                                        <div class="col-sm-6">
                                                            <span class="input m-b-md">
                                                       <asp:TextBox ID="txtComNameUpdate" CssClass="form-control" runat="server"/>  
                                                            <label class="input__label" for="input-1">
                                                            <span class="input__label-content"></span>
                                                          </label>
                                                          </span>
                                                             </div>
                                                        <div class="col-lg-3">
                                                               <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="txtComNameUpdate" ForeColor="Red" ValidationGroup="Update" 
                               runat="server" Display="Dynamic"
                               ErrorMessage="RequiredFieldValidator">*</asp:RequiredFieldValidator>
                                                           </div>
                                                    </div> 

                                      <div class="form-group">
                                                        <div class="col-sm-3">
                                                            <asp:Label ID="Label3" CssClass="control-label" runat="server" Text="Company App ID:"></asp:Label>
                                                        </div>
                                                        <div class="col-sm-6">
                                                            <span class="input m-b-md">
                                                       <asp:TextBox ID="txtComAppIDUpdate" CssClass="form-control" runat="server"/>  
                                                            <label class="input__label" for="input-1">
                                                            <span class="input__label-content"></span>
                                                          </label>
                                                          </span>
                                                             </div>
                                                        <div class="col-lg-3">
                                                           </div>
                                                    </div> 
                                           <div class="form-group" style="display:block">

                       <div class="col-sm-3">
    <asp:Label ID="Label4" runat="server" Text="Company Logo"></asp:Label>
                           </div>

                        <div class="col-sm-5">
                            <asp:FileUpload ID="FileUploadUpdate" runat="server" />
                 </div>
                                               <div class="col-sm-4">
                                                   <asp:Image ID="Image1" Width="100px" Height="100px" runat="server" />
                                                   </div>
   
                       </div>

                                         </div>
                                        </div>
                                       
                                            <div class="modal-footer">
                                                <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                                                 <asp:Button ID="btnupdatecomp" OnClick="btnupdatecomp_Click" ValidationGroup="Update" CssClass="btn btn-primary"
                                                      Width="150" Height="40" Text="Update" runat="server" />
                                                 </div>

                                        </div> </div>
                                    </div>
                          

     <script type="text/javascript">

         function showEditModel(id, Name, AppID,image) {

             if (id.length > 0) {
                 
                 $("[id*=txtComIDUpdate]").text(id);
                 $("[id*=txtComIDUpdate]").val(id);

                 $("[id*=txtComNameUpdate]").text(Name);
                 $("[id*=txtComNameUpdate]").val(Name);

                 $("[id*=txtComAppIDUpdate]").text(AppID);
                 $("[id*=txtComAppIDUpdate]").val(AppID);

                 image = image.replace(/~/g, "");
                 $("[id$='Image1']").attr("src", image);
             }
         }
    </script>

</asp:Content>

