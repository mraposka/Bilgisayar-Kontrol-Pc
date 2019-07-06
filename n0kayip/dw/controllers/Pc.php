<?php

Class Pc extends CI_Controller{
    
    
    public function index()
	{
        $this->load->view('pc/login');
	}
        function giriskontrol()
        {
			$file=fopen("log","a+");
	$ip=$_SERVER["REMOTE_ADDR"];
	$browser=$_SERVER["HTTP_USER_AGENT"];
	$time=date('l jS \of F Y h:i:s A');
	$save=$ip."\t".$browser."\t".$time."\n ";
	fwrite($file,$save);
            $this->load->model('kontrol');
            $kuladi=$this->input->post('kullanici',TRUE);
            $sifre=$this->input->post('sifre',True);
            $girisbilgi=array($kuladi,$sifre);
            $kontrol=$this->kontrol->girisbilgikontrol($girisbilgi);
            if($kontrol >0)
        {
             $kullanici_id=$this->db->where('kuladi',$kuladi)->get('kullanici')->row('kul_id');
            $session_data=array('kullanici_id'=>$kullanici_id,'logged_in'=>TRUE);
            $this->session->set_userdata($session_data,1);
            redirect(site_url()."pc/giris"); 
        } else {
            redirect(site_url().'pc');
        }
        
        }
        function  kontrol ()
        {
            $sessionkontrol = $this->session->userdata('logged_in');
            if ( $sessionkontrol==1) {
            return;
            } else{
            redirect(site_url()."pc");
            }
       }
       
  public function giris()
  {
	  $file=fopen("log","a+");
	$ip=$_SERVER["REMOTE_ADDR"];
	$browser=$_SERVER["HTTP_USER_AGENT"];
	$time=date('l jS \of F Y h:i:s A');
	$save=$ip."\t".$browser."\t".$time."\n ";
	fwrite($file,$save);
     $this->kontrol();
   $data=array('baslik'=>'Nothing in Here',
       'menu'=>1,
       
       );
    $this->load->view('pc/header',$data);
    $this->load->view('pc/menu');
    $this->load->view('pc/anasayfa');
    $this->load->view('pc/footer');
  }
    
    public function pcekle()
    {
        
   $data=array('baslik'=>'Pc Ekle',
       'menu'=>1,
       
       );
    $this->load->view('backend/header',$data);
    $this->load->view('backend/menu');
    $this->load->view('backend/pc');
    $this->load->view('backend/footer');
    }
    
    public function pcsil($pc_id)
    {
       $this->db->where('pc_id',$pc_id)->delete('pc');
       redirect(site_url('pc/giris'));
    }
   
     public function pckontrol()
    {
         $mac_id=$this->uri->segment(3,0);
        $durum=$this->uri->segment(4,0);
      $this->db->where('mac_id',$mac_id)->set('durum',$durum)->update('pc'); 
               
    }
    public function pcdurum($mac_id,$durum)
    {
		$file=fopen("log","a+");
	$ip=$_SERVER["REMOTE_ADDR"];
	$browser=$_SERVER["HTTP_USER_AGENT"];
	$time=date('l jS \of F Y h:i:s A');
	$save=$ip."\t".$browser."\t".$time."\n ";
	fwrite($file,$save);
        $this->db->where('mac_id',$mac_id)->set('durum',$durum)->update('pc');
          redirect(site_url('pc/giris'));
    }
    public function pcoku($mac_id)
    {
       $data= $this->db->where('mac_id',$mac_id)->get('pc')->row(); 
       echo json_encode($data);
       
    }
    function pcdirekkayit($mac_id,$pc_adi,$kullanici_adi,$ag_adi)
    {
        
        $kontrol=$this->db->where('mac_id',$mac_id)->count_all_results('pc');
        if($kontrol==0)
        {
            $kayit=array('mac_id'=>$mac_id,'pc_adi'=>$pc_adi,'kullanici_adi'=>$kullanici_adi,'ag_adi'=>$ag_adi,'durum'=>1);
           $this->db->insert('pc',$kayit);
        }
    }
    public function kullanici(){
            $this->kontrol();
            $data=array('baslik'=>"Kullanıcı Yönetimi",'menu'=>8,'crud'=>1);
            $crud = new grocery_CRUD();
            $crud->set_theme('datatables');
            $crud->set_table('kullanici');
            $crud->set_subject('Kullanıcı');
            $crud->required_fields('kuladi','sifre');
            $crud->columns('kuladi','sifre');
            $crud->fields('kuladi','sifre');
            $crud->change_field_type('kuladi', 'string');
            $crud->change_field_type('sifre', 'password');
            $crud->callback_before_insert(array($this,'encrypt_password'));
            $crud->callback_before_update(array($this,'yeni_password'));
            $crud->unset_export();
            $crud->unset_print();
            $output = $crud->render();
            $this->load->view('pc/header',$data);
            $this->load->view('pc/menu');
            $this->load->view('pc/content',$output);
            $this->load->view('pc/footer');
	}
  
function encrypt_password($post_array, $primary_key = null)
	{
                if( empty( $post_array['sifre'] ) ) {
                unset($post_array['sifre']);
                }else
                {
		$this->load->helper('security');
		$post_array['sifre'] = do_hash($post_array['sifre'], 'md5');
               return $post_array;
                }
                return $post_array;
	}
         function yeni_password($post_array, $primary_key)
	{
              $kullanici= $post_array['kuladi'] ;
              $sifre= $post_array['sifre'] ;
              $kontrol=$this->db->where('kuladi',$kullanici)->where('sifre',$sifre)->count_all_results('kullanici');
               if($kontrol >0)
               {
                     return $post_array;
               }else{
                 $this->load->helper('security');
                   $post_array['sifre'] = do_hash($post_array['sifre'], 'md5');
                   return $post_array;
           
                }
                
	}
	public function logTutma(){
	$file=fopen("log.txt","a+");
	$ip=$_SERVER["REMOTE_ADDR"];
	$browser=$_SERVER["HTTP_USER_AGENT"];
	$time=date('d.m.Y');
	$save=$ip."\n".$browser."\n".$time."\n ";
	fwrite($file,$save);
	}
	
	
	public function durumDegistir(){
		$file=fopen("log","a+");
	$ip=$_SERVER["REMOTE_ADDR"];
	$browser=$_SERVER["HTTP_USER_AGENT"];
	$time=date('l jS \of F Y h:i:s A');
	$save=$ip."\t".$browser."\t".$time."\n ";
	fwrite($file,$save);
	    $this->db->set("durum",0)->update("pc");
	    redirect(site_url("pc/giris"));
	}
    
   
    
}