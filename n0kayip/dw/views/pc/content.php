<div class="page-content">
 
<div class="clearfix"></div>
<div class="content">

<div class="page-title"> <a href="<?php echo site_url('sistem/giris') ?>"><i class="icon-custom-left"></i></a>
<h3>Giriş Ekranı - <span class="semi-bold"><?php  echo $baslik;?></span></h3>
</div>

  <?php  foreach($css_files as $file): ?>


	<link type="text/css" rel="stylesheet" href="<?php echo $file; ?>" />


        <?php endforeach; ?>


        <?php foreach($js_files as $file): ?>


	<script src="<?php echo $file; ?>"></script>


        <?php endforeach; ?>


        <?php echo $output; ?>   

</div>
</div>