/*
SQLyog Ultimate v8.3 
MySQL - 5.5.5-10.4.6-MariaDB : Database - inventaris
*********************************************************************
*/

/*!40101 SET NAMES utf8 */;

/*!40101 SET SQL_MODE=''*/;

/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;
CREATE DATABASE /*!32312 IF NOT EXISTS*/`inventaris` /*!40100 DEFAULT CHARACTER SET latin1 */;

USE `inventaris`;

/*Table structure for table `hak_akses` */

DROP TABLE IF EXISTS `hak_akses`;

CREATE TABLE `hak_akses` (
  `NamaHakAkses` varchar(100) NOT NULL,
  `NilaiHakAkses` longtext NOT NULL,
  PRIMARY KEY (`NamaHakAkses`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Data for the table `hak_akses` */

insert  into `hak_akses`(`NamaHakAkses`,`NilaiHakAkses`) values ('ADMINISTRATOR','1:MENU;1:Data Utama;1:Inventaris;1:Jenis Inventaris;1:Ruang Penyimpanan;1:Pegawai;1:Ruang Peminjaman;1:Data Transaksi;1:Peminjaman;1:Pengembalian;1:Data Laporan;1:Inventaris;1:PENGATURAN;1:Umum;1:Tema;1:Wallpaper;1:Konfigurasi Server;1:Hak Akses;0:Pengguna;1:Info Aplikasi'),('OPERATOR','1:MENU;1:Data Utama;1:Inventaris;0:Jenis Inventaris;0:Ruang Penyimpanan;0:Pegawai;0:Ruang Peminjaman;1:Data Transaksi;1:Peminjaman;1:Pengembalian;0:Data Laporan;0:Inventaris;1:PENGATURAN;1:Umum;1:Tema;1:Wallpaper;0:Konfigurasi Server;0:Hak Akses;0:Pengguna;1:Info Aplikasi'),('PEMINJAM','1:MENU;0:Data Utama;0:Inventaris;0:Jenis Inventaris;0:Ruang Penyimpanan;0:Pegawai;0:Ruang Peminjaman;1:Data Transaksi;1:Peminjaman;1:Pengembalian;0:Data Laporan;0:Inventaris;1:PENGATURAN;1:Umum;1:Tema;1:Wallpaper;0:Konfigurasi Server;0:Hak Akses;0:Pengguna;1:Info Aplikasi');

/*Table structure for table `inventaris` */

DROP TABLE IF EXISTS `inventaris`;

CREATE TABLE `inventaris` (
  `KodeInventaris` char(10) NOT NULL,
  `Nama` varchar(100) NOT NULL,
  `Kondisi` varchar(50) NOT NULL,
  `Keterangan` varchar(100) NOT NULL,
  `Jumlah` double NOT NULL,
  `TanggalRegister` datetime NOT NULL DEFAULT '0000-00-00 00:00:00',
  `KodeJenis` char(10) NOT NULL,
  `KodeRuang` char(10) NOT NULL,
  `KodePengguna` char(10) NOT NULL,
  `NIP` varchar(50) NOT NULL DEFAULT '',
  PRIMARY KEY (`KodeInventaris`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Data for the table `inventaris` */

insert  into `inventaris`(`KodeInventaris`,`Nama`,`Kondisi`,`Keterangan`,`Jumlah`,`TanggalRegister`,`KodeJenis`,`KodeRuang`,`KodePengguna`,`NIP`) values ('INV000001','Papan Tulis','Baru','Papan Tulis Sinar Dunia',3,'2019-03-15 17:31:44','JNS000001','RPN000003','PNG000001','NIP000001'),('INV000002','Meja Kaca','Baru','Meja Kaca',1,'2019-03-14 11:41:33','JNS000001','RPN000006','PNG000001','NIP000001'),('INV000003','Spidol','Bekas','Spidol Tulis',16,'2019-03-14 11:42:11','JNS000003','RPN000004','PNG000001','NIP000001');

/*Table structure for table `jenis` */

DROP TABLE IF EXISTS `jenis`;

CREATE TABLE `jenis` (
  `KodeJenis` char(10) NOT NULL,
  `NamaJenis` varchar(50) NOT NULL,
  `Keterangan` varchar(100) NOT NULL,
  PRIMARY KEY (`KodeJenis`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Data for the table `jenis` */

insert  into `jenis`(`KodeJenis`,`NamaJenis`,`Keterangan`) values ('JNS000001','Barang Jangka Panjang','Barang-barang Inventaris yang Digunakan dalam Jangka Waktu yang Panjang.'),('JNS000002','Barang Jangka Pendek','Barang-barang Inventaris yang Digunakan dalam Jangka Waktu yang Pendek.'),('JNS000003','ATK','(ALat Tulis Kantor)');

/*Table structure for table `pegawai` */

DROP TABLE IF EXISTS `pegawai`;

CREATE TABLE `pegawai` (
  `NIP` varchar(50) NOT NULL,
  `NamaPegawai` varchar(50) NOT NULL,
  `Jabatan` varchar(50) NOT NULL,
  `Alamat` varchar(100) NOT NULL,
  `NomorTelepon` varchar(50) NOT NULL,
  `Keterangan` varchar(100) NOT NULL,
  PRIMARY KEY (`NIP`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Data for the table `pegawai` */

insert  into `pegawai`(`NIP`,`NamaPegawai`,`Jabatan`,`Alamat`,`NomorTelepon`,`Keterangan`) values ('NIP000001','Prof. Fathul Murat S.kom','Ka. Prog RPL','Jl. Sandubaya No. 6 Mataram','+6281-910-797-510','Aktif'),('NIP000002','Arif Hidayat S.kom','Guru Produktif','Jl. Pantai Ampenan No 3 Mataram','+6283-129-230-878','Aktif'),('NIP000003','Adibul Khair S.kom','Guru Produktif','Jl. Praya Timur No 7 Lombok Tengah','+6282-145-678-876','Aktif');

/*Table structure for table `peminjaman` */

DROP TABLE IF EXISTS `peminjaman`;

CREATE TABLE `peminjaman` (
  `NomorPeminjaman` char(15) NOT NULL,
  `NomorPengembalian` char(15) NOT NULL,
  `NIP` varchar(50) NOT NULL,
  `Tanggal` datetime NOT NULL DEFAULT '0000-00-00 00:00:00',
  `KodeRuang` char(10) NOT NULL DEFAULT '',
  `TanggalPinjam` date NOT NULL DEFAULT '0000-00-00',
  `TanggalKembali` date NOT NULL DEFAULT '0000-00-00',
  `StatusPinjam` varchar(25) NOT NULL DEFAULT '',
  `KeteranganPinjam` varchar(100) NOT NULL,
  `KeteranganKembali` varchar(100) NOT NULL,
  PRIMARY KEY (`NomorPeminjaman`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Data for the table `peminjaman` */

insert  into `peminjaman`(`NomorPeminjaman`,`NomorPengembalian`,`NIP`,`Tanggal`,`KodeRuang`,`TanggalPinjam`,`TanggalKembali`,`StatusPinjam`,`KeteranganPinjam`,`KeteranganKembali`) values ('PMJ000000000001','PMB000000000001','NIP000002','2019-03-18 10:15:02','RPN000001','2019-03-18','2019-03-18','DIKEMBALIKAN','kntl','syukur'),('PMJ000000000002','PMB000000000002','NIP000001','2019-03-18 10:15:20','RPN000007','2019-03-18','2019-03-18','DIKEMBALIKAN','jhhh','jjj'),('PMJ000000000003','','NIP000003','2019-03-18 10:17:32','RPM000001','2019-03-18','0000-00-00','DIPINJAM','jbkvhcgxhfm',''),('PMJ000000000004','PMB000000000003','NIP000003','2019-08-30 13:50:56','RPN000002','2019-08-30','2019-08-30','DIKEMBALIKAN','lll','eqwe');

/*Table structure for table `peminjaman_detil` */

DROP TABLE IF EXISTS `peminjaman_detil`;

CREATE TABLE `peminjaman_detil` (
  `NomorPeminjaman` char(15) NOT NULL,
  `KodeInventaris` char(10) NOT NULL,
  `Jumlah` double NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Data for the table `peminjaman_detil` */

insert  into `peminjaman_detil`(`NomorPeminjaman`,`KodeInventaris`,`Jumlah`) values ('PMJ000000000001','INV000001',1),('PMJ000000000002','INV000001',1),('PMJ000000000003','INV000002',1),('PMJ000000000004','INV000001',2),('PMJ000000000001','INV000001',2),('PMJ000000000001','INV000001',2),('PMJ000000000002','INV000001',1),('PMJ000000000003','INV000004',2),('PMJ000000000004','INV000003',1);

/*Table structure for table `pengembalian_detil` */

DROP TABLE IF EXISTS `pengembalian_detil`;

CREATE TABLE `pengembalian_detil` (
  `NomorPengembalian` char(15) NOT NULL DEFAULT '',
  `KodeInventaris` char(10) NOT NULL,
  `Jumlah` double NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Data for the table `pengembalian_detil` */

insert  into `pengembalian_detil`(`NomorPengembalian`,`KodeInventaris`,`Jumlah`) values ('PMB000000000001','INV000001',1),('PMB000000000002','INV000001',1),('PMB000000000003','INV000001',2);

/*Table structure for table `pengguna` */

DROP TABLE IF EXISTS `pengguna`;

CREATE TABLE `pengguna` (
  `KodePengguna` char(10) NOT NULL,
  `NamaPengguna` varchar(50) NOT NULL,
  `Jabatan` varchar(50) NOT NULL,
  `Alamat` varchar(100) NOT NULL,
  `NomorTelepon` varchar(50) NOT NULL,
  `NamaHakAkses` varchar(100) NOT NULL,
  `Username` varchar(50) NOT NULL,
  `Password` varchar(50) NOT NULL,
  PRIMARY KEY (`KodePengguna`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Data for the table `pengguna` */

insert  into `pengguna`(`KodePengguna`,`NamaPengguna`,`Jabatan`,`Alamat`,`NomorTelepon`,`NamaHakAkses`,`Username`,`Password`) values ('PNG000000','ROOT','DATABASE ADMINISTRATOR','FORBIDDEN','-','-','root','root'),('PNG000001','I GEDE KRISHNA ADNYANA PUTRA','ADMINISTRATOR','Mataram','+6282-145-643-106','ADMINISTRATOR','admin','admin');

/*Table structure for table `ruang_peminjaman` */

DROP TABLE IF EXISTS `ruang_peminjaman`;

CREATE TABLE `ruang_peminjaman` (
  `KodeRuang` char(10) NOT NULL,
  `NamaRuang` varchar(50) NOT NULL,
  `Keterangan` varchar(100) NOT NULL,
  PRIMARY KEY (`KodeRuang`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Data for the table `ruang_peminjaman` */

insert  into `ruang_peminjaman`(`KodeRuang`,`NamaRuang`,`Keterangan`) values ('RPM000001','R. Produktif RPL','Inventaris Dipinjam dan Digunakan dalam Ruang Produktif RPL.');

/*Table structure for table `ruang_penyimpanan` */

DROP TABLE IF EXISTS `ruang_penyimpanan`;

CREATE TABLE `ruang_penyimpanan` (
  `KodeRuang` char(10) NOT NULL DEFAULT '',
  `NamaRuang` varchar(50) NOT NULL DEFAULT '',
  `Keterangan` varchar(100) NOT NULL DEFAULT '',
  PRIMARY KEY (`KodeRuang`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Data for the table `ruang_penyimpanan` */

insert  into `ruang_penyimpanan`(`KodeRuang`,`NamaRuang`,`Keterangan`) values ('RPN000001','R. Kelas X','Barang-barang Inventaris Sekolah yang Digunakan di Kelas X.'),('RPN000002','R. Kelas XI','Barang-barang Inventaris Sekolah yang Digunakan di Kelas XI.'),('RPN000003','R. Kelas XII','Barang-barang Inventaris Sekolah yang Digunakan di Kelas XII.'),('RPN000004','R. Tata Usaha','Barang-barang Inventaris Sekolah yang Digunakan di Ruang Tata Usaha.'),('RPN000005','R. Guru','Barang-barang Inventaris Sekolah yang Digunakan di Ruang Guru.'),('RPN000006','R. Kepala Sekolah','Barang-barang Inventaris Sekolah yang Digunakan di Ruang Kepala Sekolah.'),('RPN000007','Gudang 1','Barang-barang Inventaris Sekolah yang Disimpan agar dapat Digunakan saat Dibutuhkan.(Gudang 1)');

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;
