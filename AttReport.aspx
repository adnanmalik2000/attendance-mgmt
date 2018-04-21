<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="AttReport.aspx.cs" Inherits="AttReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

     <div class="card bg-white">

        <div class="card-header">

           <h5> Daily Attendance</h5>

      </div>
     <div class="card-block">
            <div class="row m-a-0">
              <div class="col-lg-12">

                     <div class="form-group">

                       <div class="col-sm-4">
                          <asp:TextBox ID="txtAttDate" CssClass="form-control" data-provide="datepicker" runat="server"></asp:TextBox>
                    </div>
                          <div class="col-sm-1">
                              <asp:RequiredFieldValidator ID="RFVIdate" runat="server" ControlToValidate="txtAttDate" 
                    ForeColor="Red" ValidationGroup="Search" ErrorMessage="Select date"></asp:RequiredFieldValidator>
                          </div>
                    <div class="col-sm-5">
                       <asp:Button id="btnAttd" runat="server" OnClick="btnAttd_Click" ValidationGroup="Search" Text="Search" CssClass="btn btn-primary" style="min-width:95px" ></asp:Button> 
                        <asp:Button ID="btnExport" runat="server" Enabled="false" Text="Export" OnClick="ExportToExcel" CssClass="btn btn-primary" style="min-width:95px" />
                         </div>
                   
            </div>

                  <div class="table-responsive">

    <asp:GridView ID="grdAttend" runat="server" CssClass="table table-bordered table-striped m-b-0" AutoGenerateColumns="false">

        <Columns>
              <asp:BoundField DataField="CompanyID" HeaderText="CompID"  Visible="false" />
          <asp:BoundField DataField="FirstName" HeaderText="First Name"  Visible="true" />
     <asp:BoundField DataField="LastName" HeaderText="Last name"  Visible="true" />
             <asp:BoundField DataField="CheckIn" HeaderText="Check in"  Visible="true" />
         <asp:BoundField DataField="CheckOut" HeaderText="Check out"  Visible="true" />
             <asp:BoundField DataField="THours" HeaderText="Total Time"  Visible="true" />
            <asp:BoundField DataField="OHours" HeaderText="Break out"  Visible="true" />
            <asp:BoundField DataField="WHours" HeaderText="Working hours"  Visible="true" />

             <asp:TemplateField ShowHeader="true" HeaderStyle-Width="20px">
                      <ItemTemplate>
                       <button  class="btn btn-xs btn-success" data-toggle="modal" data-target=".bs-example-modal-lg-edit"  type="button"
                             onclick="showscreenShots('<%#Eval("EmpID")%>')">
                           <i class="icon-pencil "></i></button>      
                       </ItemTemplate>
            </asp:TemplateField>

        </Columns>

    </asp:GridView>
                       </div>

                  </div></div></div>


         </div>

      <div id="showDeleteEdit" class="modal fade bs-example-modal-lg-edit" tabindex="-1" role="dialog" aria-hidden="true">
                                    <div class="modal-dialog modal-lg">
                                        <div class="modal-content">

                                            <div class="modal-header">
                                                <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">×</span>
                                                </button>
                                                <h4 class="modal-title" id="myModalLabel2">Attendance Screenshots</h4>
                                            </div>
                                            <div class="modal-body" style="min-height: 100px">
                                             <div style="display:none;">
                                            </div>
                                     <div class="control-group " style="padding-left: 20px;">
                                       <div class="form-group" style="display:block">

                                              <div class="col-sm-3">
                                             <asp:Image ID="imgcin" Width="200px" Height="200px" runat="server" />
                                             </div>

                                   <div class="col-sm-3">
                              <asp:Image ID="imgbin" Width="200px" Height="200px" runat="server" />
                                     </div>
                                               <div class="col-sm-3">
                                                   <asp:Image ID="imgbout" Width="200px" Height="200px" runat="server" />
                                                   </div>
                                           <div class="col-sm-3">
                                                   <asp:Image ID="imgcout" Width="200px" Height="200px" runat="server" />
                                                   </div>
   
                       </div>

                                         </div>
                                        </div>
                                       
                                            <div class="modal-footer">
                                                </div>

                                        </div> </div>
                                    </div>

    <script type="text/javascript">
        function showscreenShots(id) {
            var imgsrc = 'UploadImages\\';
            var txtdate = $("[id$='txtAttDate']").val();
            txtdate = txtdate.split("/");

            mo = txtdate[0];
            dy = txtdate[1];
            yr = txtdate[2];

            mo = mo.replace(/^0+/, '');
            dy = dy.replace(/^0+/, '');

            var newdate = yr + mo + dy;
          
            $("[id$='imgcin']").attr("src", imgsrc + id + "-CIN-" + newdate + '.png');
            $("[id$='imgbin']").attr("src", imgsrc + id + "-BIN-" + newdate + '.png');
            $("[id$='imgbout']").attr("src", imgsrc + id + "-BOUT-" + newdate + '.png');
            $("[id$='imgcout']").attr("src", imgsrc + id + "-COUT-" + newdate + '.png');
        }
    </script>

</asp:Content>

