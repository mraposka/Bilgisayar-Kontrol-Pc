<?php

Class seo
{
    function seoveri($deger){
    
            $search = array('Ç','ç','Ğ','ğ','ı','İ','Ö','ö','Ş','ş','Ü','ü',' ','.',"'");
            $replace = array('C','c','G','g','i','i','O','o','S','s','U','u','-','-',"-");
            $seo = str_replace($search,$replace,$deger);
           return $seo;
    }
}
