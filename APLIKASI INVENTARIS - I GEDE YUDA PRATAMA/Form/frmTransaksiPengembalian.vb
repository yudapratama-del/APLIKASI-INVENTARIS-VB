Public Class frmTransaksiPengembalian
    Dim Tampung As New DataTable
    Public PilihPeminjaman As Boolean

    Sub TampilTabel()
        Tampung = ExecuteQuery("select a.NomorPeminjaman, c.KodeInventaris, a.Jumlah, c.Nama,c.KodeRuang,b.NIP,d.NamaPegawai,d.Jabatan,b.TanggalPinjam,b.StatusPinjam,b.KeteranganPinjam from peminjaman_detil a, peminjaman b, inventaris c, pegawai d where a.NomorPeminjaman=b.NomorPeminjaman and b.NomorPengembalian='000000000000000'")
        GridControl1.DataSource = Tampung
        ExecuteGridViewAllAppearance(GridView1, , "NomorPeminjaman")

    End Sub

    Sub Databaru()
        TampilTabel()
        ExecuteInputTextValueClear(txtScan, txtNoPeminjaman, txtNoPengembalian, txtNIP, txtNamaPegawai, txtKodeRuang, txtNamaRuang, txtKeteranganKembali)
        txtTanggalKembali.DateTime = Now
        txtNoPengembalian.Text = ExecuteAutoCode("peminjaman", "NomorPengembalian", "PNB", "000000000000")
        txtScan.Focus()

    End Sub

    Sub TampilListPeminjaman()
        PilihPeminjaman = False
        frmListPeminjaman.NilaiKiriman = 1
        frmListPeminjaman.ShowDialog()
        If PilihPeminjaman = True Then
            TambahItem()
        End If
    End Sub

    Sub TampilListPegawai()
        frmListPegawai.NilaiKiriman = 3
        frmListPegawai.ShowDialog()

    End Sub

    Sub TampilListRuangPenyimpanan()
        frmListRuangPenyimpanan.NilaiKiriman = 2
        frmListRuangPenyimpanan.ShowDialog()

    End Sub

    Sub TambahItemDetil(ByVal NomorPeminjaman As String, ByVal NIP As String, ByVal TanggalPinjam As Date, ByVal StatusPinjam As String, ByVal KeteranganPinjam As String, ByVal Jabatan As String, ByVal Nama As String, ByVal KodeRuang As String, ByVal NamaPegawai As String, ByVal KodeInventaris As String, ByVal Jumlah As Integer)
        Dim CariDaftar = Tampung.Select("NomorPeminjaman='" & NomorPeminjaman & "'")
        If CariDaftar.Length <= 0 Then
            Dim BarisBaru = Tampung.NewRow
            BarisBaru.Item("NomorPeminjaman") = NomorPeminjaman
            BarisBaru.Item("NIP") = NIP
            BarisBaru.Item("TanggalPinjam") = TanggalPinjam
            BarisBaru.Item("StatusPinjam") = StatusPinjam
            BarisBaru.Item("KeteranganPinjam") = KeteranganPinjam
            BarisBaru.Item("Jabatan") = Jabatan
            BarisBaru.Item("Nama") = Nama
            BarisBaru.Item("KodeRuang") = KodeRuang
            BarisBaru.Item("NamaPegawai") = NamaPegawai
            BarisBaru.Item("KodeInventaris") = KodeInventaris
            BarisBaru.Item("Jumlah") = Jumlah
            Tampung.Rows.Add(BarisBaru)
        Else
        End If

    End Sub

    Sub TambahItem()
        Dim CariKodePeminjaman = ExecuteQuery("select d.NomorPeminjaman, c.KodeInventaris, d.Jumlah, c.Nama, c.KodeRuang, a.NIP, b.NamaPegawai, b.Jabatan, a.TanggalPinjam, a.StatusPinjam, a.KeteranganPinjam from (peminjaman a inner join inventaris c on a.NIP=c.NIP inner join pegawai b on b.NIP=c.NIP inner join peminjaman_detil d on c.KodeInventaris=d.KodeInventaris and d.NomorPeminjaman='" & txtScan.Text & "')").Select()
        If CariKodePeminjaman.Length > 0 Then
            TambahItemDetil(CariKodePeminjaman(0).Item("NomorPeminjaman"), CariKodePeminjaman(0).Item("NIP"), CariKodePeminjaman(0).Item("TanggalPinjam"), CariKodePeminjaman(0).Item("StatusPinjam"), CariKodePeminjaman(0).Item("KeteranganPinjam"), CariKodePeminjaman(0).Item("Jabatan"), CariKodePeminjaman(0).Item("Nama"), CariKodePeminjaman(0).Item("KodeRuang"), CariKodePeminjaman(0).Item("NamaPegawai"), CariKodePeminjaman(0).Item("KodeInventaris"), CariKodePeminjaman(0).Item("Jumlah"))

            txtScan.Text = ""
            txtScan.Focus()

        Else
            MessageBox.Show("Barang Tidak Ditemukan", "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtScan.Text = ""
            txtScan.Focus()

        End If
    End Sub

    Sub HapusItem()
        If GridView1.RowCount > 0 Then
            GridView1.DeleteSelectedRows()
        Else
            MessageBox.Show("Tidak Ada Data Yang Dipilih!", "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub

    Sub Simpan()
        If GridView1.RowCount <= 0 Then
            MessageBox.Show("Daftar Item Masih Kosong!", "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If
        If txtNoPeminjaman.Text = "" Or txtNoPengembalian.Text = "" Or txtNIP.Text = "" Or txtNamaPegawai.Text = "" Or txtKodeRuang.Text = "" Or txtNamaRuang.Text = "" Or txtTanggalKembali.Text = "" Then
            MessageBox.Show("Nomor Peminjaman, Tanggal Kembali, NIP, Nama Pegawai, Kode Ruang dan Nama Ruang Wajib Diisi", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Dim NomorPengembalian = ExecuteAutoCode("peminjaman", "NomorPengembalian", "PMB", "000000000000")
            'UPDATE
        ExecuteQuery("UPDATE peminjaman SET NomorPengembalian='" & txtNoPengembalian.Text & "',KodeRuang='" & txtKodeRuang.Text & "',Tanggal='" & Format(Now, "yyyy-MM-dd HH:mm:ss") & "',TanggalKembali='" & Format(txtTanggalKembali.DateTime, "yyyy-MM-dd HH:mm:ss") & "',StatusPinjam='DIKEMBALIKAN',KeteranganKembali='" & txtKeteranganKembali.Text & "' WHERE NomorPeminjaman='" & txtNoPeminjaman.Text & "'")

        For Each Isi In Tampung.Select()
            ExecuteQuery("INSERT INTO pengembalian_detil(NomorPengembalian,KodeInventaris,Jumlah) VALUES ('" & NomorPengembalian & "','" & Isi.Item("KodeInventaris") & "', " & Isi.Item("Jumlah") & ")")
        Next

        MessageBox.Show("Transaksi Berhasil!", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Databaru()

    End Sub

    Sub Tutup()
        If GridView1.RowCount > 0 Then
            If MessageBox.Show("daftar Item Telah Terisi. Lanjutkan Tutup Transaksi?", "Validasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                Me.Close()
            End If
        Else
            Me.Close()
        End If
    End Sub

    Private Sub frmTransaksiPengembalian_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.KeyPreview = True
        txtNoPengembalian.Properties.ReadOnly = True
        txtNoPengembalian.TabStop = False
        txtNoPeminjaman.Properties.ReadOnly = True
        txtNoPeminjaman.TabStop = False
        txtNIP.Properties.ReadOnly = True
        txtNIP.TabStop = False
        txtNamaPegawai.Properties.ReadOnly = True
        txtNamaPegawai.TabStop = False
        txtKodeRuang.Properties.ReadOnly = True
        txtKodeRuang.TabStop = False
        txtNamaRuang.Properties.ReadOnly = True
        txtNamaRuang.TabStop = False
        txtpengguna.Text = "Pengguna : " & My.Settings.lgnNamaPengguna
        Databaru()

    End Sub

    Private Sub btnTambahItem_Click(sender As Object, e As EventArgs) Handles btnTambahItem.Click
        TambahItem()

    End Sub

    Private Sub SimpleButton2_Click(sender As Object, e As EventArgs) Handles SimpleButton2.Click
        HapusItem()

    End Sub

    Private Sub SimpleButton4_Click(sender As Object, e As EventArgs) Handles SimpleButton4.Click
        Databaru()

    End Sub

    Private Sub SimpleButton5_Click(sender As Object, e As EventArgs) Handles SimpleButton5.Click
        Tutup()

    End Sub

    Private Sub txtScan_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles txtScan.ButtonClick
        TampilListPeminjaman()

    End Sub

    Private Sub txtScan_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtScan.KeyPress
        If e.KeyChar = Chr(13) Then
            TambahItem()

        End If
    End Sub

    Private Sub txtNIP_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles txtNIP.ButtonClick
        TampilListPegawai()

    End Sub

    Private Sub txtKodeRuang_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles txtKodeRuang.ButtonClick
        TampilListRuangPenyimpanan()

    End Sub

    Private Sub SimpleButton3_Click(sender As Object, e As EventArgs) Handles SimpleButton3.Click
        Simpan()

    End Sub
End Class