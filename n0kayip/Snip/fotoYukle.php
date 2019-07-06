<script src="https://cdnjs.cloudflare.com/ajax/libs/baguettebox.js/1.8.1/baguetteBox.min.js"></script>
<script>
    baguetteBox.run('.tz-gallery');
</script>

<?php 
$dir    = 'img/';
$files1 = scandir($dir);
$files2 = scandir($dir, 1);

for($i=2;$i<sizeof($files1);$i++){

$regular_expression_1 ="\+(.*?)\+";
$text=$files1[$i];
preg_match_all('#'.$regular_expression_1.'#' ,$text , $matches);
for ($j=0;  $j<1;$j++){
date_default_timezone_set('Europe/Turkey');
$foto_Tarih=$matches[1][3];
$suanki_Tarih=date('d.m.Y');
$matches[1][2]=str_replace('_', ':',$matches[1][2]);
$matches[1][4]=str_replace('_', ':',$matches[1][4]);
if($foto_Tarih==$suanki_Tarih){
	echo '
  <div class="col-sm-6 col-md-4">
                <div align="left" class="thumbnail">
                    <a  class="lightbox" href="'.$dir.$files1[$i].'">
                        <img  style="width:450px; height:250px;" src="'.$dir.$files1[$i].'" alt="Park">
                    </a>
                    <div class="caption">
                        <h3 align="left">Bilgisayar Adı:'.$matches[1][0].'</h3>
						<h3 align="left">Workgroup:'.$matches[1][1].'</h3>
                        <h3 align="left">Mac:'.$matches[1][2].'</h3>
						 <h3 align="left">Tarih:'.$matches[1][3].'</h3>
					<h3 align="left">Saat:'.$matches[1][4].'</h3>
                    </div>
                </div>
            </div>		'
			;
}
else{
	$file = $dir.$files1[$i];
if (!unlink($file))
  {
	  echo "
  <script>
    alert('Eski dosyaları silerken bir hata oluştu.');
  </script>";
  }
else
  {
   echo "
  <script>
    alert('Eski dosyalar silindi.".$suanki_Tarih."');
  </script>";
  }
	
}
}

}
			
?>
