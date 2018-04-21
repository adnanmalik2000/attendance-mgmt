<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <meta name="description" content=""/>
  <meta name="viewport" content="width=device-width, user-scalable=no, initial-scale=1, maximum-scale=1"/>
  <!-- build:css({.tmp,app}) styles/app.min.css -->
  <link rel="stylesheet" href="Asset/styles/webfont.css"/>
  <link rel="stylesheet" href="Asset/styles/climacons-font.css"/>
  <link rel="stylesheet" href="Asset/vendor/bootstrap/dist/css/bootstrap.css"/>
  <link rel="stylesheet" href="Asset/styles/font-awesome.css"/>
  <link rel="stylesheet" href="Asset/styles/card.css"/>
  <link rel="stylesheet" href="Asset/styles/sli.css"/>
  <link rel="stylesheet" href="Asset/styles/animate.css"/>
  <link rel="stylesheet" href="Asset/styles/app.css"/>
  <link rel="stylesheet" href="Asset/styles/app.skins.css"/>
</head>
<body>
    <form id="form1" runat="server">
    <div class="app signin usersession">
    <div class="session-wrapper">
      <div class="page-height-o row-equal align-middle">
        <div class="column">
          <div class="card bg-white no-border">
            <div class="card-block">
             
                <div class="text-center m-b">
                  <h4 class="text-uppercase">Welcome back</h4>
                  <p>Please sign in to your account</p>
                </div>
                <div class="form-inputs">
                  <label class="text-uppercase">Username</label>
                  <asp:TextBox ID="txtUsername" runat="server"  CssClass="form-control input-lg" placeholder="Username" required></asp:TextBox>
                  <label class="text-uppercase">Password</label>
                  <asp:TextBox ID="txtPassword" runat="server" type="password" CssClass="form-control input-lg" placeholder="Password" required></asp:TextBox>
                </div>
                <br />
                <asp:Button ID="btnlogin" runat="server" OnClick="btnlogin_Click" class="btn btn-primary btn-block btn-lg m-b" Text="Login" type="submit"></asp:Button>
                
             
            </div>
          
          </div>
        </div>
      </div>
    </div>
    <!-- bottom footer -->
    <footer class="session-footer">
      <nav class="footer-right">
        <ul class="nav">
          <li>
            <a href="javascript:;">Feedback</a>
          </li>
          <li>
            <a href="javascript:;" class="scroll-up">
              <i class="fa fa-angle-up"></i>
            </a>
          </li>
        </ul>
      </nav>
      <nav class="footer-left hidden-xs">
        <ul class="nav">
          <li>
            <a href="javascript:;"><span>About</span> Reactor</a>
          </li>
          <li>
            <a href="javascript:;">Privacy</a>
          </li>
          <li>
            <a href="javascript:;">Terms</a>
          </li>
          <li>
            <a href="javascript:;">Help</a>
          </li>
        </ul>
      </nav>
    </footer>
    <!-- /bottom footer -->
  </div>
  <!-- build:js({.tmp,app}) scripts/app.min.js -->
  <script src="Asset/scripts/helpers/modernizr.js"></script>
  <script src="Asset/vendor/jquery/dist/jquery.js"></script>
  <script src="Asset/vendor/bootstrap/dist/js/bootstrap.js"></script>
  <script src="Asset/vendor/fastclick/lib/fastclick.js"></script>
  <script src="Asset/vendor/perfect-scrollbar/js/perfect-scrollbar.jquery.js"></script>
  <script src="Asset/scripts/helpers/smartresize.js"></script>
  <script src="Asset/scripts/constants.js"></script>
  <script src="Asset/scripts/main.js"></script>
  <!-- endbuild -->
    </form>
</body>
</html>
