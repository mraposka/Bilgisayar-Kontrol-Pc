
<div class="page-content">

<div class="clearfix"></div>
<div class="content">

<div class="page-title"> 
<h3>PC Kontrol - <span class="semi-bold"> Paneli</span></h3>

</div>
<div class="pull-left">
<a href="<?php echo site_url("pc/durumDegistir"); ?>" class="btn btn-success">KAPAT LAN</a>

</div>
<div class="row">
<div class="col-md-12">
<div class="grid simple">

<div class="grid-body no-border">
    <br/>
    <br/>
<div class="row">
 
     <?php
    
    $pcler=$this->db->get('pc')->result_array();
    foreach ($pcler as $p_v) { 
        
        $durum=$p_v['durum'];
        ?>
<div class="col-md-3 single-colored-widget">
<div class="content-wrapper <?php if($durum==1){?>green<?php }else{?> red <?php } ?>">
<h3 class="text-white"><?php echo $p_v['pc_adi']; ?></h3>

 <img  src="<?php echo site_url('assets/img');?>/pc.png"  alt=""><br>

<div class="clearfix"></div>
</div>
<div class="heading">
<div class="pull-left">
<h4> <span class="semi-bold">DURUM : <?php if($durum==1){?>AÇIK<?php }else{?> KAPALI <?php } ?></span></h4>
<?php 

if($durum==1){
?>
<a href="<?php echo site_url('pc/pcdurum').'/'.$p_v['mac_id'].'/0';?>" class="btn btn-success">KAPAT</a>
<?php } ?>
</div>

<div class="clearfix"> </div>
</div>
</div>
    <?php } ?>

</div>


</div>
</div>
</div>
</div>
</div>

    
   

</div>
</div>