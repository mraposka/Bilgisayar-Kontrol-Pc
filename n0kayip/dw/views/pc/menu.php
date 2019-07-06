<body class="">
 
<div class="header navbar navbar-inverse ">
 
<div class="navbar-inner">
<div class="header-seperation">
<ul class="nav pull-left notifcation-center visible-xs visible-sm">
<li class="dropdown">
<a href="#main-menu" data-webarch="toggle-left-side">
<div class="iconset top-menu-toggle-white"></div>
</a>
</li>
</ul>
 
    <a href="<?php echo site_url('admin/giris'); ?>" style="color:white; padding-left: 45px; position:absolute; top: 20px; " >ADMİN PANEL</a>
 
<ul class="nav pull-right notifcation-center">
<li class="dropdown hidden-xs hidden-sm">
<a href="<?php echo site_url('sistem/giris'); ?>" class="dropdown-toggle active" data-toggle="">
<div class="iconset top-home"></div>
</a>
</li>

<li class="dropdown visible-xs visible-sm">
<a href="#" data-webarch="toggle-right-side">
<div class="iconset top-chat-white "></div>
</a>
</li>
</ul>
</div>
 
<div class="header-quick-nav">
 
<div class="pull-left">
<ul class="nav quick-section">
<li class="quicklinks"> <a href="#" class="" id="layout-condensed-toggle">
<div class="iconset top-menu-toggle-dark"></div>
</a> </li>
</ul>
<ul class="nav quick-section">
<li class="quicklinks"> <a href="#" class="">
<div class="iconset top-reload"></div>
</a> </li>
<li class="quicklinks"> <span class="h-seperate"></span></li>
<li class="quicklinks"> <a href="#" class="">
<div class="iconset top-tiles"></div>
</a> </li>

</ul>
</div>
 
 
<div class="pull-right">
<div class="chat-toggler">
<a href="#" class="dropdown-toggle" id="my-task-list" data-placement="bottom" data-content='' data-toggle="dropdown" data-original-title="Notifications">
<div class="user-details">
<div class="username">
<?php   $kullanici_id = $this->session->userdata('kullanici_id');  $kullanicibilgi=$this->db->where('kul_id',$kullanici_id)->get('kullanici')->row();
echo $kullanicibilgi->kuladi;
?>
</div>
</div>
</a>
</div>

</div>
 
</div>
 
</div>
 
</div>
 
 
<div class="page-sidebar " id="main-menu">
 
<div class="page-sidebar-wrapper scrollbar-dynamic" id="main-menu-wrapper">
  
<ul>
 <li <?php if($menu==1){ ?> class="start  open active" <?php } ?> > <a href="<?php echo site_url('pc/giris')?>"> <i class="icon-custom-home"></i> <span class="title">Giriş Ekranı</span> <span class="selected"></span> </a></li>

<!---</li>
 <li <?php if($menu==2){ ?> class="start  open active" <?php } ?>><a href="<?php echo site_url('pc/kullanici');?>"> <i class="fa fa-file-text"></i> <span class="title">Kullanıcı Yönetimi</span></a></li>
 --->
<li> <a href="<?php echo site_url('Snip/index.php'); ?>"> <i class="icon-custom-thumb"></i> <span class="title">Resimler</span> </a></li>
</ul>

<div class="clearfix"></div>
 
</div>
</div>

<div class="footer-widget">

<div class="pull-right">
   
<a href="<?php echo site_url('admin/cikis');?>"><i class="fa fa-power-off"></i>  Sistem Çıkış</a></div>
</div>