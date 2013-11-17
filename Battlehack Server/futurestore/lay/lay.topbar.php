
    <div class="navbar navbar-fixed-top navbar-default" role="navigation">
      <div class="container">
        <div class="navbar-header">
          <a class="navbar-brand" href="#">skillfully custom</a>
        </div>
        <div class="collapse navbar-collapse">
          <ul class="nav navbar-nav">
          	<?php if($_SESSION['id_u']>0){?> 
          	 <li <?php if($path=='dashboard')echo 'class="active"'; ?>><a href="/dashboard">Dashboard</a></li>
            <li <?php if($path=='create')echo 'class="active"'; ?>><a href="/create">Add Product</a></li>
            <?php } ?>
          </ul>
          <ul class="nav navbar-nav navbar-right">
           <?php if($_SESSION['id_u']>0){?> 
           <li><a href="#">Welcome, <?php echo $_SESSION['username'] ?></a></li><li> <a href="/Logout">Logout</a></li>
           <?php }else{ ?>
          	<li <?php if($path=='login')echo 'class="active"'; ?>><a href="/login">Login</a></li>
          	<li <?php if($path=='register')echo 'class="active"'; ?>><a href="/register">Register</a></li>
           <?php } ?>
          </ul>
        </div><!-- /.nav-collapse -->
      </div><!-- /.container -->
    </div><!-- /.navbar -->
