<?php

Class Site_model extends CI_Model{
    
    public function slider()
    {
        return $this->db->get('slider')->result_array();
    }
    
   
    public function anasayfa()
    {
       
        return $this->db->get('anasayfa')->result_array();
    }
    public function hizmetler()
    {
        return $this->db->get('hizmet')->result_array();
    }
    public function referans()
    {
        return $this->db->get('freferans')->result_array();
    }

public function hakkimizda()
{
    $this->db->where('kurumsal_id',1);
     return $this->db->get('kurumsal')->result_array(); 
}
public function hizmetmenu()
{
     return $this->db->get('hizmet')->result_array();
}
public function hizmet($hizmet_id)
{
    $this->db->where('hizmet_id',$hizmet_id);
     return $this->db->get('hizmet')->result_array();
}
public function projekat()
{
   return $this->db->get('projeler')->result_array();   
}
public function projeler()
{
    return $this->db->get('projeler_gorsel')->result_array(); 
}
 public function galerikat()
{
   return $this->db->get('galerikat')->result_array();   
}
public function galeri()
{
    return $this->db->get('galeri')->result_array(); 
}


     public function iletisim()
    {
        return $this->db->get('iletisim')->result_array();
    }
    
    public function sitetanim()
    {
        return $this->db->get('sitetanim')->result_array();
    }
}