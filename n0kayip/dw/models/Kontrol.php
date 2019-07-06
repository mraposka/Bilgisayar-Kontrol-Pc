<?php 
Class Kontrol extends CI_Model{
    
    public function girisbilgikontrol($girisbilgi)
    {
        $kuladi=$this->db->escape_str($girisbilgi[0]);
        $sifre=md5($this->db->escape_str($girisbilgi[1]));
        $this->db->where('kuladi',$kuladi);
        $this->db->where('sifre',$sifre);
        $kontrol=$this->db->count_all_results('kullanici');
        return $kontrol;
    }
   
  
}

?>